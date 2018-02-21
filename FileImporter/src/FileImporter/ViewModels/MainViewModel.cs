using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DokuFlex.Windows.Common;
using DokuFlex.Windows.Common.Services;
using DokuFlex.Windows.Common.Services.Data;
using log4net;

namespace FileImporter.ViewModels
{
    public class MainViewModel
    {
        public string FileName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Token { get; private set; }
        private Documentary _docType;
        private List<Documentary> _docTypes;
        private ILog _log;

        public MainViewModel()
        {
            _log = LogManager.GetLogger("FileImporter");
        }

        public async Task<bool> LoginAsync()
        {

            var dataService = DataServiceFactory.Create();

            try
            {
                var credentials = new Credentials();
                credentials.UserName = UserName;
                credentials.Password = Password;
                Token = await dataService.LoginAsync(credentials);
            }
            catch (Exception ex)
            {
                _log.Error($"'LoginAsync' call fails", ex);
                Token = string.Empty;
            }
            return !string.IsNullOrWhiteSpace(Token);
        }

        public async Task<bool> GetDocumentaryTypes()
        {
            try
            {
                _docTypes = await DokuFlexService.GetDocumentaryTypesAsync(Token);
            }
            catch (Exception ex)
            {
                _log.Error($"'GetDocumentaryTypesAsync' call fails", ex);
                return false;
            }

            return _docTypes.Count > 0;
        }

        public List<Documentary> DocumentaryTypes
        {
            get => _docTypes;
        }

        public Documentary DocumentaryType
        {
            get => _docType;
            set
            {
                if (_docType == value)
                    return;

                _docType = value;
            }
        }

        public FileFolder Folder { get; set; }
        public UserGroup UserGroup { get; set; }

        public bool IsValid()
        {

            if (string.IsNullOrWhiteSpace(FileName))
                return false;

            if (Folder == null)
                return false;

            if (UserGroup == null)
                return false;

            return true;
        }
    }
}
