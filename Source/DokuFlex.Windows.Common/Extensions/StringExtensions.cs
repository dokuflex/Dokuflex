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

namespace DokuFlex.Windows.Common.Extensions
{
    using System.Drawing.Imaging;

    public static class StringExtensions
    {
        public static ImageFormat ToImageFormat(this string fileType)
        {
            var imgFormat = ImageFormat.MemoryBmp;

            var str = fileType.ToLowerInvariant();

            switch (str)
            {
                case "bmp":

                case ".bmp":
                    imgFormat = ImageFormat.Bmp;
                    break;

                case "png":

                case ".png":
                    imgFormat = ImageFormat.Png;
                    break;

                case "tiff":

                case ".tiff":
                    imgFormat = ImageFormat.Tiff;
                    break;

                case "jpeg":

                case ".jpeg":
                    imgFormat = ImageFormat.Jpeg;
                    break;

                default:
                    break;
            }

            return imgFormat;
        }
    }
}
