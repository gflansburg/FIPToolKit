
namespace FIPDisplayProfiler
{
    partial class FIPDisplayProfiler
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FIPDisplayProfiler));
            this.label1 = new System.Windows.Forms.Label();
            this.lblFipDisplays = new System.Windows.Forms.Label();
            this.tabDevices = new System.Windows.Forms.TabControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startMinimizedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minimizeToSystemTrayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.previewVideoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoLoadLastProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadLastPlaylistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cacheSpotifyArtworkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showArtistImagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeFlightShareOnExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitWhenMSFSQuitsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.keyAPIModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.keybdeventToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendInputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fSUIPCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkMSFSTimer = new System.Windows.Forms.Timer(this.components);
            this.autoSaveSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "FIP Displays Found:";
            // 
            // lblFipDisplays
            // 
            this.lblFipDisplays.AutoSize = true;
            this.lblFipDisplays.Location = new System.Drawing.Point(119, 29);
            this.lblFipDisplays.Name = "lblFipDisplays";
            this.lblFipDisplays.Size = new System.Drawing.Size(13, 13);
            this.lblFipDisplays.TabIndex = 1;
            this.lblFipDisplays.Text = "0";
            // 
            // tabDevices
            // 
            this.tabDevices.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabDevices.Location = new System.Drawing.Point(12, 45);
            this.tabDevices.Name = "tabDevices";
            this.tabDevices.SelectedIndex = 0;
            this.tabDevices.Size = new System.Drawing.Size(732, 466);
            this.tabDevices.TabIndex = 2;
            this.tabDevices.SelectedIndexChanged += new System.EventHandler(this.tabDevices_SelectedIndexChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(756, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.saveAsToolStripMenuItem.Text = "Save &As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.closeToolStripMenuItem.Text = "&Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startMinimizedToolStripMenuItem,
            this.minimizeToSystemTrayToolStripMenuItem,
            this.previewVideoToolStripMenuItem,
            this.autoLoadLastProfileToolStripMenuItem,
            this.loadLastPlaylistToolStripMenuItem,
            this.autoSaveSettingsToolStripMenuItem,
            this.cacheSpotifyArtworkToolStripMenuItem,
            this.showArtistImagesToolStripMenuItem,
            this.closeFlightShareOnExitToolStripMenuItem,
            this.exitWhenMSFSQuitsToolStripMenuItem,
            this.keyAPIModeToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "&Options";
            // 
            // startMinimizedToolStripMenuItem
            // 
            this.startMinimizedToolStripMenuItem.CheckOnClick = true;
            this.startMinimizedToolStripMenuItem.Name = "startMinimizedToolStripMenuItem";
            this.startMinimizedToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.startMinimizedToolStripMenuItem.Text = "&Start Minimized";
            this.startMinimizedToolStripMenuItem.Click += new System.EventHandler(this.startMinimizedToolStripMenuItem_Click);
            // 
            // minimizeToSystemTrayToolStripMenuItem
            // 
            this.minimizeToSystemTrayToolStripMenuItem.Checked = true;
            this.minimizeToSystemTrayToolStripMenuItem.CheckOnClick = true;
            this.minimizeToSystemTrayToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.minimizeToSystemTrayToolStripMenuItem.Name = "minimizeToSystemTrayToolStripMenuItem";
            this.minimizeToSystemTrayToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.minimizeToSystemTrayToolStripMenuItem.Text = "&Minimize To System Tray";
            this.minimizeToSystemTrayToolStripMenuItem.Click += new System.EventHandler(this.minimizeToSystemTrayToolStripMenuItem_Click);
            // 
            // previewVideoToolStripMenuItem
            // 
            this.previewVideoToolStripMenuItem.Checked = true;
            this.previewVideoToolStripMenuItem.CheckOnClick = true;
            this.previewVideoToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.previewVideoToolStripMenuItem.Name = "previewVideoToolStripMenuItem";
            this.previewVideoToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.previewVideoToolStripMenuItem.Text = "&Preview Video";
            this.previewVideoToolStripMenuItem.Click += new System.EventHandler(this.previewVideoToolStripMenuItem_Click);
            // 
            // autoLoadLastProfileToolStripMenuItem
            // 
            this.autoLoadLastProfileToolStripMenuItem.Checked = true;
            this.autoLoadLastProfileToolStripMenuItem.CheckOnClick = true;
            this.autoLoadLastProfileToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoLoadLastProfileToolStripMenuItem.Name = "autoLoadLastProfileToolStripMenuItem";
            this.autoLoadLastProfileToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.autoLoadLastProfileToolStripMenuItem.Text = "&Auto Load Last Profile";
            this.autoLoadLastProfileToolStripMenuItem.Click += new System.EventHandler(this.autoLoadLastProfileToolStripMenuItem_Click);
            // 
            // loadLastPlaylistToolStripMenuItem
            // 
            this.loadLastPlaylistToolStripMenuItem.Checked = true;
            this.loadLastPlaylistToolStripMenuItem.CheckOnClick = true;
            this.loadLastPlaylistToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.loadLastPlaylistToolStripMenuItem.Name = "loadLastPlaylistToolStripMenuItem";
            this.loadLastPlaylistToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.loadLastPlaylistToolStripMenuItem.Text = "Auto &Play Last Playlist";
            this.loadLastPlaylistToolStripMenuItem.Click += new System.EventHandler(this.loadLastPlaylistToolStripMenuItem_Click);
            // 
            // cacheSpotifyArtworkToolStripMenuItem
            // 
            this.cacheSpotifyArtworkToolStripMenuItem.Checked = true;
            this.cacheSpotifyArtworkToolStripMenuItem.CheckOnClick = true;
            this.cacheSpotifyArtworkToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cacheSpotifyArtworkToolStripMenuItem.Name = "cacheSpotifyArtworkToolStripMenuItem";
            this.cacheSpotifyArtworkToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.cacheSpotifyArtworkToolStripMenuItem.Text = "&Cache Spotify Artwork";
            this.cacheSpotifyArtworkToolStripMenuItem.Click += new System.EventHandler(this.cacheSpotifyArtworkToolStripMenuItem_Click);
            // 
            // showArtistImagesToolStripMenuItem
            // 
            this.showArtistImagesToolStripMenuItem.CheckOnClick = true;
            this.showArtistImagesToolStripMenuItem.Name = "showArtistImagesToolStripMenuItem";
            this.showArtistImagesToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.showArtistImagesToolStripMenuItem.Text = "Show Spotify Artist &Images";
            this.showArtistImagesToolStripMenuItem.Click += new System.EventHandler(this.showArtistImagesToolStripMenuItem_Click);
            // 
            // closeFlightShareOnExitToolStripMenuItem
            // 
            this.closeFlightShareOnExitToolStripMenuItem.Checked = true;
            this.closeFlightShareOnExitToolStripMenuItem.CheckOnClick = true;
            this.closeFlightShareOnExitToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.closeFlightShareOnExitToolStripMenuItem.Name = "closeFlightShareOnExitToolStripMenuItem";
            this.closeFlightShareOnExitToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.closeFlightShareOnExitToolStripMenuItem.Text = "Close &FlightShare on Exit";
            this.closeFlightShareOnExitToolStripMenuItem.Click += new System.EventHandler(this.closeFlightShareOnExitToolStripMenuItem_Click);
            // 
            // exitWhenMSFSQuitsToolStripMenuItem
            // 
            this.exitWhenMSFSQuitsToolStripMenuItem.Checked = true;
            this.exitWhenMSFSQuitsToolStripMenuItem.CheckOnClick = true;
            this.exitWhenMSFSQuitsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.exitWhenMSFSQuitsToolStripMenuItem.Name = "exitWhenMSFSQuitsToolStripMenuItem";
            this.exitWhenMSFSQuitsToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.exitWhenMSFSQuitsToolStripMenuItem.Text = "&Exit When MSFS Quits";
            this.exitWhenMSFSQuitsToolStripMenuItem.Click += new System.EventHandler(this.exitWhenMSFSQuitsToolStripMenuItem_Click);
            // 
            // keyAPIModeToolStripMenuItem
            // 
            this.keyAPIModeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.keybdeventToolStripMenuItem,
            this.sendInputToolStripMenuItem,
            this.fSUIPCToolStripMenuItem});
            this.keyAPIModeToolStripMenuItem.Name = "keyAPIModeToolStripMenuItem";
            this.keyAPIModeToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
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
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "fip";
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "FIP Display Profiles|*.fip";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "fip";
            this.saveFileDialog1.Filter = "FIP Display Profiles|*.fip";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "FIP Display";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(104, 48);
            // 
            // openToolStripMenuItem1
            // 
            this.openToolStripMenuItem1.Name = "openToolStripMenuItem1";
            this.openToolStripMenuItem1.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem1.Text = "&Open";
            this.openToolStripMenuItem1.Click += new System.EventHandler(this.openToolStripMenuItem1_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // checkMSFSTimer
            // 
            this.checkMSFSTimer.Tick += new System.EventHandler(this.checkMSFSTimer_Tick);
            // 
            // autoSaveSettingsToolStripMenuItem
            // 
            this.autoSaveSettingsToolStripMenuItem.Checked = true;
            this.autoSaveSettingsToolStripMenuItem.CheckOnClick = true;
            this.autoSaveSettingsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoSaveSettingsToolStripMenuItem.Name = "autoSaveSettingsToolStripMenuItem";
            this.autoSaveSettingsToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.autoSaveSettingsToolStripMenuItem.Text = "Auto Sa&ve Settings";
            this.autoSaveSettingsToolStripMenuItem.Click += new System.EventHandler(this.autoSaveSettingsToolStripMenuItem_Click);
            // 
            // FIPDisplayProfiler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 523);
            this.Controls.Add(this.tabDevices);
            this.Controls.Add(this.lblFipDisplays);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FIPDisplayProfiler";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FIP Display";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FIPDisplay_FormClosing);
            this.Load += new System.EventHandler(this.FIPDisplay_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblFipDisplays;
        private System.Windows.Forms.TabControl tabDevices;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem minimizeToSystemTrayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem previewVideoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem autoLoadLastProfileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem keyAPIModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem keybdeventToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sendInputToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadLastPlaylistToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startMinimizedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showArtistImagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cacheSpotifyArtworkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeFlightShareOnExitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fSUIPCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitWhenMSFSQuitsToolStripMenuItem;
        private System.Windows.Forms.Timer checkMSFSTimer;
        private System.Windows.Forms.ToolStripMenuItem autoSaveSettingsToolStripMenuItem;
    }
}

