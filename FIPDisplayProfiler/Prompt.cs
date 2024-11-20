using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FIPDisplayProfiler
{
    public static class Prompt
    {
        public static string ShowDialog(string text, string label, string caption, int width = 500)
        {
            Form prompt = new Form()
            {
                Width = width,
                Height = 130,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label() { Left = 10, Top = 10, Text = label };
            TextBox textBox = new TextBox() { Left = 10, Top = 30, Width = width - 38, Text = text };
            Button cancel = new Button() { Text = "&Cancel", Left = width - 233, Width = 100, Top = 55, DialogResult = DialogResult.Cancel };
            cancel.Click += (sender, e) => { prompt.Close(); };
            Button confirmation = new Button() { Text = "&Ok", Left = width - 128, Width = 100, Top = 55, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(cancel);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }

        public static int ShowDialog(int value, string label, string caption, int width = 300)
        {
            Form prompt = new Form()
            {
                Width = width,
                Height = 130,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label() { Left = 10, Top = 10, Text = label };
            NumericUpDown textBox = new NumericUpDown() { Left = 10, Top = 30, Width = width - 38, Minimum = 1, Maximum = ushort.MaxValue, Value = value };
            Button cancel = new Button() { Text = "&Cancel", Left = width - 233, Width = 100, Top = 55, DialogResult = DialogResult.Cancel };
            cancel.Click += (sender, e) => { prompt.Close(); };
            Button confirmation = new Button() { Text = "Ok", Left = width - 128, Width = 100, Top = 55, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(cancel);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? value : value;
        }
    }
}
