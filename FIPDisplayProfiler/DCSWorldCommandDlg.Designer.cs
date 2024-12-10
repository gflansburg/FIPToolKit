
namespace FIPDisplayProfiler
{
    partial class DCSWorldCommandDlg
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
            this.tbName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnRemoveIcon = new System.Windows.Forms.Button();
            this.btnBrowseIcon = new System.Windows.Forms.Button();
            this.pbIcon = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbReColor = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tbValue = new System.Windows.Forms.TextBox();
            this.lblValue = new System.Windows.Forms.Label();
            this.cbSetValue = new System.Windows.Forms.ComboBox();
            this.cbFlightModel = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbInputType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbInputValueFixedStep = new System.Windows.Forms.ComboBox();
            this.cbInputValueAction = new System.Windows.Forms.ComboBox();
            this.lblValueUnitsDescripiton = new System.Windows.Forms.Label();
            this.lblSetValueDescription = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(315, 287);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(396, 287);
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
            this.btnFont.Location = new System.Drawing.Point(394, 182);
            this.btnFont.Name = "btnFont";
            this.btnFont.Size = new System.Drawing.Size(75, 23);
            this.btnFont.TabIndex = 10;
            this.btnFont.Text = "Select &Font";
            this.btnFont.UseVisualStyleBackColor = true;
            this.btnFont.Click += new System.EventHandler(this.btnFont_Click);
            // 
            // tbFont
            // 
            this.tbFont.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFont.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tbFont.Location = new System.Drawing.Point(94, 182);
            this.tbFont.Name = "tbFont";
            this.tbFont.ReadOnly = true;
            this.tbFont.Size = new System.Drawing.Size(294, 20);
            this.tbFont.TabIndex = 9;
            this.tbFont.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(57, 185);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 38;
            this.label4.Text = "Font:";
            // 
            // btnFontColor
            // 
            this.btnFontColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFontColor.Location = new System.Drawing.Point(94, 210);
            this.btnFontColor.Name = "btnFontColor";
            this.btnFontColor.Size = new System.Drawing.Size(32, 21);
            this.btnFontColor.TabIndex = 11;
            this.btnFontColor.UseVisualStyleBackColor = true;
            this.btnFontColor.Click += new System.EventHandler(this.btnFontColor_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(54, 214);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(34, 13);
            this.label18.TabIndex = 48;
            this.label18.Text = "Color:";
            // 
            // tbName
            // 
            this.tbName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbName.Location = new System.Drawing.Point(94, 12);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(376, 20);
            this.tbName.TabIndex = 1;
            this.tbName.TextChanged += new System.EventHandler(this.tbName_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(52, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 53;
            this.label2.Text = "Label:";
            // 
            // btnRemoveIcon
            // 
            this.btnRemoveIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveIcon.Location = new System.Drawing.Point(132, 265);
            this.btnRemoveIcon.Name = "btnRemoveIcon";
            this.btnRemoveIcon.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveIcon.TabIndex = 13;
            this.btnRemoveIcon.Text = "&Remove";
            this.btnRemoveIcon.UseVisualStyleBackColor = true;
            this.btnRemoveIcon.Click += new System.EventHandler(this.btnRemoveIcon_Click);
            // 
            // btnBrowseIcon
            // 
            this.btnBrowseIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseIcon.Location = new System.Drawing.Point(132, 237);
            this.btnBrowseIcon.Name = "btnBrowseIcon";
            this.btnBrowseIcon.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseIcon.TabIndex = 12;
            this.btnBrowseIcon.Text = "&Browse";
            this.btnBrowseIcon.UseVisualStyleBackColor = true;
            this.btnBrowseIcon.Click += new System.EventHandler(this.btnBrowseIcon_Click);
            // 
            // pbIcon
            // 
            this.pbIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbIcon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbIcon.Location = new System.Drawing.Point(94, 237);
            this.pbIcon.Name = "pbIcon";
            this.pbIcon.Size = new System.Drawing.Size(32, 32);
            this.pbIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbIcon.TabIndex = 54;
            this.pbIcon.TabStop = false;
            this.pbIcon.Click += new System.EventHandler(this.pbIcon_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(57, 242);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 57;
            this.label3.Text = "Icon:";
            // 
            // cbReColor
            // 
            this.cbReColor.AutoSize = true;
            this.cbReColor.Location = new System.Drawing.Point(133, 296);
            this.cbReColor.Name = "cbReColor";
            this.cbReColor.Size = new System.Drawing.Size(64, 17);
            this.cbReColor.TabIndex = 14;
            this.cbReColor.Text = "ReColor";
            this.cbReColor.UseVisualStyleBackColor = true;
            this.cbReColor.CheckedChanged += new System.EventHandler(this.cbReColor_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 58;
            this.label5.Text = "Command:";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "exe";
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Executable Files|*.exe,*.com|Batch Files|*.bat,*.cmd|All Files|*.*";
            // 
            // tbValue
            // 
            this.tbValue.Enabled = false;
            this.tbValue.Location = new System.Drawing.Point(93, 120);
            this.tbValue.Name = "tbValue";
            this.tbValue.Size = new System.Drawing.Size(127, 20);
            this.tbValue.TabIndex = 69;
            this.tbValue.TextChanged += new System.EventHandler(this.tbValue_TextChanged);
            this.tbValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbValue_KeyPress);
            // 
            // lblValue
            // 
            this.lblValue.AutoSize = true;
            this.lblValue.Location = new System.Drawing.Point(50, 123);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(37, 13);
            this.lblValue.TabIndex = 71;
            this.lblValue.Text = "Value:";
            // 
            // cbSetValue
            // 
            this.cbSetValue.DisplayMember = "Identifier";
            this.cbSetValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSetValue.Enabled = false;
            this.cbSetValue.FormattingEnabled = true;
            this.cbSetValue.Location = new System.Drawing.Point(94, 66);
            this.cbSetValue.Name = "cbSetValue";
            this.cbSetValue.Size = new System.Drawing.Size(375, 21);
            this.cbSetValue.TabIndex = 68;
            this.cbSetValue.SelectedIndexChanged += new System.EventHandler(this.cbSetValue_SelectedIndexChanged);
            // 
            // cbFlightModel
            // 
            this.cbFlightModel.DisplayMember = "Description";
            this.cbFlightModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFlightModel.FormattingEnabled = true;
            this.cbFlightModel.Location = new System.Drawing.Point(94, 38);
            this.cbFlightModel.Name = "cbFlightModel";
            this.cbFlightModel.Size = new System.Drawing.Size(374, 21);
            this.cbFlightModel.TabIndex = 76;
            this.cbFlightModel.SelectedIndexChanged += new System.EventHandler(this.cbFlightModel_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 75;
            this.label1.Text = "Flight Model:";
            // 
            // cbInputType
            // 
            this.cbInputType.DisplayMember = "Name";
            this.cbInputType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbInputType.Enabled = false;
            this.cbInputType.FormattingEnabled = true;
            this.cbInputType.Location = new System.Drawing.Point(93, 93);
            this.cbInputType.Name = "cbInputType";
            this.cbInputType.Size = new System.Drawing.Size(375, 21);
            this.cbInputType.TabIndex = 78;
            this.cbInputType.SelectedIndexChanged += new System.EventHandler(this.cbInputType_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 96);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 13);
            this.label6.TabIndex = 77;
            this.label6.Text = "Input Type:";
            // 
            // cbInputValueFixedStep
            // 
            this.cbInputValueFixedStep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbInputValueFixedStep.Enabled = false;
            this.cbInputValueFixedStep.FormattingEnabled = true;
            this.cbInputValueFixedStep.Items.AddRange(new object[] {
            "INC",
            "DEC"});
            this.cbInputValueFixedStep.Location = new System.Drawing.Point(92, 120);
            this.cbInputValueFixedStep.Name = "cbInputValueFixedStep";
            this.cbInputValueFixedStep.Size = new System.Drawing.Size(375, 21);
            this.cbInputValueFixedStep.TabIndex = 80;
            this.cbInputValueFixedStep.Visible = false;
            // 
            // cbInputValueAction
            // 
            this.cbInputValueAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbInputValueAction.Enabled = false;
            this.cbInputValueAction.FormattingEnabled = true;
            this.cbInputValueAction.Items.AddRange(new object[] {
            "TOGGLE"});
            this.cbInputValueAction.Location = new System.Drawing.Point(92, 119);
            this.cbInputValueAction.Name = "cbInputValueAction";
            this.cbInputValueAction.Size = new System.Drawing.Size(375, 21);
            this.cbInputValueAction.TabIndex = 81;
            this.cbInputValueAction.Visible = false;
            // 
            // lblValueUnitsDescripiton
            // 
            this.lblValueUnitsDescripiton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblValueUnitsDescripiton.Location = new System.Drawing.Point(373, 144);
            this.lblValueUnitsDescripiton.Name = "lblValueUnitsDescripiton";
            this.lblValueUnitsDescripiton.Size = new System.Drawing.Size(91, 35);
            this.lblValueUnitsDescripiton.TabIndex = 83;
            this.lblValueUnitsDescripiton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblSetValueDescription
            // 
            this.lblSetValueDescription.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lblSetValueDescription.Location = new System.Drawing.Point(91, 144);
            this.lblSetValueDescription.Name = "lblSetValueDescription";
            this.lblSetValueDescription.Size = new System.Drawing.Size(266, 35);
            this.lblSetValueDescription.TabIndex = 82;
            // 
            // DCSWorldCommandDlg
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(482, 322);
            this.Controls.Add(this.lblValueUnitsDescripiton);
            this.Controls.Add(this.lblSetValueDescription);
            this.Controls.Add(this.cbInputValueAction);
            this.Controls.Add(this.cbInputValueFixedStep);
            this.Controls.Add(this.cbInputType);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbFlightModel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbValue);
            this.Controls.Add(this.lblValue);
            this.Controls.Add(this.cbSetValue);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbReColor);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnRemoveIcon);
            this.Controls.Add(this.btnBrowseIcon);
            this.Controls.Add(this.pbIcon);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.label2);
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
            this.Name = "DCSWorldCommandDlg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DCS World Command";
            this.Load += new System.EventHandler(this.DCSWorldCommandDlg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).EndInit();
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
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnRemoveIcon;
        private System.Windows.Forms.Button btnBrowseIcon;
        private System.Windows.Forms.PictureBox pbIcon;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbReColor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox tbValue;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.ComboBox cbSetValue;
        private System.Windows.Forms.ComboBox cbFlightModel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbInputType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbInputValueFixedStep;
        private System.Windows.Forms.ComboBox cbInputValueAction;
        private System.Windows.Forms.Label lblValueUnitsDescripiton;
        private System.Windows.Forms.Label lblSetValueDescription;
    }
}