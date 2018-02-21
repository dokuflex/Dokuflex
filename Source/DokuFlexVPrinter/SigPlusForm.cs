using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DokuFlexVPrinter
{
    using DokuFlex.Windows.Common;

    public partial class SigPlusForm : Form
    {
        private string sigFileName;

        public string SelectedValue
        {
            get
            {
                return sigFileName;
            }
        }

        public SigPlusForm()
        {
            InitializeComponent();

            sigFileName = String.Empty;
        }

        private void cmdSign_Click(object sender, EventArgs e)
        {
            sigPlusNET1.SetTabletState(1);
        }

        private void cmdStop_Click(object sender, EventArgs e)
        {
            sigPlusNET1.SetTabletState(0);
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            sigPlusNET1.ClearTablet();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdSaveImage_Click(object sender, EventArgs e)
        {
            Image sigImage = null;

            string fileName = String.Format("{0}.png", DateTime.Now.ToString("s").Replace(":", String.Empty));

            sigFileName = String.Format("{0}\\{1}", System.IO.Path.GetTempPath(), fileName);

            //this sub shows how to create an IMAGE of your signature
            //it is important to note that an image contains no biometric info,
            //nor can be encrypted. If your goal is to store a biometric signature,
            //please look into storing either the SigString or a SIG file

            sigPlusNET1.SetImageXSize(500);
            sigPlusNET1.SetImageYSize(150);
            sigPlusNET1.SetJustifyMode(5);

            sigImage = sigPlusNET1.GetSigImage();
            sigImage.Save(sigFileName, System.Drawing.Imaging.ImageFormat.Png);

            sigPlusNET1.SetJustifyMode(0);
            //sigPlusNET1.SetTabletState(0);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cmdSaveSig_Click(object sender, EventArgs e)
        {
            //this sub shows how to save an encrypted, biometric signature
            //the AutoKey() methods below are used to perform the encryption process.
            //use SetAutoKeyData() to pass in the data you wish the signature to be
            //encrypted using. Later, to retrieve the signature, the data passed in
            //must match or the signature will not be returned. This way, you can
            //insure that the signature's context is that data, and that data alone.
            //Topaz recommends you mimic a "pen and paper" application in your choice
            //of data used for encryption. What is it the signer is reading and agreeing to?
            //this is typically the choice for encryption data
            //compression is used to make the size of the signature smaller, taking up less
            //storage space

            SaveFileDialog fd;
            fd = new SaveFileDialog();

            fd.Filter = "SIG Files (*.sig)|*.sig";
            fd.FilterIndex = 1;
            fd.ShowDialog();

            sigPlusNET1.SetSigCompressionMode(0);
            sigPlusNET1.SetEncryptionMode(0);
            sigPlusNET1.SetKeyString("0000000000000000");

            if (fd.FileName != "")
            {
                if (cboEncryption.Text != "0")
                {
                    sigPlusNET1.AutoKeyStart();
                    sigPlusNET1.SetAutoKeyData("the signature will be encrypted to this data");
                    sigPlusNET1.SetAutoKeyData("I can add as much as I like");
                    sigPlusNET1.AutoKeyFinish();
                }
                else
                {
                    //do not pass in encryption data
                }

                sigPlusNET1.SetEncryptionMode(System.Convert.ToInt32(cboEncryption.Text));
                sigPlusNET1.SetSigCompressionMode(System.Convert.ToInt32(cboCompression.Text));
                sigPlusNET1.ExportSigFile(fd.FileName);
            }
        }

        private void cmdLoadSig_Click(object sender, EventArgs e)
        {
            //this sub shows how to return an encrypted, biometric signature
            //if the data passed into SetAutoKeyData() matches the data
            //used when the signature was originally encrypted, the signature
            //will be returned and automatically rendered

            OpenFileDialog fd;
            fd = new OpenFileDialog();

            fd.Filter = "SIG Files (*.sig)|*.sig";
            fd.FilterIndex = 1;
            fd.ShowDialog();


            if (fd.FileName != "")
            {

                sigPlusNET1.SetSigCompressionMode(0);
                sigPlusNET1.SetEncryptionMode(0);
                sigPlusNET1.SetKeyString("0000000000000000");

                if (cboEncryption.Text != "0")
                {
                    sigPlusNET1.AutoKeyStart();
                    sigPlusNET1.SetAutoKeyData("the signature will be encrypted to this data");
                    sigPlusNET1.SetAutoKeyData("I can add as much as I like");
                    sigPlusNET1.AutoKeyFinish();
                }
                else
                {
                    //do not pass in encryption data
                }

                sigPlusNET1.SetEncryptionMode(System.Convert.ToInt32(cboEncryption.Text));
                sigPlusNET1.SetSigCompressionMode(System.Convert.ToInt32(cboCompression.Text));

                try
                {
                    sigPlusNET1.ImportSigFile(fd.FileName);
                }
                catch
                {
                    MessageBox.Show("Please check your SigCompressionMode and EncryptionMode settings.");
                }

                //************************************************************
                //alternately, you can return a SigString
                //ideal for database storage, as in:

                //sigPlusNET1.SetSigString(strMySig);

                //store strMySig wherever you wish, as an ASCII hex string
                //************************************************************

                if (sigPlusNET1.NumberOfTabletPoints() > 0)
                {
                    //the signature was successfully returned!
                }
            }
        }

        private void cboPenWidth_SelectedIndexChanged(object sender, EventArgs e)
        {
            //set the thickness of the ink in pixels
            sigPlusNET1.SetImagePenWidth(System.Convert.ToInt32(cboPenWidth.Text));
            sigPlusNET1.SetDisplayPenWidth(System.Convert.ToInt32(cboPenWidth.Text));
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            sigPlusNET1.SetTabletState(0);
        }
    }
}
