using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

namespace DokuSign
{
    public class CertificateInfo
    {
        public CertificateInfo(X509Certificate2 certificate)
        {
            Certificate = certificate;
        }

        public X509Certificate2 Certificate { get; private set; }

        public string DisplayName
        {
            get
            {
                return Certificate.Subject;
            }
        }
    }
}
