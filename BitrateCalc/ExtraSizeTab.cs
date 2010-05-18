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

namespace BitrateCalc
{
    public partial class ExtraSizeTab : UserControl
    {
        private ExtraTrack track = null;
        protected readonly string Filter = "All Files (*.*)|*.*";

        public event EventHandler ValueChanged;

        public ExtraTrack ExtraTrack
        {
            get { return track; }
            set
            {
                track = value;
                this.size.SizeLength = (track == null) ? SizeLength.Zero : track.SizeLength;
            }
        }

        public ExtraSizeTab()
            : base()
        {
            InitializeComponent();
        }

        protected virtual void OnValueChanged()
        {
            if (ValueChanged != null) ValueChanged(this, EventArgs.Empty);
        }

        protected virtual void SelectFile(string file)
        {
            //FileSize f = FileSize.Of2(file) ?? FileSize.Empty;
            ////size.CertainValue = f;
            //size.Text = f.ToString();
            //name.Text = System.IO.Path.GetFileName(file);
        }

        private void selectButton_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = Filter;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                SelectFile(openFileDialog.FileName);
            }
        }

        private void removeLink_LinkClicked(object sender, EventArgs e)
        {
            if (this.Parent != null) this.Parent.Controls.Remove(this);
        }

        private void size_ValueChanged(object sender, EventArgs e)
        {
            if (ExtraTrack != null) ExtraTrack.RawBytes = size.SizeLength.Bytes;
            OnValueChanged();
        }
    }
}
