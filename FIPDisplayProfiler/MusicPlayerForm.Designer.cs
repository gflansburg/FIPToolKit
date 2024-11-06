
namespace FIPDisplayProfiler
{
    partial class MusicPlayerForm
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
            this.btnTitleFont = new System.Windows.Forms.Button();
            this.tbTitleFont = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.btnFontColor = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.btnArtistFont = new System.Windows.Forms.Button();
            this.tbArtistFont = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbPath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.cbAutoPlay = new System.Windows.Forms.CheckBox();
            this.cbResume = new System.Windows.Forms.CheckBox();
            this.cbDistance = new System.Windows.Forms.Label();
            this.rbAny = new System.Windows.Forms.RadioButton();
            this.rb50 = new System.Windows.Forms.RadioButton();
            this.rb100 = new System.Windows.Forms.RadioButton();
            this.rb250 = new System.Windows.Forms.RadioButton();
            this.rb500 = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(313, 148);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Enabled = false;
            this.btnOK.Location = new System.Drawing.Point(394, 148);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnTitleFont
            // 
            this.btnTitleFont.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTitleFont.Location = new System.Drawing.Point(394, 36);
            this.btnTitleFont.Name = "btnTitleFont";
            this.btnTitleFont.Size = new System.Drawing.Size(75, 23);
            this.btnTitleFont.TabIndex = 2;
            this.btnTitleFont.Text = "Select &Font";
            this.btnTitleFont.UseVisualStyleBackColor = true;
            this.btnTitleFont.Click += new System.EventHandler(this.btnTitleFont_Click);
            // 
            // tbTitleFont
            // 
            this.tbTitleFont.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTitleFont.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tbTitleFont.Location = new System.Drawing.Point(101, 38);
            this.tbTitleFont.Name = "tbTitleFont";
            this.tbTitleFont.ReadOnly = true;
            this.tbTitleFont.Size = new System.Drawing.Size(287, 20);
            this.tbTitleFont.TabIndex = 1;
            this.tbTitleFont.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(41, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 38;
            this.label4.Text = "Title Font:";
            // 
            // btnFontColor
            // 
            this.btnFontColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFontColor.Location = new System.Drawing.Point(101, 92);
            this.btnFontColor.Name = "btnFontColor";
            this.btnFontColor.Size = new System.Drawing.Size(100, 21);
            this.btnFontColor.TabIndex = 5;
            this.btnFontColor.UseVisualStyleBackColor = true;
            this.btnFontColor.Click += new System.EventHandler(this.btnFontColor_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(61, 96);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(34, 13);
            this.label18.TabIndex = 48;
            this.label18.Text = "Color:";
            // 
            // btnArtistFont
            // 
            this.btnArtistFont.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnArtistFont.Location = new System.Drawing.Point(394, 65);
            this.btnArtistFont.Name = "btnArtistFont";
            this.btnArtistFont.Size = new System.Drawing.Size(75, 23);
            this.btnArtistFont.TabIndex = 4;
            this.btnArtistFont.Text = "&Select Font";
            this.btnArtistFont.UseVisualStyleBackColor = true;
            this.btnArtistFont.Click += new System.EventHandler(this.btnArtistFont_Click);
            // 
            // tbArtistFont
            // 
            this.tbArtistFont.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbArtistFont.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tbArtistFont.Location = new System.Drawing.Point(101, 64);
            this.tbArtistFont.Name = "tbArtistFont";
            this.tbArtistFont.ReadOnly = true;
            this.tbArtistFont.Size = new System.Drawing.Size(287, 20);
            this.tbArtistFont.TabIndex = 3;
            this.tbArtistFont.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 51;
            this.label1.Text = "Artist Font:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 52;
            this.label2.Text = "Library Path:";
            // 
            // tbPath
            // 
            this.tbPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPath.Location = new System.Drawing.Point(101, 12);
            this.tbPath.Name = "tbPath";
            this.tbPath.Size = new System.Drawing.Size(287, 20);
            this.tbPath.TabIndex = 53;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(394, 9);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 54;
            this.btnBrowse.Text = "&Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.MyMusic;
            // 
            // cbAutoPlay
            // 
            this.cbAutoPlay.AutoSize = true;
            this.cbAutoPlay.Location = new System.Drawing.Point(242, 95);
            this.cbAutoPlay.Name = "cbAutoPlay";
            this.cbAutoPlay.Size = new System.Drawing.Size(71, 17);
            this.cbAutoPlay.TabIndex = 55;
            this.cbAutoPlay.Text = "&Auto Play";
            this.cbAutoPlay.UseVisualStyleBackColor = true;
            // 
            // cbResume
            // 
            this.cbResume.AutoSize = true;
            this.cbResume.Location = new System.Drawing.Point(319, 95);
            this.cbResume.Name = "cbResume";
            this.cbResume.Size = new System.Drawing.Size(65, 17);
            this.cbResume.TabIndex = 56;
            this.cbResume.Text = "Resume";
            this.cbResume.UseVisualStyleBackColor = true;
            // 
            // cbDistance
            // 
            this.cbDistance.AutoSize = true;
            this.cbDistance.Location = new System.Drawing.Point(12, 124);
            this.cbDistance.Name = "cbDistance";
            this.cbDistance.Size = new System.Drawing.Size(83, 13);
            this.cbDistance.TabIndex = 57;
            this.cbDistance.Text = "Radio Distance:";
            // 
            // rbAny
            // 
            this.rbAny.AutoSize = true;
            this.rbAny.Checked = true;
            this.rbAny.Location = new System.Drawing.Point(102, 123);
            this.rbAny.Name = "rbAny";
            this.rbAny.Size = new System.Drawing.Size(43, 17);
            this.rbAny.TabIndex = 58;
            this.rbAny.TabStop = true;
            this.rbAny.Text = "Any";
            this.rbAny.UseVisualStyleBackColor = true;
            // 
            // rb50
            // 
            this.rb50.AutoSize = true;
            this.rb50.Location = new System.Drawing.Point(152, 123);
            this.rb50.Name = "rb50";
            this.rb50.Size = new System.Drawing.Size(57, 17);
            this.rb50.TabIndex = 59;
            this.rb50.TabStop = true;
            this.rb50.Text = "50 NM";
            this.rb50.UseVisualStyleBackColor = true;
            // 
            // rb100
            // 
            this.rb100.AutoSize = true;
            this.rb100.Location = new System.Drawing.Point(216, 123);
            this.rb100.Name = "rb100";
            this.rb100.Size = new System.Drawing.Size(63, 17);
            this.rb100.TabIndex = 60;
            this.rb100.TabStop = true;
            this.rb100.Text = "100 NM";
            this.rb100.UseVisualStyleBackColor = true;
            // 
            // rb250
            // 
            this.rb250.AutoSize = true;
            this.rb250.Location = new System.Drawing.Point(286, 123);
            this.rb250.Name = "rb250";
            this.rb250.Size = new System.Drawing.Size(63, 17);
            this.rb250.TabIndex = 61;
            this.rb250.TabStop = true;
            this.rb250.Text = "250 NM";
            this.rb250.UseVisualStyleBackColor = true;
            // 
            // rb500
            // 
            this.rb500.AutoSize = true;
            this.rb500.Location = new System.Drawing.Point(356, 123);
            this.rb500.Name = "rb500";
            this.rb500.Size = new System.Drawing.Size(63, 17);
            this.rb500.TabIndex = 62;
            this.rb500.TabStop = true;
            this.rb500.Text = "500 NM";
            this.rb500.UseVisualStyleBackColor = true;
            // 
            // MusicPlayerForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(482, 180);
            this.Controls.Add(this.rb500);
            this.Controls.Add(this.rb250);
            this.Controls.Add(this.rb100);
            this.Controls.Add(this.rb50);
            this.Controls.Add(this.rbAny);
            this.Controls.Add(this.cbDistance);
            this.Controls.Add(this.cbResume);
            this.Controls.Add(this.cbAutoPlay);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.tbPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnArtistFont);
            this.Controls.Add(this.tbArtistFont);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnFontColor);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.btnTitleFont);
            this.Controls.Add(this.tbTitleFont);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MusicPlayerForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Spotify Controller";
            this.Load += new System.EventHandler(this.SpotifyControllerForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnTitleFont;
        private System.Windows.Forms.TextBox tbTitleFont;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.Button btnFontColor;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button btnArtistFont;
        private System.Windows.Forms.TextBox tbArtistFont;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbPath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.CheckBox cbAutoPlay;
        private System.Windows.Forms.CheckBox cbResume;
        private System.Windows.Forms.Label cbDistance;
        private System.Windows.Forms.RadioButton rbAny;
        private System.Windows.Forms.RadioButton rb50;
        private System.Windows.Forms.RadioButton rb100;
        private System.Windows.Forms.RadioButton rb250;
        private System.Windows.Forms.RadioButton rb500;
    }
}