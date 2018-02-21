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

namespace DokuFlex.Windows.Common.Services
{
    using System;

    public class RestResponseException
        : Exception
    {
        public RestResponseException(string message)
            : base(message)
        {
            ErrorCode = -1;
        }

        public RestResponseException(string message, int errorCode)
            : base(message)
        {
            ErrorCode = errorCode;
        }

        public RestResponseException(string message, int errorCode, string requestData)
            : this(message, errorCode)
        {
            RequestData = requestData;
        }

        public int ErrorCode { get; private set; }
        public string RequestData { get; private set; }
    }
}
