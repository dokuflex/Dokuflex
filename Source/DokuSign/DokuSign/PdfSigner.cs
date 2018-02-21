using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.security;

namespace DokuSign
{
    public class PdfSigner
    {
        private string _inputPdf;
        private string _outputPdf;
        private Cert _cert;

        public PdfSigner(string inputPdf, string outputPdf, Cert cert)
        {
            this._inputPdf = inputPdf;
            this._outputPdf = outputPdf;
            this._cert = cert;
        }

        public void Sign(PdfSignatureAp sigAP)
        {
            PdfReader reader = new PdfReader(this._inputPdf);
            FileStream fs = new FileStream(this._outputPdf, FileMode.Create, FileAccess.Write);
            PdfStamper st = PdfStamper.CreateSignature(reader, fs, '\0', null, sigAP.Multi);

            try
            {
                PdfSignatureAppearance sap = st.SignatureAppearance;
                sap.SignDate = DateTime.Now;
                sap.Reason = sigAP.SigReason;
                sap.Contact = sigAP.SigContact;
                sap.Location = sigAP.SigLocation;

                if (sigAP.Visible)
                {
                    iTextSharp.text.Rectangle rect = st.Reader.GetPageSize(sigAP.Page);
                    sap.Image = sigAP.RawData == null ? null : iTextSharp.text.Image.GetInstance(sigAP.RawData);
                    sap.Layer2Text = sigAP.CustomText;

                    sap.SetVisibleSignature(new iTextSharp.text.Rectangle(sigAP.SigX, sigAP.SigY, sigAP.SigX + sigAP.SigW, sigAP.SigY + sigAP.SigH), sigAP.Page, null);
                }

                PdfSignature dic = new PdfSignature(PdfName.ADOBE_PPKLITE, new PdfName("adbe.pkcs7.detached"));
                dic.Reason = sap.Reason;
                dic.Location = sap.Location;
                dic.Contact = sap.Contact;
                dic.Date = new PdfDate(sap.SignDate);
                sap.CryptoDictionary = dic;

                IOcspClient ocsp = new OcspClientBouncyCastle();               
                PrivateKeySignature pks = new PrivateKeySignature(_cert.Akp, "SHA1");            
                MakeSignature.SignDetached(sap, pks, _cert.Chain, null, ocsp, null, 0, CryptoStandard.CMS);
            }
            finally
            {
                st.Close();
                fs.Close();
            }            
        }
    }
}
