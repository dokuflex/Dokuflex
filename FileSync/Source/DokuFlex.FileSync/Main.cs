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

namespace DokuFlex.FileSync
{
    using DokuFlex.Common;
    using DokuFlex.Common.ServiceAgents;
    using DokuFlex.FileSync.Commands;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Timers;
    using System.Windows.Forms;

    public partial class Main : Form
    {
        private NotifyIcon _notifyIcon;
        private ContextMenu _contextMenu;
        private FileSystemWatcher _fileSystemWatcher;
        private System.Timers.Timer _timer;
        private Synchronizer _synchronizer;
        private bool _syncing;
        private int _timeInterval;
        private TimeSpan _timeOfDay;

        public event PauseSyncEventHandler PauseSync;

        public event ResumeSyncEventHandler ResumeSync;

        private void WatchingPath(string path)
        {
            _fileSystemWatcher.EnableRaisingEvents = false;

            if (!Directory.Exists(path)) return;

            _fileSystemWatcher.Path = path;
            _fileSystemWatcher.EnableRaisingEvents = true;
        }

        private void InitializeComponents()
        {
            _contextMenu = new ContextMenu();
            _contextMenu.Popup += OnPopup;

            var separator1MenuItem = new MenuItem { Text = "-" };

            var separator2MenuItem = new MenuItem { Text = "-" };

            var separator3MenuItem = new MenuItem { Text = "-" };

            var openYourFolderMenuItem = new MenuItem { Text = Resources.OpenYourFolderText };
            openYourFolderMenuItem.Click += openYourFolderMenuItem_Click;

            var syncNewUserGroupMenuItem = new MenuItem { Text = Resources.SyncANewFolderText };
            syncNewUserGroupMenuItem.Click += syncNewUserGroupMenuItem_Click;

            var syncNowMenuItem = new MenuItem { Text = Resources.SyncNowText };
            syncNowMenuItem.Click += syncNowMenuItem_Click;

            var pauseMenuItem = new MenuItem { Text = Resources.PauseText };
            pauseMenuItem.Click += pauseMenuItem_Click;

            var resumeMenuItem = new MenuItem { Text = Resources.ResumeText };
            resumeMenuItem.Click += resumeMenuItem_Click;

            var stopSyncingMenuItem = new MenuItem { Text = Resources.StopSyncingAFolderText };
            stopSyncingMenuItem.Click += stopSyncingMenuItem_Click;

            var changeCredentialsMenuItem = new MenuItem { Text = Resources.ChangeCredentialsText };
            changeCredentialsMenuItem.Click += changeCredentialsMenuItem_Click;

            var settingsMenuItem = new MenuItem { Text = Resources.SettingsText };
            settingsMenuItem.Click += settingsMenuItem_Click;

            var helpMenuItem = new MenuItem { Text = Resources.HelpText };
            helpMenuItem.Click += helpMenuItem_Click;

            var exitMenuItem = new MenuItem { Text = Resources.ExitText };
            exitMenuItem.Click += exitMenuItem_Click;

            var syncStatusItem = new MenuItem { Text = Resources.SyncStatusText};
            syncStatusItem.Click += syncStatusItem_Click;

            _contextMenu.MenuItems.AddRange(new MenuItem[]{openYourFolderMenuItem,
            syncNewUserGroupMenuItem, separator1MenuItem, syncNowMenuItem,
            pauseMenuItem, resumeMenuItem, stopSyncingMenuItem, syncStatusItem,
            separator2MenuItem, changeCredentialsMenuItem, separator3MenuItem, settingsMenuItem, helpMenuItem, exitMenuItem});

            _notifyIcon = new NotifyIcon
            {
                ContextMenu = _contextMenu,
                Text = Resources.ApplicationTitleText,
                Icon = Icon.ExtractAssociatedIcon("FileSync.ico"),
                Visible = true
            };

            _synchronizer = new Synchronizer();
            _synchronizer.BeforeSynchronize += OnBeforeSynchronize;
            _synchronizer.SynchronizationCompleted += OnSynchronizationCompleted;
            _synchronizer.SynchronizeError += OnSynchronizeError;
            this.PauseSync += _synchronizer.OnPauseSync;
            this.ResumeSync += _synchronizer.OnResumeSync;

            _timeInterval = int.Parse(ConfigurationManager.GetValue(Constants.SyncInteval));
            var hourStr = ConfigurationManager.GetValue(Constants.SyncHour);
            var dateTime = string.IsNullOrWhiteSpace(hourStr) ? DateTime.Now : DateTime.Parse(hourStr);
            _timeOfDay = dateTime.TimeOfDay;
            int elapsedTime = 0;

            switch (_timeInterval)
            {
                case 0: //1min
                    elapsedTime = 60000;
                    break;

                case 1: //6min
                    elapsedTime = 360000;
                    break;

                case 2: //12min
                    elapsedTime = 720000;
                    break;

                case 3: //1h
                    elapsedTime = 3600000;
                    break;

                case 4: //6h
                    elapsedTime = 21600000;
                    break;

                case 5: //12h
                    elapsedTime = 43200000;
                    break;

                case 6: //1 once at day
                    elapsedTime = 60000; //Fires every minute :)
                    break;

                default:
                    break;
            }

            _timer = new System.Timers.Timer() { Interval = elapsedTime, AutoReset = true };
            _timer.Elapsed += OnElapsed;
            _timer.Enabled = bool.Parse(ConfigurationManager.GetValue(Constants.AutoSync));

            //FileSystemWatcher
            _fileSystemWatcher = new FileSystemWatcher
            {
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName,
                Filter = "*.*",
                IncludeSubdirectories = true,
                InternalBufferSize = 65536
            };

            /* Watch for changes in CreationTime and LastWrite times, and
            the renaming of files or directories. */

            // Add event handlers.
            _fileSystemWatcher.Renamed += OnRenamed;
            _fileSystemWatcher.Error += OnError;

            var syncDirectoryPath = ConfigurationManager.GetValue(Resources.SyncDirectoryPathKey);

            WatchingPath(syncDirectoryPath);
        }

        private void syncStatusItem_Click(object sender, EventArgs e)
        {
            using (var syncStatusView = new SyncStatusView())
            {
                syncStatusView.ShowDialog();
            }
        }

        private void changeCredentialsMenuItem_Click(object sender, EventArgs e)
        {
            var ticket = String.Empty;
            var credentials = new Credentials() { UserName = ConfigurationManager.GetValue(Resources.LoginEmailAddressKey) };
            credentials.SetEncryptedPassword(ConfigurationManager.GetValue(Resources.LoginPasswordKey));

            //Empty password
            ConfigurationManager.SetValue(Resources.LoginPasswordKey, String.Empty);
            ConfigurationManager.Save();

            using (var loginView = new LoginView())
            {
                if (loginView.ShowLoginDialog())
                {
                    ticket = loginView.Ticket;
                }
            }

            if (String.IsNullOrWhiteSpace(ticket))
            {
                ConfigurationManager.SetValue(Resources.LoginEmailAddressKey, credentials.UserName);
                ConfigurationManager.SetValue(Resources.LoginPasswordKey, credentials.Password);
                ConfigurationManager.Save();
            }
            else
            {
                MessageBox.Show(Resources.ChangeCredentialsSuccessfullyText, Resources.ApplicationTitleText, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void NotifySyncStarted()
        {
            _notifyIcon.ShowBalloonTip(30000, Resources.ApplicationTitleText, Resources.SyncStartedTipText, ToolTipIcon.Info);
            _notifyIcon.Icon = Icon.ExtractAssociatedIcon("FileSync.ico");
        }

        private void NotifySyncError(string message)
        {
            _notifyIcon.ShowBalloonTip(30000, Resources.ApplicationTitleText, message, ToolTipIcon.Error);
            _notifyIcon.Icon = Icon.ExtractAssociatedIcon("FileSync.ico");
        }

        private void NotifySyncComplete()
        {
            _notifyIcon.ShowBalloonTip(30000, Resources.ApplicationTitleText,
                Resources.SyncCompleteTipText, ToolTipIcon.Info);
            _notifyIcon.Icon = Icon.ExtractAssociatedIcon("FileSync.ico");
        }

        private void OnError(object sender, ErrorEventArgs e)
        {
            NotifySyncError("Unknow error");
        }

        private async void OnRenamed(object sender, RenamedEventArgs e)
        {
            var item = SyncTableManager.GetByPath(e.OldFullPath);

            if (item != null)
            {
                var command = (Command)null;

                if (item.Type == "F")
                {
                    var folder = new DirectoryInfo(e.FullPath);

                    var ticket = await DokuFlexService.GetTicketAsync();

                    var topLevelPath = ConfigurationManager.GetValue(Resources.SyncDirectoryPathKey);

                    command = new RenameDirectoryCommand(ticket, folder.Name, e.FullPath, e.OldFullPath, topLevelPath);
                }
                else
                {
                    var file = new FileInfo(e.FullPath);

                    //check if file is open by a program
                    if (!file.IsLocked())
                    {
                        var ticket = await DokuFlexService.GetTicketAsync();

                        var topLevelPath = ConfigurationManager.GetValue(Resources.SyncDirectoryPathKey);

                        command = new RenameFileCommand(ticket, file.Name, e.FullPath, e.OldFullPath, topLevelPath);
                    }
                }

                //Attach error event handler
                command.ExecuteError += OnExecuteError;

                //Detect the synchronizer object state
                if (_syncing || _synchronizer.Paused)
                {
                    _synchronizer.Synchronize(command);
                }
                else
                {
                    await _synchronizer.SynchronizeAsync(command);
                }
            }
        }

        private void OnExecuteError(object sender, ExecuteErrorEventArgs e)
        {
            NotifySyncError(e.ErrorMessage);
        }

        private async void OnElapsed(object sender, ElapsedEventArgs e)
        {
            var timeSpan = DateTime.Now.TimeOfDay;
            var autoSync = bool.Parse(ConfigurationManager.GetValue(Constants.AutoSync));

            if (_syncing || _synchronizer.Paused) return;
            if (!autoSync) return;
            if (_timeInterval == 6)
            {
                if (_timeOfDay.Hours != timeSpan.Hours) return;
                if (_timeOfDay.Minutes != timeSpan.Minutes) return;
            }

            var syncDirectoryPath = ConfigurationManager.GetValue(Resources.SyncDirectoryPathKey);

            if (String.IsNullOrWhiteSpace(syncDirectoryPath)) return;

            try
            {
                var ticket = await Session.GetTikectAsync();

                if (string.IsNullOrWhiteSpace(ticket)) return;

                var result = await _synchronizer.SynchronizeAsync(ticket, syncDirectoryPath);
            }
            catch (Exception ex)
            {
                NotifySyncError(ex.Message);
            }
        }

        private void OnSynchronizationCompleted(object sender, SynchronizationCompletedEventArgs e)
        {
            _syncing = false;
            NotifySyncComplete();
        }

        private void OnSynchronizeError(object sender, SynchronizeErrorEventArgs e)
        {
            _syncing = false;
            NotifySyncError(e.ErrorMessage);
        }

        private void OnBeforeSynchronize(object sender, BeforeSynchronizeEventArgs e)
        {
            _syncing = true;
            NotifySyncStarted();
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void helpMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("HTTP://wwww.dokuflex.com");
        }

        private void settingsMenuItem_Click(object sender, EventArgs e)
        {
            using (var settingsView = new SettingsView())
            {
                settingsView.ShowDialog();
            }
        }

        private void stopSyncingMenuItem_Click(object sender, EventArgs e)
        {
            using (var stopSyncingView = new StopSyncingView())
            {
                stopSyncingView.ShowDialog();
            }
        }

        private void resumeMenuItem_Click(object sender, EventArgs e)
        {
            if (ResumeSync != null)
            {
                ResumeSync(this, new ResumeSyncEventArgs());
            }
        }

        private void pauseMenuItem_Click(object sender, EventArgs e)
        {
            if (PauseSync != null)
            {
                PauseSync(this, new PauseSyncEventArgs());
            }
        }

        private async void syncNowMenuItem_Click(object sender, EventArgs e)
        {
            if (_syncing) return;

            var syncDirectoryPath = ConfigurationManager.GetValue(Resources.SyncDirectoryPathKey);

            if (String.IsNullOrWhiteSpace(syncDirectoryPath)) return;

            try
            {
                var ticket = await Session.GetTikectAsync();

                if (string.IsNullOrWhiteSpace(ticket)) return;

                var result = await _synchronizer.SynchronizeAsync(ticket, syncDirectoryPath);
            }
            catch (Exception ex)
            {
                NotifySyncError(ex.Message);
            }

            _timer.Start();
        }

        private async void syncNewUserGroupMenuItem_Click(object sender, EventArgs e)
        {
            var ticket = Session.GetTikect();

            if (string.IsNullOrWhiteSpace(ticket)) return;

            var group = (UserGroup)null;
            var syncDirectory = String.Empty;

            using (var syncNewUserGroup = new SyncNewUserGroupView(ticket))
            {
                if (syncNewUserGroup.ShowDialog() == DialogResult.OK)
                {
                    group = syncNewUserGroup.Group;
                    syncDirectory = syncNewUserGroup.SyncDirectory;
                }
                else
                {
                    return;
                }
            }

            var directory = string.Format("{0}\\{1}", syncDirectory, group.name);
            var fileFolderInfoList = new List<FileFolderInfo>();
            var fileFolderInfo = new FileFolderInfo()
            {
                GroupId = group.id,
                Path = directory,
                SyncFolder = true
            };

            fileFolderInfoList.Add(fileFolderInfo);

            using (var syncProgress = new SyncProgressView())
            {
                if (syncProgress.SyncNewUserGroup(ticket, directory, group))
                {
                    fileFolderInfoList.AddRange(syncProgress.FileFolderList);
                }
            }

            //Detect the synchronizer object state
            if (_syncing || _synchronizer.Paused)
            {
                _synchronizer.Synchronize(fileFolderInfoList);
            }
            else
            {
                await _synchronizer.SynchronizeAsync(ticket, directory, fileFolderInfoList);
            }

            WatchingPath(syncDirectory);
        }

        private void openYourFolderMenuItem_Click(object sender, EventArgs e)
        {
            var syncDirectoryPath = ConfigurationManager.GetValue(Resources.SyncDirectoryPathKey);

            if (String.IsNullOrWhiteSpace(syncDirectoryPath))
            {
                return;
            }

            Process.Start(syncDirectoryPath);
        }

        public async Task SyncANewUserGroupAsync(string p1, string p2, string p3)
        {
            var ticket = Session.GetTikect();

            if (string.IsNullOrWhiteSpace(ticket)) return;

            var group = new UserGroup() { id = p1, name = p2 };
            var syncDirectory = ConfigurationManager.GetValue(Resources.SyncDirectoryPathKey);

            if (string.IsNullOrWhiteSpace(syncDirectory))
                using (var syncNewUserGroup = new SyncNewUserGroupView(ticket, group))
                {
                    if (syncNewUserGroup.ShowDialog() == DialogResult.OK)
                    {
                        syncDirectory = syncNewUserGroup.SyncDirectory;
                    }
                    else
                    {
                        return;
                    }
                }

            if (SyncTableManager.GetByGroupId(p3) != null) return;

            var directory = string.Format("{0}\\{1}", syncDirectory, group.name);
            var fileFolderInfoList = new List<FileFolderInfo>();
            var fileFolderInfo = new FileFolderInfo()
            {
                GroupId = group.id,
                Path = directory,
                SyncFolder = true
            };

            fileFolderInfoList.Add(fileFolderInfo);

            using (var syncProgress = new SyncProgressView())
            {
                if (syncProgress.SyncNewUserGroup(ticket, directory, group))
                {
                    fileFolderInfoList.AddRange(syncProgress.FileFolderList);
                }
            }

            //Detect the synchronizer object state
            if (_syncing || _synchronizer.Paused)
            {
                _synchronizer.Synchronize(fileFolderInfoList);
            }
            else
            {
                await _synchronizer.SynchronizeAsync(ticket, directory, fileFolderInfoList);
            }

            WatchingPath(syncDirectory);
        }

        private void OnPopup(object sender, EventArgs e)
        {
            _contextMenu.MenuItems[3].Enabled = !_syncing;
            _contextMenu.MenuItems[4].Enabled = _contextMenu.MenuItems[4].Visible = !_synchronizer.Paused;
            _contextMenu.MenuItems[5].Enabled = _contextMenu.MenuItems[5].Visible = _synchronizer.Paused;
            _contextMenu.MenuItems[6].Enabled = SyncTableManager.ContainsFolders();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            BackgroundImage = Image.FromFile("DokuFlex.png");
        }

        public Main()
        {
            InitializeComponent();
            InitializeComponents();
        }

        private void Main_Shown(object sender, EventArgs e)
        {
            Hide();
        }
    }

    public class PauseSyncEventArgs
        : EventArgs
    {

    }

    public class ResumeSyncEventArgs
        : EventArgs
    {

    }

    public delegate void PauseSyncEventHandler(object sender, PauseSyncEventArgs e);

    public delegate void ResumeSyncEventHandler(object sender, ResumeSyncEventArgs e);
}
