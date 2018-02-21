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
using System.IO;
using iTextSharp.text.pdf;

namespace DokuSign.Controls
{
    using Extensions;
    using DokuFlex.Windows.Common;

    public partial class SigLocal : UserControl, IDigitalSignature
    {
        private PdfReader _pdfReader;
        private X509Certificate2Collection _certCollection;
        private X509Certificate2 _certificateData;
        private string _documentDir;
        private string _inputPdf;
        private string _signatureImage;
        private string _outputPdf;
        private string _documentId;
        private int _localSignatureIndex = 0;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            X509Store store = null;

            try
            {
                //Load certificates from My.
                store = new X509Store(StoreName.My);
                store.Open(OpenFlags.ReadOnly);
                _certCollection.AddRange(store.Certificates);
                store.Close();
            }
            finally
            {
                if (store != null)
                    store.Close();
            }

            if (_certCollection.Count > 0)
            {
                var certInfoList = new List<CertificateInfo>(_certCollection.Count);

                for (int i = 0; i < _certCollection.Count; i++)
                {
                    //var certificate = _certCollection[i];
                    certInfoList.Add(new CertificateInfo(_certCollection[i]));
                }

                bindingSource.DataSource = certInfoList;
                if (_certCollection.Count > 0)
                {
                    _certificateData = _certCollection[0];
                }
            }

            if (int.TryParse(ConfigurationManager.GetValue(Constants.LocalCertificate), out _localSignatureIndex))
                certificateList.SelectedIndex = _localSignatureIndex;
        }

        public SigLocal()
        {
            InitializeComponent();
            _certCollection = new X509Certificate2Collection();
        }

        private void SignatureImageChanged(object sender, SignatureImageChangedEventArgs e)
        {
            _signatureImage = e.ImageLocation;
        }

        public bool HasErrors()
        {
            var result = false;

            if (certificateList.SelectedValue == null)
            {
                errorProvider.SetError(certificateList, ErrorMessages.CertificateNoValidError);
                result = true;
            }

            return result;
        }

        public void ClearErrors()
        {
            errorProvider.SetError(certificateList, String.Empty);
            errorProvider.SetError(passwordBox, String.Empty);
        }

        public void SignDocument()
        {
            string outputPdfName = String.Format("{0}.pdf", DateTime.Now.ToString("s").Replace(":", "-"));
            this._outputPdf = Path.Combine(_documentDir, outputPdfName);

            Cert cert = null;

            try
            {
                PdfSignatureAp sigAp = new PdfSignatureAp();
                sigAp.Visible = false;
                sigAp.Multi = MultiSignature.Checked;
                sigAp.Page = 0;
                //sigAp.SigContact = "info@dokuflex.com";

                if (_certificateData.HasPrivateKey)
                {
                    var password = String.IsNullOrWhiteSpace(passwordBox.Text) ? Guid.NewGuid().ToString("N") : passwordBox.Text;
                    byte[] bytes = _certificateData.Export(X509ContentType.Pfx, password);
                    cert = new Cert(bytes, password);
                }

                PdfSigner pdfSigner = new PdfSigner(_inputPdf, this._outputPdf, cert);
                pdfSigner.Sign(sigAp);
            }
            catch (Exception ex)
            {
                DokuFlex.Windows.Common.Log.LogFactory.CreateLog().LogError(ex);
                MessageBox.Show(String.Format("{0}\nError:{1}", ErrorMessages.OpenCertificateError, ex.Message), "DokuSign");
                throw ex;
            }

            try
            {
                _documentId = _pdfReader.GetDocumentTagValue("Document ID:", 33);
            }
            catch (Exception ex)
            {
                DokuFlex.Windows.Common.Log.LogFactory.CreateLog().LogError(ex);
                MessageBox.Show(ErrorMessages.ApplicationError);
                throw ex;
            }

            ConfigurationManager.SetValue(Constants.LocalCertificate, certificateList.SelectedIndex.ToString());
            ConfigurationManager.Save();
        }

        public string DocumentSigned
        {
            get { return this._outputPdf; }
        }

        public string SignatureImage
        {
            get { return this._signatureImage; }
        }

        public IDictionary<int, SignaturePosition> GetSignaturePositions()
        {
            return null;
        }

        private void certificateList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (certificateList.SelectedItem == null)
            {
                _certificateData = null;
            }
            else
            {
                _certificateData = certificateList.SelectedValue as X509Certificate2;
            }

            _localSignatureIndex = certificateList.SelectedIndex;
        }

        public void LoadInformation(string inputPdf, string documentDir)
        {
            this._inputPdf = inputPdf;
            this._documentDir = documentDir;
            this._pdfReader = new PdfReader(inputPdf);
        }

        public string BiometricSignature
        {
            get { return String.Empty; }
        }

        public string DocumentId
        {
            get { return _documentId; }
        }
    }
}
