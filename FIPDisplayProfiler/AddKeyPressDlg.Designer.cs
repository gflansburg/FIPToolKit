
namespace FIPDisplayProfiler
{
    partial class AddKeyPressDlg
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
            this.tbKeyStroke = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbKeyPressLength = new System.Windows.Forms.ComboBox();
            this.cbKeyPressBreak = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(313, 94);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
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
            // tbKeyStroke
            // 
            this.tbKeyStroke.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbKeyStroke.Location = new System.Drawing.Point(68, 38);
            this.tbKeyStroke.Name = "tbKeyStroke";
            this.tbKeyStroke.ReadOnly = true;
            this.tbKeyStroke.Size = new System.Drawing.Size(401, 20);
            this.tbKeyStroke.TabIndex = 2;
            this.tbKeyStroke.Enter += new System.EventHandler(this.tbKeyStroke_Enter);
            this.tbKeyStroke.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbKeyStroke_KeyDown);
            this.tbKeyStroke.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbKeyStroke_KeyUp);
            this.tbKeyStroke.Leave += new System.EventHandler(this.tbKeyStroke_Leave);
            this.tbKeyStroke.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tbKeyStroke_MouseDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 51;
            this.label1.Text = "Key:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 58;
            this.label5.Text = "Length:";
            // 
            // cbKeyPressLength
            // 
            this.cbKeyPressLength.DisplayMember = "Name";
            this.cbKeyPressLength.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbKeyPressLength.FormattingEnabled = true;
            this.cbKeyPressLength.Location = new System.Drawing.Point(69, 65);
            this.cbKeyPressLength.Name = "cbKeyPressLength";
            this.cbKeyPressLength.Size = new System.Drawing.Size(401, 21);
            this.cbKeyPressLength.TabIndex = 3;
            // 
            // cbKeyPressBreak
            // 
            this.cbKeyPressBreak.DisplayMember = "Name";
            this.cbKeyPressBreak.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbKeyPressBreak.FormattingEnabled = true;
            this.cbKeyPressBreak.Location = new System.Drawing.Point(68, 11);
            this.cbKeyPressBreak.Name = "cbKeyPressBreak";
            this.cbKeyPressBreak.Size = new System.Drawing.Size(401, 21);
            this.cbKeyPressBreak.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 60;
            this.label2.Text = "Break:";
            // 
            // AddKeyPressDlg
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(482, 126);
            this.Controls.Add(this.cbKeyPressBreak);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbKeyPressLength);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbKeyStroke);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddKeyPressDlg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Key Press";
            this.Load += new System.EventHandler(this.AddKeyPressDlg_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox tbKeyStroke;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbKeyPressLength;
        private System.Windows.Forms.ComboBox cbKeyPressBreak;
        private System.Windows.Forms.Label label2;
    }
}