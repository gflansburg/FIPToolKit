
namespace FIPDisplayProfiler
{
    partial class DCSWorldCommandSequenceDlg
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
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
            this.label3 = new System.Windows.Forms.Label();
            this.cbReColor = new System.Windows.Forms.CheckBox();
            this.lvDCSWorldCommands = new System.Windows.Forms.ListView();
            this.breakColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.commandColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnAddDCSWorldCommand = new System.Windows.Forms.Button();
            this.btnMoveDown = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnMoveUp = new System.Windows.Forms.Button();
            this.pbIcon = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(384, 363);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(465, 363);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "exe";
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Executable Files|*.exe,*.com|Batch Files|*.bat,*.cmd|All Files|*.*";
            // 
            // btnFont
            // 
            this.btnFont.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFont.Location = new System.Drawing.Point(465, 252);
            this.btnFont.Name = "btnFont";
            this.btnFont.Size = new System.Drawing.Size(75, 23);
            this.btnFont.TabIndex = 7;
            this.btnFont.Text = "Select &Font";
            this.btnFont.UseVisualStyleBackColor = true;
            this.btnFont.Click += new System.EventHandler(this.btnFont_Click);
            // 
            // tbFont
            // 
            this.tbFont.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFont.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tbFont.Location = new System.Drawing.Point(68, 254);
            this.tbFont.Name = "tbFont";
            this.tbFont.ReadOnly = true;
            this.tbFont.Size = new System.Drawing.Size(391, 20);
            this.tbFont.TabIndex = 3;
            this.tbFont.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 257);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 38;
            this.label4.Text = "Font:";
            // 
            // btnFontColor
            // 
            this.btnFontColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFontColor.Location = new System.Drawing.Point(68, 282);
            this.btnFontColor.Name = "btnFontColor";
            this.btnFontColor.Size = new System.Drawing.Size(32, 21);
            this.btnFontColor.TabIndex = 8;
            this.btnFontColor.UseVisualStyleBackColor = true;
            this.btnFontColor.Click += new System.EventHandler(this.btnFontColor_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(27, 287);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(34, 13);
            this.label18.TabIndex = 48;
            this.label18.Text = "Color:";
            // 
            // tbName
            // 
            this.tbName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbName.Location = new System.Drawing.Point(69, 12);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(472, 20);
            this.tbName.TabIndex = 1;
            this.tbName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbName_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 53;
            this.label2.Text = "Label:";
            // 
            // btnRemoveIcon
            // 
            this.btnRemoveIcon.Location = new System.Drawing.Point(106, 338);
            this.btnRemoveIcon.Name = "btnRemoveIcon";
            this.btnRemoveIcon.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveIcon.TabIndex = 10;
            this.btnRemoveIcon.Text = "&Remove";
            this.btnRemoveIcon.UseVisualStyleBackColor = true;
            this.btnRemoveIcon.Click += new System.EventHandler(this.btnRemoveIcon_Click);
            // 
            // btnBrowseIcon
            // 
            this.btnBrowseIcon.Location = new System.Drawing.Point(106, 309);
            this.btnBrowseIcon.Name = "btnBrowseIcon";
            this.btnBrowseIcon.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseIcon.TabIndex = 9;
            this.btnBrowseIcon.Text = "&Browse";
            this.btnBrowseIcon.UseVisualStyleBackColor = true;
            this.btnBrowseIcon.Click += new System.EventHandler(this.btnBrowseIcon_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 314);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 57;
            this.label3.Text = "Icon:";
            // 
            // cbReColor
            // 
            this.cbReColor.AutoSize = true;
            this.cbReColor.Location = new System.Drawing.Point(107, 368);
            this.cbReColor.Name = "cbReColor";
            this.cbReColor.Size = new System.Drawing.Size(64, 17);
            this.cbReColor.TabIndex = 11;
            this.cbReColor.Text = "ReColor";
            this.cbReColor.UseVisualStyleBackColor = true;
            this.cbReColor.CheckedChanged += new System.EventHandler(this.cbReColor_CheckedChanged);
            // 
            // lvDCSWorldCommands
            // 
            this.lvDCSWorldCommands.AutoArrange = false;
            this.lvDCSWorldCommands.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.breakColumnHeader,
            this.commandColumnHeader});
            this.lvDCSWorldCommands.FullRowSelect = true;
            this.lvDCSWorldCommands.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvDCSWorldCommands.HideSelection = false;
            this.lvDCSWorldCommands.Location = new System.Drawing.Point(69, 38);
            this.lvDCSWorldCommands.MultiSelect = false;
            this.lvDCSWorldCommands.Name = "lvDCSWorldCommands";
            this.lvDCSWorldCommands.Size = new System.Drawing.Size(471, 208);
            this.lvDCSWorldCommands.TabIndex = 6;
            this.lvDCSWorldCommands.UseCompatibleStateImageBehavior = false;
            this.lvDCSWorldCommands.View = System.Windows.Forms.View.Details;
            this.lvDCSWorldCommands.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.lvDCSWorldCommands_ColumnWidthChanging);
            this.lvDCSWorldCommands.SelectedIndexChanged += new System.EventHandler(this.lvDCSWorldCommands_SelectedIndexChanged);
            this.lvDCSWorldCommands.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvDCSWorldCommands_MouseDoubleClick);
            // 
            // breakColumnHeader
            // 
            this.breakColumnHeader.Text = "Break (ms)";
            this.breakColumnHeader.Width = 135;
            // 
            // commandColumnHeader
            // 
            this.commandColumnHeader.Text = "X-Plane Command";
            this.commandColumnHeader.Width = 330;
            // 
            // btnAddDCSWorldCommand
            // 
            this.btnAddDCSWorldCommand.BackgroundImage = global::FIPDisplayProfiler.Properties.Resources.add;
            this.btnAddDCSWorldCommand.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnAddDCSWorldCommand.Location = new System.Drawing.Point(10, 182);
            this.btnAddDCSWorldCommand.Name = "btnAddDCSWorldCommand";
            this.btnAddDCSWorldCommand.Size = new System.Drawing.Size(51, 24);
            this.btnAddDCSWorldCommand.TabIndex = 5;
            this.btnAddDCSWorldCommand.UseVisualStyleBackColor = true;
            this.btnAddDCSWorldCommand.Click += new System.EventHandler(this.btnAddDCSWorldCommand_Click);
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.BackgroundImage = global::FIPDisplayProfiler.Properties.Resources.down_arrow;
            this.btnMoveDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnMoveDown.Location = new System.Drawing.Point(11, 139);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(51, 24);
            this.btnMoveDown.TabIndex = 4;
            this.btnMoveDown.UseVisualStyleBackColor = true;
            this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackgroundImage = global::FIPDisplayProfiler.Properties.Resources.delete;
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDelete.Location = new System.Drawing.Point(10, 109);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(51, 24);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.BackgroundImage = global::FIPDisplayProfiler.Properties.Resources.up_arrow;
            this.btnMoveUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnMoveUp.Location = new System.Drawing.Point(11, 79);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(51, 24);
            this.btnMoveUp.TabIndex = 2;
            this.btnMoveUp.UseVisualStyleBackColor = true;
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
            // 
            // pbIcon
            // 
            this.pbIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbIcon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbIcon.Location = new System.Drawing.Point(68, 309);
            this.pbIcon.Name = "pbIcon";
            this.pbIcon.Size = new System.Drawing.Size(32, 32);
            this.pbIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbIcon.TabIndex = 54;
            this.pbIcon.TabStop = false;
            this.pbIcon.Click += new System.EventHandler(this.pbIcon_Click);
            // 
            // DCSWorldCommandSequenceDlg
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(553, 395);
            this.Controls.Add(this.btnAddDCSWorldCommand);
            this.Controls.Add(this.btnMoveDown);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnMoveUp);
            this.Controls.Add(this.lvDCSWorldCommands);
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
            this.Name = "DCSWorldCommandSequenceDlg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DCS World Command Sequence";
            this.Load += new System.EventHandler(this.DCSWorldCommandSequenceDlg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
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
        private System.Windows.Forms.ListView lvDCSWorldCommands;
        private System.Windows.Forms.ColumnHeader breakColumnHeader;
        private System.Windows.Forms.ColumnHeader commandColumnHeader;
        private System.Windows.Forms.Button btnMoveDown;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnMoveUp;
        private System.Windows.Forms.Button btnAddDCSWorldCommand;
    }
}