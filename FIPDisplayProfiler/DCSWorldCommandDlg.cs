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
    public partial class DCSWorldCommandDlg : Form
    {
        public FIPDCSWorldCommandButton Button { get; set; }

        private Font _fontHolder;
        private string _iconFilename;
        private IEnumerable<DCSBIOSControl> dcsbiosControls;
        private List<DCSBIOSInputInterface> dcsbiosInputInterfaces = new List<DCSBIOSInputInterface>();

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


        public DCSWorldCommandDlg()
        {
            InitializeComponent();
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
            Button.Label = tbName.Text;
            Button.Font = _fontHolder;
            Button.Color = btnFontColor.BackColor;
            Button.IconFilename = _iconFilename;
            Button.ReColor = cbReColor.Checked;
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

        private void DCSWorldCommandDlg_Load(object sender, EventArgs e)
        {
            cbFlightModel.SelectedIndex = IndexOfFlightModel(Button.FlightModel);
            cbSetValue.SelectedIndex = IndexOfSetValue(Button.Command);
            if (cbSetValue.SelectedIndex > 0)
            {
                LoadInputTypes(cbSetValue.SelectedItem as DCSBIOSControl);
                lblSetValueDescription.Text = (cbSetValue.SelectedItem as DCSBIOSControl).Description;
                lblValueUnitsDescripiton.Text = string.Format("Max Value\r\n{0}", GetMaxValueForInterface((cbInputType.SelectedItem as DCSWorldCommandDlgInputType).Interface.Interface).ToString());
            }
            cbInputType.SelectedIndex = IndexOfInputType(Button.DCSAction);
            cbInputValueAction.SelectedIndex = 0;
            cbInputValueFixedStep.SelectedIndex = Button.FixedStepArgument.Equals("DEC") ? 1 : 0;
            switch (Button.DCSAction)
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
            tbName.Text = Button.Label;
            _fontHolder = Button.Font;
            tbFont.Font = new Font(Button.Font.FontFamily, tbFont.Font.Size, Button.Font.Style, Button.Font.Unit, Button.Font.GdiCharSet);
            tbFont.Text = Button.Font.FontFamily.Name;
            btnFontColor.BackColor = Button.Color;
            _iconFilename = Button.IconFilename;
            pbIcon.Image = Button.Icon;
            cbReColor.Checked = Button.ReColor;
            SetVisibility();
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
                return cbFlightModel.SelectedIndex > 0 && cbSetValue.SelectedIndex > 0;
            }
        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {
            SetVisibility();
        }

        private void cbSetValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbName.Text = cbSetValue.SelectedIndex > 0 ? (cbSetValue.SelectedItem as DCSBIOSControl).Description : string.Empty;
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
            btnOK.Enabled = IsCommandEnabled && !string.IsNullOrEmpty(tbName.Text);
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
