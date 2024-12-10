using DCS_BIOS;
using DCS_BIOS.ControlLocator;
using DCS_BIOS.Json;
using DCS_BIOS.misc;
using DCS_BIOS.Serialized;
using FIPToolKit.FlightSim;
using FIPToolKit.Models;
using FIPToolKit.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FIPDisplayProfiler
{
    public partial class AddDCSWorldCommandDlg : Form
    {
        public FIPDCSWorldCommandButton Button { get; set; }

        private IEnumerable<DCSBIOSControl> dcsbiosControls;
        private List<DCSBIOSInputInterface> dcsbiosInputInterfaces = new List<DCSBIOSInputInterface>();

        private class DCSWorldCommandDlgBreak
        {
            public KeyPressLengths BreakLength { get; set; }
            public string Name { get; set; }
        }

        private class DCSWorldCommandDlgInputType
        {
            public DCSBIOSInputInterface Interface { get; set; }
            public string Name { get; set; }
        }

        private class DCSWorldCommandDlgAction
        {
            public FIPButtonAction Action { get; set; }
            public string Name { get; set; }
        }


        public AddDCSWorldCommandDlg()
        {
            InitializeComponent();
            foreach (KeyPressLengths keyPressLength in (KeyPressLengths[])Enum.GetValues(typeof(KeyPressLengths)))
            {
                cbBreak.Items.Add(new DCSWorldCommandDlgBreak()
                {
                    BreakLength = keyPressLength,
                    Name = Regex.Replace(keyPressLength.ToString(), "(\\B[A-Z])", " $1").Replace(" A ", " a ").Replace(" And ", " and ")
                });
            }
            LoadFlightModels();
        }

        private void LoadFlightModels()
        {
            foreach (var module in DCSAircraft.Modules)
            {
                if (!DCSAircraft.IsNoFrameLoadedYet(module) && module.ID < DCSBIOSConstants.META_MODULE_ID_START_RANGE) //!DCSAircraft.IsNS430(module) &&  
                {
                    cbFlightModel.Items.Add(module);
                }
            }
            cbFlightModel.Items.Insert(0, new DCSAircraft(0, "--Select Aircraft--", string.Empty, DCSBIOSLocation.None));
        }

        private int IndexOfBreakLength(KeyPressLengths breakLength)
        {
            for (int i = 0; i < cbBreak.Items.Count; i++)
            {
                DCSWorldCommandDlgBreak addFSUIPCCommandDlgBreak = cbBreak.Items[i] as DCSWorldCommandDlgBreak;
                if (addFSUIPCCommandDlgBreak.BreakLength == breakLength)
                {
                    return i;
                }
            }
            return -1;
        }

        private int IndexOfFlightModel(int id)
        {
            DCSAircraft aircraft = cbFlightModel.Items.Cast<DCSAircraft>().FirstOrDefault(a => a.ID == id);
            if (aircraft != null)
            {
                return cbFlightModel.Items.IndexOf(aircraft);
            }
            return 0;
        }

        private int IndexOfInputType(DCSBIOSInputType inputType)
        {
            for (int i = 0; i < cbInputType.Items.Count; i++)
            {
                DCSWorldCommandDlgInputType dcsWorldCommandDlgInputType = cbInputType.Items[i] as DCSWorldCommandDlgInputType;
                if (dcsWorldCommandDlgInputType.Interface.Interface == inputType)
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
                for (int i = 0; i < cbSetValue.Items.Count; i++)
                {
                    DCSBIOSControl control = cbSetValue.Items[i] as DCSBIOSControl;
                    if (control.Identifier.Equals(command, StringComparison.OrdinalIgnoreCase))
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Button.Break = ((DCSWorldCommandDlgBreak)cbBreak.SelectedItem).BreakLength;
            Button.FlightModel = (cbFlightModel.SelectedItem as DCSAircraft).ID;
            Button.Command = (cbSetValue.SelectedItem as DCSBIOSControl).Identifier;
            Button.DCSAction = (cbInputType.SelectedItem as DCSWorldCommandDlgInputType).Interface.Interface;
            Button.VariableChangeValue = (cbInputType.SelectedItem as DCSWorldCommandDlgInputType).Interface.SuggestedStep;
            switch (Button.DCSAction)
            {
                case DCSBIOSInputType.FIXED_STEP:
                    //INC / DEC
                    Button.Value = cbInputValueFixedStep.SelectedItem.ToString();
                    break;
                case DCSBIOSInputType.ACTION:
                    //TOGGLE
                    Button.Value = cbInputValueAction.SelectedItem.ToString();
                    break;
                case DCSBIOSInputType.SET_STATE:
                    //INTEGER
                    Button.Value = tbValue.Text;
                    break;
                case DCSBIOSInputType.VARIABLE_STEP:
                    //INTEGER
                    Button.Value = tbValue.Text;
                    break;
                case DCSBIOSInputType.SET_STRING:
                    //STRING
                    Button.Value = tbValue.Text;
                    break;
            }
            Button.IsDirty = true;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void AddDCSWorldCommandDlg_Load(object sender, EventArgs e)
        {
            cbBreak.SelectedIndex = IndexOfBreakLength(Button.Break);
            cbFlightModel.SelectedIndex = IndexOfFlightModel(Button.FlightModel);
            cbSetValue.SelectedIndex = IndexOfSetValue(Button.Command);
            cbInputType.SelectedIndex = IndexOfInputType(Button.DCSAction);
            switch(Button.DCSAction)
            {
                case DCSBIOSInputType.FIXED_STEP:
                    //INC / DEC
                    cbInputValueFixedStep.SelectedItem = Button.FixedStepArgument;
                    break;
                case DCSBIOSInputType.ACTION:
                    //TOGGLE
                    cbInputValueAction.SelectedItem = Button.ActionArgument;
                    break;
                case DCSBIOSInputType.SET_STATE:
                    //INTEGER
                    tbValue.Text = Button.Value;
                    break;
                case DCSBIOSInputType.VARIABLE_STEP:
                    //INTEGER
                    tbValue.Text = Button.Value;
                    break;
                case DCSBIOSInputType.SET_STRING:
                    //STRING
                    tbValue.Text = Button.Value;
                    break;
            }
            if (cbSetValue.SelectedIndex > 0)
            {
                LoadInputTypes(cbSetValue.SelectedItem as DCSBIOSControl);
                lblSetValueDescription.Text = (cbSetValue.SelectedItem as DCSBIOSControl).Description;
                lblValueUnitsDescripiton.Text = string.Format("Max Value\r\n{0}", GetMaxValueForInterface((cbInputType.SelectedItem as DCSWorldCommandDlgInputType).Interface.Interface).ToString());
            }
            SetVisibility();
        }

        private bool IsCommandEnabled
        {
            get
            {
                return cbFlightModel.SelectedIndex > 0 && cbSetValue.SelectedIndex > 0;
            }
        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {
            SetVisibility();
        }

        private void cbSetValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSetValue.SelectedIndex > 0)
            {
                LoadInputTypes(cbSetValue.SelectedItem as DCSBIOSControl);
                lblSetValueDescription.Text = (cbSetValue.SelectedItem as DCSBIOSControl).Description;
                lblValueUnitsDescripiton.Text = string.Format("Max Value\r\n{0}", GetMaxValueForInterface((cbInputType.SelectedItem as DCSWorldCommandDlgInputType).Interface.Interface).ToString());
            }
            else
            {
                lblSetValueDescription.Text = string.Empty;
                lblValueUnitsDescripiton.Text = string.Empty;
            }
            SetVisibility();
        }

        private void LoadInputTypes(DCSBIOSControl dcsbiosControl)
        {
            try
            {
                cbInputType.Items.Clear();
                foreach (var dcsbiosControlInput in dcsbiosControl.Inputs)
                {
                    DCSBIOSInputInterface inputInterface = new DCSBIOSInputInterface();
                    inputInterface.Consume(dcsbiosControl.Identifier, dcsbiosControlInput);
                    dcsbiosInputInterfaces.Add(inputInterface);
                    cbInputType.Items.Add(new DCSWorldCommandDlgInputType()
                    {
                        Interface = inputInterface,
                        Name = inputInterface.Interface.ToString().ToTitleCase()
                    });
                }
                cbInputType.SelectedIndex = 0;
            }
            catch (Exception)
            {
            }
        }

        public int GetMaxValueForInterface(DCSBIOSInputType dcsbiosInputType)
        {
            if (dcsbiosInputType == DCSBIOSInputType.SET_STRING)
            {
                return 0;
            }
            var searched = dcsbiosInputInterfaces.FirstOrDefault(x => x.Interface == dcsbiosInputType);
            return searched?.MaxValue ?? 0;
        }

        private void tbValue_TextChanged(object sender, EventArgs e)
        {
            SetVisibility();
        }

        private void tbValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((cbInputType.SelectedItem as DCSWorldCommandDlgInputType).Interface.Interface != DCSBIOSInputType.SET_STRING)
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void cbFlightModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSubList();
            SetVisibility();
        }

        private void LoadSubList()
        {
            cbSetValue.Items.Clear();
            if (cbFlightModel.SelectedIndex > 0)
            {
                DCSBIOSControlLocator.DCSAircraft = cbFlightModel.SelectedItem as DCSAircraft;
                SetEmulationModeFlag(DCSBIOSControlLocator.DCSAircraft);
                dcsbiosControls = DCSBIOSControlLocator.GetInputControls();
                if (dcsbiosControls != null)
                {
                    List<DCSBIOSControl> controls = dcsbiosControls.ToList();
                    controls.Sort((x, y) => x.Identifier.CompareTo(y.Identifier));
                    cbSetValue.Items.AddRange(controls.ToArray());
                }
            }
            cbSetValue.Items.Insert(0, new DCSBIOSControl() { Identifier = "--Select Command--" });
            cbSetValue.SelectedIndex = 0;
        }

        private void SetEmulationModeFlag(DCSAircraft aircraft)
        {
            if (DCSAircraft.IsNoFrameLoadedYet(aircraft))
            {
                Common.SetEmulationModes(EmulationMode.DCSBIOSInputEnabled | EmulationMode.DCSBIOSOutputEnabled);
            }
            else if (DCSAircraft.IsKeyEmulator(aircraft))
            {
                Common.SetEmulationModes(EmulationMode.KeyboardEmulationOnly);
            }
            else if (DCSAircraft.IsKeyEmulatorSRS(aircraft))
            {
                Common.SetEmulationModes(EmulationMode.KeyboardEmulationOnly);
                Common.SetEmulationModes(EmulationMode.SRSEnabled);
            }
            else if (DCSAircraft.IsFlamingCliff(aircraft))
            {
                Common.SetEmulationModes(EmulationMode.SRSEnabled); //???
                Common.SetEmulationModes(EmulationMode.DCSBIOSOutputEnabled);
            }
            else
            {
                Common.SetEmulationModes(EmulationMode.DCSBIOSOutputEnabled | EmulationMode.DCSBIOSInputEnabled);
            }
        }

        private void SetVisibility(DCSBIOSInputType dcsbiosInputType)
        {
            switch (dcsbiosInputType)
            {
                case DCSBIOSInputType.FIXED_STEP:
                    {
                        //INC / DEC
                        cbInputValueFixedStep.Visible = true;
                        cbInputValueAction.Visible = false;
                        tbValue.Visible = false;
                        lblValueUnitsDescripiton.Visible = false;
                        cbInputValueFixedStep.SelectedIndex = 0;
                        break;
                    }
                case DCSBIOSInputType.ACTION:
                    {
                        //TOGGLE
                        cbInputValueFixedStep.Visible = false;
                        cbInputValueAction.Visible = true;
                        tbValue.Visible = false;
                        lblValueUnitsDescripiton.Visible = false;
                        cbInputValueAction.SelectedIndex = 0;
                        break;
                    }
                case DCSBIOSInputType.SET_STATE:
                    {
                        //INTEGER
                        cbInputValueFixedStep.Visible = false;
                        cbInputValueAction.Visible = false;
                        tbValue.Visible = true;
                        lblValueUnitsDescripiton.Visible = true;
                        break;
                    }
                case DCSBIOSInputType.VARIABLE_STEP:
                    {
                        //INTEGER
                        cbInputValueFixedStep.Visible = false;
                        cbInputValueAction.Visible = false;
                        tbValue.Visible = true;
                        lblValueUnitsDescripiton.Visible = true;
                        break;
                    }
                case DCSBIOSInputType.SET_STRING:
                    {
                        //STRING
                        cbInputValueFixedStep.Visible = false;
                        cbInputValueAction.Visible = false;
                        tbValue.Visible = true;
                        lblValueUnitsDescripiton.Visible = false;
                        break;
                    }
            }
        }

        private void SetVisibility()
        {
            tbValue.Enabled = cbInputType.SelectedIndex != -1 && cbSetValue.SelectedIndex > 0 && cbFlightModel.SelectedIndex > 0;
            cbInputValueAction.Enabled = cbInputType.SelectedIndex != -1 && cbSetValue.SelectedIndex > 0 && cbFlightModel.SelectedIndex > 0;
            cbInputValueFixedStep.Enabled = cbInputType.SelectedIndex != -1 && cbSetValue.SelectedIndex > 0 && cbFlightModel.SelectedIndex > 0;
            btnOK.Enabled = IsCommandEnabled && cbBreak.SelectedIndex != -1;
            cbInputType.Enabled = cbSetValue.SelectedIndex > 0 && cbFlightModel.SelectedIndex > 0;
            cbSetValue.Enabled = cbFlightModel.SelectedIndex > 0;
            if (cbInputType.SelectedIndex != -1)
            {
                SetVisibility((cbInputType.SelectedItem as DCSWorldCommandDlgInputType).Interface.Interface);
            }
        }

        private void cbInputType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetVisibility();
        }
    }
}
