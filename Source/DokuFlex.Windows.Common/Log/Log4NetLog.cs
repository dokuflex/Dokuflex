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

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace DokuFlex.Windows.Common.Log
{
    using System;
    using System.Reflection;
    using System.Globalization;
    using DokuFlex.Windows.Common.Services;

    public class Log4NetLog : ILog
    {
        // Create a logger for use in this class
        private readonly log4net.ILog _log;

        public Log4NetLog()
        {
            _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        }
        /*
        /// <summary>
        /// <see cref="DokuFlex.Windows.Common.Log.ILog"/>
        /// </summary>
        /// <param name="message"><see cref="DokuFlex.Windows.Common.Log.ILog"/></param>
        /// <param name="args"><see cref="DokuFlex.Windows.Common.Log.ILog"/></param>
        public void LogError(string message, params object[] args)
        {
            Exception exception = args[0] as Exception;

            if (!String.IsNullOrWhiteSpace(message))
            {
                var messageToTrace = string.Format(CultureInfo.InvariantCulture, message, args);

                _log.Error(messageToTrace);
            }
        }*/

        /// <summary>
        /// <see cref="DokuFlex.Windows.Common.Log.ILog"/>
        /// </summary>
        /// <param name="message"><see cref="DokuFlex.Windows.Common.Log.ILog"/></param>
        /// <param name="exception"><see cref="DokuFlex.Windows.Common.Log.ILog"/></param>
        /// <param name="args"><see cref="DokuFlex.Windows.Common.Log.ILog"/></param>
        public void LogError(string message, Exception exception, params object[] args)
        {
            if (!String.IsNullOrWhiteSpace(message) && exception != null)
            {
                var messageToTrace = string.Format(CultureInfo.InvariantCulture, message, args);

                var exceptionData = exception.ToString(); // The ToString() create a string representation of the current exception

                _log.Error(String.Format(CultureInfo.InvariantCulture, "{0} Exception:{1}", messageToTrace, exceptionData));

                //Enviar al server
                Assembly asm = exception.TargetSite.Module.Assembly;
                AssemblyName name = asm.GetName();

                DataServiceFactory.Create().SendAppLog(string.Empty, string.Empty, name.Name + " " + name.Version.ToString(), string.Empty, exception.Source, exception.ToString(), exception.StackTrace);
            }
        }

        public void LogError(Exception exception)
        {
            if (exception != null)
            {
                var messageToTrace = string.Format(CultureInfo.InvariantCulture, exception.Message);

                var exceptionData = exception.ToString(); // The ToString() create a string representation of the current exception

                _log.Error(String.Format(CultureInfo.InvariantCulture, "{0} Exception:{1}", messageToTrace, exceptionData));

                //Enviar al server
                Assembly asm = exception.TargetSite.Module.Assembly;
                AssemblyName name = asm.GetName();

                DataServiceFactory.Create().SendAppLog(string.Empty, string.Empty, name.Name + " " + name.Version.ToString(), string.Empty, exception.Source, exception.ToString(), exception.StackTrace);
            }
        }

        public void LogInfo(string message)
        {
            _log.Info(message);
        }
    }
}
