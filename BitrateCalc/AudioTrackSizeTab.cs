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

namespace BitrateCalc
{
    public partial class AudioTrackSizeTab : UserControl
    {
        private AudioTrack track = null;

        protected readonly string Filter = "All Files (*.*)|*.*";

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

        public AudioTrackSizeTab() : base()
        {
            InitializeComponent();
            this.audioCodec.Items.AddRange(Enum.GetValues(typeof(AudioCodec)).Cast<object>().ToArray());
        }
                
        protected virtual void OnValueChanged()
        {
            if (ValueChanged != null) ValueChanged(this, EventArgs.Empty);
        }

        protected virtual void SelectAudioFile(string file)
        {
            //FileSize f = FileSize.Of2(file) ?? FileSize.Empty;
            ////size.CertainValue = f;
            //size.Text = f.ToString();
            //name.Text = System.IO.Path.GetFileName(file);

            //AudioType aud2Type = VideoUtil.guessAudioType(file);
            //if (audio1Type.Items.Contains(aud2Type))
            //    audio1Type.SelectedItem = aud2Type;

            //MediaInfo info;
            //try
            //{
            //    info = new MediaInfo(file);
            //    MediaInfoWrapper.AudioTrack atrack = info.Audio[0];
            //    // this.length = atrack.Duration
            //    //if (atrack.Format == "DTS" && (atrack.BitRate == "768000" || atrack.BitRate == "1536000"))
            //    {
            //        audio1Bitrate.Value = (Convert.ToInt32(atrack.BitRate) / 1000);
            //    }
            //}
            //catch (Exception i)
            //{
            //    MessageBox.Show("The following error ocurred when trying to get Media info for file " + file + "\r\n" + i.Message, "Error parsing mediainfo data", MessageBoxButtons.OK);                
            //}
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
