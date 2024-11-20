
namespace FIPDisplayProfiler
{
    partial class AltimeterForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label4 = new System.Windows.Forms.Label();
            this.tbFont = new System.Windows.Forms.TextBox();
            this.btnFont = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.btnFontColor = new System.Windows.Forms.Button();
            this.cbDrawRim = new System.Windows.Forms.CheckBox();
            this.cbDrawNumerals = new System.Windows.Forms.CheckBox();
            this.pbGaugeImage = new System.Windows.Forms.PictureBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.btnFaceColorHigh = new System.Windows.Forms.Button();
            this.btnFaceColorLow = new System.Windows.Forms.Button();
            this.btnRimColorOutside = new System.Windows.Forms.Button();
            this.cbDrawThousandsHand = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cbDrawHundredsHand = new System.Windows.Forms.CheckBox();
            this.cbDrawTenThousandsHand = new System.Windows.Forms.CheckBox();
            this.btnNeedleColor = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.cbFaceGradientMode = new System.Windows.Forms.ComboBox();
            this.label24 = new System.Windows.Forms.Label();
            this.numRimWidth = new System.Windows.Forms.NumericUpDown();
            this.cbDrawTickMarks = new System.Windows.Forms.CheckBox();
            this.label27 = new System.Windows.Forms.Label();
            this.numFaceTickMarkWidth = new System.Windows.Forms.NumericUpDown();
            this.label28 = new System.Windows.Forms.Label();
            this.numFaceTickMarkLength = new System.Windows.Forms.NumericUpDown();
            this.label29 = new System.Windows.Forms.Label();
            this.numHundredsHandLengthOffset = new System.Windows.Forms.NumericUpDown();
            this.label30 = new System.Windows.Forms.Label();
            this.numTenThousandsHandLengthOffset = new System.Windows.Forms.NumericUpDown();
            this.label31 = new System.Windows.Forms.Label();
            this.numThousandsHandLengthOffset = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.btnRimColorInside = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cbShowKollsman = new System.Windows.Forms.CheckBox();
            this.cbShowAltitudeStripes = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbGaugeImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRimWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFaceTickMarkWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFaceTickMarkLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHundredsHandLengthOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTenThousandsHandLengthOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numThousandsHandLengthOffset)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(702, 280);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(783, 280);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "jpg";
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.png;*.gif;*.tif;*.tiff|Jpeg Images|*.jpg;*.jpeg|" +
    "Bitmap Images|*.bmp|PNG Images|*.png|GIF Images|*.gif|TIFF Images|*.tif;*.tiff";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(417, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 128;
            this.label4.Text = "Font:";
            // 
            // tbFont
            // 
            this.tbFont.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFont.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tbFont.Location = new System.Drawing.Point(454, 17);
            this.tbFont.Name = "tbFont";
            this.tbFont.ReadOnly = true;
            this.tbFont.Size = new System.Drawing.Size(303, 20);
            this.tbFont.TabIndex = 125;
            this.tbFont.TabStop = false;
            // 
            // btnFont
            // 
            this.btnFont.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFont.Location = new System.Drawing.Point(763, 16);
            this.btnFont.Name = "btnFont";
            this.btnFont.Size = new System.Drawing.Size(75, 23);
            this.btnFont.TabIndex = 126;
            this.btnFont.Text = "Select &Font";
            this.btnFont.UseVisualStyleBackColor = true;
            this.btnFont.Click += new System.EventHandler(this.btnFont_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(413, 50);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(34, 13);
            this.label18.TabIndex = 129;
            this.label18.Text = "Color:";
            // 
            // btnFontColor
            // 
            this.btnFontColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFontColor.Location = new System.Drawing.Point(454, 45);
            this.btnFontColor.Name = "btnFontColor";
            this.btnFontColor.Size = new System.Drawing.Size(100, 21);
            this.btnFontColor.TabIndex = 127;
            this.btnFontColor.UseVisualStyleBackColor = true;
            this.btnFontColor.Click += new System.EventHandler(this.btnFontColor_Click);
            // 
            // cbDrawRim
            // 
            this.cbDrawRim.AutoSize = true;
            this.cbDrawRim.Location = new System.Drawing.Point(5, 150);
            this.cbDrawRim.Name = "cbDrawRim";
            this.cbDrawRim.Size = new System.Drawing.Size(72, 17);
            this.cbDrawRim.TabIndex = 152;
            this.cbDrawRim.Text = "Draw Rim";
            this.cbDrawRim.UseVisualStyleBackColor = true;
            this.cbDrawRim.CheckedChanged += new System.EventHandler(this.cbDrawRim_CheckedChanged);
            // 
            // cbDrawNumerals
            // 
            this.cbDrawNumerals.AutoSize = true;
            this.cbDrawNumerals.Location = new System.Drawing.Point(435, 236);
            this.cbDrawNumerals.Name = "cbDrawNumerals";
            this.cbDrawNumerals.Size = new System.Drawing.Size(98, 17);
            this.cbDrawNumerals.TabIndex = 130;
            this.cbDrawNumerals.Text = "Draw Numerals";
            this.cbDrawNumerals.UseVisualStyleBackColor = true;
            // 
            // pbGaugeImage
            // 
            this.pbGaugeImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbGaugeImage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbGaugeImage.Location = new System.Drawing.Point(454, 75);
            this.pbGaugeImage.Name = "pbGaugeImage";
            this.pbGaugeImage.Size = new System.Drawing.Size(100, 100);
            this.pbGaugeImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbGaugeImage.TabIndex = 140;
            this.pbGaugeImage.TabStop = false;
            this.pbGaugeImage.Click += new System.EventHandler(this.pbGaugeImage_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(560, 75);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 158;
            this.btnBrowse.Text = "&Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(414, 74);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(34, 13);
            this.label12.TabIndex = 149;
            this.label12.Text = "Face:";
            // 
            // btnFaceColorHigh
            // 
            this.btnFaceColorHigh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFaceColorHigh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFaceColorHigh.Location = new System.Drawing.Point(737, 101);
            this.btnFaceColorHigh.Name = "btnFaceColorHigh";
            this.btnFaceColorHigh.Size = new System.Drawing.Size(101, 21);
            this.btnFaceColorHigh.TabIndex = 161;
            this.btnFaceColorHigh.UseVisualStyleBackColor = true;
            this.btnFaceColorHigh.Click += new System.EventHandler(this.btnFaceColorHigh_Click);
            // 
            // btnFaceColorLow
            // 
            this.btnFaceColorLow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFaceColorLow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFaceColorLow.Location = new System.Drawing.Point(737, 128);
            this.btnFaceColorLow.Name = "btnFaceColorLow";
            this.btnFaceColorLow.Size = new System.Drawing.Size(101, 21);
            this.btnFaceColorLow.TabIndex = 162;
            this.btnFaceColorLow.UseVisualStyleBackColor = true;
            this.btnFaceColorLow.Click += new System.EventHandler(this.btnFaceColorLow_Click);
            // 
            // btnRimColorOutside
            // 
            this.btnRimColorOutside.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRimColorOutside.Location = new System.Drawing.Point(266, 148);
            this.btnRimColorOutside.Name = "btnRimColorOutside";
            this.btnRimColorOutside.Size = new System.Drawing.Size(111, 21);
            this.btnRimColorOutside.TabIndex = 148;
            this.btnRimColorOutside.UseVisualStyleBackColor = true;
            this.btnRimColorOutside.Click += new System.EventHandler(this.btnRimColorOutside_Click);
            // 
            // cbDrawThousandsHand
            // 
            this.cbDrawThousandsHand.AutoSize = true;
            this.cbDrawThousandsHand.Location = new System.Drawing.Point(6, 48);
            this.cbDrawThousandsHand.Name = "cbDrawThousandsHand";
            this.cbDrawThousandsHand.Size = new System.Drawing.Size(144, 17);
            this.cbDrawThousandsHand.TabIndex = 132;
            this.cbDrawThousandsHand.Text = "Draw Thousands Needle";
            this.cbDrawThousandsHand.UseVisualStyleBackColor = true;
            this.cbDrawThousandsHand.CheckedChanged += new System.EventHandler(this.cbDrawThousandsHand_CheckedChanged);
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(660, 190);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(71, 13);
            this.label14.TabIndex = 154;
            this.label14.Text = "Needle Color:";
            // 
            // cbDrawHundredsHand
            // 
            this.cbDrawHundredsHand.AutoSize = true;
            this.cbDrawHundredsHand.Location = new System.Drawing.Point(5, 74);
            this.cbDrawHundredsHand.Name = "cbDrawHundredsHand";
            this.cbDrawHundredsHand.Size = new System.Drawing.Size(137, 17);
            this.cbDrawHundredsHand.TabIndex = 135;
            this.cbDrawHundredsHand.Text = "Draw Hundreds Needle";
            this.cbDrawHundredsHand.UseVisualStyleBackColor = true;
            this.cbDrawHundredsHand.CheckedChanged += new System.EventHandler(this.cbDrawHundredsHand_CheckedChanged);
            // 
            // cbDrawTenThousandsHand
            // 
            this.cbDrawTenThousandsHand.AutoSize = true;
            this.cbDrawTenThousandsHand.Location = new System.Drawing.Point(6, 22);
            this.cbDrawTenThousandsHand.Name = "cbDrawTenThousandsHand";
            this.cbDrawTenThousandsHand.Size = new System.Drawing.Size(166, 17);
            this.cbDrawTenThousandsHand.TabIndex = 138;
            this.cbDrawTenThousandsHand.Text = "Draw Ten Thousands Needle";
            this.cbDrawTenThousandsHand.UseVisualStyleBackColor = true;
            this.cbDrawTenThousandsHand.CheckedChanged += new System.EventHandler(this.cbDrawTenThousandsHand_CheckedChanged);
            // 
            // btnNeedleColor
            // 
            this.btnNeedleColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNeedleColor.Enabled = false;
            this.btnNeedleColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNeedleColor.Location = new System.Drawing.Point(737, 186);
            this.btnNeedleColor.Name = "btnNeedleColor";
            this.btnNeedleColor.Size = new System.Drawing.Size(101, 21);
            this.btnNeedleColor.TabIndex = 141;
            this.btnNeedleColor.UseVisualStyleBackColor = true;
            this.btnNeedleColor.Click += new System.EventHandler(this.btnNeedleColor_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(560, 104);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 159;
            this.btnRemove.Text = "&Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // cbFaceGradientMode
            // 
            this.cbFaceGradientMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbFaceGradientMode.DisplayMember = "Name";
            this.cbFaceGradientMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFaceGradientMode.FormattingEnabled = true;
            this.cbFaceGradientMode.Location = new System.Drawing.Point(737, 74);
            this.cbFaceGradientMode.Name = "cbFaceGradientMode";
            this.cbFaceGradientMode.Size = new System.Drawing.Size(101, 21);
            this.cbFaceGradientMode.Sorted = true;
            this.cbFaceGradientMode.TabIndex = 160;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(222, 204);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(38, 13);
            this.label24.TabIndex = 164;
            this.label24.Text = "Width:";
            // 
            // numRimWidth
            // 
            this.numRimWidth.Location = new System.Drawing.Point(266, 202);
            this.numRimWidth.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numRimWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRimWidth.Name = "numRimWidth";
            this.numRimWidth.Size = new System.Drawing.Size(111, 20);
            this.numRimWidth.TabIndex = 153;
            this.numRimWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cbDrawTickMarks
            // 
            this.cbDrawTickMarks.AutoSize = true;
            this.cbDrawTickMarks.Location = new System.Drawing.Point(5, 101);
            this.cbDrawTickMarks.Name = "cbDrawTickMarks";
            this.cbDrawTickMarks.Size = new System.Drawing.Size(107, 17);
            this.cbDrawTickMarks.TabIndex = 147;
            this.cbDrawTickMarks.Text = "Draw Tick Marks";
            this.cbDrawTickMarks.UseVisualStyleBackColor = true;
            this.cbDrawTickMarks.CheckedChanged += new System.EventHandler(this.cbDrawTickMarks_CheckedChanged);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(222, 100);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(38, 13);
            this.label27.TabIndex = 165;
            this.label27.Text = "Width:";
            // 
            // numFaceTickMarkWidth
            // 
            this.numFaceTickMarkWidth.Location = new System.Drawing.Point(266, 98);
            this.numFaceTickMarkWidth.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numFaceTickMarkWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numFaceTickMarkWidth.Name = "numFaceTickMarkWidth";
            this.numFaceTickMarkWidth.Size = new System.Drawing.Size(111, 20);
            this.numFaceTickMarkWidth.TabIndex = 150;
            this.numFaceTickMarkWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(217, 124);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(43, 13);
            this.label28.TabIndex = 166;
            this.label28.Text = "Length:";
            // 
            // numFaceTickMarkLength
            // 
            this.numFaceTickMarkLength.Location = new System.Drawing.Point(266, 122);
            this.numFaceTickMarkLength.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numFaceTickMarkLength.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numFaceTickMarkLength.Name = "numFaceTickMarkLength";
            this.numFaceTickMarkLength.Size = new System.Drawing.Size(111, 20);
            this.numFaceTickMarkLength.TabIndex = 151;
            this.numFaceTickMarkLength.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(187, 48);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(74, 13);
            this.label29.TabIndex = 167;
            this.label29.Text = "Length Offset:";
            // 
            // numHundredsHandLengthOffset
            // 
            this.numHundredsHandLengthOffset.Location = new System.Drawing.Point(267, 72);
            this.numHundredsHandLengthOffset.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numHundredsHandLengthOffset.Minimum = new decimal(new int[] {
            75,
            0,
            0,
            -2147483648});
            this.numHundredsHandLengthOffset.Name = "numHundredsHandLengthOffset";
            this.numHundredsHandLengthOffset.Size = new System.Drawing.Size(110, 20);
            this.numHundredsHandLengthOffset.TabIndex = 136;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(186, 74);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(74, 13);
            this.label30.TabIndex = 168;
            this.label30.Text = "Length Offset:";
            // 
            // numTenThousandsHandLengthOffset
            // 
            this.numTenThousandsHandLengthOffset.Location = new System.Drawing.Point(267, 20);
            this.numTenThousandsHandLengthOffset.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numTenThousandsHandLengthOffset.Minimum = new decimal(new int[] {
            75,
            0,
            0,
            -2147483648});
            this.numTenThousandsHandLengthOffset.Name = "numTenThousandsHandLengthOffset";
            this.numTenThousandsHandLengthOffset.Size = new System.Drawing.Size(111, 20);
            this.numTenThousandsHandLengthOffset.TabIndex = 139;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(187, 22);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(74, 13);
            this.label31.TabIndex = 169;
            this.label31.Text = "Length Offset:";
            // 
            // numThousandsHandLengthOffset
            // 
            this.numThousandsHandLengthOffset.Location = new System.Drawing.Point(267, 46);
            this.numThousandsHandLengthOffset.Maximum = new decimal(new int[] {
            45,
            0,
            0,
            0});
            this.numThousandsHandLengthOffset.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            -2147483648});
            this.numThousandsHandLengthOffset.Name = "numThousandsHandLengthOffset";
            this.numThousandsHandLengthOffset.Size = new System.Drawing.Size(111, 20);
            this.numThousandsHandLengthOffset.TabIndex = 133;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(187, 151);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 170;
            this.label2.Text = "Outside Color:";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(672, 105);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 13);
            this.label7.TabIndex = 171;
            this.label7.Text = "Start Color:";
            // 
            // label21
            // 
            this.label21.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(651, 77);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(80, 13);
            this.label21.TabIndex = 172;
            this.label21.Text = "Gradient Mode:";
            // 
            // label33
            // 
            this.label33.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(675, 132);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(56, 13);
            this.label33.TabIndex = 173;
            this.label33.Text = "End Color:";
            // 
            // btnRimColorInside
            // 
            this.btnRimColorInside.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRimColorInside.Location = new System.Drawing.Point(266, 175);
            this.btnRimColorInside.Name = "btnRimColorInside";
            this.btnRimColorInside.Size = new System.Drawing.Size(111, 21);
            this.btnRimColorInside.TabIndex = 174;
            this.btnRimColorInside.UseVisualStyleBackColor = true;
            this.btnRimColorInside.Click += new System.EventHandler(this.btnRimColorInside_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(195, 179);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 175;
            this.label3.Text = "Inside Color:";
            // 
            // cbShowKollsman
            // 
            this.cbShowKollsman.AutoSize = true;
            this.cbShowKollsman.Location = new System.Drawing.Point(435, 189);
            this.cbShowKollsman.Name = "cbShowKollsman";
            this.cbShowKollsman.Size = new System.Drawing.Size(140, 17);
            this.cbShowKollsman.TabIndex = 176;
            this.cbShowKollsman.Text = "Show Kollsman Window";
            this.cbShowKollsman.UseVisualStyleBackColor = true;
            // 
            // cbShowAltitudeStripes
            // 
            this.cbShowAltitudeStripes.AutoSize = true;
            this.cbShowAltitudeStripes.Location = new System.Drawing.Point(435, 213);
            this.cbShowAltitudeStripes.Name = "cbShowAltitudeStripes";
            this.cbShowAltitudeStripes.Size = new System.Drawing.Size(126, 17);
            this.cbShowAltitudeStripes.TabIndex = 177;
            this.cbShowAltitudeStripes.Text = "Show Altitude Stripes";
            this.cbShowAltitudeStripes.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cbShowAltitudeStripes);
            this.groupBox1.Controls.Add(this.cbShowKollsman);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnRimColorInside);
            this.groupBox1.Controls.Add(this.label33);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.numThousandsHandLengthOffset);
            this.groupBox1.Controls.Add(this.label31);
            this.groupBox1.Controls.Add(this.numTenThousandsHandLengthOffset);
            this.groupBox1.Controls.Add(this.label30);
            this.groupBox1.Controls.Add(this.numHundredsHandLengthOffset);
            this.groupBox1.Controls.Add(this.label29);
            this.groupBox1.Controls.Add(this.numFaceTickMarkLength);
            this.groupBox1.Controls.Add(this.label28);
            this.groupBox1.Controls.Add(this.numFaceTickMarkWidth);
            this.groupBox1.Controls.Add(this.label27);
            this.groupBox1.Controls.Add(this.cbDrawTickMarks);
            this.groupBox1.Controls.Add(this.numRimWidth);
            this.groupBox1.Controls.Add(this.label24);
            this.groupBox1.Controls.Add(this.cbFaceGradientMode);
            this.groupBox1.Controls.Add(this.btnRemove);
            this.groupBox1.Controls.Add(this.btnNeedleColor);
            this.groupBox1.Controls.Add(this.cbDrawTenThousandsHand);
            this.groupBox1.Controls.Add(this.cbDrawHundredsHand);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.cbDrawThousandsHand);
            this.groupBox1.Controls.Add(this.btnRimColorOutside);
            this.groupBox1.Controls.Add(this.btnFaceColorLow);
            this.groupBox1.Controls.Add(this.btnFaceColorHigh);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.btnBrowse);
            this.groupBox1.Controls.Add(this.pbGaugeImage);
            this.groupBox1.Controls.Add(this.cbDrawNumerals);
            this.groupBox1.Controls.Add(this.cbDrawRim);
            this.groupBox1.Controls.Add(this.btnFontColor);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.btnFont);
            this.groupBox1.Controls.Add(this.tbFont);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(846, 261);
            this.groupBox1.TabIndex = 120;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Altimeter Properties";
            // 
            // AltimeterForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(870, 314);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AltimeterForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Altimeter";
            this.Load += new System.EventHandler(this.FSUIPCAltimeterForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbGaugeImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRimWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFaceTickMarkWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFaceTickMarkLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHundredsHandLengthOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTenThousandsHandLengthOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numThousandsHandLengthOffset)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbFont;
        private System.Windows.Forms.Button btnFont;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button btnFontColor;
        private System.Windows.Forms.CheckBox cbDrawRim;
        private System.Windows.Forms.CheckBox cbDrawNumerals;
        private System.Windows.Forms.PictureBox pbGaugeImage;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnFaceColorHigh;
        private System.Windows.Forms.Button btnFaceColorLow;
        private System.Windows.Forms.Button btnRimColorOutside;
        private System.Windows.Forms.CheckBox cbDrawThousandsHand;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox cbDrawHundredsHand;
        private System.Windows.Forms.CheckBox cbDrawTenThousandsHand;
        private System.Windows.Forms.Button btnNeedleColor;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.ComboBox cbFaceGradientMode;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.NumericUpDown numRimWidth;
        private System.Windows.Forms.CheckBox cbDrawTickMarks;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.NumericUpDown numFaceTickMarkWidth;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.NumericUpDown numFaceTickMarkLength;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.NumericUpDown numHundredsHandLengthOffset;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.NumericUpDown numTenThousandsHandLengthOffset;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.NumericUpDown numThousandsHandLengthOffset;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Button btnRimColorInside;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbShowKollsman;
        private System.Windows.Forms.CheckBox cbShowAltitudeStripes;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}