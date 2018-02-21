using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DokuSign
{
    public interface IControlValidation
    {
        /// <summary>
        /// Validate data
        /// </summary>
        /// <returns>True if contains errors</returns>
        bool HasErrors();

        /// <summary>
        /// Clear errors messages
        /// </summary>
        void ClearErrors();
    }
}
