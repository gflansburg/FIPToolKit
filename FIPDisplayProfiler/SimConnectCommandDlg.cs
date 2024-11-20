using FIPToolKit.FlightSim;
using FIPToolKit.Models;
using FIPToolKit.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FIPDisplayProfiler
{
    public partial class SimConnectCommandDlg : Form
    {
        public FIPSimConnectCommandButton Button { get; set; }

        private Font _fontHolder;
        private string _iconFilename;

        private class SimConnectCommandDlgAction
        {
            public FIPButtonAction Action { get; set; }
            public string Name { get; set; }
        }


        public SimConnectCommandDlg()
        {
            InitializeComponent();
            List<SimConnectEvent> events = SimConnectEvents.Instance.Events.Values.ToList();
            events.Sort((x, y) => x.Name.CompareTo(y.Name));
            cbSetValue.Items.AddRange(events.ToArray());
            cbSetValue.Items.Insert(0, new SimConnectEvent() { Id = SimConnectEventId.SelectAttributeToWrite });
        }

        private int IndexOfSetValue(string command)
        {
            if (!string.IsNullOrEmpty(command))
            {
                SimConnectEventId id;
                if (Enum.TryParse(command, out id))
                {
                    SimConnectEvent commandSet = cbSetValue.Items.Cast<SimConnectEvent>().FirstOrDefault(x => x.Id == id);
                    if (commandSet != null)
                    {
                        return cbSetValue.Items.IndexOf(commandSet);
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
            Button.Action = FIPButtonAction.Set;
            Button.Command = ((SimConnectEvent)cbSetValue.SelectedItem).Id.ToString();
            Button.Value = tbValue.Text;
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

        private void SimConnectCommandDlg_Load(object sender, EventArgs e)
        {
            cbSetValue.SelectedIndex = IndexOfSetValue(Button.Command);
            tbValue.Text = Button.Value;
            lblSetValueDescription.Text = (cbSetValue.SelectedItem as SimConnectEvent).Description;
            lblValueUnitsDescripiton.Text = (cbSetValue.SelectedItem as SimConnectEvent).Units;
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
                return cbSetValue.SelectedIndex > 0 && ((cbSetValue.SelectedItem as SimConnectEvent).Units.Equals("N/A", StringComparison.OrdinalIgnoreCase) || (!string.IsNullOrEmpty(tbValue.Text)));
            }
        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {
            btnOK.Enabled = IsCommandEnabled && !string.IsNullOrEmpty(tbName.Text);
        }

        private void cbSetValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbName.Text = cbSetValue.SelectedIndex > 0 ? (cbSetValue.SelectedItem as SimConnectEvent).Name : string.Empty;
            btnOK.Enabled = IsCommandEnabled && !string.IsNullOrEmpty(tbName.Text);
            lblSetValueDescription.Text = (cbSetValue.SelectedItem as SimConnectEvent).Description;
            lblValueUnitsDescripiton.Text = (cbSetValue.SelectedItem as SimConnectEvent).Units;
        }

        private void tbValue_TextChanged(object sender, EventArgs e)
        {
            btnOK.Enabled = IsCommandEnabled && !string.IsNullOrEmpty(tbName.Text);
        }

        private void tbValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
