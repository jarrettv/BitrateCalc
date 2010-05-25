using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BitrateCalc
{
    public partial class SizeBitrateBox : UserControl
    {
        private SizeLength sizeLength;
        private SizeUnit unit;

        public event EventHandler ValueChanged;

        public bool ReadOnly
        {
            get { return NumUpDown.ReadOnly; }
            set { NumUpDown.ReadOnly = value; }
        }

        public bool IsBitrateUnit { get { return (int)unit >= 100; } }

        public SizeLength SizeLength
        {
            get { return sizeLength; }
            set 
            { 
                sizeLength = value;
                NumUpDown.Value = 
                    unit == SizeUnit.KB ? (decimal)sizeLength.KB :
                    unit == SizeUnit.MB ? (decimal)sizeLength.MB :
                    unit == SizeUnit.GB ? (decimal)sizeLength.GB :
                    unit == SizeUnit.Kbps ? (decimal)sizeLength.Kbps :
                    unit == SizeUnit.Mbps ? (decimal)sizeLength.Mbps : 
                    (decimal)sizeLength.Bytes;
                //if (unit == SizeUnit.Kbps || unit == SizeUnit.Mbps)
                //    if (sizeLength.UnknownLength) NumUpDown.Text = string.Empty;
            }
        }

        public SizeUnit SizeUnit
        {
            get { return unit; }
            set 
            { 
                unit = value;
                UnitSwitch.Text = unit.ToString().Length == 2 ? unit.ToString() : unit.ToString().ToLower();
                switch (unit)
                {
                    //case SizeUnit.Bytes:
                    //    NumUpDown.Maximum = SizeLength.Max.Bytes;
                    //    NumUpDown.Increment = 1024;
                    //    NumUpDown.DecimalPlaces = 0;
                    //    break;
                    case SizeUnit.KB:
                        NumUpDown.Maximum = (decimal)SizeLength.Max.KB;
                        NumUpDown.Increment = 16;
                        NumUpDown.DecimalPlaces = 0;
                        break;
                    case SizeUnit.MB:
                        NumUpDown.Maximum = (decimal)SizeLength.Max.MB;
                        NumUpDown.Increment = 0.1M;
                        NumUpDown.DecimalPlaces = 1;
                        break;
                    case SizeUnit.GB:
                        NumUpDown.Maximum = (decimal)SizeLength.Max.GB;
                        NumUpDown.Increment = 0.01M;
                        NumUpDown.DecimalPlaces = 2;
                        break;
                    case SizeUnit.Kbps:
                        NumUpDown.Maximum = (decimal)SizeLength.Max.Kbps;
                        NumUpDown.Increment = 50;
                        NumUpDown.DecimalPlaces = 0;
                        break;
                    case SizeUnit.Mbps:
                        NumUpDown.Maximum = (decimal)SizeLength.Max.Mbps;
                        NumUpDown.Increment = 0.01M;
                        NumUpDown.DecimalPlaces = 2;
                        break;
                }
            }
        }

        public SizeBitrateBox()
        {
            InitializeComponent();
        }

        private void UnitSwitch_Click(object sender, EventArgs e)
        {
            var names = Enum.GetNames(typeof(SizeUnit)).ToList();
            int i = names.Select(s => s.ToLower()).ToList().IndexOf(UnitSwitch.Text.ToLower());
            SizeUnit = i < names.Count - 1 ? 
                (SizeUnit)Enum.Parse(typeof(SizeUnit), names[i + 1]) : 
                (SizeUnit)Enum.Parse(typeof(SizeUnit), names[0]); // go back to first
            SizeLength = sizeLength; // trigger set logic
        }

        private void NumUpDown_ValueChanged(object sender, EventArgs e)
        {
            switch (unit)
            {
                //case SizeUnit.Bytes:
                //    sizeLength = sizeLength.ToNewSize((long)NumUpDown.Value);
                //    break;
                case SizeUnit.KB:
                    sizeLength = sizeLength.ToNewSize((long)(NumUpDown.Value * 1024M));
                    break;
                case SizeUnit.MB:
                    sizeLength = sizeLength.ToNewSize((long)(NumUpDown.Value * 1024M * 1024M));
                    break;
                case SizeUnit.GB:
                    sizeLength = sizeLength.ToNewSize((long)(NumUpDown.Value * 1024M * 1024M * 1024M));
                    break;
                case SizeUnit.Kbps:
                    if (sizeLength.Length.TotalSeconds > 0)
                        sizeLength = sizeLength.ToNewSize((long)(NumUpDown.Value / 8M * 1000M * (decimal)sizeLength.Length.TotalSeconds));
                    //else
                    //    sizeLength = new SizeLength((long)(NumUpDown.Value / 8M * 1000M), TimeSpan.FromTicks(1));
                    break;
                case SizeUnit.Mbps:
                    if (sizeLength.Length.TotalSeconds > 0)
                        sizeLength = sizeLength.ToNewSize((long)(NumUpDown.Value / 8M * 1000000M * (decimal)sizeLength.Length.TotalSeconds));
                    break;
            }
            if (ValueChanged != null) ValueChanged(sender, e);
        }

        private void NumUpDown_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) NumUpDown_ValueChanged(sender, e);
        }

        private void NumUpDown_Focus(object sender, EventArgs e)
        {
            NumUpDown.Select(0, NumUpDown.Text.Length);
        }
    }
}
