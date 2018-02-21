// =================================================================================================================
// Paina Solutions
// DokuFlex
// =================================================================================================================
// ©2013 DokuFlex. All rights reserved. Certain content used with permission from contributors
// http://www.dokuflex.com/apps/officetools/license/contributors
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance
// with the License. You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software distributed under the License is
// distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and limitations under the License.
// =================================================================================================================

namespace DokuFlex.Scan
{
    using Saraff.Twain;

    using DokuFlex.Windows.Common.Log;
    using System;

    public static class TwainFactory
    {
        private static readonly Twain32 _twain;

        static TwainFactory()
        {
            _twain = new Twain32();
            _twain.ShowUI = false;
            _twain.OpenDSM();
        }

        public static Twain32 Create()
        {
            return _twain;
        }
    }
}
