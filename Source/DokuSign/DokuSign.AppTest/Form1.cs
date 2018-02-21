using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PrinterPlusPlusSDK;
using System.IO;

namespace DokuSign.AppTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //string windowsFolder = Environment.GetFolderPath(Environment.SpecialFolder.Programs);
            //MessageBox.Show(windowsFolder);
            //string driverPath = Path.Combine(windowsFolder, "spool", "drivers", "w32x86", "PSCRIPT5.DLL");
            //MessageBox.Show(driverPath);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var fileName = String.Empty;

            using (var openDlg = new OpenFileDialog())
            {
                openDlg.Filter = "PS|*.ps";

                if (openDlg.ShowDialog() != DialogResult.Cancel)
                {
                    fileName = openDlg.FileName;
                }
            }

            var processor = new Processor();
            processor.Process("ASadf", fileName);
            //processor.Process();
        }
    }
}
