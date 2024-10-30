using FIPToolKit.Models;
using FIPToolKit.Tools;
using FSUIPC;
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
    public partial class FSUIPCCommandDlg : Form
    {
        public FIPFSUIPCCommandButton Button { get; set; }

        private Font _fontHolder;
        private string _iconFilename;

        private class FSUIPCCommandDlgControlSet
        {
            public FIPFSUIPCControlSet ControlSet { get; set; }
            public string Name { get; set; }
        }

        private class FSUIPCCommandDlgAction
        {
            public FIPFSUIPCButtonAction Action { get; set; }
            public string Name { get; set; }
        }

        private class FSUIPCCommandDlgFsControl
        {
            public FsControl? Control { get; set; }
            public string Name { get; set; }
        }

        private class FSUIPCCommandDlgFSUIPCControl
        {
            public FSUIPCControl? Control { get; set; }
            public string Name { get; set; }
        }

        private class FSUIPCCommandDlgFSUIPCAutoPilotControl
        {
            public FSUIPCAutoPilotControl? Control { get; set; }
            public string Name { get; set; }
        }

        private class FSUIPCCommandDlgFSUIPCAxisControl
        {
            public FSUIPCAxisControl? Control { get; set; }
            public string Name { get; set; }
        }

        public FSUIPCCommandDlg()
        {
            InitializeComponent();
            foreach (FIPFSUIPCControlSet controlSet in (FIPFSUIPCControlSet[])Enum.GetValues(typeof(FIPFSUIPCControlSet)))
            {
                cbCommandSet.Items.Add(new FSUIPCCommandDlgControlSet()
                {
                    ControlSet = controlSet,
                    Name = Regex.Replace(controlSet.ToString(), "(\\B[A-Z])", " $1").Replace(" A ", " a ").Replace(" And ", " and ").Replace("Fsuipc", "FSUIPC").Replace("Fs ", "FS ")
                });
            }
            foreach (FIPFSUIPCButtonAction action in (FIPFSUIPCButtonAction[])Enum.GetValues(typeof(FIPFSUIPCButtonAction)))
            {
                cbAction.Items.Add(new FSUIPCCommandDlgAction()
                {
                    Action = action,
                    Name = Regex.Replace(action.ToString(), "(\\B[A-Z])", " $1").Replace(" A ", " a ").Replace(" And ", " and ")
                });
            }
            cbFsControl.Items.Add(new FSUIPCCommandDlgFsControl()
            {
                Name = "--Select Command--"
            });
            cbFSUIPCControl.Items.Add(new FSUIPCCommandDlgFSUIPCControl()
            {
                Name = "--Select Command--"
            });
            cbFSUIPCAutoPilotControl.Items.Add(new FSUIPCCommandDlgFSUIPCAutoPilotControl()
            {
                Name = "--Select Command--"
            });
            cbFSUIPCAxisControl.Items.Add(new FSUIPCCommandDlgFSUIPCAxisControl()
            {
                Name = "--Select Command--"
            });
            List<FSUIPCCommandDlgFsControl> fsControls = new List<FSUIPCCommandDlgFsControl>();
            foreach (FsControl control in (FsControl[])Enum.GetValues(typeof(FsControl)))
            {
                fsControls.Add(new FSUIPCCommandDlgFsControl()
                {
                    Control = control,
                    Name = FIPFSUIPCCommandButton.ToString(control)
                }); ;
            }
            fsControls.Sort((x, y) => x.Name.CompareTo(y.Name));
            cbFsControl.Items.AddRange(fsControls.ToArray());
            List<FSUIPCCommandDlgFSUIPCControl> controls = new List<FSUIPCCommandDlgFSUIPCControl>();
            foreach (FSUIPCControl control in (FSUIPCControl[])Enum.GetValues(typeof(FSUIPCControl)))
            {
                controls.Add(new FSUIPCCommandDlgFSUIPCControl()
                {
                    Control = control,
                    Name = FIPFSUIPCCommandButton.ToString(control)
                });
            }
            controls.Sort((x, y) => x.Name.CompareTo(y.Name));
            cbFSUIPCControl.Items.AddRange(controls.ToArray());
            List<FSUIPCCommandDlgFSUIPCAutoPilotControl> autoPilotControls = new List<FSUIPCCommandDlgFSUIPCAutoPilotControl>();
            foreach (FSUIPCAutoPilotControl control in (FSUIPCAutoPilotControl[])Enum.GetValues(typeof(FSUIPCAutoPilotControl)))
            {
                autoPilotControls.Add(new FSUIPCCommandDlgFSUIPCAutoPilotControl()
                {
                    Control = control,
                    Name = FIPFSUIPCCommandButton.ToString(control)
                });
            }
            autoPilotControls.Sort((x, y) => x.Name.CompareTo(y.Name));
            cbFSUIPCAutoPilotControl.Items.AddRange(autoPilotControls.ToArray());
            List<FSUIPCCommandDlgFSUIPCAxisControl> axisControls = new List<FSUIPCCommandDlgFSUIPCAxisControl>();
            foreach (FSUIPCAxisControl control in (FSUIPCAxisControl[])Enum.GetValues(typeof(FSUIPCAxisControl)))
            {
                axisControls.Add(new FSUIPCCommandDlgFSUIPCAxisControl()
                {
                    Control = control,
                    Name = FIPFSUIPCCommandButton.ToString(control)
                });
            }
            axisControls.Sort((x, y) => x.Name.CompareTo(y.Name));
            cbFSUIPCAxisControl.Items.AddRange(axisControls.ToArray());
        }

        private int IndexOfControlSet(FIPFSUIPCControlSet controlSet)
        {
            for (int i = 0; i < cbCommandSet.Items.Count; i++)
            {
                FSUIPCCommandDlgControlSet fSUIPCCommandDlgControlSet = cbCommandSet.Items[i] as FSUIPCCommandDlgControlSet;
                if (fSUIPCCommandDlgControlSet.ControlSet == controlSet)
                {
                    return i;
                }
            }
            return -1;
        }

        private int IndexOfButtonAction(FIPFSUIPCButtonAction action)
        {
            for (int i = 0; i < cbCommandSet.Items.Count; i++)
            {
                FSUIPCCommandDlgAction fSUIPCCommandDlgAction = cbAction.Items[i] as FSUIPCCommandDlgAction;
                if (fSUIPCCommandDlgAction.Action == action)
                {
                    return i;
                }
            }
            return -1;
        }

        private int IndexOfFsControl(FsControl control)
        {
            for (int i = 0; i < cbFsControl.Items.Count; i++)
            {
                FSUIPCCommandDlgFsControl fSUIPCCommandDlgFsControl = cbFsControl.Items[i] as FSUIPCCommandDlgFsControl;
                if (fSUIPCCommandDlgFsControl.Control == control)
                {
                    return i;
                }
            }
            return -1;
        }

        private int IndexOfFSUIPCControl(FSUIPCControl control)
        {
            for (int i = 0; i < cbFSUIPCControl.Items.Count; i++)
            {
                FSUIPCCommandDlgFSUIPCControl fSUIPCCommandDlgFSUIPCControl = cbFSUIPCControl.Items[i] as FSUIPCCommandDlgFSUIPCControl;
                if (fSUIPCCommandDlgFSUIPCControl.Control == control)
                {
                    return i;
                }
            }
            return -1;
        }

        private int IndexOfFSUIPCAutoPilotControl(FSUIPCAutoPilotControl control)
        {
            for (int i = 0; i < cbFSUIPCAutoPilotControl.Items.Count; i++)
            {
                FSUIPCCommandDlgFSUIPCAutoPilotControl fSUIPCCommandDlgFSUIPCAutoPilotControl = cbFSUIPCAutoPilotControl.Items[i] as FSUIPCCommandDlgFSUIPCAutoPilotControl;
                if (fSUIPCCommandDlgFSUIPCAutoPilotControl.Control == control)
                {
                    return i;
                }
            }
            return -1;
        }

        private int IndexOfFSUIPCAxisControl(FSUIPCAxisControl control)
        {
            for (int i = 0; i < cbFSUIPCAxisControl.Items.Count; i++)
            {
                FSUIPCCommandDlgFSUIPCAxisControl fSUIPCCommandDlgFSUIPCAxisControl = cbFSUIPCAxisControl.Items[i] as FSUIPCCommandDlgFSUIPCAxisControl;
                if (fSUIPCCommandDlgFSUIPCAxisControl.Control == control)
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
            Button.Action = (cbAction.SelectedItem as FSUIPCCommandDlgAction).Action;
            Button.ControlSet = (cbCommandSet.SelectedItem as FSUIPCCommandDlgControlSet).ControlSet;
            Button.Value = Convert.ToInt32(tbValue.Text);
            Button.Control = (cbFsControl.SelectedItem as FSUIPCCommandDlgFsControl).Control;
            Button.FSUIPCControl = (cbFSUIPCControl.SelectedItem as FSUIPCCommandDlgFSUIPCControl).Control;
            Button.FSUIPCAutoPilotControl = (cbFSUIPCAutoPilotControl.SelectedItem as FSUIPCCommandDlgFSUIPCAutoPilotControl).Control;
            Button.FSUIPCAxisControl = (cbFSUIPCAxisControl.SelectedItem as FSUIPCCommandDlgFSUIPCAxisControl).Control;
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

        private void FSUIPCCommandDlg_Load(object sender, EventArgs e)
        {
            cbFsControl.SelectedIndex = Button.Control.HasValue ? IndexOfFsControl(Button.Control.Value) : 0;
            cbFSUIPCControl.SelectedIndex = Button.FSUIPCControl.HasValue ? IndexOfFSUIPCControl(Button.FSUIPCControl.Value) : 0;
            cbFSUIPCAutoPilotControl.SelectedIndex = Button.FSUIPCAutoPilotControl.HasValue ? IndexOfFSUIPCAutoPilotControl(Button.FSUIPCAutoPilotControl.Value) : 0;
            cbFSUIPCAxisControl.SelectedIndex = Button.FSUIPCAxisControl.HasValue ? IndexOfFSUIPCAxisControl(Button.FSUIPCAxisControl.Value) : 0;
            tbName.Text = Button.Label;
            _fontHolder = Button.Font;
            tbFont.Font = new Font(Button.Font.FontFamily, tbFont.Font.Size, Button.Font.Style, Button.Font.Unit, Button.Font.GdiCharSet);
            tbFont.Text = Button.Font.FontFamily.Name;
            btnFontColor.BackColor = Button.Color;
            _iconFilename = Button.IconFilename;
            pbIcon.Image = Button.Icon;
            cbReColor.Checked = Button.ReColor;
            tbValue.Text = Button.Value.ToString();
            cbAction.SelectedIndex = IndexOfButtonAction(Button.Action);
            cbCommandSet.SelectedIndex = IndexOfControlSet(Button.ControlSet);
            cbFsControl.Visible = false;
            cbFSUIPCControl.Visible = false;
            cbFSUIPCAutoPilotControl.Visible = false;
            cbFSUIPCAxisControl.Visible = false;
            switch (Button.ControlSet)
            {
                case FIPFSUIPCControlSet.FsControl:
                    cbFsControl.Visible = true;
                    break;
                case FIPFSUIPCControlSet.FsuipcControl:
                    cbFSUIPCControl.Visible = true;
                    break;
                case FIPFSUIPCControlSet.FsuipcAutoPilotControl:
                    cbFSUIPCAutoPilotControl.Visible = true;
                    break;
                case FIPFSUIPCControlSet.FsuipcAxisControl:
                    cbFSUIPCAxisControl.Visible = true;
                    break;
            }
            tbValue.Enabled = (Button.Action == FIPFSUIPCButtonAction.Set);
            btnOK.Enabled = IsCommandEnabled && !string.IsNullOrEmpty(tbName.Text);
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

        private bool IsCommandEnabled
        {
            get
            {
                FSUIPCCommandDlgControlSet controlSet = cbCommandSet.SelectedItem as FSUIPCCommandDlgControlSet;
                if (controlSet != null)
                {
                    switch (controlSet.ControlSet)
                    {
                        case FIPFSUIPCControlSet.FsControl:
                            return cbFsControl.SelectedIndex > 0;
                        case FIPFSUIPCControlSet.FsuipcControl:
                            return cbFSUIPCControl.SelectedIndex > 0;
                        case FIPFSUIPCControlSet.FsuipcAutoPilotControl:
                            return cbFSUIPCAutoPilotControl.SelectedIndex > 0;
                        case FIPFSUIPCControlSet.FsuipcAxisControl:
                            return cbFSUIPCAxisControl.SelectedIndex > 0;
                    }
                }
                return false;
            }
        }

        private void cbCommandSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            FSUIPCCommandDlgControlSet controlSet = cbCommandSet.SelectedItem as FSUIPCCommandDlgControlSet;
            cbFsControl.Visible = false;
            cbFSUIPCControl.Visible = false;
            cbFSUIPCAutoPilotControl.Visible = false;
            cbFSUIPCAxisControl.Visible = false;
            switch(controlSet.ControlSet)
            {
                case FIPFSUIPCControlSet.FsControl:
                    cbFsControl.Visible = true;
                    break;
                case FIPFSUIPCControlSet.FsuipcControl:
                    cbFSUIPCControl.Visible = true;
                    break;
                case FIPFSUIPCControlSet.FsuipcAutoPilotControl:
                    cbFSUIPCAutoPilotControl.Visible = true;
                    break;
                case FIPFSUIPCControlSet.FsuipcAxisControl:
                    cbFSUIPCAxisControl.Visible = true;
                    break;
            }
        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {
            btnOK.Enabled = IsCommandEnabled && !string.IsNullOrEmpty(tbName.Text);
        }

        private void cbFsControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbName.Text = cbFsControl.SelectedIndex > 0 ? (cbFsControl.SelectedItem as FSUIPCCommandDlgFsControl).Name : string.Empty;
            btnOK.Enabled = IsCommandEnabled && !string.IsNullOrEmpty(tbName.Text);
        }

        private void cbFSUIPCControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbName.Text = cbFSUIPCControl.SelectedIndex > 0 ? (cbFSUIPCControl.SelectedItem as FSUIPCCommandDlgFSUIPCControl).Name : string.Empty;
            btnOK.Enabled = IsCommandEnabled && !string.IsNullOrEmpty(tbName.Text);
        }

        private void cbFSUIPCAutoPilotControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbName.Text = cbFSUIPCAutoPilotControl.SelectedIndex > 0 ? (cbFSUIPCAutoPilotControl.SelectedItem as FSUIPCCommandDlgFSUIPCAutoPilotControl).Name : string.Empty;
            btnOK.Enabled = IsCommandEnabled && !string.IsNullOrEmpty(tbName.Text);
        }

        private void cbFSUIPCAxisControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbName.Text = cbFSUIPCAxisControl.SelectedIndex > 0 ? (cbFSUIPCAxisControl.SelectedItem as FSUIPCCommandDlgFSUIPCAxisControl).Name : string.Empty;
            btnOK.Enabled = IsCommandEnabled && !string.IsNullOrEmpty(tbName.Text);
        }

        private void cbAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            FSUIPCCommandDlgAction action = cbAction.SelectedItem as FSUIPCCommandDlgAction;
            tbValue.Enabled = (action.Action == FIPFSUIPCButtonAction.Set);
        }

        private void tbValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
