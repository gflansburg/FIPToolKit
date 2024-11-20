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
    public partial class AddFSUIPCCommandDlg : Form
    {
        public FIPFSUIPCCommandButton Button { get; set; }

        private class AddFSUIPCCommandDlgBreak
        {
            public KeyPressLengths BreakLength { get; set; }
            public string Name { get; set; }
        }

        private class FSUIPCCommandDlgControlSet
        {
            public FIPFSUIPCControlSet ControlSet { get; set; }
            public string Name { get; set; }
        }

        private class FSUIPCCommandDlgAction
        {
            public FIPButtonAction Action { get; set; }
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

        public AddFSUIPCCommandDlg()
        {
            InitializeComponent();
            foreach (KeyPressLengths keyPressLength in (KeyPressLengths[])Enum.GetValues(typeof(KeyPressLengths)))
            {
                cbBreak.Items.Add(new AddFSUIPCCommandDlgBreak()
                {
                    BreakLength = keyPressLength,
                    Name = Regex.Replace(keyPressLength.ToString(), "(\\B[A-Z])", " $1").Replace(" A ", " a ").Replace(" And ", " and ")
                });
            }
            foreach (FIPFSUIPCControlSet controlSet in (FIPFSUIPCControlSet[])Enum.GetValues(typeof(FIPFSUIPCControlSet)))
            {
                cbCommandSet.Items.Add(new FSUIPCCommandDlgControlSet()
                {
                    ControlSet = controlSet,
                    Name = Regex.Replace(controlSet.ToString(), "(\\B[A-Z])", " $1").Replace(" A ", " a ").Replace(" And ", " and ").Replace("Fsuipc", "FSUIPC").Replace("Fs ", "FS ")
                });
            }
            foreach (FIPButtonAction action in (FIPButtonAction[])Enum.GetValues(typeof(FIPButtonAction)))
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

        private int IndexOfBreakLength(KeyPressLengths breakLength)
        {
            for (int i = 0; i < cbBreak.Items.Count; i++)
            {
                AddFSUIPCCommandDlgBreak addFSUIPCCommandDlgBreak = cbBreak.Items[i] as AddFSUIPCCommandDlgBreak;
                if (addFSUIPCCommandDlgBreak.BreakLength == breakLength)
                {
                    return i;
                }
            }
            return -1;
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

        private int IndexOfButtonAction(FIPButtonAction action)
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
            Button.Break = ((AddFSUIPCCommandDlgBreak)cbBreak.SelectedItem).BreakLength;
            Button.Action = (cbAction.SelectedItem as FSUIPCCommandDlgAction).Action;
            Button.ControlSet = (cbCommandSet.SelectedItem as FSUIPCCommandDlgControlSet).ControlSet;
            Button.Value = tbValue.Text;
            Button.Command = (cbFsControl.SelectedItem as FSUIPCCommandDlgFsControl).Control.ToString();
            Button.SimControl = (cbFSUIPCControl.SelectedItem as FSUIPCCommandDlgFSUIPCControl).Control.ToString();
            Button.AutoPilotControl = (cbFSUIPCAutoPilotControl.SelectedItem as FSUIPCCommandDlgFSUIPCAutoPilotControl).Control.ToString();
            Button.AxisControl = (cbFSUIPCAxisControl.SelectedItem as FSUIPCCommandDlgFSUIPCAxisControl).Control.ToString();
            Button.IsDirty = true;
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void AddFSUIPCCommandDlg_Load(object sender, EventArgs e)
        {
            cbFsControl.SelectedIndex = !string.IsNullOrEmpty(Button.Command) ? IndexOfFsControl((FsControl)Enum.Parse(typeof(FsControl), Button.Command, true)) : 0;
            cbFSUIPCControl.SelectedIndex = !string.IsNullOrEmpty(Button.SimControl) ? IndexOfFSUIPCControl((FSUIPCControl)Enum.Parse(typeof(FSUIPCControl), Button.SimControl, true)) : 0;
            cbFSUIPCAutoPilotControl.SelectedIndex = !string.IsNullOrEmpty(Button.AutoPilotControl) ? IndexOfFSUIPCAutoPilotControl((FSUIPCAutoPilotControl)Enum.Parse(typeof(FSUIPCAutoPilotControl), Button.AutoPilotControl, true)) : 0;
            cbFSUIPCAxisControl.SelectedIndex = !string.IsNullOrEmpty(Button.AxisControl) ? IndexOfFSUIPCAxisControl((FSUIPCAxisControl)Enum.Parse(typeof(FSUIPCAxisControl), Button.AxisControl, true)) : 0;
            tbValue.Text = Button.Value;
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
            tbValue.Enabled = (Button.Action == FIPButtonAction.Set);
            btnOK.Enabled = IsCommandEnabled;
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
            switch (controlSet.ControlSet)
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

        private void cbFsControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnOK.Enabled = IsCommandEnabled && cbBreak.SelectedIndex != -1;
        }

        private void cbFSUIPCControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnOK.Enabled = IsCommandEnabled && cbBreak.SelectedIndex != -1;
        }

        private void cbFSUIPCAutoPilotControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnOK.Enabled = IsCommandEnabled && cbBreak.SelectedIndex != -1;
        }

        private void cbFSUIPCAxisControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnOK.Enabled = IsCommandEnabled && cbBreak.SelectedIndex != -1;
        }

        private void cbAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            FSUIPCCommandDlgAction action = cbAction.SelectedItem as FSUIPCCommandDlgAction;
            tbValue.Enabled = (action.Action == FIPButtonAction.Set);
        }

        private void tbValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void cbBreak_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnOK.Enabled = IsCommandEnabled && cbBreak.SelectedIndex != -1;
        }
    }
}
