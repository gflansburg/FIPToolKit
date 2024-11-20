
namespace FIPDisplayProfiler
{
    partial class AddFSUIPCCommandDlg
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
            this.tbValue = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbFSUIPCAxisControl = new System.Windows.Forms.ComboBox();
            this.cbAction = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbCommandSet = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbFSUIPCAutoPilotControl = new System.Windows.Forms.ComboBox();
            this.cbFSUIPCControl = new System.Windows.Forms.ComboBox();
            this.cbFsControl = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(313, 94);
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
            this.btnOK.Location = new System.Drawing.Point(394, 94);
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
            // tbValue
            // 
            this.tbValue.Location = new System.Drawing.Point(342, 66);
            this.tbValue.Name = "tbValue";
            this.tbValue.Size = new System.Drawing.Size(127, 20);
            this.tbValue.TabIndex = 8;
            this.tbValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbValue_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(298, 71);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 13);
            this.label7.TabIndex = 74;
            this.label7.Text = "Value:";
            // 
            // cbFSUIPCAxisControl
            // 
            this.cbFSUIPCAxisControl.DisplayMember = "Name";
            this.cbFSUIPCAxisControl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFSUIPCAxisControl.FormattingEnabled = true;
            this.cbFSUIPCAxisControl.Location = new System.Drawing.Point(94, 65);
            this.cbFSUIPCAxisControl.Name = "cbFSUIPCAxisControl";
            this.cbFSUIPCAxisControl.Size = new System.Drawing.Size(195, 21);
            this.cbFSUIPCAxisControl.TabIndex = 7;
            this.cbFSUIPCAxisControl.SelectedIndexChanged += new System.EventHandler(this.cbFSUIPCAxisControl_SelectedIndexChanged);
            // 
            // cbAction
            // 
            this.cbAction.DisplayMember = "Name";
            this.cbAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAction.FormattingEnabled = true;
            this.cbAction.Location = new System.Drawing.Point(341, 38);
            this.cbAction.Name = "cbAction";
            this.cbAction.Size = new System.Drawing.Size(128, 21);
            this.cbAction.TabIndex = 3;
            this.cbAction.SelectedIndexChanged += new System.EventHandler(this.cbAction_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(295, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 13);
            this.label6.TabIndex = 71;
            this.label6.Text = "Action:";
            // 
            // cbCommandSet
            // 
            this.cbCommandSet.DisplayMember = "Name";
            this.cbCommandSet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCommandSet.FormattingEnabled = true;
            this.cbCommandSet.Location = new System.Drawing.Point(94, 38);
            this.cbCommandSet.Name = "cbCommandSet";
            this.cbCommandSet.Size = new System.Drawing.Size(195, 21);
            this.cbCommandSet.TabIndex = 2;
            this.cbCommandSet.SelectedIndexChanged += new System.EventHandler(this.cbCommandSet_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(31, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 69;
            this.label5.Text = "Command:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 68;
            this.label1.Text = "Command Set:";
            // 
            // cbFSUIPCAutoPilotControl
            // 
            this.cbFSUIPCAutoPilotControl.DisplayMember = "Name";
            this.cbFSUIPCAutoPilotControl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFSUIPCAutoPilotControl.FormattingEnabled = true;
            this.cbFSUIPCAutoPilotControl.Location = new System.Drawing.Point(94, 65);
            this.cbFSUIPCAutoPilotControl.Name = "cbFSUIPCAutoPilotControl";
            this.cbFSUIPCAutoPilotControl.Size = new System.Drawing.Size(195, 21);
            this.cbFSUIPCAutoPilotControl.TabIndex = 6;
            this.cbFSUIPCAutoPilotControl.SelectedIndexChanged += new System.EventHandler(this.cbFSUIPCAutoPilotControl_SelectedIndexChanged);
            // 
            // cbFSUIPCControl
            // 
            this.cbFSUIPCControl.DisplayMember = "Name";
            this.cbFSUIPCControl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFSUIPCControl.FormattingEnabled = true;
            this.cbFSUIPCControl.Location = new System.Drawing.Point(94, 65);
            this.cbFSUIPCControl.Name = "cbFSUIPCControl";
            this.cbFSUIPCControl.Size = new System.Drawing.Size(195, 21);
            this.cbFSUIPCControl.TabIndex = 77;
            this.cbFSUIPCControl.SelectedIndexChanged += new System.EventHandler(this.cbFSUIPCControl_SelectedIndexChanged);
            // 
            // cbFsControl
            // 
            this.cbFsControl.DisplayMember = "Name";
            this.cbFsControl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFsControl.FormattingEnabled = true;
            this.cbFsControl.Location = new System.Drawing.Point(94, 65);
            this.cbFsControl.Name = "cbFsControl";
            this.cbFsControl.Size = new System.Drawing.Size(195, 21);
            this.cbFsControl.TabIndex = 4;
            this.cbFsControl.SelectedIndexChanged += new System.EventHandler(this.cbFsControl_SelectedIndexChanged);
            // 
            // AddFSUIPCCommandDlg
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(482, 126);
            this.Controls.Add(this.cbFsControl);
            this.Controls.Add(this.cbFSUIPCControl);
            this.Controls.Add(this.cbFSUIPCAutoPilotControl);
            this.Controls.Add(this.tbValue);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbFSUIPCAxisControl);
            this.Controls.Add(this.cbAction);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbCommandSet);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbBreak);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddFSUIPCCommandDlg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FSUIPC Command";
            this.Load += new System.EventHandler(this.AddFSUIPCCommandDlg_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ComboBox cbBreak;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbValue;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbFSUIPCAxisControl;
        private System.Windows.Forms.ComboBox cbAction;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbCommandSet;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbFSUIPCAutoPilotControl;
        private System.Windows.Forms.ComboBox cbFSUIPCControl;
        private System.Windows.Forms.ComboBox cbFsControl;
    }
}