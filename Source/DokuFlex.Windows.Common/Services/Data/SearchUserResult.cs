using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DokuFlex.Windows.Common.Services.Data
{
    public class SearchUserResult
    {
        public string id { get; set; }

        public string name { get; set; }

        public string extension { get; set; }

        public string position { get; set; }

        public string username { get; set; }

        public string phone { get; set; }

        public string fax { get; set; }

        public string location { get; set; }

        public string email { get; set; }

        public string department { get; set; }

        public string surname { get; set; }

        public string fullName { get {
            return String.Format("{0} {1}", this.name, this.surname);
        } 
        }

    }
}
