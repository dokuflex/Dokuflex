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

namespace DokuFlex.IconOverlayHandlersBase
{
    using System;
    using System.Runtime.InteropServices;
    using Microsoft.Win32;
    using System.IO;
    using DokuFlex.FileSync;

    // This is set to false as we should not attempt to register this assembly with COM
    [ComVisible(false)]
    [Guid("8cbdfce5-97b4-44f4-8607-b4d900248662")]
    public class IconOverlayHandlerBase : IShellIconOverlayIdentifier
    {
        protected virtual string OverlayIconFilePath
        {
            get
            {
                return Path.Combine(new string[] 
                { 
                    Path.GetPathRoot(Environment.SystemDirectory),
                    "ProgramData",
                    "DokuFlex",
                    "IconOverlays"
                }).ToLowerInvariant();
            }
        }

        protected virtual int Priority
        {
            get
            {
                return 0;  // 0-100 (0 is highest priority)
            }
        }

        protected virtual SyncTableItemState ItemState 
        {
            get
            {
                return SyncTableItemState.InSync;
            }
        }

        public int IsMemberOf(string path, uint attributes)
        {
            SyncTableManager.Reload();
            
            var item = SyncTableManager.GetByPath(path);

            if (item == null)
            {
                unchecked
                {
                    return (int)HRESULT.S_FALSE;
                }
            }

            try
            {
                unchecked
                {
                    return item.State == ItemState ? (int)HRESULT.S_OK : (int)HRESULT.S_FALSE;
                }
            }
            catch
            {
                unchecked
                {
                    return (int)HRESULT.E_FAIL;
                }
            }
        }

        public int GetOverlayInfo(IntPtr iconFileBuffer, int iconFileBufferSize, out int iconIndex, out uint flags)
        {
            string fname = OverlayIconFilePath;

            int bytesCount = System.Text.Encoding.Unicode.GetByteCount(fname);

            byte[] bytes = System.Text.Encoding.Unicode.GetBytes(fname);

            if (bytes.Length + 2 < iconFileBufferSize)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    Marshal.WriteByte(iconFileBuffer, i, bytes[i]);
                }
                //write the "\0\0"
                Marshal.WriteByte(iconFileBuffer, bytes.Length, 0);
                Marshal.WriteByte(iconFileBuffer, bytes.Length + 1, 0);
            }

            iconIndex = 0;
            flags = (int)(HFLAGS.ISIOI_ICONFILE | HFLAGS.ISIOI_ICONINDEX);
            return (int)HRESULT.S_OK;
        }

        public int GetPriority(out int priority)
        {
            priority = Priority;
            return (int)HRESULT.S_OK;
        }

        [ComRegisterFunction]
        public static void Register(Type t)
        {
            RegistryKey rk = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\ShellIconOverlayIdentifiers\" + t.Name);
            rk.SetValue(string.Empty, t.GUID.ToString("B").ToUpper());
            rk.Close();
            ShellInterop.SHChangeNotify(0x08000000, 0, IntPtr.Zero, IntPtr.Zero);
        }

        [ComUnregisterFunction]
        public static void Unregister(Type t)
        {
            Registry.LocalMachine.DeleteSubKeyTree(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\ShellIconOverlayIdentifiers\" + t.Name);
            ShellInterop.SHChangeNotify(0x08000000, 0, IntPtr.Zero, IntPtr.Zero);
        }
    }
}
