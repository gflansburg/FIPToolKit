using FIPToolKit.Models;
using FIPToolKit.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace FIPDisplayProfiler
{
    public partial class KeyPressDlg : Form
    {
        public FIPKeyPressButton Button { get; set; }

        private Font _fontHolder;
        private string _iconFilename;
        private DateTime _keyDownTime;

        private class KeyPressDlgKeyPressLength
        {
            public KeyPressLengths KeyPressLength { get; set; }
            public string Name { get; set; }
        }

        public KeyPressDlg()
        {
            InitializeComponent();
            foreach (KeyPressLengths keyPressLength in (KeyPressLengths[])Enum.GetValues(typeof(KeyPressLengths)))
            {
                if (keyPressLength != KeyPressLengths.Zero)
                {
                    cbKeyPressLength.Items.Add(new KeyPressDlgKeyPressLength()
                    {
                        KeyPressLength = keyPressLength,
                        Name = Regex.Replace(keyPressLength.ToString(), "(\\B[A-Z])", " $1").Replace(" A ", " a ").Replace(" And ", " and ")
                    });
                }
            }
        }

        private int IndexOfKeyPressLength(KeyPressLengths keyPressLength)
        {
            for (int i = 0; i < cbKeyPressLength.Items.Count; i++)
            {
                KeyPressDlgKeyPressLength keyPressDlgKeyPressLength = cbKeyPressLength.Items[i] as KeyPressDlgKeyPressLength;
                if (keyPressDlgKeyPressLength.KeyPressLength == keyPressLength)
                {
                    return i;
                }
            }
            return -1;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Button.Label = tbName.Text;
            Button.Font = _fontHolder;
            Button.Color = btnFontColor.BackColor;
            Button.IconFilename = _iconFilename;
            Button.ReColor = cbReColor.Checked;
            Button.KeyPressLength = ((KeyPressDlgKeyPressLength)cbKeyPressLength.SelectedItem).KeyPressLength;
            Button.VirtualKeyCodes = (HashSet<VirtualKeyCode>)tbKeyStroke.Tag;
            Button.IsDirty = true;
            DialogResult = DialogResult.OK;
            this.Close();
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

        private void KeyPressDlg_Load(object sender, EventArgs e)
        {
            tbName.Text = Button.Label;
            tbKeyStroke.Text = Button.VirtualKeyCodesAsString;
            tbKeyStroke.Tag = Button.VirtualKeyCodes;
            _fontHolder = Button.Font;
            tbFont.Font = new Font(Button.Font.FontFamily, tbFont.Font.Size, Button.Font.Style, Button.Font.Unit, Button.Font.GdiCharSet);
            tbFont.Text = Button.Font.FontFamily.Name;
            btnFontColor.BackColor = Button.Color;
            _iconFilename = Button.IconFilename;
            pbIcon.Image = Button.Icon;
            cbReColor.Checked = Button.ReColor;
            cbKeyPressLength.SelectedIndex = IndexOfKeyPressLength(Button.KeyPressLength);
            btnOK.Enabled = !string.IsNullOrEmpty(tbKeyStroke.Text) && !string.IsNullOrEmpty(tbName.Text);
        }

        private void btnRemoveIcon_Click(object sender, EventArgs e)
        {
            pbIcon.Image = null;
            _iconFilename = String.Empty;
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

        private void tbKeyStroke_Enter(object sender, EventArgs e)
        {
            tbKeyStroke.BackColor = Color.Yellow;
        }

        private void tbKeyStroke_Leave(object sender, EventArgs e)
        {
            tbKeyStroke.BackColor = SystemColors.Control;
        }

        private void tbKeyStroke_KeyDown(object sender, KeyEventArgs e)
        {
            _keyDownTime = DateTime.Now;
            VirtualKeyCode keyPressed = (VirtualKeyCode)e.KeyValue;
            HashSet<VirtualKeyCode> virtualKeyCodes = KeyModifiers.GetPressedVirtualKeyCodesThatAreModifiers();
            virtualKeyCodes.Add(keyPressed);
            tbKeyStroke.Text = KeyModifiers.GetVirtualKeyCodesAsString(virtualKeyCodes);
            tbKeyStroke.Tag = virtualKeyCodes;
            e.SuppressKeyPress = true;
        }

        private void tbKeyStroke_KeyUp(object sender, KeyEventArgs e)
        {
            TimeSpan span = DateTime.Now - _keyDownTime;
            KeyPressLengths keyPressLength = ((KeyPressDlgKeyPressLength)cbKeyPressLength.SelectedItem).KeyPressLength;
            int elapsedMilliSeconds = (int)span.TotalMilliseconds;
            foreach (KeyPressLengths keyPressLengths in (KeyPressLengths[])Enum.GetValues(typeof(KeyPressLengths)))
            {
                if (keyPressLengths != KeyPressLengths.Indefinite && keyPressLengths != KeyPressLengths.Zero)
                {
                    if (elapsedMilliSeconds >= (int)keyPressLengths)
                    {
                        keyPressLength = keyPressLengths;
                    }
                }
            }
            cbKeyPressLength.SelectedIndex = IndexOfKeyPressLength(keyPressLength);
            btnOK.Enabled = !string.IsNullOrEmpty(tbKeyStroke.Text) && !string.IsNullOrEmpty(tbName.Text);
        }

        private void tbKeyStroke_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tbKeyStroke.Text = String.Empty;
            tbKeyStroke.Tag = null;
        }

        private void tbName_KeyPress(object sender, KeyPressEventArgs e)
        {
            btnOK.Enabled = !string.IsNullOrEmpty(tbKeyStroke.Text) && !string.IsNullOrEmpty(tbName.Text);
        }
    }
}
