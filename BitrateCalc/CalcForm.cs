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
        protected TimeSpan VideoDuration
        {
            get { return TimeSpan.FromSeconds((double)totalSeconds.Value); }
        }
        
        #region Event Handlers

        private void CalcForm_Load(object sender, EventArgs e)
        {
            if (Settings.Default.AutoCheckForUpdate) ThreadPool.QueueUserWorkItem((w) => Updater.CheckForUpdate(ShowUpdateDialog));

            if (Settings.Default.ShowTotalSeconds) picTime_Click(null, null);

            this.framerate.Text = Settings.Default.VideoFramerate.ToString();

            this.videoCodec.Items.AddRange(Enum.GetValues(typeof(VideoCodec)).Cast<object>().ToArray());
            this.videoCodec.SelectedItem = Enum.Parse(typeof(VideoCodec), Settings.Default.VideoCodec);
            this.bframes.Checked = Settings.Default.VideoHasBframes;

            // set complexity from settings
            this.complexity.Minimum = 0;
            this.complexity.Maximum = 100;
            if (Settings.Default.VideoComplexity < Settings.Default.MinComplexity || Settings.Default.VideoComplexity > Settings.Default.MaxComplexity)
                this.complexity.Value = (Settings.Default.MinComplexity + Settings.Default.MaxComplexity) / 2;
            else 
                this.complexity.Value = Settings.Default.VideoComplexity;
            this.complexity.Minimum = Settings.Default.MinComplexity;
            this.complexity.Maximum = Settings.Default.MaxComplexity;

            this.width.Value = Settings.Default.VideoDimension.Width;
            this.height.Value = Settings.Default.VideoDimension.Height;

            this.container.Items.AddRange(Enum.GetValues(typeof(Container)).Cast<object>().ToArray());
            this.container.SelectedItem = Enum.Parse(typeof(Container), Settings.Default.Container);

            switch (Settings.Default.CalcBy)
            {
                case 0: averageBitrateRadio.Checked = true;
                    videoSize.SizeLength = videoSize.SizeLength.ToNewSize(Settings.Default.VideoBytes);
                    break;
                case 1: bppRadio.Checked = true;
                    bpp.Value = (decimal)Settings.Default.BitsPerPixel;
                    break;
                case 2: qEstRadio.Checked = true;
                    qest.Value = (decimal)Settings.Default.QualityEstimate;
                    break;
                default: fileSizeRadio.Checked = true;
                    totalSize.SizeLength = totalSize.SizeLength.ToNewSize(Settings.Default.TotalBytes);
                    break;
            }

            videoSize.SizeUnit = (SizeUnit)Enum.Parse(typeof(SizeUnit), Settings.Default.VideoSizeUnit);
            totalSize.SizeUnit = (SizeUnit)Enum.Parse(typeof(SizeUnit), Settings.Default.TotalSizeUnit);


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

            // add presets to the preset menu
            var items = PresetSize.Presets.Select(p => new ToolStripMenuItem(p.Preset, null, new EventHandler(preset_Changed)) 
            { 
                Tag = p.Size
            });
            presetMenu.Items.AddRange(items.ToArray());

            AddAudio();

            OnVideoDurationChanged(Settings.Default.VideoDuration);
            Calculate();

            // wire the input boxes to auto-select text on focus
            AddAutoSelectHandler(this);
        }

        private void preset_Changed(object sender, EventArgs e)
        {
            var t = (ToolStripItem)sender;
            totalSize.SizeLength = totalSize.SizeLength.ToNewSize((long)t.Tag);
            UpdatePresetLabel(t.Text);
        }

        private void value_Changed(object sender, EventArgs e)
        {
            Calculate();
        }

		private void time_Changed(object sender, System.EventArgs e)
		{
            TimeSpan duration = new TimeSpan((int)hours.Value, (int)minutes.Value, (int)seconds.Value);
            OnVideoDurationChanged(duration);
            Calculate();
		}

        private void totalSeconds_Changed(object sender, System.EventArgs e)
        {
            TimeSpan duration = TimeSpan.FromSeconds((double)totalSeconds.Value);
            OnVideoDurationChanged(duration);
            Calculate();
        }

        private void frames_Changed(object sender, EventArgs e)
        {
            TimeSpan duration = TimeSpan.FromSeconds((double)frames.Value / double.Parse(framerate.Text));
            OnVideoDurationChanged(duration);
            Calculate();
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
                videoSize.ReadOnly = !averageBitrateRadio.Checked;
                bpp.ReadOnly = !bppRadio.Checked;
                qest.ReadOnly = !qEstRadio.Checked;
                totalSize.ReadOnly = !fileSizeRadio.Checked;
                presetLink.Enabled = !totalSize.ReadOnly;
			}
		}

        private void framerate_Changed(object sender, EventArgs e)
        {
            frames.ValueChanged -= new EventHandler(frames_Changed);
            try
            {
                double fps = double.Parse(framerate.Text);
                frames.Value = (int)(VideoDuration.TotalSeconds * fps);
                Calculate();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            frames.ValueChanged += new EventHandler(frames_Changed);
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
        
        private void presetLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            presetMenu.Show(presetLink, 0, presetLink.Height);
        }

        #endregion

        protected void UpdatePresetLabel(string presetName)
        {
            if (presetName != null)
            {
                presetLink.Text = "Preset: " + presetName;
                return;
            }

            presetLink.Text = "Preset: (custom)";
            foreach (PresetSize preset in PresetSize.Presets)
            {
                if ((long)Math.Round(totalSize.SizeLength.MB) * 1024L * 1024L == preset.Size)
                {
                    presetLink.Text = "Preset: " + preset.Preset;
                    break;
                }
            }
        }

        protected void OnVideoDurationChanged(TimeSpan duration)
        {
            if (duration < TimeSpan.Zero) duration = TimeSpan.Zero;

            hours.ValueChanged -= new EventHandler(time_Changed);
            minutes.ValueChanged -= new EventHandler(time_Changed);
            seconds.ValueChanged -= new EventHandler(time_Changed);
            totalSeconds.ValueChanged -= new EventHandler(totalSeconds_Changed);
            frames.ValueChanged -= new EventHandler(frames_Changed);
            videoSize.ValueChanged -= new EventHandler(value_Changed);
            totalSize.ValueChanged -= new EventHandler(value_Changed);

            try
            {
                hours.Value = duration.Hours;
                minutes.Value = duration.Minutes;
                seconds.Value = duration.Seconds;
                totalSeconds.Value = (decimal)duration.TotalSeconds;
                timeText.Text = string.Format("{0}h {1}m {2}s", duration.Hours, duration.Minutes, duration.Seconds);
                frames.Value = Math.Ceiling((decimal)duration.TotalSeconds * decimal.Parse(framerate.Text));

                // only specify duration on the item being calculated
                if (averageBitrateRadio.Checked) videoSize.SizeLength = videoSize.SizeLength.ToNewLength(duration, !videoSize.IsBitrateUnit);
                if (fileSizeRadio.Checked) totalSize.SizeLength = totalSize.SizeLength.ToNewLength(duration, !totalSize.IsBitrateUnit);

                UpdateAudioExtraDurations(duration);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }

            hours.ValueChanged += new EventHandler(time_Changed);
            minutes.ValueChanged += new EventHandler(time_Changed);
            seconds.ValueChanged += new EventHandler(time_Changed);
            totalSeconds.ValueChanged += new EventHandler(totalSeconds_Changed);
            frames.ValueChanged += new EventHandler(frames_Changed);
            videoSize.ValueChanged += new EventHandler(value_Changed);
            totalSize.ValueChanged += new EventHandler(value_Changed);
        }

        private void UpdateAudioExtraDurations(TimeSpan duration)
        {
            foreach (Control c in audioExtraFlow.Controls)
            {
                if (c is AudioTrackSizeTab)
                {
                    AudioTrackSizeTab a = (AudioTrackSizeTab)c;
                    if (a.AudioTrack != null)
                    {
                        a.AudioTrack = new AudioTrack(duration)
                        {
                            RawBytes = a.AudioTrack.RawBytes,
                            SamplingRate = a.AudioTrack.SamplingRate,
                            AudioCodec = a.AudioTrack.AudioCodec
                        };
                    }
                    else a.AudioTrack = new AudioTrack(duration);
                }
                else if (c is ExtraSizeTab)
                {
                    ExtraSizeTab a = (ExtraSizeTab)c;
                    if (a.ExtraTrack != null)
                    {
                        a.ExtraTrack = new ExtraTrack(duration) { RawBytes = a.ExtraTrack.RawBytes };
                    }
                    else a.ExtraTrack = new ExtraTrack(duration);
                }
            }
        }
        
        protected AudioTrackSizeTab AddAudio()
        {
            AudioTrackSizeTab a = new AudioTrackSizeTab(VideoDuration);
            a.ValueChanged += (o, s) => Calculate();
            audioExtraFlow.Controls.Add(a);
            return a;
        }

        protected ExtraSizeTab AddExtra()
        {
            ExtraSizeTab a = new ExtraSizeTab(VideoDuration);
            a.ValueChanged += (o, s) => Calculate();
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
            videoSize.ValueChanged -= new EventHandler(value_Changed);
            bpp.ValueChanged -= new EventHandler(value_Changed);
            qest.ValueChanged -= new EventHandler(value_Changed);
            totalSize.ValueChanged -= new EventHandler(value_Changed);

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
                UpdatePresetLabel(null);
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
            videoSize.ValueChanged += new EventHandler(value_Changed);
            bpp.ValueChanged += new EventHandler(value_Changed);
            qest.ValueChanged += new EventHandler(value_Changed);
            totalSize.ValueChanged += new EventHandler(value_Changed);
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
