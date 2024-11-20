using FIPToolKit.Models;
using FIPToolKit.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using XPlaneConnect;

namespace FIPDisplayProfiler
{
    public partial class XPlaneCommandDlg : Form
    {
        public FIPXPlaneCommandButton Button { get; set; }

        private Font _fontHolder;
        private string _iconFilename;

        private class XPlaneCommandDlgAction
        {
            public FIPButtonAction Action { get; set; }
            public string Name { get; set; }
        }


        public XPlaneCommandDlg()
        {
            InitializeComponent();
            foreach (FIPButtonAction action in (FIPButtonAction[])Enum.GetValues(typeof(FIPButtonAction)))
            {
                cbAction.Items.Add(new CommandDlgAction()
                {
                    Action = action,
                    Name = action.ToString().ToTitleCase()
                });
            }
            List<XPlaneCommand> commands = XPlaneConnect.XPlaneStructs.Commands.CommandList.Values.ToList();
            commands.Sort((x, y) => x.Name.CompareTo(y.Name));
            cbCommand.Items.AddRange(commands.ToArray());
            cbCommand.Items.Insert(0, new XPlaneCommand(string.Empty, string.Empty, "--Select Command--", XPlaneCommands.NoneNone));

            List<DataRefElement> datarefs = XPlaneStructs.DataRefs.DataRefList.Values.Where(d => d.Writable).ToList();
            datarefs.Sort((x, y) => x.Name.CompareTo(y.Name));
            cbSetValue.Items.AddRange(datarefs.ToArray());
            cbSetValue.Items.Insert(0, new DataRefElement() { Name = "--Select Attribute To Write--" });
        }

        private int IndexOfButtonAction(FIPButtonAction action)
        {
            for (int i = 0; i < cbAction.Items.Count; i++)
            {
                CommandDlgAction commandDlgAction = cbAction.Items[i] as CommandDlgAction;
                if (commandDlgAction.Action == action)
                {
                    return i;
                }
            }
            return -1;
        }

        private int IndexOfSetValue(string command)
        {
            if (!string.IsNullOrEmpty(command))
            {
                DataRefId id;
                if (Enum.TryParse(command, out id))
                {
                    DataRefElement commandSet = cbSetValue.Items.Cast<DataRefElement>().FirstOrDefault(x => x.Id == id);
                    if (commandSet != null)
                    {
                        return cbSetValue.Items.IndexOf(commandSet);
                    }
                }
            }
            return 0;
        }

        private int IndexOfCommand(string command)
        {
            if (!string.IsNullOrEmpty(command))
            {
                XPlaneCommands id;
                if (Enum.TryParse(command, out id))
                {
                    XPlaneCommand commandSet = cbCommand.Items.Cast<XPlaneCommand>().FirstOrDefault(x => x.Id == id);
                    if (commandSet != null)
                    {
                        return cbCommand.Items.IndexOf(commandSet);
                    }
                }
            }
            return 0;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Button.Label = tbName.Text;
            Button.Font = _fontHolder;
            Button.Color = btnFontColor.BackColor;
            Button.IconFilename = _iconFilename;
            Button.ReColor = cbReColor.Checked;
            Button.Action = (cbAction.SelectedItem as CommandDlgAction).Action;
            Button.Command = Button.Action == FIPButtonAction.Set ? ((DataRefElement)cbSetValue.SelectedItem).Id.ToString() : ((XPlaneCommand)cbCommand.SelectedItem).Id.ToString();
            Button.Value = (Button.Action == FIPButtonAction.Set ? tbValue.Text : string.Empty);
            Button.IsDirty = true;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnFont_Click(object sender, EventArgs e)
        {
            fontDialog1.Font = _fontHolder;
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                _fontHolder = fontDialog1.Font;
                tbFont.Font = new Font(fontDialog1.Font.FontFamily, tbFont.Font.Size, fontDialog1.Font.Style, fontDialog1.Font.Unit, fontDialog1.Font.GdiCharSet);
                tbFont.Text = fontDialog1.Font.Name;
            }
        }

        private void btnFontColor_Click(object sender, EventArgs e)
        {
            colorDialog1.CustomColors = Properties.Settings.Default.CustomColors;
            colorDialog1.Color = btnFontColor.BackColor;
            if (colorDialog1.ShowDialog(this) == DialogResult.OK)
            {
                btnFontColor.BackColor = colorDialog1.Color;
                Properties.Settings.Default.CustomColors = colorDialog1.CustomColors;
                Properties.Settings.Default.Save();
                if (!string.IsNullOrEmpty(_iconFilename))
                {
                    pbIcon.Image = cbReColor.Checked ? FIPToolKit.Drawing.ImageHelper.ChangeToColor(Image.FromFile(_iconFilename), btnFontColor.BackColor) : Image.FromFile(_iconFilename);
                }
            }
        }

        private void XPlaneCommandDlg_Load(object sender, EventArgs e)
        {
            cbAction.SelectedIndex = IndexOfButtonAction(Button.Action);
            switch(Button.Action)
            {
                case FIPButtonAction.Set:
                    cbCommand.SelectedIndex = 0;
                    cbSetValue.SelectedIndex = IndexOfSetValue(Button.Command);
                    tbValue.Text = Button.Value;
                    lblSetValueDescription.Text = (cbSetValue.SelectedItem as DataRefElement).Description;
                    lblValueUnitsDescripiton.Text = (cbSetValue.SelectedItem as DataRefElement).Units;
                    break;
                case FIPButtonAction.Toggle:
                    cbSetValue.SelectedIndex = 0;
                    cbCommand.SelectedIndex = IndexOfCommand(Button.Command);
                    lblCommandDescription.Text = (cbCommand.SelectedItem as XPlaneCommand).Description;
                    break;
            }
            ShowDropDowns();
            tbName.Text = Button.Label;
            _fontHolder = Button.Font;
            tbFont.Font = new Font(Button.Font.FontFamily, tbFont.Font.Size, Button.Font.Style, Button.Font.Unit, Button.Font.GdiCharSet);
            tbFont.Text = Button.Font.FontFamily.Name;
            btnFontColor.BackColor = Button.Color;
            _iconFilename = Button.IconFilename;
            pbIcon.Image = Button.Icon;
            cbReColor.Checked = Button.ReColor;
            btnOK.Enabled = IsCommandEnabled && !string.IsNullOrEmpty(tbName.Text);
        }

        private void btnRemoveIcon_Click(object sender, EventArgs e)
        {
            pbIcon.Image = null;
            _iconFilename = string.Empty;
        }

        private void btnBrowseIcon_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.png;*.gif;*.tif;*.tiff;*.ico|Jpeg Images|*.jpg;*.jpeg|Bitmap Images|*.bmp|PNG Images|*.png|GIF Images|*.gif|TIFF Images|*.tif;*.tiff|Icons|*.ico";
            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            openFileDialog1.FileName = _iconFilename;
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                _iconFilename = openFileDialog1.FileName;
                pbIcon.Image = cbReColor.Checked ? FIPToolKit.Drawing.ImageHelper.ChangeToColor(Image.FromFile(_iconFilename), btnFontColor.BackColor) : Image.FromFile(_iconFilename);
            }
        }

        private void cbReColor_CheckedChanged(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(_iconFilename))
            {
                pbIcon.Image = cbReColor.Checked ? FIPToolKit.Drawing.ImageHelper.ChangeToColor(Image.FromFile(_iconFilename), btnFontColor.BackColor) : Image.FromFile(_iconFilename);
            }
        }

        private void pbIcon_Click(object sender, EventArgs e)
        {
            btnBrowseIcon_Click(sender, e);
        }

        private bool IsCommandEnabled
        {
            get
            {
                switch ((cbAction.SelectedItem as CommandDlgAction).Action)
                {
                    case FIPButtonAction.Set:
                        return cbSetValue.SelectedIndex > 0 && !string.IsNullOrEmpty(tbValue.Text);
                    case FIPButtonAction.Toggle:
                        return cbCommand.SelectedIndex > 0;
                }
                return false;
            }
        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {
            btnOK.Enabled = IsCommandEnabled && !string.IsNullOrEmpty(tbName.Text);
        }

        private void cbCommand_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbName.Text = cbCommand.SelectedIndex > 0 ? (cbCommand.SelectedItem as XPlaneCommand).Name : string.Empty;
            btnOK.Enabled = IsCommandEnabled && !string.IsNullOrEmpty(tbName.Text);
            lblCommandDescription.Text = (cbCommand.SelectedItem as XPlaneCommand).Description;
        }

        private void cbAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowDropDowns();
        }

        private void ShowDropDowns()
        {
            switch ((cbAction.SelectedItem as CommandDlgAction).Action)
            {
                case FIPButtonAction.Set:
                    cbCommand.Visible = false;
                    lblCommandDescription.Visible = false;
                    cbSetValue.Visible = true;
                    tbValue.Visible = true;
                    lblSetValueDescription.Visible = true;
                    lblValueUnitsDescripiton.Visible = true;
                    lblValue.Visible = true;
                    break;
                case FIPButtonAction.Toggle:
                    cbCommand.Visible = true;
                    lblCommandDescription.Visible = true;
                    cbSetValue.Visible = false;
                    tbValue.Visible = false;
                    lblSetValueDescription.Visible = false;
                    lblValueUnitsDescripiton.Visible = false;
                    lblValue.Visible = false;
                    break;
            }
        }

        private void cbSetValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbName.Text = cbSetValue.SelectedIndex > 0 ? (cbSetValue.SelectedItem as DataRefElement).Name : string.Empty;
            btnOK.Enabled = IsCommandEnabled && !string.IsNullOrEmpty(tbName.Text);
            lblSetValueDescription.Text = (cbSetValue.SelectedItem as DataRefElement).Description;
            lblValueUnitsDescripiton.Text = (cbSetValue.SelectedItem as DataRefElement).Units;
        }

        private void tbValue_TextChanged(object sender, EventArgs e)
        {
            btnOK.Enabled = IsCommandEnabled && !string.IsNullOrEmpty(tbName.Text);
        }

        private void tbValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((cbAction.SelectedItem as CommandDlgAction).Action == FIPButtonAction.Set && cbSetValue.SelectedItem.GetType() != typeof(StringDataRefElement))
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }
    }
}
