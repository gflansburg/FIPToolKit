
namespace FIPDisplayProfiler
{
    partial class XPlaneCommandDlg
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
            this.cbCommand = new System.Windows.Forms.ComboBox();
            this.tbValue = new System.Windows.Forms.TextBox();
            this.lblValue = new System.Windows.Forms.Label();
            this.cbSetValue = new System.Windows.Forms.ComboBox();
            this.cbAction = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lblCommandDescription = new System.Windows.Forms.Label();
            this.lblSetValueDescription = new System.Windows.Forms.Label();
            this.lblValueUnitsDescripiton = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(313, 236);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(394, 236);
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
            this.btnFont.Location = new System.Drawing.Point(395, 131);
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
            this.tbFont.Location = new System.Drawing.Point(95, 133);
            this.tbFont.Name = "tbFont";
            this.tbFont.ReadOnly = true;
            this.tbFont.Size = new System.Drawing.Size(294, 20);
            this.tbFont.TabIndex = 9;
            this.tbFont.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(58, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 38;
            this.label4.Text = "Font:";
            // 
            // btnFontColor
            // 
            this.btnFontColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFontColor.Location = new System.Drawing.Point(95, 159);
            this.btnFontColor.Name = "btnFontColor";
            this.btnFontColor.Size = new System.Drawing.Size(32, 21);
            this.btnFontColor.TabIndex = 11;
            this.btnFontColor.UseVisualStyleBackColor = true;
            this.btnFontColor.Click += new System.EventHandler(this.btnFontColor_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(54, 163);
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
            this.btnRemoveIcon.Location = new System.Drawing.Point(133, 214);
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
            this.btnBrowseIcon.Location = new System.Drawing.Point(133, 186);
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
            this.pbIcon.Location = new System.Drawing.Point(95, 186);
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
            this.label3.Location = new System.Drawing.Point(58, 191);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 57;
            this.label3.Text = "Icon:";
            // 
            // cbReColor
            // 
            this.cbReColor.AutoSize = true;
            this.cbReColor.Location = new System.Drawing.Point(134, 245);
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
            this.label5.Location = new System.Drawing.Point(30, 68);
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
            // cbCommand
            // 
            this.cbCommand.DisplayMember = "Name";
            this.cbCommand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCommand.FormattingEnabled = true;
            this.cbCommand.Location = new System.Drawing.Point(94, 65);
            this.cbCommand.Name = "cbCommand";
            this.cbCommand.Size = new System.Drawing.Size(375, 21);
            this.cbCommand.TabIndex = 4;
            this.cbCommand.SelectedIndexChanged += new System.EventHandler(this.cbCommand_SelectedIndexChanged);
            // 
            // tbValue
            // 
            this.tbValue.Location = new System.Drawing.Point(341, 65);
            this.tbValue.Name = "tbValue";
            this.tbValue.Size = new System.Drawing.Size(127, 20);
            this.tbValue.TabIndex = 69;
            this.tbValue.TextChanged += new System.EventHandler(this.tbValue_TextChanged);
            this.tbValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbValue_KeyPress);
            // 
            // lblValue
            // 
            this.lblValue.AutoSize = true;
            this.lblValue.Location = new System.Drawing.Point(298, 68);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(37, 13);
            this.lblValue.TabIndex = 71;
            this.lblValue.Text = "Value:";
            // 
            // cbSetValue
            // 
            this.cbSetValue.DisplayMember = "Name";
            this.cbSetValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSetValue.FormattingEnabled = true;
            this.cbSetValue.Location = new System.Drawing.Point(94, 65);
            this.cbSetValue.Name = "cbSetValue";
            this.cbSetValue.Size = new System.Drawing.Size(195, 21);
            this.cbSetValue.TabIndex = 68;
            this.cbSetValue.SelectedIndexChanged += new System.EventHandler(this.cbSetValue_SelectedIndexChanged);
            // 
            // cbAction
            // 
            this.cbAction.DisplayMember = "Name";
            this.cbAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAction.FormattingEnabled = true;
            this.cbAction.Location = new System.Drawing.Point(94, 38);
            this.cbAction.Name = "cbAction";
            this.cbAction.Size = new System.Drawing.Size(128, 21);
            this.cbAction.TabIndex = 67;
            this.cbAction.SelectedIndexChanged += new System.EventHandler(this.cbAction_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(47, 41);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 13);
            this.label6.TabIndex = 70;
            this.label6.Text = "Action:";
            // 
            // lblCommandDescription
            // 
            this.lblCommandDescription.Location = new System.Drawing.Point(94, 93);
            this.lblCommandDescription.Name = "lblCommandDescription";
            this.lblCommandDescription.Size = new System.Drawing.Size(374, 35);
            this.lblCommandDescription.TabIndex = 72;
            // 
            // lblSetValueDescription
            // 
            this.lblSetValueDescription.Location = new System.Drawing.Point(95, 93);
            this.lblSetValueDescription.Name = "lblSetValueDescription";
            this.lblSetValueDescription.Size = new System.Drawing.Size(194, 35);
            this.lblSetValueDescription.TabIndex = 73;
            // 
            // lblValueUnitsDescripiton
            // 
            this.lblValueUnitsDescripiton.Location = new System.Drawing.Point(341, 93);
            this.lblValueUnitsDescripiton.Name = "lblValueUnitsDescripiton";
            this.lblValueUnitsDescripiton.Size = new System.Drawing.Size(127, 35);
            this.lblValueUnitsDescripiton.TabIndex = 74;
            // 
            // XPlaneCommandDlg
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(482, 271);
            this.Controls.Add(this.lblValueUnitsDescripiton);
            this.Controls.Add(this.lblSetValueDescription);
            this.Controls.Add(this.lblCommandDescription);
            this.Controls.Add(this.tbValue);
            this.Controls.Add(this.lblValue);
            this.Controls.Add(this.cbSetValue);
            this.Controls.Add(this.cbAction);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbCommand);
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
            this.Name = "XPlaneCommandDlg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "X-Plane Command";
            this.Load += new System.EventHandler(this.XPlaneCommandDlg_Load);
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
        private System.Windows.Forms.ComboBox cbCommand;
        private System.Windows.Forms.TextBox tbValue;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.ComboBox cbSetValue;
        private System.Windows.Forms.ComboBox cbAction;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblCommandDescription;
        private System.Windows.Forms.Label lblSetValueDescription;
        private System.Windows.Forms.Label lblValueUnitsDescripiton;
    }
}