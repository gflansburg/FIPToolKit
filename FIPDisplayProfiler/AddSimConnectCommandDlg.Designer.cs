
namespace FIPDisplayProfiler
{
    partial class AddSimConnectCommandDlg
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
            this.cbBreak = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblValueUnitsDescripiton = new System.Windows.Forms.Label();
            this.lblSetValueDescription = new System.Windows.Forms.Label();
            this.tbValue = new System.Windows.Forms.TextBox();
            this.lblValue = new System.Windows.Forms.Label();
            this.cbSetValue = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(314, 111);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Enabled = false;
            this.btnOK.Location = new System.Drawing.Point(395, 111);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // cbBreak
            // 
            this.cbBreak.DisplayMember = "Name";
            this.cbBreak.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBreak.FormattingEnabled = true;
            this.cbBreak.Location = new System.Drawing.Point(94, 11);
            this.cbBreak.Name = "cbBreak";
            this.cbBreak.Size = new System.Drawing.Size(375, 21);
            this.cbBreak.TabIndex = 1;
            this.cbBreak.SelectedIndexChanged += new System.EventHandler(this.cbBreak_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 60;
            this.label2.Text = "Break:";
            // 
            // lblValueUnitsDescripiton
            // 
            this.lblValueUnitsDescripiton.Location = new System.Drawing.Point(302, 66);
            this.lblValueUnitsDescripiton.Name = "lblValueUnitsDescripiton";
            this.lblValueUnitsDescripiton.Size = new System.Drawing.Size(167, 35);
            this.lblValueUnitsDescripiton.TabIndex = 84;
            // 
            // lblSetValueDescription
            // 
            this.lblSetValueDescription.Location = new System.Drawing.Point(96, 66);
            this.lblSetValueDescription.Name = "lblSetValueDescription";
            this.lblSetValueDescription.Size = new System.Drawing.Size(194, 35);
            this.lblSetValueDescription.TabIndex = 83;
            // 
            // tbValue
            // 
            this.tbValue.Location = new System.Drawing.Point(342, 38);
            this.tbValue.Name = "tbValue";
            this.tbValue.Size = new System.Drawing.Size(127, 20);
            this.tbValue.TabIndex = 79;
            this.tbValue.TextChanged += new System.EventHandler(this.tbValue_TextChanged);
            this.tbValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbValue_KeyPress);
            // 
            // lblValue
            // 
            this.lblValue.AutoSize = true;
            this.lblValue.Location = new System.Drawing.Point(299, 41);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(37, 13);
            this.lblValue.TabIndex = 81;
            this.lblValue.Text = "Value:";
            // 
            // cbSetValue
            // 
            this.cbSetValue.DisplayMember = "Name";
            this.cbSetValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSetValue.FormattingEnabled = true;
            this.cbSetValue.Location = new System.Drawing.Point(95, 38);
            this.cbSetValue.Name = "cbSetValue";
            this.cbSetValue.Size = new System.Drawing.Size(195, 21);
            this.cbSetValue.TabIndex = 78;
            this.cbSetValue.SelectedIndexChanged += new System.EventHandler(this.cbSetValue_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(31, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 76;
            this.label5.Text = "Command:";
            // 
            // AddSimConnectCommandDlg
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(482, 146);
            this.Controls.Add(this.lblValueUnitsDescripiton);
            this.Controls.Add(this.lblSetValueDescription);
            this.Controls.Add(this.tbValue);
            this.Controls.Add(this.lblValue);
            this.Controls.Add(this.cbSetValue);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbBreak);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddSimConnectCommandDlg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SimConnect Command";
            this.Load += new System.EventHandler(this.AddSimConnectCommandDlg_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ComboBox cbBreak;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblValueUnitsDescripiton;
        private System.Windows.Forms.Label lblSetValueDescription;
        private System.Windows.Forms.TextBox tbValue;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.ComboBox cbSetValue;
        private System.Windows.Forms.Label label5;
    }
}