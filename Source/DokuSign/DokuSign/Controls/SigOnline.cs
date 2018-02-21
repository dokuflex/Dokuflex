using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DokuFlex.Windows.Common.Services;
using DokuFlex.Windows.Common.Services.Data;
using DokuFlex.WinForms.Common;
using iTextSharp.text.pdf;

namespace DokuSign.Controls
{
    using Extensions;

    public partial class SigOnline : UserControl, IDigitalSignature
    {
        private string _ticket;
        private PdfReader _pdfReader;
        private string _inputPdf;
        private string _documentId;

        //protected override async void OnLoad(EventArgs e)
        //{
        //    base.OnLoad(e);

        //    
        //}

        public string CertificatePass
        {
            get
            {
                return passwordBox.Text;
            }
        }

        public SigOnline()
        {
            InitializeComponent();
        }

        public async Task LoadInformation(string inputPdf)
        {
            _inputPdf = inputPdf;
            _pdfReader = new PdfReader(inputPdf);

            this.Cursor = Cursors.WaitCursor;

            Task<string> theTask = Session.GetTikectAsync();

            try
            {
                _ticket = await theTask;

                var certificates = await DokuFlexService.ListCertificatesAsync(_ticket);

                certificateList.DataSource = new BindingList<Certificate>(certificates);
            }
            catch (Exception ex)
            {
                DokuFlex.Windows.Common.Log.LogFactory.CreateLog().LogError(ex);
                MessageBox.Show(string.Format("{0}\n\n{1}", ErrorMessages.ApplicationError, ex.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        public void SignDocument()
        {
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
        }

        public string DocumentId
        {
            get { return _documentId; }
        }

        public string DocumentSigned
        {
            get { return _inputPdf; }
        }

        public string SignatureImage
        {
            get { return String.Empty; }
        }

        public string BiometricSignature
        {
            get { return certificateList.SelectedValue.ToString(); }
        }

        public IDictionary<int, SignaturePosition> GetSignaturePositions()
        {
            return null;
        }

        public bool HasErrors()
        {
            var result = false;

            if (certificateList.SelectedIndex == -1)
            {
                errorProvider.SetError(certificateList, ErrorMessages.CertificateNoValidError);
                result = true;
            }

            return result;
        }

        public void ClearErrors()
        {
            errorProvider.SetError(certificateList, String.Empty);
        }
    }
}
