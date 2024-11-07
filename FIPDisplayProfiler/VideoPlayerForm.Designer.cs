
namespace FIPDisplayProfiler
{
    partial class VideoPlayerForm
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnFont = new System.Windows.Forms.Button();
            this.tbFont = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.btnFontColor = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.label5 = new System.Windows.Forms.Label();
            this.tbFilename = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.chkMaintainAspectRadio = new System.Windows.Forms.CheckBox();
            this.chkPortraitMode = new System.Windows.Forms.CheckBox();
            this.chkShowControls = new System.Windows.Forms.CheckBox();
            this.chkResumePlayback = new System.Windows.Forms.CheckBox();
            this.cbPauseOtherMedia = new System.Windows.Forms.CheckBox();
            this.btnSubtitleFont = new System.Windows.Forms.Button();
            this.tbSubtitleFont = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(313, 164);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(394, 164);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "mp4";
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "All Video Files|*.avi;*.mpg;*.mp4;*.mpeg;*.mpv;*.m4v;*.wmv;*.flv,*.mov;*.m3u;*.m3" +
    "u8|Video Files|*.avi;*.mpg;*.mp4;*.mpeg;*.mpv;*.m4v;*.wmv;*.flv,*.mov|Playlists|" +
    "*.m3u;*.m3u8";
            // 
            // btnFont
            // 
            this.btnFont.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFont.Location = new System.Drawing.Point(394, 39);
            this.btnFont.Name = "btnFont";
            this.btnFont.Size = new System.Drawing.Size(75, 23);
            this.btnFont.TabIndex = 4;
            this.btnFont.Text = "Select &Font";
            this.btnFont.UseVisualStyleBackColor = true;
            this.btnFont.Click += new System.EventHandler(this.btnFont_Click);
            // 
            // tbFont
            // 
            this.tbFont.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFont.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tbFont.Location = new System.Drawing.Point(83, 41);
            this.tbFont.Name = "tbFont";
            this.tbFont.ReadOnly = true;
            this.tbFont.Size = new System.Drawing.Size(305, 20);
            this.tbFont.TabIndex = 3;
            this.tbFont.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 38;
            this.label4.Text = "Title Font:";
            // 
            // btnFontColor
            // 
            this.btnFontColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFontColor.Location = new System.Drawing.Point(83, 99);
            this.btnFontColor.Name = "btnFontColor";
            this.btnFontColor.Size = new System.Drawing.Size(100, 21);
            this.btnFontColor.TabIndex = 5;
            this.btnFontColor.UseVisualStyleBackColor = true;
            this.btnFontColor.Click += new System.EventHandler(this.btnFontColor_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(42, 104);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(34, 13);
            this.label18.TabIndex = 48;
            this.label18.Text = "Color:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 49;
            this.label5.Text = "Filename:";
            // 
            // tbFilename
            // 
            this.tbFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFilename.Location = new System.Drawing.Point(83, 12);
            this.tbFilename.Name = "tbFilename";
            this.tbFilename.ReadOnly = true;
            this.tbFilename.Size = new System.Drawing.Size(305, 20);
            this.tbFilename.TabIndex = 1;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(394, 10);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "&Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // chkMaintainAspectRadio
            // 
            this.chkMaintainAspectRadio.AutoSize = true;
            this.chkMaintainAspectRadio.Location = new System.Drawing.Point(83, 136);
            this.chkMaintainAspectRadio.Name = "chkMaintainAspectRadio";
            this.chkMaintainAspectRadio.Size = new System.Drawing.Size(130, 17);
            this.chkMaintainAspectRadio.TabIndex = 51;
            this.chkMaintainAspectRadio.Text = "Maintain Aspect Ratio";
            this.chkMaintainAspectRadio.UseVisualStyleBackColor = true;
            // 
            // chkPortraitMode
            // 
            this.chkPortraitMode.AutoSize = true;
            this.chkPortraitMode.Location = new System.Drawing.Point(376, 102);
            this.chkPortraitMode.Name = "chkPortraitMode";
            this.chkPortraitMode.Size = new System.Drawing.Size(89, 17);
            this.chkPortraitMode.TabIndex = 52;
            this.chkPortraitMode.Text = "Portrait Mode";
            this.chkPortraitMode.UseVisualStyleBackColor = true;
            // 
            // chkShowControls
            // 
            this.chkShowControls.AutoSize = true;
            this.chkShowControls.Location = new System.Drawing.Point(376, 136);
            this.chkShowControls.Name = "chkShowControls";
            this.chkShowControls.Size = new System.Drawing.Size(94, 17);
            this.chkShowControls.TabIndex = 53;
            this.chkShowControls.Text = "Show Controls";
            this.chkShowControls.UseVisualStyleBackColor = true;
            // 
            // chkResumePlayback
            // 
            this.chkResumePlayback.AutoSize = true;
            this.chkResumePlayback.Location = new System.Drawing.Point(228, 102);
            this.chkResumePlayback.Name = "chkResumePlayback";
            this.chkResumePlayback.Size = new System.Drawing.Size(112, 17);
            this.chkResumePlayback.TabIndex = 54;
            this.chkResumePlayback.Text = "Resume Playback";
            this.chkResumePlayback.UseVisualStyleBackColor = true;
            // 
            // cbPauseOtherMedia
            // 
            this.cbPauseOtherMedia.AutoSize = true;
            this.cbPauseOtherMedia.Location = new System.Drawing.Point(228, 136);
            this.cbPauseOtherMedia.Name = "cbPauseOtherMedia";
            this.cbPauseOtherMedia.Size = new System.Drawing.Size(117, 17);
            this.cbPauseOtherMedia.TabIndex = 55;
            this.cbPauseOtherMedia.Text = "Pause Other Media";
            this.cbPauseOtherMedia.UseVisualStyleBackColor = true;
            // 
            // btnSubtitleFont
            // 
            this.btnSubtitleFont.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubtitleFont.Location = new System.Drawing.Point(394, 68);
            this.btnSubtitleFont.Name = "btnSubtitleFont";
            this.btnSubtitleFont.Size = new System.Drawing.Size(75, 23);
            this.btnSubtitleFont.TabIndex = 57;
            this.btnSubtitleFont.Text = "Select &Font";
            this.btnSubtitleFont.UseVisualStyleBackColor = true;
            this.btnSubtitleFont.Click += new System.EventHandler(this.btnSubtitleFont_Click);
            // 
            // tbSubtitleFont
            // 
            this.tbSubtitleFont.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSubtitleFont.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tbSubtitleFont.Location = new System.Drawing.Point(83, 70);
            this.tbSubtitleFont.Name = "tbSubtitleFont";
            this.tbSubtitleFont.ReadOnly = true;
            this.tbSubtitleFont.Size = new System.Drawing.Size(305, 20);
            this.tbSubtitleFont.TabIndex = 56;
            this.tbSubtitleFont.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 58;
            this.label1.Text = "Subtitle Font:";
            // 
            // VideoPlayerForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(482, 196);
            this.Controls.Add(this.btnSubtitleFont);
            this.Controls.Add(this.tbSubtitleFont);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbPauseOtherMedia);
            this.Controls.Add(this.chkResumePlayback);
            this.Controls.Add(this.chkShowControls);
            this.Controls.Add(this.chkPortraitMode);
            this.Controls.Add(this.chkMaintainAspectRadio);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.tbFilename);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnFontColor);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.btnFont);
            this.Controls.Add(this.tbFont);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VideoPlayerForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Video Player";
            this.Load += new System.EventHandler(this.VideoPlayerForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnFont;
        private System.Windows.Forms.TextBox tbFont;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.Button btnFontColor;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbFilename;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.CheckBox chkMaintainAspectRadio;
        private System.Windows.Forms.CheckBox chkPortraitMode;
        private System.Windows.Forms.CheckBox chkShowControls;
        private System.Windows.Forms.CheckBox chkResumePlayback;
        private System.Windows.Forms.CheckBox cbPauseOtherMedia;
        private System.Windows.Forms.Button btnSubtitleFont;
        private System.Windows.Forms.TextBox tbSubtitleFont;
        private System.Windows.Forms.Label label1;
    }
}