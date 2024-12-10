using FIPToolKit.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FIPDisplayMSFS
{
    public partial class DCSWorldSettings : Form
    {
        public DCSWorldSettings()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = tbDCSBiosPath.Text;
            if (folderBrowserDialog1.ShowDialog(this) == DialogResult.OK)
            {
                tbDCSBiosPath.Text = folderBrowserDialog1.SelectedPath;
                btnOK.Enabled = !string.IsNullOrEmpty(tbFromIP.Text) && !string.IsNullOrEmpty(tbToIP.Text) && numFromPort.Value > 0 && numToPort.Value > 0 && !string.IsNullOrEmpty(tbDCSBiosPath.Text) && tbFromIP.Text.IsIPAddress() && tbToIP.Text.IsIPAddress();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            FIPDisplayMSFS.Properties.Settings.Default.DCSFromIPAddress = tbFromIP.Text;
            FIPDisplayMSFS.Properties.Settings.Default.DCSToIPAddress = tbToIP.Text;
            FIPDisplayMSFS.Properties.Settings.Default.DCSFromPort = Convert.ToUInt16(numFromPort.Value);
            FIPDisplayMSFS.Properties.Settings.Default.DCSToPort = Convert.ToUInt16(numToPort.Value);
            FIPDisplayMSFS.Properties.Settings.Default.DCSBIOSJSONLocation = tbDCSBiosPath.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void DCSWorldSettings_Load(object sender, EventArgs e)
        {
            tbFromIP.Text = FIPDisplayMSFS.Properties.Settings.Default.DCSFromIPAddress;
            tbToIP.Text = FIPDisplayMSFS.Properties.Settings.Default.DCSToIPAddress;
            numFromPort.Value = FIPDisplayMSFS.Properties.Settings.Default.DCSFromPort;
            numToPort.Value = FIPDisplayMSFS.Properties.Settings.Default.DCSToPort;
            tbDCSBiosPath.Text = string.IsNullOrEmpty(FIPDisplayMSFS.Properties.Settings.Default.DCSBIOSJSONLocation) ? Environment.ExpandEnvironmentVariables("%userprofile%\\Saved Games\\DCS\\Scripts\\DCS-BIOS\\doc\\json") : FIPDisplayMSFS.Properties.Settings.Default.DCSBIOSJSONLocation;
            btnOK.Enabled = !string.IsNullOrEmpty(tbFromIP.Text) && !string.IsNullOrEmpty(tbToIP.Text) && numFromPort.Value > 0 && numToPort.Value > 0 && !string.IsNullOrEmpty(tbDCSBiosPath.Text) && tbFromIP.Text.IsIPAddress() && tbToIP.Text.IsIPAddress();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            btnOK.Enabled = !string.IsNullOrEmpty(tbFromIP.Text) && !string.IsNullOrEmpty(tbToIP.Text) && numFromPort.Value > 0 && numToPort.Value > 0 && !string.IsNullOrEmpty(tbDCSBiosPath.Text) && tbFromIP.Text.IsIPAddress() && tbToIP.Text.IsIPAddress();
        }

        private void tbFromIP_TextChanged(object sender, EventArgs e)
        {
            btnOK.Enabled = !string.IsNullOrEmpty(tbFromIP.Text) && !string.IsNullOrEmpty(tbToIP.Text) && numFromPort.Value > 0 && numToPort.Value > 0 && !string.IsNullOrEmpty(tbDCSBiosPath.Text) && tbFromIP.Text.IsIPAddress() && tbToIP.Text.IsIPAddress();
        }

        private void numFromPort_ValueChanged(object sender, EventArgs e)
        {
            btnOK.Enabled = !string.IsNullOrEmpty(tbFromIP.Text) && !string.IsNullOrEmpty(tbToIP.Text) && numFromPort.Value > 0 && numToPort.Value > 0 && !string.IsNullOrEmpty(tbDCSBiosPath.Text) && tbFromIP.Text.IsIPAddress() && tbToIP.Text.IsIPAddress();
        }

        private void tbToIP_TextChanged(object sender, EventArgs e)
        {
            btnOK.Enabled = !string.IsNullOrEmpty(tbFromIP.Text) && !string.IsNullOrEmpty(tbToIP.Text) && numFromPort.Value > 0 && numToPort.Value > 0 && !string.IsNullOrEmpty(tbDCSBiosPath.Text) && tbFromIP.Text.IsIPAddress() && tbToIP.Text.IsIPAddress();
        }

        private void numToPort_ValueChanged(object sender, EventArgs e)
        {
            btnOK.Enabled = !string.IsNullOrEmpty(tbFromIP.Text) && !string.IsNullOrEmpty(tbToIP.Text) && numFromPort.Value > 0 && numToPort.Value > 0 && !string.IsNullOrEmpty(tbDCSBiosPath.Text) && tbFromIP.Text.IsIPAddress() && tbToIP.Text.IsIPAddress();
        }

        private void lnkHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SpotifyAPI.Web.Auth.AuthUtil.OpenBrowser("https://github.com/DCS-Skunkworks/DCSFlightpanels/wiki/Installation");
        }
    }
}
