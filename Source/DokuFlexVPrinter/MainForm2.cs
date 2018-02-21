using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DokuFlexVPrinter
{
    public partial class MainForm2 : Form
    {
        private string psFilename;
        private string outputPdfFile;
        private string dirName;

        public MainForm2()
        {
            InitializeComponent();
        }

        public MainForm2(string psFilename, string outputPdfFile, string dirName)
        {
            InitializeComponent();

            // TODO: Complete member initialization
            this.psFilename = psFilename;
            this.outputPdfFile = outputPdfFile;
            this.dirName = dirName;
        }

        private void cbxSignatureType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbxSignatureType.SelectedIndex)
            {
                case 0 :
                    sigLocalUCtrl1.BringToFront();
                    sigLocalUCtrl1.OpenFiles(psFilename, outputPdfFile, dirName);
                    break;

                case 1 :
                    sigPlusUCtrl1.BringToFront();
                    break;

                default:
                    break;
            }
        }
    }
}
