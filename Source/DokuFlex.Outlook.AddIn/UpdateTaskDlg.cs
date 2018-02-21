using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DokuFlex.Outlook.AddIn
{
    public partial class UpdateTaskDlg : Form
    {
        public UpdateTaskDlg()
        {
            InitializeComponent();
        }

        public bool ShowTaskResultDlg(string taskNo, string taskUrl)
        {
            linkLabel.Text = String.IsNullOrWhiteSpace(taskNo) ? "Vínculo" : taskNo;
            urlTextBox.Text = taskUrl;

            return this.ShowDialog() == DialogResult.OK;
        }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(urlTextBox.Text);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void copyLinkButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(urlTextBox.Text);
        }
    }
}
