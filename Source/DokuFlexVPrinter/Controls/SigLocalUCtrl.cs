using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;
using iTextSharp.text.pdf;
using Org.Eurekaa.PDF.iSafePDF;
using System.IO;
using System.Drawing.Imaging;

namespace DokuFlexVPrinter.Controls
{
    public partial class SigLocalUCtrl : UserControl
    {
        private X509Certificate2Collection _certCollection;
        private X509Certificate2 _certificateData = null;
        private PDFEncryption PDFEnc = new PDFEncryption();
        private string psFileName;
        private string outputPdfFile;
        private string dirName;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            X509Store store = null;

            try
            {
                store = new X509Store(StoreName.My);
                store.Open(OpenFlags.ReadOnly);
                _certCollection = store.Certificates;
            }
            finally
            {
                if (store != null)
                    store.Close();
            }

            if (_certCollection.Count > 0)
            {
                for (int i = 0; i < _certCollection.Count; i++)
                {
                    cbxCertificates.Items.Add(_certCollection[i]);
                }
            }
        }

        public bool HasErrors()
        {
            var result = false;

            if (cbxCertificates.SelectedItem == null)
            {
                errorProvider.SetError(cbxCertificates, ErrorMessages.CertificateEmptyError);
                result = true;
            }

            return result;
        }

        public void ClearErrors()
        {

        }

        public void SignPdf()
        {
            Cert myCert = null;

            try
            {
                if (_certificateData != null)
                {
                    byte[] bytes = _certificateData.Export(X509ContentType.Pfx, String.Empty);
                    myCert = new Cert(bytes);
                }
                else
                {
                    myCert = new Cert(cbxCertificates.Text, passwordBox.Text, String.Empty, String.Empty, String.Empty);
                }
            }
            catch
            {
                return;
            }

            var pdfSigFileName = String.Format("{0}-Sig.pdf", DateTime.Now.ToString("s").Replace(":", String.Empty));

            PDFSigner pdfs = new PDFSigner(outputPdfFile, Path.Combine(dirName, pdfSigFileName), myCert);
            PDFSignatureAP sigAp = new PDFSignatureAP();
            sigAp.SigReason = Reasontext.Text;
            sigAp.SigContact = Contacttext.Text;
            sigAp.SigLocation = Locationtext.Text;
            sigAp.Visible = chkSigVisible.Checked;
            sigAp.Multi = multiSigChkBx.Checked;
            sigAp.Page = sigLocationUCtrl1.numberOfPages;
            sigAp.CustomText = custSigText.Text;

            if (sigImgBox.Image != null)
            {
                MemoryStream ms = new MemoryStream();
                sigImgBox.Image.Save(ms, ImageFormat.Bmp);
                sigAp.RawData = ms.ToArray();
                ms.Close();
            }

            sigAp.SigX = (float)sigLocationUCtrl1.SigPosX;
            sigAp.SigY = (float)sigLocationUCtrl1.SigPosY;
            sigAp.SigW = (float)sigLocationUCtrl1.SigWidth;
            sigAp.SigH = (float)sigLocationUCtrl1.SigHeight;

            pdfs.Sign(sigAp, false, PDFEnc);
        }

        public void OpenFiles(string psFilename, string outputPdfFile, string dirName)
        {
            this.psFileName = psFilename;
            this.outputPdfFile = outputPdfFile;
            this.dirName = dirName;
            sigLocationUCtrl1.OpenPdf(outputPdfFile, dirName);
        }

        public SigLocalUCtrl()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var openFile = new OpenFileDialog())
            {
                openFile.Filter = "Certificate file (*.pfx)|*.pfx|Certificate file (*.p12)|*.p12";
                openFile.Title = "Select a file";

                if (openFile.ShowDialog() != DialogResult.Cancel)
                {
                    cbxCertificates.SelectedIndex = cbxCertificates.Items.Add(openFile.FileName);
                }
            }
        }

        private void browseBtn_Click(object sender, EventArgs e)
        {
            using (var openFile = new OpenFileDialog())
            {
                openFile.Filter = "*.jpg|*.gif|*.bmp|*.png";
                openFile.Title = "Select a file";

                if (openFile.ShowDialog() != DialogResult.Cancel)
                {
                    sigImgBox.ImageLocation = openFile.FileName;
                    sigLocationUCtrl1.OpenSignPicture(openFile.FileName);
                }
            }
        }

        private void chkVisibleSignature_CheckedChanged(object sender, EventArgs e)
        {
            sigLocationUCtrl1.Enabled = sigPanel2.Enabled = chkSigVisible.Checked;
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            sigImgBox.ImageLocation = String.Empty;
            sigLocationUCtrl1.ClearSigPicture();
        }

        private void cbxCertificates_SelectedIndexChanged(object sender, EventArgs e)
        {
            _certificateData = cbxCertificates.SelectedItem as X509Certificate2;
        }
    }
}
