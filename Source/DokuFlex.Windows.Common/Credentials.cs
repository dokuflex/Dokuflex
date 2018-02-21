// =================================================================================================================
// Paina Solutions
// DokuFlex
// =================================================================================================================
// ©2013 DokuFlex. All rights reserved. Certain content used with permission from contributors.
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance
// with the License. You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software distributed under the License is
// distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and limitations under the License.
// =================================================================================================================

namespace DokuFlex.Windows.Common
{
    using System;
    using System.Text;
    using System.Security.Cryptography;

    using DokuFlex.Windows.Common.Extensions;

    public class Credentials
    {
        private string _userName;

        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
            }
        }

        private string _password;

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {

                _password = value;
            }
        }

        public void SetEncryptedPassword(string encryptedPassword)
        {
            _password = encryptedPassword;
        }

        public bool ContainCredententials()
        {
            return !(string.IsNullOrWhiteSpace(_userName) ||
                string.IsNullOrWhiteSpace(_password));
        }
    }
}
