﻿using FIPToolKit.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FIPDisplayProfiler
{
    public partial class ScreenMirrorForm : Form
    {
        public FIPScreenMirror Page { get; set; }
        
        private Font _fontHolder;

        public ScreenMirrorForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Page.Font = _fontHolder;
            Page.FontColor = btnFontColor.BackColor;
            Page.IsDirty = true;
            Page.ScreenIndex = cbScreen.SelectedIndex;
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
            }
        }

        private void ScreenMirrorForm_Load(object sender, EventArgs e)
        {
            _fontHolder = Page.Font;
            tbFont.Font = new Font(Page.Font.FontFamily, tbFont.Font.Size, Page.Font.Style, Page.Font.Unit, Page.Font.GdiCharSet);
            tbFont.Text = Page.Font.FontFamily.Name;
            btnFontColor.BackColor = Page.FontColor;
            foreach (var screen in Screen.AllScreens)
            {
                cbScreen.Items.Add(screen.DeviceName);
            }
            cbScreen.SelectedIndex = Page.ScreenIndex;
        }
    }
}