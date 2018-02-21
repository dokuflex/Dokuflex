using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DokuFlex.Windows.Common.Services
{
    public static class DataServiceFactory
    {
        private static IDataService dataService = new DataService();

        public static IDataService Create()
        {
            return dataService;
        }
    }
}
