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

namespace DokuFlex.WinForms.Common
{
    using System;
    using System.Threading.Tasks;
    using DokuFlex.Windows.Common;
    using DokuFlex.Windows.Common.Services;

    public static class Session
    {
        public static string GetTikect()
        {
            var ticket = String.Empty;
            var credentials = new Credentials();

            credentials.UserName = ConfigurationManager.GetValue(Constants.LoginUserName);

            if (String.IsNullOrWhiteSpace(credentials.UserName))
            {
                using (var form = new LoginForm())
                {
                    if (form.ShowLoginDialog())
                    {
                        ticket = form.Ticket;
                    }
                }

                return ticket;
            }

            credentials.SetEncryptedPassword(ConfigurationManager.GetValue(Constants.LoginPassword));

            try
            {
                ticket = DataServiceFactory.Create().Login(credentials);
            }
            catch (Exception)
            {
                //silent exception
            }

            if (String.IsNullOrWhiteSpace(ticket))
            {
                using (var form = new LoginForm())
                {
                    if (form.ShowLoginDialog())
                    {
                        ticket = form.Ticket;
                    }
                }              
            }

            return ticket;
        }

        public static async Task<string> GetTikectAsync()
        {
            var ticket = String.Empty;
            var credentials = new Credentials();

            credentials.UserName = ConfigurationManager.GetValue(Constants.LoginUserName);

            if (String.IsNullOrWhiteSpace(credentials.UserName))
            {
                using (var form = new LoginForm())
                {
                    if (form.ShowLoginDialog())
                    {
                        ticket = form.Ticket;
                    }
                }

                return ticket;
            }

            credentials.SetEncryptedPassword(ConfigurationManager.GetValue(Constants.LoginPassword));

            try
            {
                ticket = await DataServiceFactory.Create().LoginAsync(credentials);
            }
            catch (AggregateException)
            {
                //silent exception
            }

            if (String.IsNullOrWhiteSpace(ticket))
            {
                using (var form = new LoginForm())
                {
                    if (form.ShowLoginDialog())
                    {
                        ticket = form.Ticket;
                    }
                }
            }

            return ticket;
        }

        public static void ChangeCredentials()
        {
            using (var form = new LoginForm())
            {
                form.ShowLoginDialog();
            }
        }
    }
}
