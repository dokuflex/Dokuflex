using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using iTextSharp.text.pdf;

namespace DokuSign.Controls
{
    using Extensions;

    public partial class SigBiometric : UserControl, IDigitalSignature
    {
        private PdfReader _pdfReader;
        private string _sigImage;
        private string _sigBiometric;
        private int _pageNumber;
        private string _documentId;
        private string _documentDir;
        private string _inputPdf;
        private readonly IDictionary<int, SignaturePosition> _sigPositions;

        private void UpdateSavePositionInfo()
        {
            StringBuilder pageNumbers = new StringBuilder();

            foreach (int item in _sigPositions.Keys)
            {
                pageNumbers.AppendFormat("{0},", item.ToString());
            }

            savePositionsLabel.Text = String.Format("{0}: {1}", StringResources.PageNumbers, pageNumbers.ToString());
        }


        public SigBiometric()
        {
            InitializeComponent();
            _sigPositions = new Dictionary<int, SignaturePosition>();
        }

        public int NumberOfPages
        {
            get
            {
                return Convert.ToInt32(numberOfPagesUpDown.Value);
            }
            set
            {
                numberOfPagesUpDown.Maximum = value;
                numberOfPagesUpDown.Minimum = numberOfPagesUpDown.Value = 1;
                numberOfPagesUpDown_ValueChanged(numberOfPagesUpDown, null);
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
            }
        }

        public string DocumentSigned
        {
            get { return _inputPdf; }
        }

        public string SignatureImage
        {
            get { return _sigImage; }
        }

        public string BiometricSignature
        {
            get { return _sigBiometric; }
        }

        public bool HasErrors()
        {
            var result = false;

            if (_sigPositions.Count == 0)
            {
                errorProvider.SetError(savePositionsLabel, ErrorMessages.SigImageNoSaveCorrdinatesError);
                result = true;
            }

            return result;
        }

        public void ClearErrors()
        {
            errorProvider.SetError(savePositionsLabel, String.Empty);
        }

        public IDictionary<int, SignaturePosition> GetSignaturePositions()
        {
            return _sigPositions;
        }

        public void LoadInformation(string inputPdf, string documentDir)
        {
            _inputPdf = inputPdf;
            _documentDir = documentDir;
            _pdfReader = new PdfReader(inputPdf);
            numberOfPagesUpDown.Maximum = _pdfReader.NumberOfPages;
            numberOfPagesUpDown_ValueChanged(numberOfPagesUpDown, null);
            pagePreview1.ShowSignatureImage(String.Empty);
        }

        private void btnSign_Click(object sender, EventArgs e)
        {
            sigPlusNET1.SetTabletState(1);
        }

        private void btnFinalize_Click(object sender, EventArgs e)
        {
            sigPlusNET1.SetTabletState(0);
            //sigPlusNET1.SetImageXSize(500);
            //sigPlusNET1.SetImageYSize(150);
            sigPlusNET1.SetImageXSize(100);
            sigPlusNET1.SetImageYSize(30);
            sigPlusNET1.SetJustifyMode(5);

            var fileName = DateTime.Now.ToString("s").Replace(":", String.Empty);
            _sigImage = String.Format("{0}\\{1}.png", _documentDir, fileName);
            _sigBiometric = String.Format("{0}\\{1}.sig", _documentDir, fileName);

            var image = sigPlusNET1.GetSigImage();
            image.Save(_sigImage, System.Drawing.Imaging.ImageFormat.Png);

            sigPlusNET1.ExportSigFile(_sigBiometric);
            sigPlusNET1.SetJustifyMode(0);

            pagePreview1.ShowSignatureImage(_sigImage);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            sigPlusNET1.ClearTablet();
        }

        public string DocumentId
        {
            get { return _documentId; }
        }

        private void numberOfPagesUpDown_ValueChanged(object sender, EventArgs e)
        {
            _pageNumber = Convert.ToInt32(numberOfPagesUpDown.Value);

            if (_pdfReader != null)
            {
                iTextSharp.text.Rectangle rect = _pdfReader.GetPageSize(_pageNumber);

                if (Directory.Exists(_documentDir))
                {
                    string imageLocation = Path.Combine(_documentDir, String.Format("Page-{0}.png", _pageNumber));
                    pagePreview1.ShowPage(rect.Width, rect.Height, imageLocation);
                }
                else
                {
                    pagePreview1.ShowPage(rect.Width, rect.Height, String.Empty);
                }
            }
        }

        private void btnSavePosition_Click(object sender, EventArgs e)
        {
            if (_sigPositions.ContainsKey(_pageNumber))
            {
                _sigPositions.Remove(_pageNumber);
            }

            var sigPosition = new SignaturePosition(pagePreview1.SigWidth, pagePreview1.SigHeight,
                pagePreview1.SigPosX, pagePreview1.SigPosY);

            _sigPositions.Add(_pageNumber, sigPosition);

            UpdateSavePositionInfo();
        }

        private void btnRemovePosition_Click(object sender, EventArgs e)
        {
            if (_sigPositions.ContainsKey(_pageNumber))
            {
                _sigPositions.Remove(_pageNumber);
            }

            UpdateSavePositionInfo();
        }
    }
}
