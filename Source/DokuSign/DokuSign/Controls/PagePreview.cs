using System;
using System.Drawing;
using System.Windows.Forms;

namespace DokuSign.Controls
{
    public partial class PagePreview : UserControl
    {
        private int _sigPosX;
        private int _sigPosY;
        private int _sigWidth;
        private int _sigHeight;
        private float _pageWidth;
        private float _pageHeight;
        private PickBox _pickBox;
        private string _imageLocation;

        private bool autoPositioned;

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

        public void ShowPage(float pageWidth, float pageHeight, string imageLocation)
        {
            pagePreviewPanel.BackgroundImage = null;
            pagePreviewPanel.Top = 0;

            _pageHeight = pageHeight;
            _pageWidth = pageWidth;
            _imageLocation = imageLocation;

            if (_pageWidth > _pageHeight)
            {
                pagePreviewPanel.Width = pagePreviewPanel.Parent.Width;
                pagePreviewPanel.Height = Convert.ToInt32((pagePreviewPanel.Width * _pageHeight) / _pageWidth);
            }
            else
            {
                pagePreviewPanel.Height = pagePreviewPanel.Parent.Height;
                pagePreviewPanel.Width = Convert.ToInt32((pagePreviewPanel.Height * _pageWidth * 1.4) / _pageHeight);
            }

            pagePreviewPanel.Left = (pagePreviewPanel.Parent.Width - pagePreviewPanel.Width) / 2;
            pagePreviewPanel.Top = (pagePreviewPanel.Parent.Height - pagePreviewPanel.Height) / 2;

            if (String.IsNullOrWhiteSpace(imageLocation))
                pagePreviewPanel.BackgroundImage = null;
            else
                pagePreviewPanel.BackgroundImage = Bitmap.FromFile(imageLocation);
        }

        public void ShowSignatureImage(string imageLocation)
        {
            if (String.IsNullOrWhiteSpace(imageLocation))
                sigPicture.ImageLocation = String.Empty;
            else
                sigPicture.ImageLocation = imageLocation;

            if (!autoPositioned)
            {
                autoPositioned = true;
                sigPicture.Left = (int)((sigPicture.Parent.Width - sigPicture.Width) / 2.0);
                sigPicture.Top = (int)((sigPicture.Parent.Height - sigPicture.Height) / 2.0); ;
                sigPicture_Move(sigPicture, null);

                sigPicture.Width = 100;
                sigPicture.Height = 50;
                sigPicture_Resize(sigPicture, null);

            }

            /*
            sigPicture.Left = 0;
            sigPicture.Top = sigPicture.Parent.Height - sigPicture.Height;
            sigPicture_Move(sigPicture, null);

            sigPicture.Width = 50;
            sigPicture.Height = 20;
            sigPicture_Resize(sigPicture, null);
             * */
        }

        public PagePreview()
        {
            InitializeComponent();
            _pickBox = new PickBox();
            _pickBox.WireControl(sigPicture);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (_pageWidth > 0 && _pageHeight > 0)
            {
                ShowPage(_pageWidth, _pageHeight, _imageLocation);
                ShowSignatureImage(sigPicture.ImageLocation);
            }
        }

        private void sigPicture_Move(object sender, EventArgs e)
        {
            _sigPosX = Convert.ToInt32((_pageWidth * sigPicture.Left) / pagePreviewPanel.Width);
            _sigPosY = sigPicture.Parent.Height - sigPicture.Top - sigPicture.Height;
            _sigPosY = Convert.ToInt32((_pageHeight * _sigPosY) / pagePreviewPanel.Height);
        }

        private void sigPicture_Resize(object sender, EventArgs e)
        {
            _sigWidth = Convert.ToInt32((_pageWidth * sigPicture.Width) / pagePreviewPanel.Width);
            _sigHeight = Convert.ToInt32((_pageHeight * sigPicture.Height) / pagePreviewPanel.Height);
        }
    }
}
