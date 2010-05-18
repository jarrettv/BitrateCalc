namespace BitrateCalc
{
    partial class AudioTrackSizeTab
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AudioTrackSizeTab));
            this.selectButton = new System.Windows.Forms.Button();
            this.audioCodec = new System.Windows.Forms.ComboBox();
            this.audio1TypeLabel = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.audioLabel = new System.Windows.Forms.Label();
            this.name = new System.Windows.Forms.TextBox();
            this.removeLink = new System.Windows.Forms.LinkLabel();
            this.removalToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.size = new BitrateCalc.SizeBitrateBox();
            this.SuspendLayout();
            // 
            // selectButton
            // 
            this.selectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.selectButton.Location = new System.Drawing.Point(194, 27);
            this.selectButton.Name = "selectButton";
            this.selectButton.Size = new System.Drawing.Size(24, 21);
            this.selectButton.TabIndex = 0;
            this.selectButton.Text = "...";
            this.selectButton.Click += new System.EventHandler(this.selectButton_Click);
            // 
            // audioCodec
            // 
            this.audioCodec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.audioCodec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.audioCodec.Location = new System.Drawing.Point(235, 27);
            this.audioCodec.Name = "audioCodec";
            this.audioCodec.Size = new System.Drawing.Size(75, 21);
            this.audioCodec.TabIndex = 1;
            this.audioCodec.SelectedIndexChanged += new System.EventHandler(this.audioCodec_SelectedIndexChanged);
            // 
            // audio1TypeLabel
            // 
            this.audio1TypeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.audio1TypeLabel.Location = new System.Drawing.Point(232, 8);
            this.audio1TypeLabel.Name = "audio1TypeLabel";
            this.audio1TypeLabel.Size = new System.Drawing.Size(40, 16);
            this.audio1TypeLabel.TabIndex = 26;
            this.audio1TypeLabel.Text = "Type";
            // 
            // audioLabel
            // 
            this.audioLabel.Location = new System.Drawing.Point(30, 8);
            this.audioLabel.Name = "audioLabel";
            this.audioLabel.Size = new System.Drawing.Size(40, 16);
            this.audioLabel.TabIndex = 31;
            this.audioLabel.Text = "Audio";
            // 
            // name
            // 
            this.name.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.name.Location = new System.Drawing.Point(8, 28);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(183, 20);
            this.name.TabIndex = 32;
            this.name.TabStop = false;
            // 
            // removeLink
            // 
            this.removeLink.Cursor = System.Windows.Forms.Cursors.Hand;
            this.removeLink.Image = ((System.Drawing.Image)(resources.GetObject("removeLink.Image")));
            this.removeLink.Location = new System.Drawing.Point(5, 3);
            this.removeLink.Name = "removeLink";
            this.removeLink.Padding = new System.Windows.Forms.Padding(16, 0, 3, 3);
            this.removeLink.Size = new System.Drawing.Size(27, 23);
            this.removeLink.TabIndex = 4;
            this.removeLink.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.removalToolTip.SetToolTip(this.removeLink, "Audio track");
            this.removeLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.removeLink_LinkClicked);
            this.removeLink.Click += new System.EventHandler(this.removeLink_LinkClicked);
            // 
            // removalToolTip
            // 
            this.removalToolTip.AutomaticDelay = 300;
            this.removalToolTip.ToolTipTitle = "Remove";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(322, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 33;
            this.label2.Text = "Size";
            // 
            // size
            // 
            this.size.AutoSize = true;
            this.size.Location = new System.Drawing.Point(325, 25);
            this.size.Name = "size";
            this.size.ReadOnly = false;
            this.size.Size = new System.Drawing.Size(107, 22);
            this.size.SizeUnit = BitrateCalc.SizeUnit.Kbps;
            this.size.TabIndex = 34;
            this.size.ValueChanged += new System.EventHandler(this.size_ValueChanged);
            // 
            // AudioTrackSizeTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.size);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.removeLink);
            this.Controls.Add(this.name);
            this.Controls.Add(this.audioLabel);
            this.Controls.Add(this.selectButton);
            this.Controls.Add(this.audioCodec);
            this.Controls.Add(this.audio1TypeLabel);
            this.Name = "AudioTrackSizeTab";
            this.Size = new System.Drawing.Size(435, 50);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button selectButton;
        private System.Windows.Forms.ComboBox audioCodec;
        private System.Windows.Forms.Label audio1TypeLabel;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label audioLabel;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.LinkLabel removeLink;
        private System.Windows.Forms.ToolTip removalToolTip;
        private SizeBitrateBox size;
        private System.Windows.Forms.Label label2;
    }
}
