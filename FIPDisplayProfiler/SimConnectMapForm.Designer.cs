
namespace FIPDisplayProfiler
{
    partial class SimConnectMapForm
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
            this.btnFont = new System.Windows.Forms.Button();
            this.tbFont = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.btnFontColor = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.tbVatSimId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numMaxAIAircraft = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numRadius = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.numMaxMPAircraft = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.tbAIPClientToken = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxAIAircraft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRadius)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxMPAircraft)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(313, 170);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(394, 170);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnFont
            // 
            this.btnFont.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFont.Location = new System.Drawing.Point(394, 10);
            this.btnFont.Name = "btnFont";
            this.btnFont.Size = new System.Drawing.Size(75, 23);
            this.btnFont.TabIndex = 2;
            this.btnFont.Text = "Select &Font";
            this.btnFont.UseVisualStyleBackColor = true;
            this.btnFont.Click += new System.EventHandler(this.btnFont_Click);
            // 
            // tbFont
            // 
            this.tbFont.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFont.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tbFont.Location = new System.Drawing.Point(108, 12);
            this.tbFont.Name = "tbFont";
            this.tbFont.ReadOnly = true;
            this.tbFont.Size = new System.Drawing.Size(280, 20);
            this.tbFont.TabIndex = 1;
            this.tbFont.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(71, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 38;
            this.label4.Text = "Font:";
            // 
            // btnFontColor
            // 
            this.btnFontColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFontColor.Location = new System.Drawing.Point(108, 40);
            this.btnFontColor.Name = "btnFontColor";
            this.btnFontColor.Size = new System.Drawing.Size(100, 21);
            this.btnFontColor.TabIndex = 3;
            this.btnFontColor.UseVisualStyleBackColor = true;
            this.btnFontColor.Click += new System.EventHandler(this.btnFontColor_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(68, 44);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(34, 13);
            this.label18.TabIndex = 48;
            this.label18.Text = "Color:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 49;
            this.label1.Text = "VatSim CID:";
            // 
            // tbVatSimId
            // 
            this.tbVatSimId.Location = new System.Drawing.Point(108, 67);
            this.tbVatSimId.Name = "tbVatSimId";
            this.tbVatSimId.Size = new System.Drawing.Size(100, 20);
            this.tbVatSimId.TabIndex = 4;
            this.tbVatSimId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbVatSimId_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 51;
            this.label2.Text = "Max AI Aircraft:";
            // 
            // numMaxAIAircraft
            // 
            this.numMaxAIAircraft.Location = new System.Drawing.Point(108, 93);
            this.numMaxAIAircraft.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numMaxAIAircraft.Name = "numMaxAIAircraft";
            this.numMaxAIAircraft.Size = new System.Drawing.Size(100, 20);
            this.numMaxAIAircraft.TabIndex = 5;
            this.numMaxAIAircraft.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(58, 149);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 53;
            this.label3.Text = "Radius:";
            // 
            // numRadius
            // 
            this.numRadius.Location = new System.Drawing.Point(108, 145);
            this.numRadius.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numRadius.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRadius.Name = "numRadius";
            this.numRadius.Size = new System.Drawing.Size(100, 20);
            this.numRadius.TabIndex = 7;
            this.numRadius.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(215, 149);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 13);
            this.label5.TabIndex = 54;
            this.label5.Text = "Km.";
            // 
            // numMaxMPAircraft
            // 
            this.numMaxMPAircraft.Location = new System.Drawing.Point(108, 119);
            this.numMaxMPAircraft.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numMaxMPAircraft.Name = "numMaxMPAircraft";
            this.numMaxMPAircraft.Size = new System.Drawing.Size(100, 20);
            this.numMaxMPAircraft.TabIndex = 6;
            this.numMaxMPAircraft.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 122);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 13);
            this.label6.TabIndex = 56;
            this.label6.Text = "Max MP Aircraft:";
            // 
            // tbAIPClientToken
            // 
            this.tbAIPClientToken.Location = new System.Drawing.Point(108, 171);
            this.tbAIPClientToken.Name = "tbAIPClientToken";
            this.tbAIPClientToken.Size = new System.Drawing.Size(199, 20);
            this.tbAIPClientToken.TabIndex = 60;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 175);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 13);
            this.label7.TabIndex = 59;
            this.label7.Text = "AIP Client Token:";
            // 
            // SimConnectMapForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(482, 202);
            this.Controls.Add(this.tbAIPClientToken);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.numMaxMPAircraft);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numRadius);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numMaxAIAircraft);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbVatSimId);
            this.Controls.Add(this.label1);
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
            this.Name = "SimConnectMapForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sim Map";
            this.Load += new System.EventHandler(this.SimMapForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numMaxAIAircraft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRadius)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxMPAircraft)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnFont;
        private System.Windows.Forms.TextBox tbFont;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.Button btnFontColor;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbVatSimId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numMaxAIAircraft;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numRadius;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numMaxMPAircraft;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbAIPClientToken;
        private System.Windows.Forms.Label label7;
    }
}