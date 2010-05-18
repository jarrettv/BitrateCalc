namespace BitrateCalc
{
    partial class SizeBitrateBox
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.NumUpDown = new System.Windows.Forms.NumericUpDown();
            this.UnitSwitch = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.NumUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // NumUpDown
            // 
            this.NumUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NumUpDown.Location = new System.Drawing.Point(0, 2);
            this.NumUpDown.Name = "NumUpDown";
            this.NumUpDown.Size = new System.Drawing.Size(105, 20);
            this.NumUpDown.TabIndex = 0;
            this.NumUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.NumUpDown.ThousandsSeparator = true;
            this.NumUpDown.ValueChanged += new System.EventHandler(this.NumUpDown_ValueChanged);
            // 
            // UnitSwitch
            // 
            this.UnitSwitch.Dock = System.Windows.Forms.DockStyle.Right;
            this.UnitSwitch.Location = new System.Drawing.Point(108, 0);
            this.UnitSwitch.Name = "UnitSwitch";
            this.UnitSwitch.Size = new System.Drawing.Size(42, 22);
            this.UnitSwitch.TabIndex = 1;
            this.UnitSwitch.Text = "kbps";
            this.UnitSwitch.UseVisualStyleBackColor = true;
            this.UnitSwitch.Click += new System.EventHandler(this.UnitSwitch_Click);
            // 
            // SizeBitrateBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.UnitSwitch);
            this.Controls.Add(this.NumUpDown);
            this.Name = "SizeBitrateBox";
            this.Size = new System.Drawing.Size(150, 22);
            ((System.ComponentModel.ISupportInitialize)(this.NumUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Button UnitSwitch;
        protected System.Windows.Forms.NumericUpDown NumUpDown;

    }
}
