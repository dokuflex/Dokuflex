using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.Security;

namespace DokuSign
{
    public class Cert
    {
        private byte[] _rawData;
        private AsymmetricKeyParameter _akp;
        private Org.BouncyCastle.X509.X509Certificate[] _chain;
        private string _path;
        private string _password;

        public Org.BouncyCastle.X509.X509Certificate[] Chain
        {
            get { return _chain; }
        }

        public AsymmetricKeyParameter Akp
        {
            get { return _akp; }
        }

        private void processCert()
        {
            string alias = null;
            Pkcs12Store pk12;

            //First we'll read the certificate file
            Stream fs;

            if (String.IsNullOrWhiteSpace(this._path)) 
                fs = new MemoryStream(this._rawData);               
            else
                fs = new FileStream(this._path, FileMode.Open, FileAccess.Read);

            pk12 = new Pkcs12Store(fs, this._password.ToCharArray());

            //then Iterate throught certificate entries to find the private key entry
            foreach (string al in pk12.Aliases)
            {
                if (pk12.IsKeyEntry(al) && pk12.GetKey(al).Key.IsPrivate)
                {
                    alias = al;
                    break;
                }
            }

            fs.Close();

            this._akp = pk12.GetKey(alias).Key;
            X509CertificateEntry[] ce = pk12.GetCertificateChain(alias);
            this._chain = new Org.BouncyCastle.X509.X509Certificate[ce.Length];

            for (int k = 0; k < ce.Length; ++k)
                _chain[k] = ce[k].Certificate;
        }

        public Cert()
        {
            _rawData = null;
            _password = String.Empty;
        }

        public Cert(string cpath, string password)
        {
            this._path = cpath;
            this._password = password;
            this.processCert();
        }

        public Cert(byte[] rawData, string password)
        {
            this._rawData = rawData;
            this._password = password;
            this.processCert();
        }
    }
}
