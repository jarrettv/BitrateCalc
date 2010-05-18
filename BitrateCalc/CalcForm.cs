// ****************************************************************************
// 
// Copyright (C) 2005-2009  Doom9 & al
// 
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
// 
// ****************************************************************************

using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using BitrateCalc.Properties;
using System.Threading;
using System.Xml.Linq;


namespace BitrateCalc
{ 
	/// <summary>
	/// Summary description for Calculator.
	/// </summary>
	partial class CalcForm : System.Windows.Forms.Form
	{
        private bool calculating = false;
        private bool isUpdating = false;

        protected TimeSpan VideoDuration
        {
            get { return new TimeSpan((long)totalSeconds.Value * TimeSpan.TicksPerSecond); }
        }
        
        #region Event Handlers

        private void CalcForm_Load(object sender, EventArgs e)
        {
            if (Settings.Default.AutoCheckForUpdate) ThreadPool.QueueUserWorkItem((w) => Updater.CheckForUpdate(ShowUpdateDialog));

            this.framerate.SelectedIndex = 0;
            this.videoCodec.Items.AddRange(Enum.GetValues(typeof(VideoCodec)).Cast<object>().ToArray());
            this.videoCodec.SelectedItem = VideoCodec.H264; // TODO: get from settings
            this.container.Items.AddRange(Enum.GetValues(typeof(Container)).Cast<object>().ToArray());
            this.container.SelectedItem = BitrateCalc.Container.Mkv; // TODO: get from settings

            // set complexity from settings
            this.complexity.Minimum = 0;
            this.complexity.Maximum = 100;
            this.complexity.Value = (Settings.Default.MinComplexity + Settings.Default.MaxComplexity) / 2;
            this.complexity.Minimum = Settings.Default.MinComplexity;
            this.complexity.Maximum = Settings.Default.MaxComplexity;

            // wire the input boxes to auto-select text on focus
            AddAutoSelectHandler(this);

            // set focus of calculate by
            this.videoSize.Click += (s, args) => videoSize.Focus();
            this.bppRadio.Click += (s, args) => bpp.Focus();
            this.qEstRadio.Click += (s, args) => qest.Focus();
            this.fileSizeRadio.Click += (s, args) => totalSize.Focus();

            // smart focus for audio and extras (try to fix render bug w/ scrollbar)
            this.audioExtraFlow.ControlRemoved += (s, args) =>
            {
                int idx = (int)args.Control.Tag;
                if (idx > 0) audioExtraFlow.Controls[idx - 1].Focus();
                TagIndexes();
            };
            this.audioExtraFlow.ControlAdded += (s, args) =>
            {
                args.Control.Focus();
                TagIndexes();
            };

            AddAudio();
        }

        private void value_Changed(object sendor, EventArgs e)
        {
            Calculate();
        }

		private void textField_TextChanged(object sender, System.EventArgs e)
		{
            lock(this)
            {
                if(sender is NumericUpDown && !this.isUpdating)
                {
                    this.isUpdating = true;
                    NumericUpDown tb = (NumericUpDown)sender;
                    decimal value = tb.Value;
                    if (tb == totalSeconds)
                    {
                        int hours = (int)value / 3600;
                        value -= hours * 3600;
                        int minutes = (int)value / 60;
                        value -= minutes * 60;
                        if (hours <= this.hours.Maximum)
                        {
                            this.hours.Value = hours;
                            this.minutes.Value = minutes;
                            this.seconds.Value = value;
                        }
                        else // We set to max available time and set frames accordingly
                        {
                            this.hours.Value = this.hours.Maximum;
                            this.minutes.Value = this.minutes.Maximum - 1; //59 mins
                            this.seconds.Value = this.seconds.Maximum - 1; //59 seconds
                        }
                        OnVideoDurationChanged();
                        UpdateTotalSeconds();
                        UpdateTotalFrames();
                    }
                    else if (tb == frames)
                    {
                        int secs = (int)(value / decimal.Parse(framerate.Text));
                        totalSeconds.Text = secs.ToString();
                        int hours = secs / 3600;
                        secs -= hours * 3600;
                        int minutes = secs / 60;
                        secs -= minutes * 60;
                        if (hours < this.hours.Maximum)
                        {
                            this.hours.Value = hours;
                            this.minutes.Value = minutes;
                            this.seconds.Value = secs;
                        }
                        else //Set to max available time and set frames accordingly
                        {
                            this.hours.Value = this.hours.Maximum;
                            this.minutes.Value = this.minutes.Maximum - 1; //59 minutes
                            this.seconds.Value = this.seconds.Maximum - 1; //59 seconds
                            UpdateTotalFrames();
                        }
                        UpdateTotalSeconds();
                        OnVideoDurationChanged();
                    }
                    this.isUpdating = false;
                }
                Calculate();
            }
		}

        private void time_ValueChanged(object sender, System.EventArgs e)
        {
            lock (this)
            {
                if (isUpdating)
                    return;

                this.isUpdating = true;
                NumericUpDown ud = (NumericUpDown)sender;

                if (this.hours.Value.Equals(this.hours.Maximum))
                {
                    if (this.minutes.Value == 60)
                    {
                        this.minutes.Value = 59;
                        UpdateTotalSeconds();
                        UpdateTotalFrames();
                        isUpdating = false;
                        return; // we can't increase the time
                    }
                    else if (this.seconds.Value == 60 && this.minutes.Value == 59)
                    {
                        this.seconds.Value = 59;
                        UpdateTotalSeconds();
                        UpdateTotalFrames();
                        isUpdating = false;
                        return; // we can't increase the time
                    }
                }
                if (ud.Value == 60) // time to wrap
                {
                    ud.Value = 0;
                    if (ud == seconds)
                    {
                        if (minutes.Value == 59)
                        {
                            minutes.Value = 0;
                            if (!this.hours.Value.Equals(this.hours.Maximum))
                                hours.Value += 1;
                        }
                        else
                            minutes.Value += 1;
                    }
                    else if (ud == minutes)
                    {
                        minutes.Value = 0;
                        if (this.hours.Value < this.hours.Maximum)
                            hours.Value += 1;
                    }
                }
                UpdateTotalSeconds();
                UpdateTotalFrames();
                OnVideoDurationChanged();
                Calculate();

                this.isUpdating = false;
            }
        }

		/// <summary>
		/// handles the change in between bitrate and size calculation mode
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void calculationMode_CheckedChanged(object sender, System.EventArgs e)
		{
			RadioButton rb = (RadioButton)sender;
			if (rb.Checked)
			{
                if (rb == averageBitrateRadio)
                {
                    totalSize.ReadOnly = true;
                    videoSize.ReadOnly = false;
                    bpp.ReadOnly = true;
                    qest.ReadOnly = true;
                }
                else if (rb == bppRadio)
                {
                    totalSize.ReadOnly = true;
                    videoSize.ReadOnly = true;
                    bpp.ReadOnly = false;
                    qest.ReadOnly = true;
                }
                else if (rb == qEstRadio)
                {
                    totalSize.ReadOnly = true;
                    videoSize.ReadOnly = true;
                    bpp.ReadOnly = true;
                    qest.ReadOnly = false;
                }
                else
                {
                    totalSize.ReadOnly = false;
                    videoSize.ReadOnly = true;
                    bpp.ReadOnly = true;
                    qest.ReadOnly = true;
                }
			}
		}

        private void framerate_Changed(object sender, EventArgs e)
        {
            if (!isUpdating)
            {
                isUpdating = true;
                double fps = double.Parse(framerate.Text);
                int length = (int)totalSeconds.Value;
                int numberOfFrames = (int)(length * fps);
                frames.Value = numberOfFrames;
                Calculate();
                isUpdating = false;
            }
        }

        private void addExtraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddExtra();
        }

        private void addAudioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddAudio();
        }

        private void addAudioLink_Clicked(object sender, EventArgs e)
        {
            addAudioExtraMenu.Show(addAudioLink, 0, addAudioLink.Height);
        }

        /// <summary>
        /// Switches between showing h m s and total seconds
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picTime_Click(object sender, EventArgs e)
        {
            Image i = picTime.Image;
            picTime.Image = picTime.InitialImage;
            picTime.InitialImage = i;

            bool showTotal = hoursLabel.Visible;

            hours.Visible = !showTotal;
            hoursLabel.Visible = !showTotal;
            minutes.Visible = !showTotal;
            minutesLabel.Visible = !showTotal;
            seconds.Visible = !showTotal;
            secondsLabel.Visible = !showTotal;

            totalSeconds.Visible = showTotal;
            totalSecondsLabel.Visible = showTotal;
            timeText.Visible = showTotal;

            this.toolTip1.SetToolTip(this.picTime, showTotal ? "Show hours, minutes, seconds" : "Show total seconds");
        }

        private void checkForUpdatesNowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateStatus status = Updater.CheckForUpdate(ShowUpdateDialog);

            if (status == UpdateStatus.UpdateFailed)
                MessageBox.Show(this, "Failed to check for update.  Please ty again later.", "Warning");
            else if (status == UpdateStatus.NoUpdate)
                MessageBox.Show(this, "There are no updates available at this time.", "Update Check");
        }

        private void checkForUpdatesOnStartupToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.AutoCheckForUpdate = checkForUpdatesOnStartupToolStripMenuItem.Checked;
            Settings.Default.Save();
        }

        private void helpLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            helpMenu.Show(helpLink, 0, helpLink.Height);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (AboutForm f = new AboutForm())
            {
                f.ShowDialog(this);
            }
        }

        private void visitWebpageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://mewiki.project357.com/wiki/MeGUI/Tools/Bitrate_Calculator");
        }

        #endregion

        protected void OnVideoDurationChanged()
        {
            foreach (Control c in audioExtraFlow.Controls)
            {
                if (c is AudioTrackSizeTab)
                {
                    AudioTrackSizeTab a = (AudioTrackSizeTab)c;
                    if (a.AudioTrack != null)
                    {
                        a.AudioTrack = new AudioTrack(VideoDuration)
                        {
                            RawBytes = a.AudioTrack.RawBytes,
                            SamplingRate = a.AudioTrack.SamplingRate,
                            AudioCodec = a.AudioTrack.AudioCodec
                        };
                    }
                    else
                    {
                        a.AudioTrack = new AudioTrack(VideoDuration);
                    }
                }
                else if (c is ExtraSizeTab)
                {
                    ExtraSizeTab a = (ExtraSizeTab)c;
                    if (a.ExtraTrack != null)
                    {
                        a.ExtraTrack = new ExtraTrack(VideoDuration)
                        {
                            RawBytes = a.ExtraTrack.RawBytes
                        };
                    }
                    else
                    {
                        a.ExtraTrack = new ExtraTrack(VideoDuration);
                    }
                }
            }
        }

        protected void UpdateTotalFrames()
        {
            int secs = (int)totalSeconds.Value;
            double fps = double.Parse(framerate.Text);
            int frameNumber = (int)((double)secs * fps);
            frames.Value = frameNumber;
        }

        protected void UpdateTotalSeconds()
        {
            int secs = (int)this.hours.Value * 3600 + (int)this.minutes.Value * 60 + (int)this.seconds.Value;
            totalSeconds.Value = secs;
            timeText.Text = string.Format("{0}h {1}m {2}s", this.hours.Value, this.minutes.Value, this.seconds.Value);
        }

        protected AudioTrackSizeTab AddAudio()
        {
            AudioTrackSizeTab a = new AudioTrackSizeTab();
            a.ValueChanged += (o, s) => Calculate();
            a.AudioTrack = new AudioTrack(VideoDuration);
            audioExtraFlow.Controls.Add(a);
            return a;
        }

        protected ExtraSizeTab AddExtra()
        {
            ExtraSizeTab a = new ExtraSizeTab();
            a.ValueChanged += (o, s) => Calculate();
            a.ExtraTrack = new ExtraTrack(VideoDuration);
            audioExtraFlow.Controls.Add(a);
            return a;
        }

        protected void TagIndexes()
        {
            for (int i = 0; i < audioExtraFlow.Controls.Count; i++)
            {
                audioExtraFlow.Controls[i].Tag = i;
            }
        }

        protected void AddAutoSelectHandler(Control control)
        {
            // auto select text when focus numeric field
            foreach (Control c in control.Controls)
            {
                if (c.HasChildren) AddAutoSelectHandler(c);
                if (c is NumericUpDown)
                {
                    ((NumericUpDown)c).Enter += (s, args) => ((NumericUpDown)s).Select(0, ((NumericUpDown)s).Text.Length);
                    ((NumericUpDown)c).Click += (s, args) => ((NumericUpDown)s).Select(0, ((NumericUpDown)s).Text.Length);
                }
            }
        }

        /// <summary>
        /// Gets the audio streams by finding them in the form
        /// </summary>
        /// <returns></returns>
        protected IEnumerable<AudioTrack> GetAudio()
        {
            foreach (Control c in audioExtraFlow.Controls)
            {
                if (c is AudioTrackSizeTab)
                {
                    AudioTrackSizeTab a = (AudioTrackSizeTab)c;
                    if (a.AudioTrack != null) yield return a.AudioTrack;
                }
            }
        }

        /// <summary>
        /// Gets the total extra size by finding the extras and summing their sizes
        /// </summary>
        /// <returns>Total size of extra data</returns>
        protected IEnumerable<ExtraTrack> GetExtras()
        {
            foreach (Control c in audioExtraFlow.Controls)
            {
                if (c is ExtraSizeTab)
                {
                    ExtraSizeTab a = (ExtraSizeTab)c;
                    if (a.ExtraTrack != null) yield return a.ExtraTrack;
                }
            }
        }

        /// <summary>
        /// Calculates by the selected method
        /// </summary>
        protected void Calculate()
        {
            if (calculating) return;
            calculating = true;
            try
            {
                VideoTrack video = new VideoTrack((long)frames.Value, float.Parse(framerate.Text));
                video.FrameSize = new Size((int)width.Value, (int)height.Value);
                video.VideoCodec = (VideoCodec)videoCodec.SelectedItem;
                video.HasBframes = bframes.Checked;

                Calculator data = new Calculator((Container)container.SelectedItem, video, GetAudio().ToArray(), GetExtras().ToArray());
                data.QualityCoeffient = (float)complexity.Value / 100F;

                if (fileSizeRadio.Checked) // get video, bpp, qest
                {
                    data.TotalBytes = totalSize.SizeLength.Bytes;
                    data.CalcByTotalSize();
                }
                else if (this.bppRadio.Checked) // get video, quest, total
                {
                    data.BitsPerPixel = (float)bpp.Value;
                    data.CalcByBitsPerPixel();
                }
                else if (this.qEstRadio.Checked) // get video, bpp, total
                {
                    data.QualityEstimate = (float)qest.Value;
                    data.CalcByQualityEstimate();
                }
                else // given video size, get total, bpp, quest
                {
                    video.RawBytes = videoSize.SizeLength.Bytes;
                    data.CalcByVideoSize();
                }

                totalSize.SizeLength = data.TotalSizeLength;
                videoSize.SizeLength = video.SizeLength;
                bpp.Value = (decimal)data.BitsPerPixel;
                qest.Value = (decimal)data.QualityEstimate;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                if (fileSizeRadio.Checked)
                {
                    bpp.Value = 0;
                    qest.Value = 0;
                    videoSize.SizeLength = SizeLength.Zero;
                }
                else if (this.bppRadio.Checked)
                {
                    qest.Value = 0;
                    videoSize.SizeLength = SizeLength.Zero;
                    totalSize.SizeLength = SizeLength.Zero;
                }
                else if (this.qEstRadio.Checked)
                {
                    bpp.Value = 0;
                    videoSize.SizeLength = SizeLength.Zero;
                    totalSize.SizeLength = SizeLength.Zero;
                }
                else
                {
                    bpp.Value = 0;
                    qest.Value = 0;
                    totalSize.SizeLength = SizeLength.Zero;
                }
            }
            calculating = false;
        }

        private void ShowUpdateDialog(Version appVersion, Version newVersion, XDocument doc)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<Version, Version, XDocument>(ShowUpdateDialog), appVersion, newVersion, doc);
                return;
            }

            using (UpdateForm f = new UpdateForm())
            {
                f.Text = string.Format(f.Text, appVersion);
                f.MoreInfoLink = (string)doc.Root.Element("info");
                f.Info = string.Format(f.Info, newVersion, (DateTime)doc.Root.Element("date"));
                if (f.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    Updater.LaunchUpdater(doc);
                    this.Close();
                }
            }
        }
	}
}
