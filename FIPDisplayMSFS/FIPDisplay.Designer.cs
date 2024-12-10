
namespace FIPDisplayMSFS
{
    partial class FIPDisplay
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FIPDisplay));
            this.btnOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.timerSpotify = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.keyAPIModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.keybdeventToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendInputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fSUIPCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xPlaneSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dCSWorldSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.webView21)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOK.Location = new System.Drawing.Point(317, 446);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Visible = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.SystemColors.Window;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.label1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label1.Location = new System.Drawing.Point(53, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(339, 412);
            this.label1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.SystemColors.Window;
            this.label2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(404, 438);
            this.label2.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox1.Image = global::FIPDisplayMSFS.Properties.Resources.Information_icon;
            this.pictureBox1.Location = new System.Drawing.Point(13, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // webView21
            // 
            this.webView21.AllowExternalDrop = true;
            this.webView21.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webView21.CreationProperties = null;
            this.webView21.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webView21.Location = new System.Drawing.Point(0, 0);
            this.webView21.Name = "webView21";
            this.webView21.Size = new System.Drawing.Size(404, 438);
            this.webView21.TabIndex = 4;
            this.webView21.Visible = false;
            this.webView21.ZoomFactor = 1D;
            // 
            // timerSpotify
            // 
            this.timerSpotify.Tick += new System.EventHandler(this.timerSpotify_Tick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "FIP Display";
            this.notifyIcon1.Visible = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.keyAPIModeToolStripMenuItem,
            this.xPlaneSettingsToolStripMenuItem,
            this.dCSWorldSettingsToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(186, 120);
            // 
            // keyAPIModeToolStripMenuItem
            // 
            this.keyAPIModeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.keybdeventToolStripMenuItem,
            this.sendInputToolStripMenuItem,
            this.fSUIPCToolStripMenuItem});
            this.keyAPIModeToolStripMenuItem.Name = "keyAPIModeToolStripMenuItem";
            this.keyAPIModeToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.keyAPIModeToolStripMenuItem.Text = "&Key API Mode";
            // 
            // keybdeventToolStripMenuItem
            // 
            this.keybdeventToolStripMenuItem.Name = "keybdeventToolStripMenuItem";
            this.keybdeventToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.keybdeventToolStripMenuItem.Text = "keybd_event";
            this.keybdeventToolStripMenuItem.Click += new System.EventHandler(this.keybdeventToolStripMenuItem_Click);
            // 
            // sendInputToolStripMenuItem
            // 
            this.sendInputToolStripMenuItem.Name = "sendInputToolStripMenuItem";
            this.sendInputToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.sendInputToolStripMenuItem.Text = "SendInput";
            this.sendInputToolStripMenuItem.Click += new System.EventHandler(this.sendInputToolStripMenuItem_Click);
            // 
            // fSUIPCToolStripMenuItem
            // 
            this.fSUIPCToolStripMenuItem.Name = "fSUIPCToolStripMenuItem";
            this.fSUIPCToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.fSUIPCToolStripMenuItem.Text = "FSUIPC";
            this.fSUIPCToolStripMenuItem.Click += new System.EventHandler(this.fSUIPCToolStripMenuItem_Click);
            // 
            // xPlaneSettingsToolStripMenuItem
            // 
            this.xPlaneSettingsToolStripMenuItem.Name = "xPlaneSettingsToolStripMenuItem";
            this.xPlaneSettingsToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.xPlaneSettingsToolStripMenuItem.Text = "X-Plane Settings...";
            this.xPlaneSettingsToolStripMenuItem.Click += new System.EventHandler(this.xPlaneSettingsToolStripMenuItem_Click);
            // 
            // dCSWorldSettingsToolStripMenuItem
            // 
            this.dCSWorldSettingsToolStripMenuItem.Name = "dCSWorldSettingsToolStripMenuItem";
            this.dCSWorldSettingsToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.dCSWorldSettingsToolStripMenuItem.Text = "DCS World Settings...";
            this.dCSWorldSettingsToolStripMenuItem.Click += new System.EventHandler(this.dCSWorldSettingsToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.aboutToolStripMenuItem.Text = "About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(182, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // FIPDisplay
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnOK;
            this.ClientSize = new System.Drawing.Size(404, 481);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.webView21);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FIPDisplay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FIPDisplayMSFS";
            this.Load += new System.EventHandler(this.FIPDisplay_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.webView21)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer1;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
        private System.Windows.Forms.Timer timerSpotify;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xPlaneSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dCSWorldSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem keyAPIModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem keybdeventToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sendInputToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fSUIPCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}

