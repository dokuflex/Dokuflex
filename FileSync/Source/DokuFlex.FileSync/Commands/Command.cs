// =================================================================================================================
// DokuFlex
// =================================================================================================================
// ©2013 DokuFlex. All rights reserved. Certain content used with permission from contributors
// http://www.dokuflex.com/allwinproducts/license/contributors
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance
// with the License. You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software distributed under the License is
// distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and limitations under the License.
// =================================================================================================================

namespace DokuFlex.FileSync.Commands
{
    using System;
    using System.Threading.Tasks;
    using DokuFlex.Common.Log;

    public class BeforeExecuteEventArgs
        : EventArgs
    {

    }

    public class AfterExecuteEventArgs
        : EventArgs
    {

    }

    public class ExecuteErrorEventArgs
        : EventArgs
    {
        private Exception _exceptionObject;

        public ExecuteErrorEventArgs(Exception exceptionObject, bool asyncCommand = false)
        {
            _exceptionObject = exceptionObject;
            AsyncCommand = asyncCommand;
        }

        public Exception ExceptionObject
        {
            get
            {
                return _exceptionObject;
            }
        }

        public string ErrorMessage
        {
            get
            {
                return _exceptionObject == null ? String.Empty : _exceptionObject.Message;
            }
        }

        public bool AsyncCommand { get; set; }
    }

    public delegate void BeforeExecuteEventHandler(object sender, BeforeExecuteEventArgs e);

    public delegate void AfterExecuteEventHandler(object sender, AfterExecuteEventArgs e);

    public delegate void ExecuteErrorEventHandler(object sender, ExecuteErrorEventArgs e);

    public abstract class Command
    {
        public static string SourcePath { get; set; }

        protected virtual void DoExecute()
        {
        }

        protected virtual async Task<bool> DoExecuteAsync()
        {
            return await new Task<bool>(() => { return false;});
        }

        public event BeforeExecuteEventHandler BeforeExecute;

        public event AfterExecuteEventHandler AfterExecute;

        public event ExecuteErrorEventHandler ExecuteError;

        public virtual bool IsAsync
        {
            get { return false; }
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        public void Execute()
        {
            if (BeforeExecute != null)
            {
                BeforeExecute(this, new BeforeExecuteEventArgs());
            }

            try
            {
                DoExecute();
            }
            catch (Exception ex)
            {
                LogFactory.CreateLog().LogError(ex.Message, ex);

                if (ExecuteError != null)
                {
                    ExecuteError(this, new ExecuteErrorEventArgs(ex));
                }
            }

            if (AfterExecute != null)
            {
                AfterExecute(this, new AfterExecuteEventArgs());
            }
        }

        public async Task<bool> ExecuteAsync()
        {
            var result = false;

            if (BeforeExecute != null)
            {
                BeforeExecute(this, new BeforeExecuteEventArgs());
            }

            try
            {
                result = await DoExecuteAsync();
            }
            catch (Exception ex)
            {
                LogFactory.CreateLog().LogError(ex.Message, ex);

                if (ExecuteError != null)
                {
                    ExecuteError(this, new ExecuteErrorEventArgs(ex, true));
                }
            }

            if (AfterExecute != null)
            {
                AfterExecute(this, new AfterExecuteEventArgs());
            }

            return result;
        }
    }
}
