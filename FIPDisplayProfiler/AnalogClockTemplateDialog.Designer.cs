
namespace FIPDisplayProfiler
{
    partial class AnalogClockTemplateDialog
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbAviation = new System.Windows.Forms.GroupBox();
            this.rbCessnaAltimeter = new System.Windows.Forms.RadioButton();
            this.rbCessnaAirspeed = new System.Windows.Forms.RadioButton();
            this.rbCessnaClock2 = new System.Windows.Forms.RadioButton();
            this.rbCessnaClock1 = new System.Windows.Forms.RadioButton();
            this.gbCities = new System.Windows.Forms.GroupBox();
            this.rbShanghai = new System.Windows.Forms.RadioButton();
            this.rbHonolulu = new System.Windows.Forms.RadioButton();
            this.rbDenver = new System.Windows.Forms.RadioButton();
            this.rbSydney = new System.Windows.Forms.RadioButton();
            this.rbTokyo = new System.Windows.Forms.RadioButton();
            this.rbBerlin = new System.Windows.Forms.RadioButton();
            this.rbLondon = new System.Windows.Forms.RadioButton();
            this.rbChicago = new System.Windows.Forms.RadioButton();
            this.rbLosAngeles = new System.Windows.Forms.RadioButton();
            this.rbNewYork = new System.Windows.Forms.RadioButton();
            this.rbMoscow = new System.Windows.Forms.RadioButton();
            this.rbHongKong = new System.Windows.Forms.RadioButton();
            this.rbKarachi = new System.Windows.Forms.RadioButton();
            this.rbParis = new System.Windows.Forms.RadioButton();
            this.gbAviation.SuspendLayout();
            this.gbCities.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Enabled = false;
            this.btnOK.Location = new System.Drawing.Point(152, 328);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(71, 328);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // gbAviation
            // 
            this.gbAviation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbAviation.Controls.Add(this.rbCessnaAltimeter);
            this.gbAviation.Controls.Add(this.rbCessnaAirspeed);
            this.gbAviation.Controls.Add(this.rbCessnaClock2);
            this.gbAviation.Controls.Add(this.rbCessnaClock1);
            this.gbAviation.Location = new System.Drawing.Point(13, 13);
            this.gbAviation.Name = "gbAviation";
            this.gbAviation.Size = new System.Drawing.Size(214, 118);
            this.gbAviation.TabIndex = 2;
            this.gbAviation.TabStop = false;
            this.gbAviation.Text = "Aviation";
            // 
            // rbCessnaAltimeter
            // 
            this.rbCessnaAltimeter.AutoSize = true;
            this.rbCessnaAltimeter.Location = new System.Drawing.Point(7, 43);
            this.rbCessnaAltimeter.Name = "rbCessnaAltimeter";
            this.rbCessnaAltimeter.Size = new System.Drawing.Size(103, 17);
            this.rbCessnaAltimeter.TabIndex = 2;
            this.rbCessnaAltimeter.TabStop = true;
            this.rbCessnaAltimeter.Text = "Cessna Altimeter";
            this.rbCessnaAltimeter.UseVisualStyleBackColor = true;
            this.rbCessnaAltimeter.CheckedChanged += new System.EventHandler(this.rbCessnaAltimeter_CheckedChanged);
            // 
            // rbCessnaAirspeed
            // 
            this.rbCessnaAirspeed.AutoSize = true;
            this.rbCessnaAirspeed.Location = new System.Drawing.Point(7, 19);
            this.rbCessnaAirspeed.Name = "rbCessnaAirspeed";
            this.rbCessnaAirspeed.Size = new System.Drawing.Size(104, 17);
            this.rbCessnaAirspeed.TabIndex = 1;
            this.rbCessnaAirspeed.TabStop = true;
            this.rbCessnaAirspeed.Text = "Cessna Airspeed";
            this.rbCessnaAirspeed.UseVisualStyleBackColor = true;
            this.rbCessnaAirspeed.CheckedChanged += new System.EventHandler(this.rbCessnaAirspeed_CheckedChanged);
            // 
            // rbCessnaClock2
            // 
            this.rbCessnaClock2.AutoSize = true;
            this.rbCessnaClock2.Location = new System.Drawing.Point(7, 90);
            this.rbCessnaClock2.Name = "rbCessnaClock2";
            this.rbCessnaClock2.Size = new System.Drawing.Size(99, 17);
            this.rbCessnaClock2.TabIndex = 4;
            this.rbCessnaClock2.TabStop = true;
            this.rbCessnaClock2.Text = "Cessna Clock 2";
            this.rbCessnaClock2.UseVisualStyleBackColor = true;
            this.rbCessnaClock2.CheckedChanged += new System.EventHandler(this.rbCessnaClock2_CheckedChanged);
            // 
            // rbCessnaClock1
            // 
            this.rbCessnaClock1.AutoSize = true;
            this.rbCessnaClock1.Location = new System.Drawing.Point(7, 66);
            this.rbCessnaClock1.Name = "rbCessnaClock1";
            this.rbCessnaClock1.Size = new System.Drawing.Size(99, 17);
            this.rbCessnaClock1.TabIndex = 3;
            this.rbCessnaClock1.TabStop = true;
            this.rbCessnaClock1.Text = "Cessna Clock 1";
            this.rbCessnaClock1.UseVisualStyleBackColor = true;
            this.rbCessnaClock1.CheckedChanged += new System.EventHandler(this.rbCessnaClock1_CheckedChanged);
            // 
            // gbCities
            // 
            this.gbCities.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbCities.Controls.Add(this.rbShanghai);
            this.gbCities.Controls.Add(this.rbHonolulu);
            this.gbCities.Controls.Add(this.rbDenver);
            this.gbCities.Controls.Add(this.rbSydney);
            this.gbCities.Controls.Add(this.rbTokyo);
            this.gbCities.Controls.Add(this.rbBerlin);
            this.gbCities.Controls.Add(this.rbLondon);
            this.gbCities.Controls.Add(this.rbChicago);
            this.gbCities.Controls.Add(this.rbLosAngeles);
            this.gbCities.Controls.Add(this.rbNewYork);
            this.gbCities.Controls.Add(this.rbMoscow);
            this.gbCities.Controls.Add(this.rbHongKong);
            this.gbCities.Controls.Add(this.rbKarachi);
            this.gbCities.Controls.Add(this.rbParis);
            this.gbCities.Location = new System.Drawing.Point(13, 138);
            this.gbCities.Name = "gbCities";
            this.gbCities.Size = new System.Drawing.Size(214, 184);
            this.gbCities.TabIndex = 3;
            this.gbCities.TabStop = false;
            this.gbCities.Text = "Cities";
            // 
            // rbShanghai
            // 
            this.rbShanghai.AutoSize = true;
            this.rbShanghai.Location = new System.Drawing.Point(129, 90);
            this.rbShanghai.Name = "rbShanghai";
            this.rbShanghai.Size = new System.Drawing.Size(70, 17);
            this.rbShanghai.TabIndex = 15;
            this.rbShanghai.TabStop = true;
            this.rbShanghai.Text = "Shanghai";
            this.rbShanghai.UseVisualStyleBackColor = true;
            this.rbShanghai.CheckedChanged += new System.EventHandler(this.rbShanghai_CheckedChanged);
            // 
            // rbHonolulu
            // 
            this.rbHonolulu.AutoSize = true;
            this.rbHonolulu.Location = new System.Drawing.Point(7, 113);
            this.rbHonolulu.Name = "rbHonolulu";
            this.rbHonolulu.Size = new System.Drawing.Size(67, 17);
            this.rbHonolulu.TabIndex = 9;
            this.rbHonolulu.TabStop = true;
            this.rbHonolulu.Text = "Honolulu";
            this.rbHonolulu.UseVisualStyleBackColor = true;
            this.rbHonolulu.CheckedChanged += new System.EventHandler(this.rbHonolulu_CheckedChanged);
            // 
            // rbDenver
            // 
            this.rbDenver.AutoSize = true;
            this.rbDenver.Location = new System.Drawing.Point(7, 43);
            this.rbDenver.Name = "rbDenver";
            this.rbDenver.Size = new System.Drawing.Size(60, 17);
            this.rbDenver.TabIndex = 6;
            this.rbDenver.TabStop = true;
            this.rbDenver.Text = "Denver";
            this.rbDenver.UseVisualStyleBackColor = true;
            this.rbDenver.CheckedChanged += new System.EventHandler(this.rbDenver_CheckedChanged);
            // 
            // rbSydney
            // 
            this.rbSydney.AutoSize = true;
            this.rbSydney.Location = new System.Drawing.Point(129, 159);
            this.rbSydney.Name = "rbSydney";
            this.rbSydney.Size = new System.Drawing.Size(60, 17);
            this.rbSydney.TabIndex = 18;
            this.rbSydney.TabStop = true;
            this.rbSydney.Text = "Sydney";
            this.rbSydney.UseVisualStyleBackColor = true;
            this.rbSydney.CheckedChanged += new System.EventHandler(this.rbSydney_CheckedChanged);
            // 
            // rbTokyo
            // 
            this.rbTokyo.AutoSize = true;
            this.rbTokyo.Location = new System.Drawing.Point(129, 136);
            this.rbTokyo.Name = "rbTokyo";
            this.rbTokyo.Size = new System.Drawing.Size(55, 17);
            this.rbTokyo.TabIndex = 17;
            this.rbTokyo.TabStop = true;
            this.rbTokyo.Text = "Tokyo";
            this.rbTokyo.UseVisualStyleBackColor = true;
            this.rbTokyo.CheckedChanged += new System.EventHandler(this.rbTokyo_CheckedChanged);
            // 
            // rbBerlin
            // 
            this.rbBerlin.AutoSize = true;
            this.rbBerlin.Location = new System.Drawing.Point(129, 19);
            this.rbBerlin.Name = "rbBerlin";
            this.rbBerlin.Size = new System.Drawing.Size(51, 17);
            this.rbBerlin.TabIndex = 12;
            this.rbBerlin.TabStop = true;
            this.rbBerlin.Text = "Berlin";
            this.rbBerlin.UseVisualStyleBackColor = true;
            this.rbBerlin.CheckedChanged += new System.EventHandler(this.rbBerlin_CheckedChanged);
            // 
            // rbLondon
            // 
            this.rbLondon.AutoSize = true;
            this.rbLondon.Location = new System.Drawing.Point(7, 136);
            this.rbLondon.Name = "rbLondon";
            this.rbLondon.Size = new System.Drawing.Size(61, 17);
            this.rbLondon.TabIndex = 10;
            this.rbLondon.TabStop = true;
            this.rbLondon.Text = "London";
            this.rbLondon.UseVisualStyleBackColor = true;
            this.rbLondon.CheckedChanged += new System.EventHandler(this.rbLondon_CheckedChanged);
            // 
            // rbChicago
            // 
            this.rbChicago.AutoSize = true;
            this.rbChicago.Location = new System.Drawing.Point(7, 66);
            this.rbChicago.Name = "rbChicago";
            this.rbChicago.Size = new System.Drawing.Size(64, 17);
            this.rbChicago.TabIndex = 7;
            this.rbChicago.TabStop = true;
            this.rbChicago.Text = "Chicago";
            this.rbChicago.UseVisualStyleBackColor = true;
            this.rbChicago.CheckedChanged += new System.EventHandler(this.rbChicago_CheckedChanged);
            // 
            // rbLosAngeles
            // 
            this.rbLosAngeles.AutoSize = true;
            this.rbLosAngeles.Location = new System.Drawing.Point(7, 19);
            this.rbLosAngeles.Name = "rbLosAngeles";
            this.rbLosAngeles.Size = new System.Drawing.Size(83, 17);
            this.rbLosAngeles.TabIndex = 5;
            this.rbLosAngeles.TabStop = true;
            this.rbLosAngeles.Text = "Los Angeles";
            this.rbLosAngeles.UseVisualStyleBackColor = true;
            this.rbLosAngeles.CheckedChanged += new System.EventHandler(this.rbLosAngeles_CheckedChanged);
            // 
            // rbNewYork
            // 
            this.rbNewYork.AutoSize = true;
            this.rbNewYork.Location = new System.Drawing.Point(6, 89);
            this.rbNewYork.Name = "rbNewYork";
            this.rbNewYork.Size = new System.Drawing.Size(72, 17);
            this.rbNewYork.TabIndex = 8;
            this.rbNewYork.TabStop = true;
            this.rbNewYork.Text = "New York";
            this.rbNewYork.UseVisualStyleBackColor = true;
            this.rbNewYork.CheckedChanged += new System.EventHandler(this.rbNewYork_CheckedChanged);
            // 
            // rbMoscow
            // 
            this.rbMoscow.AutoSize = true;
            this.rbMoscow.Location = new System.Drawing.Point(129, 43);
            this.rbMoscow.Name = "rbMoscow";
            this.rbMoscow.Size = new System.Drawing.Size(65, 17);
            this.rbMoscow.TabIndex = 13;
            this.rbMoscow.TabStop = true;
            this.rbMoscow.Text = "Moscow";
            this.rbMoscow.UseVisualStyleBackColor = true;
            this.rbMoscow.CheckedChanged += new System.EventHandler(this.rbMoscow_CheckedChanged);
            // 
            // rbHongKong
            // 
            this.rbHongKong.AutoSize = true;
            this.rbHongKong.Location = new System.Drawing.Point(129, 113);
            this.rbHongKong.Name = "rbHongKong";
            this.rbHongKong.Size = new System.Drawing.Size(79, 17);
            this.rbHongKong.TabIndex = 16;
            this.rbHongKong.TabStop = true;
            this.rbHongKong.Text = "Hong Kong";
            this.rbHongKong.UseVisualStyleBackColor = true;
            this.rbHongKong.CheckedChanged += new System.EventHandler(this.rbHongKong_CheckedChanged);
            // 
            // rbKarachi
            // 
            this.rbKarachi.AutoSize = true;
            this.rbKarachi.Location = new System.Drawing.Point(129, 66);
            this.rbKarachi.Name = "rbKarachi";
            this.rbKarachi.Size = new System.Drawing.Size(61, 17);
            this.rbKarachi.TabIndex = 14;
            this.rbKarachi.TabStop = true;
            this.rbKarachi.Text = "Karachi";
            this.rbKarachi.UseVisualStyleBackColor = true;
            this.rbKarachi.CheckedChanged += new System.EventHandler(this.rbKarachi_CheckedChanged);
            // 
            // rbParis
            // 
            this.rbParis.AutoSize = true;
            this.rbParis.Location = new System.Drawing.Point(7, 159);
            this.rbParis.Name = "rbParis";
            this.rbParis.Size = new System.Drawing.Size(48, 17);
            this.rbParis.TabIndex = 11;
            this.rbParis.TabStop = true;
            this.rbParis.Text = "Paris";
            this.rbParis.UseVisualStyleBackColor = true;
            this.rbParis.CheckedChanged += new System.EventHandler(this.rbParis_CheckedChanged);
            // 
            // AnalogClockTemplateDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(239, 363);
            this.Controls.Add(this.gbCities);
            this.Controls.Add(this.gbAviation);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AnalogClockTemplateDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Analog Clock Template";
            this.gbAviation.ResumeLayout(false);
            this.gbAviation.PerformLayout();
            this.gbCities.ResumeLayout(false);
            this.gbCities.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox gbAviation;
        private System.Windows.Forms.RadioButton rbCessnaClock1;
        private System.Windows.Forms.RadioButton rbCessnaAirspeed;
        private System.Windows.Forms.RadioButton rbCessnaClock2;
        private System.Windows.Forms.RadioButton rbCessnaAltimeter;
        private System.Windows.Forms.GroupBox gbCities;
        private System.Windows.Forms.RadioButton rbHongKong;
        private System.Windows.Forms.RadioButton rbKarachi;
        private System.Windows.Forms.RadioButton rbParis;
        private System.Windows.Forms.RadioButton rbMoscow;
        private System.Windows.Forms.RadioButton rbNewYork;
        private System.Windows.Forms.RadioButton rbLosAngeles;
        private System.Windows.Forms.RadioButton rbChicago;
        private System.Windows.Forms.RadioButton rbLondon;
        private System.Windows.Forms.RadioButton rbSydney;
        private System.Windows.Forms.RadioButton rbTokyo;
        private System.Windows.Forms.RadioButton rbBerlin;
        private System.Windows.Forms.RadioButton rbShanghai;
        private System.Windows.Forms.RadioButton rbHonolulu;
        private System.Windows.Forms.RadioButton rbDenver;
    }
}