
namespace FIPDisplayProfiler
{
    partial class AddDCSWorldCommandDlg
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
            this.label5 = new System.Windows.Forms.Label();
            this.tbValue = new System.Windows.Forms.TextBox();
            this.lblValue = new System.Windows.Forms.Label();
            this.cbSetValue = new System.Windows.Forms.ComboBox();
            this.lblSetValueDescription = new System.Windows.Forms.Label();
            this.lblValueUnitsDescripiton = new System.Windows.Forms.Label();
            this.cbFlightModel = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbInputType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbInputValueFixedStep = new System.Windows.Forms.ComboBox();
            this.cbInputValueAction = new System.Windows.Forms.ComboBox();
            this.cbBreak = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(315, 190);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(396, 190);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
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
            // lblSetValueDescription
            // 
            this.lblSetValueDescription.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lblSetValueDescription.Location = new System.Drawing.Point(94, 144);
            this.lblSetValueDescription.Name = "lblSetValueDescription";
            this.lblSetValueDescription.Size = new System.Drawing.Size(266, 35);
            this.lblSetValueDescription.TabIndex = 73;
            // 
            // lblValueUnitsDescripiton
            // 
            this.lblValueUnitsDescripiton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblValueUnitsDescripiton.Location = new System.Drawing.Point(376, 144);
            this.lblValueUnitsDescripiton.Name = "lblValueUnitsDescripiton";
            this.lblValueUnitsDescripiton.Size = new System.Drawing.Size(91, 35);
            this.lblValueUnitsDescripiton.TabIndex = 74;
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
            // cbBreak
            // 
            this.cbBreak.DisplayMember = "Name";
            this.cbBreak.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBreak.FormattingEnabled = true;
            this.cbBreak.Location = new System.Drawing.Point(92, 11);
            this.cbBreak.Name = "cbBreak";
            this.cbBreak.Size = new System.Drawing.Size(375, 21);
            this.cbBreak.TabIndex = 82;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(48, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 83;
            this.label2.Text = "Break:";
            // 
            // AddDCSWorldCommandDlg
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(482, 225);
            this.Controls.Add(this.cbBreak);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbInputValueAction);
            this.Controls.Add(this.cbInputValueFixedStep);
            this.Controls.Add(this.cbInputType);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbFlightModel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblValueUnitsDescripiton);
            this.Controls.Add(this.lblSetValueDescription);
            this.Controls.Add(this.tbValue);
            this.Controls.Add(this.lblValue);
            this.Controls.Add(this.cbSetValue);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddDCSWorldCommandDlg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DCS World Command";
            this.Load += new System.EventHandler(this.AddDCSWorldCommandDlg_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbValue;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.ComboBox cbSetValue;
        private System.Windows.Forms.Label lblSetValueDescription;
        private System.Windows.Forms.Label lblValueUnitsDescripiton;
        private System.Windows.Forms.ComboBox cbFlightModel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbInputType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbInputValueFixedStep;
        private System.Windows.Forms.ComboBox cbInputValueAction;
        private System.Windows.Forms.ComboBox cbBreak;
        private System.Windows.Forms.Label label2;
    }
}