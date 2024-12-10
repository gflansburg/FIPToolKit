namespace FIPDisplayProfiler
{
    partial class DCSWorldSettings
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbDCSBiosPath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbFromIP = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numFromPort = new System.Windows.Forms.NumericUpDown();
            this.numToPort = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.tbToIP = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.lnkHelp = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.numFromPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numToPort)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "DCS-BIOS JSON Location:";
            // 
            // tbDCSBiosPath
            // 
            this.tbDCSBiosPath.Location = new System.Drawing.Point(155, 13);
            this.tbDCSBiosPath.Name = "tbDCSBiosPath";
            this.tbDCSBiosPath.ReadOnly = true;
            this.tbDCSBiosPath.Size = new System.Drawing.Size(275, 20);
            this.tbDCSBiosPath.TabIndex = 1;
            this.tbDCSBiosPath.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(436, 10);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "&Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "IP Address To Read From:";
            // 
            // tbFromIP
            // 
            this.tbFromIP.Location = new System.Drawing.Point(156, 40);
            this.tbFromIP.Name = "tbFromIP";
            this.tbFromIP.Size = new System.Drawing.Size(274, 20);
            this.tbFromIP.TabIndex = 4;
            this.tbFromIP.TextChanged += new System.EventHandler(this.tbFromIP_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(50, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Port To Read From:";
            // 
            // numFromPort
            // 
            this.numFromPort.Location = new System.Drawing.Point(155, 67);
            this.numFromPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numFromPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numFromPort.Name = "numFromPort";
            this.numFromPort.Size = new System.Drawing.Size(120, 20);
            this.numFromPort.TabIndex = 6;
            this.numFromPort.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numFromPort.ValueChanged += new System.EventHandler(this.numFromPort_ValueChanged);
            // 
            // numToPort
            // 
            this.numToPort.Location = new System.Drawing.Point(154, 120);
            this.numToPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numToPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numToPort.Name = "numToPort";
            this.numToPort.Size = new System.Drawing.Size(120, 20);
            this.numToPort.TabIndex = 10;
            this.numToPort.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numToPort.ValueChanged += new System.EventHandler(this.numToPort_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(59, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Port To Write To:";
            // 
            // tbToIP
            // 
            this.tbToIP.Location = new System.Drawing.Point(155, 93);
            this.tbToIP.Name = "tbToIP";
            this.tbToIP.Size = new System.Drawing.Size(274, 20);
            this.tbToIP.TabIndex = 8;
            this.tbToIP.TextChanged += new System.EventHandler(this.tbToIP_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(121, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "IP Address To Write To:";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(435, 150);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 11;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(353, 150);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.ShowNewFolderButton = false;
            // 
            // lnkHelp
            // 
            this.lnkHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lnkHelp.AutoSize = true;
            this.lnkHelp.Location = new System.Drawing.Point(14, 155);
            this.lnkHelp.Name = "lnkHelp";
            this.lnkHelp.Size = new System.Drawing.Size(85, 13);
            this.lnkHelp.TabIndex = 13;
            this.lnkHelp.TabStop = true;
            this.lnkHelp.Text = "WIKI: Instalation";
            this.lnkHelp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkHelp_LinkClicked);
            // 
            // DCSWorldSettings
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(523, 185);
            this.Controls.Add(this.lnkHelp);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.numToPort);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbToIP);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numFromPort);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbFromIP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.tbDCSBiosPath);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DCSWorldSettings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DCS World Settings";
            this.Load += new System.EventHandler(this.DCSWorldSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numFromPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numToPort)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbDCSBiosPath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbFromIP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numFromPort;
        private System.Windows.Forms.NumericUpDown numToPort;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbToIP;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.LinkLabel lnkHelp;
    }
}