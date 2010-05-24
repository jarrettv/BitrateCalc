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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using System.IO;
using System.Diagnostics;
using BitrateCalc.Properties;

namespace BitrateCalc
{
    public partial class AudioTrackSizeTab : UserControl
    {
        private AudioTrack track = null;

        protected readonly string Filter = "Audio Files|*.ac3;*.dts;*.aac;*.ogg;*.mp3;*.flac;*.wav;*.mp2;*.eac3;*.truehd;*.dtsma|All Files (*.*)|*.*";

        public event EventHandler ValueChanged;

        public AudioTrack AudioTrack
        {
            get { return track; }
            set
            {
                track = value;
                this.audioCodec.SelectedIndex = (track == null) ? 0 : this.audioCodec.Items.IndexOf(track.AudioCodec);
                this.size.SizeLength = (track == null) ? SizeLength.Zero : track.SizeLength;
            }
        }

        public AudioTrackSizeTab() : this(TimeSpan.Zero) { }

        public AudioTrackSizeTab(TimeSpan duration) : base()
        {
            InitializeComponent();
            this.audioCodec.Items.AddRange(Enum.GetValues(typeof(AudioCodec)).Cast<object>().ToArray());
            this.AudioTrack = new AudioTrack(duration)
            {
                AudioCodec = (AudioCodec)Enum.Parse(typeof(AudioCodec), Settings.Default.AudioCodec),
                RawBytes = Settings.Default.AudioBytes
            };
            size.SizeUnit = (SizeUnit)Enum.Parse(typeof(SizeUnit), Settings.Default.AudioSizeUnit);
        }
                
        protected virtual void OnValueChanged()
        {
            if (ValueChanged != null) ValueChanged(this, EventArgs.Empty);
        }

        protected virtual void SelectAudioFile(string file)
        {
            try
            {
                // TODO: actually read file info
                var s = size.SizeLength.ToNewSize(new FileInfo(file).Length);
                size.SizeUnit = s.MB > 1024 ? SizeUnit.GB : SizeUnit.MB;
                size.SizeLength = s;
                name.Text = Path.GetFileName(file);
                string ext = Path.GetExtension(file).Substring(1).ToLower();
                audioCodec.SelectedItem = AudioCodecs.List.Where(a => a.ToString().ToLower()
                    .StartsWith(ext)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void selectButton_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = Filter;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                SelectAudioFile(openFileDialog.FileName);
            }
        }

        private void removeLink_LinkClicked(object sender, EventArgs e)
        {
            if (this.Parent != null) this.Parent.Controls.Remove(this);
        }

        private void audioCodec_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AudioTrack != null) AudioTrack.AudioCodec = (AudioCodec)audioCodec.SelectedItem;
            OnValueChanged();
        }

        private void size_ValueChanged(object sender, EventArgs e)
        {
            if (AudioTrack != null) AudioTrack.RawBytes = size.SizeLength.Bytes;
            OnValueChanged();
        }
    }
}
