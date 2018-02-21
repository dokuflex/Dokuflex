using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using Org.Eurekaa.PDF.iSafePDF.Lib;

namespace DokuFlexVPrinter.Controls
{
    public partial class SigLocationUCtrl : UserControl
    {
        private PdfReader reader = null;
        private PickBox pb = new PickBox();
        private string _fileName;
        private string _dirName;
        private int _sigPosX;
        private int _sigPosY;
        private int _sigWidth;
        private int _sigHeight;

        public int numberOfPages
        {
            get
            {
                return Convert.ToInt32(numberOfPagesUpDown.Value);
            }
        }

        public int SigPosX
        {
            get
            {
                return _sigPosX;
            }
        }

        public int SigPosY
        {
            get
            {
                return _sigPosY;
            }
        }

        public int SigWidth
        {
            get
            {
                return _sigWidth;
            }
        }

        public int SigHeight
        {
            get
            {
                return _sigHeight;
            }
        }

        public SigLocationUCtrl()
        {
            InitializeComponent();
            pb.WireControl(sigPicture);
        }

        public void OpenPdf(string fileName, string dirName)
        {
            _fileName = fileName;
            _dirName = dirName;

            reader = new PdfReader(fileName);

            numberOfPagesUpDown.Maximum = reader.NumberOfPages;
            numberOfPagesUpDown.Minimum = numberOfPagesUpDown.Value = 1;
            numberOfPagesUpDown_ValueChanged(numberOfPagesUpDown, null);

            sigPicture.Left = 0;
            sigPicture.Top = sigPicture.Parent.Height - sigPicture.Height;
            sigPicture.Width = 50;
            sigPicture.Height = 20;
        }

        private void numberOfPagesUpDown_ValueChanged(object sender, EventArgs e)
        {          
            iTextSharp.text.Rectangle rect = reader.GetPageSize(Convert.ToInt32(numberOfPagesUpDown.Value));

            pagePreviewPanel.Top = 0;

            if (rect.Width > rect.Height)
            {
                pagePreviewPanel.Width = pagePreviewPanel.Parent.Width;
                pagePreviewPanel.Height = Convert.ToInt32((pagePreviewPanel.Width * rect.Height) / rect.Width);
            }
            else
            {
                pagePreviewPanel.Height = pagePreviewPanel.Parent.Height;
                pagePreviewPanel.Width = Convert.ToInt32((pagePreviewPanel.Height * rect.Width) / rect.Height);
            }

            pagePreviewPanel.Left = (pagePreviewPanel.Parent.Width - pagePreviewPanel.Width) / 2;
            pagePreviewPanel.Top = (pagePreviewPanel.Parent.Height - pagePreviewPanel.Height) / 2;

            var imagePath = String.Format("{0}\\Page-{1}.png", _dirName, Convert.ToInt32(numberOfPagesUpDown.Value));

            if (File.Exists(imagePath))
            {
                pagePreviewPanel.BackgroundImage = Bitmap.FromFile(imagePath);
            }
            else
            {
                pagePreviewPanel.BackgroundImage = null;
            }
        }

        internal void OpenSignPicture(string fileName)
        {
            sigPicture.ImageLocation = fileName;
        }

        internal void ClearSigPicture()
        {
            sigPicture.ImageLocation = String.Empty;
        }

        private void sigPicture_Move(object sender, EventArgs e)
        {
            iTextSharp.text.Rectangle rect = reader.GetPageSize(Convert.ToInt32(numberOfPagesUpDown.Value));
            _sigPosX = Convert.ToInt32((rect.Width * sigPicture.Left) / pagePreviewPanel.Width);
            _sigPosY = sigPicture.Parent.Height - sigPicture.Top - sigPicture.Height;
            _sigPosY = Convert.ToInt32((rect.Height * (float)_sigPosY) / pagePreviewPanel.Height);
        }

        private void sigPicture_Resize(object sender, EventArgs e)
        {
            iTextSharp.text.Rectangle rect = reader.GetPageSize(Convert.ToInt32(numberOfPagesUpDown.Value));
            _sigWidth = Convert.ToInt32((rect.Width * sigPicture.Width) / pagePreviewPanel.Width);
            _sigHeight = Convert.ToInt32((rect.Height * sigPicture.Height) / pagePreviewPanel.Height);
        }
    }
}
