using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DokuSign.Controls
{
    public partial class SigImage : UserControl, IControlValidation
    {
        public event SignatureImageChangedEventHandler SignatureImageChanged;

        public string CustomText
        {
            get
            {
                return custSigText.Text;
            }
        }

        public SigImage()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var openFile = new OpenFileDialog())
            {
                openFile.Filter = "JPG|*.jpg|GIF|*.gif|BMP|*.bmp|PNG|*.png";
                openFile.Title = "Seleccionar imagen";

                if (openFile.ShowDialog() != DialogResult.Cancel)
                {
                    sigImgBox.ImageLocation = openFile.FileName;

                    if (SignatureImageChanged != null)
                    {
                        SignatureImageChanged(this, new SignatureImageChangedEventArgs(openFile.FileName));
                    }
                }
            } 
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            sigImgBox.ImageLocation = String.Empty;

            if (SignatureImageChanged != null)
            {
                SignatureImageChanged(this, new SignatureImageChangedEventArgs(String.Empty));
            }
        }

        public bool HasErrors()
        {
            var result = false;

            if (String.IsNullOrWhiteSpace(sigImgBox.ImageLocation))
            {
                errorProvider.SetError(label27, ErrorMessages.SigImageNoValidError);
                result = true;
            }

            return result;
        }

        public void ClearErrors()
        {
            errorProvider.SetError(label27, String.Empty);
        }
    }

    public class SignatureImageChangedEventArgs : EventArgs
    {
        public string ImageLocation { get; set; }

        public SignatureImageChangedEventArgs(string imageLocation)
        {
            this.ImageLocation = imageLocation;
        }
    }

    public delegate void SignatureImageChangedEventHandler(object sender, SignatureImageChangedEventArgs e);
}
