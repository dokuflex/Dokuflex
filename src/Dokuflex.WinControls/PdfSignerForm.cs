// Copyright (c) Dokuflex, Co. All rights reserved.
// See License.txt in the project root for license information.

namespace Dokuflex.WinControls
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Collections.Generic;
    using Telerik.WinControls.UI;
    using Telerik.WinControls.UI.Data;
    using UserControls;

    public partial class PdfSignerForm : RadForm
    {
        private UserControl signatureCtrl = null;
        private readonly ICollection<SignatureImage> signatureImages;

        private void InitializeControls()
        {

        }

        public PdfSignerForm()
        {
            InitializeComponent();
            signatureImages = new List<SignatureImage>();
        }

        private PictureBox CreateSignatureImageControl(Point location)
        {
            var imageSize = new Size(200, 120); // Default size
            var imageCtrl = new PictureBox
            {
                Size = imageSize,
                Location = location
            };

            imageCtrl.Click += ImageCtrl_Click;

            return imageCtrl;
        }

        private void AddSignatureToPage(int pageNumber)
        {
            var imageCtrl = CreateSignatureImageControl(new Point(300, 300));
            pdfViewer.Controls.Add(imageCtrl);
            imageCtrl.Show();
        }

        private void ImageCtrl_Click(object sender, EventArgs e)
        {
            MessageBox.Show("AFAFaasf");
        }

        private void ClearSignaturePanel()
        {
            if (signatureCtrl != null && !signatureCtrl.IsDisposed)
            {
                settingsPanel.Controls.Remove(signatureCtrl);
                signatureCtrl.Hide();
                signatureCtrl.Dispose();
            }
        }

        private void ShowBiometricSignature()
        {
            signatureCtrl = new BiometricSignatureControl
            {
                Parent = settingsPanel,
                Dock = DockStyle.Top
            };

            signatureCtrl.Show();
            signatureCtrl.BringToFront();

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            WindowState = FormWindowState.Maximized;
            pdfViewer.LoadDocument(@"C:\Patterns & Practices\Unity Application Block\Developer’s Guide to Microsoft Unity.pdf");
        }

        private void signatureTypeDdList_SelectedIndexChanged(object sender, PositionChangedEventArgs e)
        {
            ClearSignaturePanel();

            var index = signatureTypeDdList.SelectedIndex;

            switch (index)
            {
                case 3:
                    ShowBiometricSignature();
                    break;
                default:
                    break;
            }
        }

        private void signPageBtn_Click(object sender, EventArgs e)
        {
            AddSignatureToPage(0);
        }
    }
}
