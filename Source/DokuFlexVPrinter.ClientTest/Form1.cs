using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DokuFlexVPrinter.ClientTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //var txtFilename = System.IO.Path.GetTempPath;

            var fileName = "C:\\Users\\Emilio\\Documents\\Visual Studio 2013\\Projects\\DokuFlex\\DokuFlexVPrinter_TONIPC_a_ruiz_20141006_173026_6.ps";

            var processor = new Processor();
            processor.Process("DokuFlexVPrinter_TONIPC_a_ruiz_20141006_173026_6", fileName);
        }
    }
}
