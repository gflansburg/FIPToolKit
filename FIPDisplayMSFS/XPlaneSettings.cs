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
    public partial class XPlaneSettings : Form
    {
        public XPlaneSettings()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.XPlaneIPAddress = tbIPAddress.Text;
            Properties.Settings.Default.XPlanePort = Convert.ToUInt16(numPort.Value);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void XPlaneSettings_Load(object sender, EventArgs e)
        {
            tbIPAddress.Text = Properties.Settings.Default.XPlaneIPAddress;
            numPort.Value = Properties.Settings.Default.XPlanePort;
            btnOK.Enabled = !string.IsNullOrEmpty(tbIPAddress.Text) && numPort.Value > 0 && tbIPAddress.Text.IsIPAddress();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            btnOK.Enabled = !string.IsNullOrEmpty(tbIPAddress.Text) && numPort.Value > 0 && tbIPAddress.Text.IsIPAddress();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            btnOK.Enabled = !string.IsNullOrEmpty(tbIPAddress.Text) && numPort.Value > 0 && tbIPAddress.Text.IsIPAddress();
        }
    }
}
