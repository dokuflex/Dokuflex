using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DokuSign
{
    public interface IDigitalSignature : IControlValidation
    {      
        /// <summary>
        /// Sign a document and create a PDF signed copy
        /// </summary>
        void SignDocument();

        string DocumentId { get; }

        /// <summary>
        /// Access to the PDF document signed file
        /// </summary>
        string DocumentSigned { get; }

        /// <summary>
        /// Access to the signature image file
        /// </summary>
        string SignatureImage { get;  }

        /// <summary>
        /// Access to the biometric signature file
        /// </summary>
        string BiometricSignature { get; }

        /// <summary>
        /// Get the signature positions dictionary
        /// </summary>
        /// <returns>Key/Value pair of signature position</returns>
        IDictionary<int, SignaturePosition> GetSignaturePositions();
    }
}
