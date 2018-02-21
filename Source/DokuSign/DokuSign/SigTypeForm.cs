using DokuFlex.Windows.Common;
using DokuFlex.WinForms.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DokuSign
{
    public partial class SigTypeForm : Form
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            switch (SignatureIndex)
            {
                case 1 :
                    rbtnSigOnLine.Checked = true;
                    break;

                case 2:
                    rbtnSigBiometric.Checked = true;
                    break;

                default:
                    rbtnSigLocal.Checked = true;
                    break;
            }
        }

        public int SignatureIndex { get; set; }

        public SigTypeForm()
        {
            InitializeComponent();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            var tag = rbtnSigLocal.Checked == true ? rbtnSigLocal.Tag.ToString()
                : rbtnSigOnLine.Checked == true ? rbtnSigOnLine.Tag.ToString()
                : rbtnSigBiometric.Checked == true ? rbtnSigBiometric.Tag.ToString()
                : null;

            SignatureIndex = int.Parse(tag);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            using (var form = new SettingsForm())
            {
                form.ShowDialog();
            }
        }
    }
}
