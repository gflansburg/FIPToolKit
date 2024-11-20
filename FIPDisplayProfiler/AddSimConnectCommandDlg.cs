using FIPToolKit.FlightSim;
using FIPToolKit.Models;
using FIPToolKit.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FIPDisplayProfiler
{
    public partial class AddSimConnectCommandDlg : Form
    {
        public FIPSimConnectCommandButton Button { get; set; }

        private class AddSimConnectCommandDlgBreak
        {
            public KeyPressLengths BreakLength { get; set; }
            public string Name { get; set; }
        }

        public AddSimConnectCommandDlg()
        {
            InitializeComponent();
            foreach (KeyPressLengths keyPressLength in (KeyPressLengths[])Enum.GetValues(typeof(KeyPressLengths)))
            {
                cbBreak.Items.Add(new AddSimConnectCommandDlgBreak()
                {
                    BreakLength = keyPressLength,
                    Name = Regex.Replace(keyPressLength.ToString(), "(\\B[A-Z])", " $1").Replace(" A ", " a ").Replace(" And ", " and ")
                });
            }
            List<SimConnectEvent> events = SimConnectEvents.Instance.Events.Values.ToList();
            events.Sort((x, y) => x.Name.CompareTo(y.Name));
            cbSetValue.Items.AddRange(events.ToArray());
            cbSetValue.Items.Insert(0, new SimConnectEvent() { Name = "--Select Attribute To Write--" });
        }

        private int IndexOfBreakLength(KeyPressLengths breakLength)
        {
            for (int i = 0; i < cbBreak.Items.Count; i++)
            {
                AddSimConnectCommandDlgBreak addSimConnectCommandDlgBreak = cbBreak.Items[i] as AddSimConnectCommandDlgBreak;
                if (addSimConnectCommandDlgBreak.BreakLength == breakLength)
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
            Button.Break = ((AddSimConnectCommandDlgBreak)cbBreak.SelectedItem).BreakLength;
            Button.Action = FIPButtonAction.Set;
            Button.Command = ((SimConnectEvent)cbSetValue.SelectedItem).Id.ToString();
            Button.Value = tbValue.Text;
            Button.IsDirty = true;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void AddSimConnectCommandDlg_Load(object sender, EventArgs e)
        {
            cbSetValue.SelectedIndex = IndexOfSetValue(Button.Command);
            tbValue.Text = Button.Value;
            lblSetValueDescription.Text = (cbSetValue.SelectedItem as SimConnectEvent).Description;
            lblValueUnitsDescripiton.Text = (cbSetValue.SelectedItem as SimConnectEvent).Units;
            btnOK.Enabled = IsCommandEnabled;
        }

        private bool IsCommandEnabled
        {
            get
            {
                return cbSetValue.SelectedIndex > 0 && !string.IsNullOrEmpty(tbValue.Text);
            }
        }

        private void tbValue_TextChanged(object sender, EventArgs e)
        {
            btnOK.Enabled = IsCommandEnabled && cbBreak.SelectedIndex != -1;
        }

        private void cbSetValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnOK.Enabled = IsCommandEnabled && cbBreak.SelectedIndex != -1;
            lblSetValueDescription.Text = (cbSetValue.SelectedItem as SimConnectEvent).Description;
            lblValueUnitsDescripiton.Text = (cbSetValue.SelectedItem as SimConnectEvent).Units;
        }

        private void cbBreak_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnOK.Enabled = IsCommandEnabled && cbBreak.SelectedIndex != -1;
        }

        private void tbValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
