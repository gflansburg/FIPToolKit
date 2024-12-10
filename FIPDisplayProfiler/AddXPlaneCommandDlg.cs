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
using XPlaneConnect;
using static XPlaneConnect.Commands;

namespace FIPDisplayProfiler
{
    public partial class AddXPlaneCommandDlg : Form
    {
        public FIPXPlaneCommandButton Button { get; set; }

        private class AddXPlaneCommandDlgBreak
        {
            public KeyPressLengths BreakLength { get; set; }
            public string Name { get; set; }
        }

        public AddXPlaneCommandDlg()
        {
            InitializeComponent();
            foreach (KeyPressLengths keyPressLength in (KeyPressLengths[])Enum.GetValues(typeof(KeyPressLengths)))
            {
                cbBreak.Items.Add(new AddXPlaneCommandDlgBreak()
                {
                    BreakLength = keyPressLength,
                    Name = Regex.Replace(keyPressLength.ToString(), "(\\B[A-Z])", " $1").Replace(" A ", " a ").Replace(" And ", " and ")
                });
            }
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

        private int IndexOfBreakLength(KeyPressLengths breakLength)
        {
            for (int i = 0; i < cbBreak.Items.Count; i++)
            {
                AddXPlaneCommandDlgBreak addXPlaneCommandDlgBreak = cbBreak.Items[i] as AddXPlaneCommandDlgBreak;
                if (addXPlaneCommandDlgBreak.BreakLength == breakLength)
                {
                    return i;
                }
            }
            return -1;
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
            Button.Break = ((AddXPlaneCommandDlgBreak)cbBreak.SelectedItem).BreakLength;
            Button.Action = (cbAction.SelectedItem as CommandDlgAction).Action;
            Button.Command = Button.Action == FIPButtonAction.Set ? ((DataRefElement)cbSetValue.SelectedItem).Id.ToString() : ((XPlaneCommand)cbCommand.SelectedItem).Id.ToString();
            Button.Value = (Button.Action == FIPButtonAction.Set ? tbValue.Text : string.Empty);
            Button.IsDirty = true;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void AddXPlaneCommandDlg_Load(object sender, EventArgs e)
        {
            cbBreak.SelectedIndex = IndexOfBreakLength(Button.Break);
            cbAction.SelectedIndex = IndexOfButtonAction(Button.Action);
            switch (Button.Action)
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
            btnOK.Enabled = IsCommandEnabled;
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

        private void cbCommand_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnOK.Enabled = IsCommandEnabled && cbBreak.SelectedIndex != -1;
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

        private void tbValue_TextChanged(object sender, EventArgs e)
        {
            btnOK.Enabled = IsCommandEnabled && cbBreak.SelectedIndex != -1;
        }

        private void cbSetValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnOK.Enabled = IsCommandEnabled && cbBreak.SelectedIndex != -1;
            lblSetValueDescription.Text = (cbSetValue.SelectedItem as DataRefElement).Description;
            lblValueUnitsDescripiton.Text = (cbSetValue.SelectedItem as DataRefElement).Units;
        }

        private void cbBreak_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnOK.Enabled = IsCommandEnabled && cbBreak.SelectedIndex != -1;
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
