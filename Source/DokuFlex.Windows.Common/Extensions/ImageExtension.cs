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
    using System;
    using System.Drawing;

    public static class ImageExtension
    {
        public static Image ToBlackAndWhite(this Image image)
        {
            var bitmap = new Bitmap(image);

            int x, y;

            // Loop through the images pixels to reset color. 
            for (x = 0; x < bitmap.Width; x++)
            {
                for (y = 0; y < bitmap.Height; y++)
                {
                    Color pixelColor = bitmap.GetPixel(x, y);

                    if (pixelColor == Color.White || pixelColor == Color.Black)
                    {
                        continue;
                    }

                    Color newColor = Color.FromArgb(pixelColor.R, 0, 0);
                    bitmap.SetPixel(x, y, newColor);
                }
            }

            return bitmap;
        }
    }
}
