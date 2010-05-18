using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace BitrateCalc
{
    partial class CalcForm
    {
        private System.Windows.Forms.GroupBox videoGroupbox;
        private System.Windows.Forms.NumericUpDown hours;
        private System.Windows.Forms.NumericUpDown minutes;
        private System.Windows.Forms.NumericUpDown seconds;
        private System.Windows.Forms.Label hoursLabel;
        private System.Windows.Forms.Label minutesLabel;
        private System.Windows.Forms.Label secondsLabel;
        private System.Windows.Forms.Label framerateLabel;
        private System.Windows.Forms.Label totalSecondsLabel;
        private System.Windows.Forms.GroupBox sizeGroupbox;
        private System.Windows.Forms.RadioButton averageBitrateRadio;
        private System.Windows.Forms.RadioButton fileSizeRadio;
        private System.Windows.Forms.Label nbFramesLabel;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.CheckBox bframes;
        private ComboBox videoCodec;
        private ComboBox container;
        private NumericUpDown frames;
        private NumericUpDown totalSeconds;
        private ContextMenuStrip addAudioExtraMenu;
        private ToolStripMenuItem addAudioToolStripMenuItem;
        private ToolStripMenuItem addExtraToolStripMenuItem;
        private Label label7;
        private Label label9;
        private NumericUpDown height;
        private Label label10;
        private NumericUpDown width;
        private Label label8;
        private TrackBar complexity;
        private Label label2;
        private RadioButton bppRadio;
        private NumericUpDown bpp;
        private Label label3;
        private RadioButton qEstRadio;
        private NumericUpDown qest;
        private GroupBox audioExtraGroupbox;
        private FlowLayoutPanel audioExtraFlow;
        private LinkLabel addAudioLink;
        private PictureBox picTime;
        private ToolTip addToolTip;
        private ToolTip toolTip1;
        private TextBox timeText;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public CalcForm()
        {
            InitializeComponent();
        }

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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CalcForm));
            this.videoGroupbox = new System.Windows.Forms.GroupBox();
            this.framerate = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.height = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.width = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.videoCodec = new System.Windows.Forms.ComboBox();
            this.frames = new System.Windows.Forms.NumericUpDown();
            this.bframes = new System.Windows.Forms.CheckBox();
            this.nbFramesLabel = new System.Windows.Forms.Label();
            this.framerateLabel = new System.Windows.Forms.Label();
            this.secondsLabel = new System.Windows.Forms.Label();
            this.minutesLabel = new System.Windows.Forms.Label();
            this.hoursLabel = new System.Windows.Forms.Label();
            this.seconds = new System.Windows.Forms.NumericUpDown();
            this.minutes = new System.Windows.Forms.NumericUpDown();
            this.hours = new System.Windows.Forms.NumericUpDown();
            this.complexity = new System.Windows.Forms.TrackBar();
            this.picTime = new System.Windows.Forms.PictureBox();
            this.totalSecondsLabel = new System.Windows.Forms.Label();
            this.totalSeconds = new System.Windows.Forms.NumericUpDown();
            this.timeText = new System.Windows.Forms.TextBox();
            this.container = new System.Windows.Forms.ComboBox();
            this.sizeGroupbox = new System.Windows.Forms.GroupBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.qEstRadio = new System.Windows.Forms.RadioButton();
            this.qest = new System.Windows.Forms.NumericUpDown();
            this.bppRadio = new System.Windows.Forms.RadioButton();
            this.bpp = new System.Windows.Forms.NumericUpDown();
            this.fileSizeRadio = new System.Windows.Forms.RadioButton();
            this.averageBitrateRadio = new System.Windows.Forms.RadioButton();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.addAudioExtraMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addAudioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addExtraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.audioExtraGroupbox = new System.Windows.Forms.GroupBox();
            this.addAudioLink = new System.Windows.Forms.LinkLabel();
            this.audioExtraFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.addToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.helpLink = new System.Windows.Forms.LinkLabel();
            this.helpMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.checkForUpdatesOnStartupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForUpdatesNowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.visitWebpageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.videoSize = new BitrateCalc.SizeBitrateBox();
            this.totalSize = new BitrateCalc.SizeBitrateBox();
            this.videoGroupbox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.height)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.width)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.frames)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seconds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.complexity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.totalSeconds)).BeginInit();
            this.sizeGroupbox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.qest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bpp)).BeginInit();
            this.addAudioExtraMenu.SuspendLayout();
            this.audioExtraGroupbox.SuspendLayout();
            this.helpMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // videoGroupbox
            // 
            this.videoGroupbox.Controls.Add(this.framerate);
            this.videoGroupbox.Controls.Add(this.label3);
            this.videoGroupbox.Controls.Add(this.label7);
            this.videoGroupbox.Controls.Add(this.label9);
            this.videoGroupbox.Controls.Add(this.height);
            this.videoGroupbox.Controls.Add(this.label10);
            this.videoGroupbox.Controls.Add(this.width);
            this.videoGroupbox.Controls.Add(this.label8);
            this.videoGroupbox.Controls.Add(this.label2);
            this.videoGroupbox.Controls.Add(this.videoCodec);
            this.videoGroupbox.Controls.Add(this.frames);
            this.videoGroupbox.Controls.Add(this.bframes);
            this.videoGroupbox.Controls.Add(this.nbFramesLabel);
            this.videoGroupbox.Controls.Add(this.framerateLabel);
            this.videoGroupbox.Controls.Add(this.secondsLabel);
            this.videoGroupbox.Controls.Add(this.minutesLabel);
            this.videoGroupbox.Controls.Add(this.hoursLabel);
            this.videoGroupbox.Controls.Add(this.seconds);
            this.videoGroupbox.Controls.Add(this.minutes);
            this.videoGroupbox.Controls.Add(this.hours);
            this.videoGroupbox.Controls.Add(this.complexity);
            this.videoGroupbox.Controls.Add(this.picTime);
            this.videoGroupbox.Controls.Add(this.totalSecondsLabel);
            this.videoGroupbox.Controls.Add(this.totalSeconds);
            this.videoGroupbox.Controls.Add(this.timeText);
            this.videoGroupbox.Location = new System.Drawing.Point(13, 10);
            this.videoGroupbox.Name = "videoGroupbox";
            this.videoGroupbox.Size = new System.Drawing.Size(470, 150);
            this.videoGroupbox.TabIndex = 0;
            this.videoGroupbox.TabStop = false;
            this.videoGroupbox.Text = "Video";
            // 
            // framerate
            // 
            this.framerate.FormattingEnabled = true;
            this.framerate.Items.AddRange(new object[] {
            "23.976",
            "24",
            "25",
            "29.97",
            "30",
            "50",
            "59.94",
            "60"});
            this.framerate.Location = new System.Drawing.Point(255, 45);
            this.framerate.Name = "framerate";
            this.framerate.Size = new System.Drawing.Size(70, 21);
            this.framerate.TabIndex = 29;
            this.framerate.TextChanged += new System.EventHandler(this.framerate_Changed);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(291, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "High";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(136, 76);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Complexity";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(401, 77);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(38, 13);
            this.label9.TabIndex = 25;
            this.label9.Text = "Height";
            // 
            // height
            // 
            this.height.Increment = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.height.Location = new System.Drawing.Point(404, 96);
            this.height.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.height.Name = "height";
            this.height.Size = new System.Drawing.Size(50, 21);
            this.height.TabIndex = 11;
            this.height.Value = new decimal(new int[] {
            720,
            0,
            0,
            0});
            this.height.ValueChanged += new System.EventHandler(this.textField_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(336, 77);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 13);
            this.label10.TabIndex = 23;
            this.label10.Text = "Width";
            // 
            // width
            // 
            this.width.Increment = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.width.Location = new System.Drawing.Point(339, 96);
            this.width.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.width.Name = "width";
            this.width.Size = new System.Drawing.Size(50, 21);
            this.width.TabIndex = 10;
            this.width.Value = new decimal(new int[] {
            1280,
            0,
            0,
            0});
            this.width.ValueChanged += new System.EventHandler(this.textField_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(139, 122);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(26, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Low";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Codec";
            // 
            // videoCodec
            // 
            this.videoCodec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.videoCodec.FormattingEnabled = true;
            this.videoCodec.Location = new System.Drawing.Point(14, 95);
            this.videoCodec.Name = "videoCodec";
            this.videoCodec.Size = new System.Drawing.Size(103, 21);
            this.videoCodec.TabIndex = 7;
            this.videoCodec.SelectedIndexChanged += new System.EventHandler(this.value_Changed);
            // 
            // frames
            // 
            this.frames.Location = new System.Drawing.Point(374, 45);
            this.frames.Maximum = new decimal(new int[] {
            10000000,
            10000000,
            10000000,
            0});
            this.frames.Name = "frames";
            this.frames.Size = new System.Drawing.Size(80, 21);
            this.frames.TabIndex = 5;
            this.frames.ThousandsSeparator = true;
            this.frames.ValueChanged += new System.EventHandler(this.textField_TextChanged);
            // 
            // bframes
            // 
            this.bframes.Checked = true;
            this.bframes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.bframes.Location = new System.Drawing.Point(14, 121);
            this.bframes.Name = "bframes";
            this.bframes.Size = new System.Drawing.Size(75, 17);
            this.bframes.TabIndex = 8;
            this.bframes.Text = "B-frames";
            this.bframes.CheckedChanged += new System.EventHandler(this.value_Changed);
            // 
            // nbFramesLabel
            // 
            this.nbFramesLabel.AutoSize = true;
            this.nbFramesLabel.Location = new System.Drawing.Point(371, 26);
            this.nbFramesLabel.Name = "nbFramesLabel";
            this.nbFramesLabel.Size = new System.Drawing.Size(69, 13);
            this.nbFramesLabel.TabIndex = 10;
            this.nbFramesLabel.Text = "Total Frames";
            // 
            // framerateLabel
            // 
            this.framerateLabel.AutoSize = true;
            this.framerateLabel.Location = new System.Drawing.Point(252, 26);
            this.framerateLabel.Name = "framerateLabel";
            this.framerateLabel.Size = new System.Drawing.Size(57, 13);
            this.framerateLabel.TabIndex = 8;
            this.framerateLabel.Text = "Framerate";
            // 
            // secondsLabel
            // 
            this.secondsLabel.AutoSize = true;
            this.secondsLabel.Location = new System.Drawing.Point(165, 26);
            this.secondsLabel.Name = "secondsLabel";
            this.secondsLabel.Size = new System.Drawing.Size(47, 13);
            this.secondsLabel.TabIndex = 4;
            this.secondsLabel.Text = "Seconds";
            // 
            // minutesLabel
            // 
            this.minutesLabel.AutoSize = true;
            this.minutesLabel.Location = new System.Drawing.Point(107, 26);
            this.minutesLabel.Name = "minutesLabel";
            this.minutesLabel.Size = new System.Drawing.Size(44, 13);
            this.minutesLabel.TabIndex = 2;
            this.minutesLabel.Text = "Minutes";
            // 
            // hoursLabel
            // 
            this.hoursLabel.AutoSize = true;
            this.hoursLabel.Location = new System.Drawing.Point(49, 26);
            this.hoursLabel.Name = "hoursLabel";
            this.hoursLabel.Size = new System.Drawing.Size(35, 13);
            this.hoursLabel.TabIndex = 0;
            this.hoursLabel.Text = "Hours";
            // 
            // seconds
            // 
            this.seconds.Location = new System.Drawing.Point(168, 45);
            this.seconds.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.seconds.Name = "seconds";
            this.seconds.Size = new System.Drawing.Size(45, 21);
            this.seconds.TabIndex = 3;
            this.seconds.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.seconds.ValueChanged += new System.EventHandler(this.time_ValueChanged);
            // 
            // minutes
            // 
            this.minutes.Location = new System.Drawing.Point(110, 45);
            this.minutes.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.minutes.Name = "minutes";
            this.minutes.Size = new System.Drawing.Size(45, 21);
            this.minutes.TabIndex = 2;
            this.minutes.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.minutes.ValueChanged += new System.EventHandler(this.time_ValueChanged);
            // 
            // hours
            // 
            this.hours.Location = new System.Drawing.Point(52, 45);
            this.hours.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.hours.Name = "hours";
            this.hours.Size = new System.Drawing.Size(45, 21);
            this.hours.TabIndex = 1;
            this.hours.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.hours.ValueChanged += new System.EventHandler(this.time_ValueChanged);
            // 
            // complexity
            // 
            this.complexity.LargeChange = 2;
            this.complexity.Location = new System.Drawing.Point(134, 95);
            this.complexity.Maximum = 78;
            this.complexity.Minimum = 72;
            this.complexity.Name = "complexity";
            this.complexity.Size = new System.Drawing.Size(188, 45);
            this.complexity.TabIndex = 9;
            this.complexity.TickFrequency = 5;
            this.complexity.TickStyle = System.Windows.Forms.TickStyle.None;
            this.complexity.Value = 75;
            this.complexity.ValueChanged += new System.EventHandler(this.value_Changed);
            // 
            // picTime
            // 
            this.picTime.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picTime.Image = ((System.Drawing.Image)(resources.GetObject("picTime.Image")));
            this.picTime.InitialImage = ((System.Drawing.Image)(resources.GetObject("picTime.InitialImage")));
            this.picTime.Location = new System.Drawing.Point(12, 32);
            this.picTime.Name = "picTime";
            this.picTime.Size = new System.Drawing.Size(32, 32);
            this.picTime.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picTime.TabIndex = 27;
            this.picTime.TabStop = false;
            this.toolTip1.SetToolTip(this.picTime, "Show total seconds");
            this.picTime.Click += new System.EventHandler(this.picTime_Click);
            // 
            // totalSecondsLabel
            // 
            this.totalSecondsLabel.Location = new System.Drawing.Point(49, 26);
            this.totalSecondsLabel.Name = "totalSecondsLabel";
            this.totalSecondsLabel.Size = new System.Drawing.Size(126, 15);
            this.totalSecondsLabel.TabIndex = 6;
            this.totalSecondsLabel.Text = "Total Length in Seconds";
            this.totalSecondsLabel.Visible = false;
            // 
            // totalSeconds
            // 
            this.totalSeconds.Location = new System.Drawing.Point(52, 45);
            this.totalSeconds.Maximum = new decimal(new int[] {
            90000,
            0,
            0,
            0});
            this.totalSeconds.Name = "totalSeconds";
            this.totalSeconds.Size = new System.Drawing.Size(65, 21);
            this.totalSeconds.TabIndex = 1;
            this.totalSeconds.ThousandsSeparator = true;
            this.totalSeconds.Visible = false;
            this.totalSeconds.ValueChanged += new System.EventHandler(this.textField_TextChanged);
            // 
            // timeText
            // 
            this.timeText.Location = new System.Drawing.Point(123, 45);
            this.timeText.Name = "timeText";
            this.timeText.ReadOnly = true;
            this.timeText.Size = new System.Drawing.Size(89, 21);
            this.timeText.TabIndex = 28;
            this.timeText.TabStop = false;
            this.timeText.Text = "0h 0m 0s";
            this.timeText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.timeText.Visible = false;
            // 
            // container
            // 
            this.container.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.container.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.container.FormattingEnabled = true;
            this.container.ItemHeight = 13;
            this.container.Location = new System.Drawing.Point(505, 35);
            this.container.Name = "container";
            this.container.Size = new System.Drawing.Size(91, 21);
            this.container.TabIndex = 14;
            this.container.SelectedIndexChanged += new System.EventHandler(this.value_Changed);
            // 
            // sizeGroupbox
            // 
            this.sizeGroupbox.Controls.Add(this.videoSize);
            this.sizeGroupbox.Controls.Add(this.totalSize);
            this.sizeGroupbox.Controls.Add(this.linkLabel1);
            this.sizeGroupbox.Controls.Add(this.qEstRadio);
            this.sizeGroupbox.Controls.Add(this.qest);
            this.sizeGroupbox.Controls.Add(this.bppRadio);
            this.sizeGroupbox.Controls.Add(this.bpp);
            this.sizeGroupbox.Controls.Add(this.fileSizeRadio);
            this.sizeGroupbox.Controls.Add(this.averageBitrateRadio);
            this.sizeGroupbox.Location = new System.Drawing.Point(499, 71);
            this.sizeGroupbox.Name = "sizeGroupbox";
            this.sizeGroupbox.Size = new System.Drawing.Size(161, 276);
            this.sizeGroupbox.TabIndex = 15;
            this.sizeGroupbox.TabStop = false;
            this.sizeGroupbox.Text = "Calculate By";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(19, 249);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(65, 13);
            this.linkLabel1.TabIndex = 22;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Preset: DVD";
            // 
            // qEstRadio
            // 
            this.qEstRadio.AutoSize = true;
            this.qEstRadio.Location = new System.Drawing.Point(16, 141);
            this.qEstRadio.Name = "qEstRadio";
            this.qEstRadio.Size = new System.Drawing.Size(103, 17);
            this.qEstRadio.TabIndex = 17;
            this.qEstRadio.TabStop = true;
            this.qEstRadio.Text = "Quality Estimate";
            this.qEstRadio.UseVisualStyleBackColor = true;
            this.qEstRadio.CheckedChanged += new System.EventHandler(this.calculationMode_CheckedChanged);
            // 
            // qest
            // 
            this.qest.DecimalPlaces = 2;
            this.qest.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.qest.Location = new System.Drawing.Point(19, 164);
            this.qest.Name = "qest";
            this.qest.ReadOnly = true;
            this.qest.Size = new System.Drawing.Size(70, 21);
            this.qest.TabIndex = 17;
            this.qest.TabStop = false;
            this.qest.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.qest.Value = new decimal(new int[] {
            640,
            0,
            0,
            131072});
            this.qest.ValueChanged += new System.EventHandler(this.textField_TextChanged);
            // 
            // bppRadio
            // 
            this.bppRadio.AutoSize = true;
            this.bppRadio.Location = new System.Drawing.Point(16, 83);
            this.bppRadio.Name = "bppRadio";
            this.bppRadio.Size = new System.Drawing.Size(86, 17);
            this.bppRadio.TabIndex = 16;
            this.bppRadio.TabStop = true;
            this.bppRadio.Text = "Bits Per Pixel";
            this.bppRadio.UseVisualStyleBackColor = true;
            this.bppRadio.CheckedChanged += new System.EventHandler(this.calculationMode_CheckedChanged);
            // 
            // bpp
            // 
            this.bpp.DecimalPlaces = 3;
            this.bpp.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.bpp.Location = new System.Drawing.Point(19, 106);
            this.bpp.Name = "bpp";
            this.bpp.ReadOnly = true;
            this.bpp.Size = new System.Drawing.Size(69, 21);
            this.bpp.TabIndex = 16;
            this.bpp.TabStop = false;
            this.bpp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.bpp.Value = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.bpp.ValueChanged += new System.EventHandler(this.textField_TextChanged);
            // 
            // fileSizeRadio
            // 
            this.fileSizeRadio.AutoSize = true;
            this.fileSizeRadio.Checked = true;
            this.fileSizeRadio.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileSizeRadio.Location = new System.Drawing.Point(16, 199);
            this.fileSizeRadio.Name = "fileSizeRadio";
            this.fileSizeRadio.Size = new System.Drawing.Size(102, 17);
            this.fileSizeRadio.TabIndex = 18;
            this.fileSizeRadio.TabStop = true;
            this.fileSizeRadio.Text = "Total File Size";
            this.fileSizeRadio.CheckedChanged += new System.EventHandler(this.calculationMode_CheckedChanged);
            // 
            // averageBitrateRadio
            // 
            this.averageBitrateRadio.AutoSize = true;
            this.averageBitrateRadio.Location = new System.Drawing.Point(16, 25);
            this.averageBitrateRadio.Name = "averageBitrateRadio";
            this.averageBitrateRadio.Size = new System.Drawing.Size(73, 17);
            this.averageBitrateRadio.TabIndex = 15;
            this.averageBitrateRadio.TabStop = true;
            this.averageBitrateRadio.Text = "Video Size";
            this.averageBitrateRadio.CheckedChanged += new System.EventHandler(this.calculationMode_CheckedChanged);
            // 
            // addAudioExtraMenu
            // 
            this.addAudioExtraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addAudioToolStripMenuItem,
            this.addExtraToolStripMenuItem});
            this.addAudioExtraMenu.Name = "contextMenuStrip1";
            this.addAudioExtraMenu.Size = new System.Drawing.Size(132, 48);
            // 
            // addAudioToolStripMenuItem
            // 
            this.addAudioToolStripMenuItem.Name = "addAudioToolStripMenuItem";
            this.addAudioToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.addAudioToolStripMenuItem.Text = "Add &Audio";
            this.addAudioToolStripMenuItem.Click += new System.EventHandler(this.addAudioToolStripMenuItem_Click);
            // 
            // addExtraToolStripMenuItem
            // 
            this.addExtraToolStripMenuItem.Name = "addExtraToolStripMenuItem";
            this.addExtraToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.addExtraToolStripMenuItem.Text = "Add &Extra";
            this.addExtraToolStripMenuItem.Click += new System.EventHandler(this.addExtraToolStripMenuItem_Click);
            // 
            // audioExtraGroupbox
            // 
            this.audioExtraGroupbox.ContextMenuStrip = this.addAudioExtraMenu;
            this.audioExtraGroupbox.Controls.Add(this.addAudioLink);
            this.audioExtraGroupbox.Controls.Add(this.audioExtraFlow);
            this.audioExtraGroupbox.Location = new System.Drawing.Point(13, 169);
            this.audioExtraGroupbox.Name = "audioExtraGroupbox";
            this.audioExtraGroupbox.Size = new System.Drawing.Size(470, 178);
            this.audioExtraGroupbox.TabIndex = 13;
            this.audioExtraGroupbox.TabStop = false;
            this.audioExtraGroupbox.Text = "Audio && Extra";
            // 
            // addAudioLink
            // 
            this.addAudioLink.Cursor = System.Windows.Forms.Cursors.Hand;
            this.addAudioLink.Image = ((System.Drawing.Image)(resources.GetObject("addAudioLink.Image")));
            this.addAudioLink.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.addAudioLink.Location = new System.Drawing.Point(387, 0);
            this.addAudioLink.Name = "addAudioLink";
            this.addAudioLink.Padding = new System.Windows.Forms.Padding(16, 3, 2, 3);
            this.addAudioLink.Size = new System.Drawing.Size(49, 18);
            this.addAudioLink.TabIndex = 12;
            this.addAudioLink.TabStop = true;
            this.addAudioLink.Text = "Add";
            this.addAudioLink.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.addToolTip.SetToolTip(this.addAudioLink, "Audio track or extra data");
            this.addAudioLink.Click += new System.EventHandler(this.addAudioLink_Clicked);
            // 
            // audioExtraFlow
            // 
            this.audioExtraFlow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.audioExtraFlow.AutoScroll = true;
            this.audioExtraFlow.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.audioExtraFlow.Location = new System.Drawing.Point(3, 18);
            this.audioExtraFlow.Name = "audioExtraFlow";
            this.audioExtraFlow.Size = new System.Drawing.Size(462, 157);
            this.audioExtraFlow.TabIndex = 13;
            this.audioExtraFlow.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.value_Changed);
            // 
            // addToolTip
            // 
            this.addToolTip.AutomaticDelay = 300;
            this.addToolTip.ToolTipTitle = "Add";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(502, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Container";
            // 
            // helpLink
            // 
            this.helpLink.AutoSize = true;
            this.helpLink.Location = new System.Drawing.Point(633, 14);
            this.helpLink.Name = "helpLink";
            this.helpLink.Size = new System.Drawing.Size(27, 13);
            this.helpLink.TabIndex = 17;
            this.helpLink.TabStop = true;
            this.helpLink.Text = "help";
            this.helpLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.helpLink_LinkClicked);
            // 
            // helpMenu
            // 
            this.helpMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkForUpdatesOnStartupToolStripMenuItem,
            this.checkForUpdatesNowToolStripMenuItem,
            this.visitWebpageToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpMenu.Name = "helpMenu";
            this.helpMenu.Size = new System.Drawing.Size(230, 92);
            // 
            // checkForUpdatesOnStartupToolStripMenuItem
            // 
            this.checkForUpdatesOnStartupToolStripMenuItem.Checked = true;
            this.checkForUpdatesOnStartupToolStripMenuItem.CheckOnClick = true;
            this.checkForUpdatesOnStartupToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkForUpdatesOnStartupToolStripMenuItem.Name = "checkForUpdatesOnStartupToolStripMenuItem";
            this.checkForUpdatesOnStartupToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.checkForUpdatesOnStartupToolStripMenuItem.Text = "Check for Updates on Startup";
            this.checkForUpdatesOnStartupToolStripMenuItem.CheckedChanged += new System.EventHandler(this.checkForUpdatesOnStartupToolStripMenuItem_CheckedChanged);
            // 
            // checkForUpdatesNowToolStripMenuItem
            // 
            this.checkForUpdatesNowToolStripMenuItem.Name = "checkForUpdatesNowToolStripMenuItem";
            this.checkForUpdatesNowToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.checkForUpdatesNowToolStripMenuItem.Text = "Check for Updates Now";
            this.checkForUpdatesNowToolStripMenuItem.Click += new System.EventHandler(this.checkForUpdatesNowToolStripMenuItem_Click);
            // 
            // visitWebpageToolStripMenuItem
            // 
            this.visitWebpageToolStripMenuItem.Name = "visitWebpageToolStripMenuItem";
            this.visitWebpageToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.visitWebpageToolStripMenuItem.Text = "View MeGUI Wiki";
            this.visitWebpageToolStripMenuItem.Click += new System.EventHandler(this.visitWebpageToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // videoSize
            // 
            this.videoSize.AutoSize = true;
            this.videoSize.Location = new System.Drawing.Point(19, 45);
            this.videoSize.Name = "videoSize";
            this.videoSize.ReadOnly = true;
            this.videoSize.Size = new System.Drawing.Size(122, 24);
            this.videoSize.SizeUnit = BitrateCalc.SizeUnit.Kbps;
            this.videoSize.TabIndex = 24;
            this.videoSize.ValueChanged += new System.EventHandler(this.textField_TextChanged);
            // 
            // totalSize
            // 
            this.totalSize.AutoSize = true;
            this.totalSize.Location = new System.Drawing.Point(19, 220);
            this.totalSize.Name = "totalSize";
            this.totalSize.ReadOnly = false;
            this.totalSize.Size = new System.Drawing.Size(122, 24);
            this.totalSize.SizeUnit = BitrateCalc.SizeUnit.GB;
            this.totalSize.TabIndex = 23;
            this.totalSize.ValueChanged += new System.EventHandler(this.textField_TextChanged);
            // 
            // CalcForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.ClientSize = new System.Drawing.Size(676, 360);
            this.Controls.Add(this.helpLink);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.container);
            this.Controls.Add(this.audioExtraGroupbox);
            this.Controls.Add(this.videoGroupbox);
            this.Controls.Add(this.sizeGroupbox);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "CalcForm";
            this.Text = "Bitrate Calculator";
            this.Load += new System.EventHandler(this.CalcForm_Load);
            this.videoGroupbox.ResumeLayout(false);
            this.videoGroupbox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.height)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.width)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.frames)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seconds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.complexity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.totalSeconds)).EndInit();
            this.sizeGroupbox.ResumeLayout(false);
            this.sizeGroupbox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.qest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bpp)).EndInit();
            this.addAudioExtraMenu.ResumeLayout(false);
            this.audioExtraGroupbox.ResumeLayout(false);
            this.helpMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private ComboBox framerate;
        private Label label1;
        private LinkLabel linkLabel1;
        private SizeBitrateBox videoSize;
        private SizeBitrateBox totalSize;
        private LinkLabel helpLink;
        private ContextMenuStrip helpMenu;
        private ToolStripMenuItem checkForUpdatesOnStartupToolStripMenuItem;
        private ToolStripMenuItem checkForUpdatesNowToolStripMenuItem;
        private ToolStripMenuItem visitWebpageToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
    }
}
