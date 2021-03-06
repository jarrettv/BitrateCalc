namespace BitrateCalc
{
    partial class ExtraSizeTab
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExtraSizeTab));
            this.label2 = new System.Windows.Forms.Label();
            this.selectButton = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.audioLabel = new System.Windows.Forms.Label();
            this.name = new System.Windows.Forms.TextBox();
            this.removeLink = new System.Windows.Forms.LinkLabel();
            this.removalToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.size = new BitrateCalc.SizeBitrateBox();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(322, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Size";
            // 
            // selectButton
            // 
            this.selectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.selectButton.Location = new System.Drawing.Point(279, 27);
            this.selectButton.Name = "selectButton";
            this.selectButton.Size = new System.Drawing.Size(24, 21);
            this.selectButton.TabIndex = 1;
            this.selectButton.Text = "...";
            this.selectButton.Click += new System.EventHandler(this.selectButton_Click);
            // 
            // audioLabel
            // 
            this.audioLabel.Location = new System.Drawing.Point(30, 8);
            this.audioLabel.Name = "audioLabel";
            this.audioLabel.Size = new System.Drawing.Size(40, 16);
            this.audioLabel.TabIndex = 31;
            this.audioLabel.Text = "Extra";
            // 
            // name
            // 
            this.name.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.name.Location = new System.Drawing.Point(8, 28);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(268, 20);
            this.name.TabIndex = 0;
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
            this.removeLink.TabIndex = 3;
            this.removeLink.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.removalToolTip.SetToolTip(this.removeLink, "Extra data");
            this.removeLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.removeLink_LinkClicked);
            this.removeLink.Click += new System.EventHandler(this.removeLink_LinkClicked);
            // 
            // removalToolTip
            // 
            this.removalToolTip.AutomaticDelay = 300;
            this.removalToolTip.ToolTipTitle = "Remove";
            // 
            // size
            // 
            this.size.AutoSize = true;
            this.size.Location = new System.Drawing.Point(325, 25);
            this.size.Name = "size";
            this.size.ReadOnly = false;
            this.size.Size = new System.Drawing.Size(107, 22);
            this.size.SizeUnit = BitrateCalc.SizeUnit.MB;
            this.size.TabIndex = 32;
            this.size.ValueChanged += new System.EventHandler(this.size_ValueChanged);
            // 
            // ExtraSizeTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.size);
            this.Controls.Add(this.removeLink);
            this.Controls.Add(this.name);
            this.Controls.Add(this.audioLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.selectButton);
            this.Name = "ExtraSizeTab";
            this.Size = new System.Drawing.Size(435, 50);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button selectButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label audioLabel;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.LinkLabel removeLink;
        private System.Windows.Forms.ToolTip removalToolTip;
        private BitrateCalc.SizeBitrateBox size;
    }
}
