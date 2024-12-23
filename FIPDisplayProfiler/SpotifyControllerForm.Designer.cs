﻿
namespace FIPDisplayProfiler
{
    partial class SpotifyControllerForm
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
            this.tbClientId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbSecretId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lnkSpotifyDeveloper = new System.Windows.Forms.LinkLabel();
            this.cbPauseOtherMedia = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(313, 151);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(394, 151);
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
            this.btnTitleFont.Location = new System.Drawing.Point(394, 12);
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
            this.tbTitleFont.Location = new System.Drawing.Point(68, 14);
            this.tbTitleFont.Name = "tbTitleFont";
            this.tbTitleFont.ReadOnly = true;
            this.tbTitleFont.Size = new System.Drawing.Size(320, 20);
            this.tbTitleFont.TabIndex = 1;
            this.tbTitleFont.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 38;
            this.label4.Text = "Title Font:";
            // 
            // btnFontColor
            // 
            this.btnFontColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFontColor.Location = new System.Drawing.Point(68, 69);
            this.btnFontColor.Name = "btnFontColor";
            this.btnFontColor.Size = new System.Drawing.Size(100, 21);
            this.btnFontColor.TabIndex = 5;
            this.btnFontColor.UseVisualStyleBackColor = true;
            this.btnFontColor.Click += new System.EventHandler(this.btnFontColor_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(28, 73);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(34, 13);
            this.label18.TabIndex = 48;
            this.label18.Text = "Color:";
            // 
            // btnArtistFont
            // 
            this.btnArtistFont.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnArtistFont.Location = new System.Drawing.Point(394, 41);
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
            this.tbArtistFont.Location = new System.Drawing.Point(68, 43);
            this.tbArtistFont.Name = "tbArtistFont";
            this.tbArtistFont.ReadOnly = true;
            this.tbArtistFont.Size = new System.Drawing.Size(320, 20);
            this.tbArtistFont.TabIndex = 3;
            this.tbArtistFont.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 51;
            this.label1.Text = "Artist Font:";
            // 
            // tbClientId
            // 
            this.tbClientId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbClientId.Location = new System.Drawing.Point(68, 96);
            this.tbClientId.Name = "tbClientId";
            this.tbClientId.Size = new System.Drawing.Size(401, 20);
            this.tbClientId.TabIndex = 52;
            this.tbClientId.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 53;
            this.label2.Text = "Client ID:";
            // 
            // tbSecretId
            // 
            this.tbSecretId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSecretId.Location = new System.Drawing.Point(68, 122);
            this.tbSecretId.Name = "tbSecretId";
            this.tbSecretId.Size = new System.Drawing.Size(401, 20);
            this.tbSecretId.TabIndex = 54;
            this.tbSecretId.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 55;
            this.label3.Text = "Secret ID:";
            // 
            // lnkSpotifyDeveloper
            // 
            this.lnkSpotifyDeveloper.AutoSize = true;
            this.lnkSpotifyDeveloper.Location = new System.Drawing.Point(186, 156);
            this.lnkSpotifyDeveloper.Name = "lnkSpotifyDeveloper";
            this.lnkSpotifyDeveloper.Size = new System.Drawing.Size(121, 13);
            this.lnkSpotifyDeveloper.TabIndex = 56;
            this.lnkSpotifyDeveloper.TabStop = true;
            this.lnkSpotifyDeveloper.Text = "Spotify Developer Portal";
            this.lnkSpotifyDeveloper.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSpotifyDeveloper_LinkClicked);
            // 
            // cbPauseOtherMedia
            // 
            this.cbPauseOtherMedia.AutoSize = true;
            this.cbPauseOtherMedia.Location = new System.Drawing.Point(68, 155);
            this.cbPauseOtherMedia.Name = "cbPauseOtherMedia";
            this.cbPauseOtherMedia.Size = new System.Drawing.Size(117, 17);
            this.cbPauseOtherMedia.TabIndex = 57;
            this.cbPauseOtherMedia.Text = "Pause Other Media";
            this.cbPauseOtherMedia.UseVisualStyleBackColor = true;
            // 
            // SpotifyControllerForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(482, 183);
            this.Controls.Add(this.cbPauseOtherMedia);
            this.Controls.Add(this.lnkSpotifyDeveloper);
            this.Controls.Add(this.tbSecretId);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbClientId);
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
            this.Name = "SpotifyControllerForm";
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
        private System.Windows.Forms.TextBox tbClientId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbSecretId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel lnkSpotifyDeveloper;
        private System.Windows.Forms.CheckBox cbPauseOtherMedia;
    }
}