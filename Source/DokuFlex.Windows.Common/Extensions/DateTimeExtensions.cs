// =================================================================================================================
// Paina Solutions
// DokuFlex
// =================================================================================================================
// ©2013 DokuFlex. All rights reserved. Certain content used with permission from contributors
// http://www.dokuflex.com/windowsapps/license/contributors
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance 
// with the License. You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software distributed under the License is 
// distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and limitations under the License.
// =================================================================================================================

namespace DokuFlex.Windows.Common.Extensions
{
    using System;

    public static class DateTimeExtensions
    {
        public static long ToUnixEpoch(this DateTime date)
        {
            var unixEpoch = new DateTime(1970, 1, 1);
            var timeSpan = date.Subtract(unixEpoch);
            var epochMillisSpan = timeSpan.Ticks / 10000;

            return Convert.ToInt64(epochMillisSpan);
        }

        public static DateTime FromUnixEpoch(this long millisecons)
        {
            var unixEpoch = new DateTime(1970, 1, 1);
            DateTime dt = unixEpoch + TimeSpan.FromMilliseconds(millisecons);
            return dt;
        }
    }
}
