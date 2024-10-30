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
    public partial class AddKeyPressDlg : Form
    {
        public FIPKeyPressButton Button { get; set; }

        private DateTime _keyDownTime;

        private class KeyPressDlgKeyPressLength
        {
            public KeyPressLengths KeyPressLength { get; set; }
            public string Name { get; set; }
        }

        public AddKeyPressDlg()
        {
            InitializeComponent();
            foreach (KeyPressLengths keyPressLength in (KeyPressLengths[])Enum.GetValues(typeof(KeyPressLengths)))
            {
                KeyPressDlgKeyPressLength keyPressDlgKeyPressLength = new KeyPressDlgKeyPressLength()
                {
                    KeyPressLength = keyPressLength,
                    Name = Regex.Replace(keyPressLength.ToString(), "(\\B[A-Z])", " $1").Replace(" A ", " a ").Replace(" And ", " and ")
                };
                if (keyPressLength != KeyPressLengths.Zero)
                {
                    cbKeyPressLength.Items.Add(keyPressDlgKeyPressLength);
                }
                cbKeyPressBreak.Items.Add(keyPressDlgKeyPressLength);
            }
        }

        private int IndexOfKeyPressLength(ComboBox cb, KeyPressLengths keyPressLength)
        {
            for (int i = 0; i < cb.Items.Count; i++)
            {
                KeyPressDlgKeyPressLength keyPressDlgKeyPressLength = cb.Items[i] as KeyPressDlgKeyPressLength;
                if (keyPressDlgKeyPressLength.KeyPressLength == keyPressLength)
                {
                    return i;
                }
            }
            return -1;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Button.KeyPressBreak = ((KeyPressDlgKeyPressLength)cbKeyPressBreak.SelectedItem).KeyPressLength;
            Button.KeyPressLength = ((KeyPressDlgKeyPressLength)cbKeyPressLength.SelectedItem).KeyPressLength;
            Button.VirtualKeyCodes = (HashSet<VirtualKeyCode>)tbKeyStroke.Tag;
            Button.IsDirty = true;
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void AddKeyPressDlg_Load(object sender, EventArgs e)
        {
            tbKeyStroke.Text = Button.VirtualKeyCodesAsString;
            tbKeyStroke.Tag = Button.VirtualKeyCodes;
            cbKeyPressLength.SelectedIndex = IndexOfKeyPressLength(cbKeyPressLength, Button.KeyPressLength);
            cbKeyPressBreak.SelectedIndex = IndexOfKeyPressLength(cbKeyPressBreak, Button.KeyPressBreak);
            btnOK.Enabled = !string.IsNullOrEmpty(tbKeyStroke.Text);
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
            cbKeyPressLength.SelectedIndex = IndexOfKeyPressLength(cbKeyPressLength, keyPressLength);
            btnOK.Enabled = !string.IsNullOrEmpty(tbKeyStroke.Text);
        }

        private void tbKeyStroke_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tbKeyStroke.Text = String.Empty;
            tbKeyStroke.Tag = null;
        }
    }
}
