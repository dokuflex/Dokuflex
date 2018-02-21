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
    using System.Collections.Generic;
    using System.Windows.Forms;
    using System.Linq;
    using System.Threading.Tasks;
    using DokuFlex.Common.Log;

    public class CommandProcessor
    {
        private Queue<Command> _commands;
        private Queue<Command> _asyncCommands;
        private bool _pause;
        private bool _processing;
        private bool _error;

        private void ProcessCommands()
        {
            Command.SourcePath = string.Empty;

            while (_commands.Any() && !_pause)
            {
                var command = _commands.Peek();

                if (command is ICommandFacade)
                {
                    if ((command as ICommandFacade).CanExecute())
                    {
                        command.Execute();
                    }
                }
                else
                {
                    command.Execute();
                }

                try
                {
                    if (!_error) _commands.Dequeue();
                }
                catch (Exception ex)
                {
                    _pause = true;
                    var newMsg = string.Format("Call to _commands.Remove(command) throw and exception. Exception: {0}", ex.Message);
                    LogFactory.CreateLog().LogError(newMsg, ex);
                    throw new Exception(newMsg);
                }
            }
        }

        private async Task<int> ProcessCommandsAsync()
        {
            Command.SourcePath = string.Empty;
            var commandsCount = 0;

            while (_asyncCommands.Any() && !_pause)
            {
                var result = false;
                var command = _asyncCommands.Peek();

                if (command is ICommandFacade)
                {
                    if ((command as ICommandFacade).CanExecute())
                    {
                        result = await command.ExecuteAsync();
                    }
                }
                else
                {
                    result = await command.ExecuteAsync();
                }

                if (result) commandsCount++;

                try
                {
                    if (!_error) _asyncCommands.Dequeue();
                }
                catch (Exception ex)
                {
                    _pause = true;
                    var newMsg = string.Format("Call to _commands.Remove(command) throw and exception. Exception: {0}", ex.Message);
                    LogFactory.CreateLog().LogError(newMsg, ex);
                    throw new Exception(newMsg);
                }
            }

            return commandsCount;
        }

        public CommandProcessor()
        {
            _commands = new Queue<Command>();
            _asyncCommands = new Queue<Command>();
        }

        public bool Executing
        {
            get
            {
                return _processing;
            }
        }

        public Command CurrentCommand
        {
            get { return _commands.FirstOrDefault(); }
        }

        public void AddCommand(Command command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            command.ExecuteError += new ExecuteErrorEventHandler(OnExecuteError);

            if (command.IsAsync)
            {
                _asyncCommands.Enqueue(command);
            }
            else
            {
                _commands.Enqueue(command);
            }
        }

        private void OnExecuteError(object sender, ExecuteErrorEventArgs e)
        {
            _error = true;
            _pause = false;

            if (e.AsyncCommand)
                _asyncCommands.Dequeue();
            else
                _commands.Dequeue();
        }

        public void ExecuteCommands()
        {
            if (_processing)
            {
                return;
            }

            _processing = true;
            _error = false;
            _pause = false;

            try
            {
                ProcessCommands();
            }
            finally
            {
                _processing = false;
            }
        }

        public async Task<bool> ExecuteCommandsAsync()
        {
            if (_processing)
            {
                return false;
            }

            _processing = true;
            _error = false;
            _pause = false;

            try
            {
                ProcessCommands();
                return await ProcessCommandsAsync() > 0;
            }
            finally
            {
                _processing = false;
            }
        }

        public void Pause()
        {
            if (_processing) _pause = true;
        }
    }
}
