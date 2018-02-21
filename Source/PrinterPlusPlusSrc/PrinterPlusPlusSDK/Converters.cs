/*
Printer++ Virtual Printer Processor
Copyright (C) 2012 - Printer++

This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
*/
//using System;
//using System.Diagnostics;
//using System.Threading;

//namespace PrinterPlusPlusSDK
//{
//    public class Converters
//    {
//        //private Process p;
//        //private int elapsedTime;
//        //private bool eventHandled;
//        //public void PostscriptToText(string psFilename, string txtFilename)
//        //{
//        //    var appDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
//        //    var fileName = string.Format("{0}\\Converters\\ps2txt.exe", appDir);
//        //    try
//        //    {
//        //        p = new Process();
//        //        // Start a process to print a file and raise an event when done.
//        //        p.StartInfo.FileName = fileName;
//        //        p.StartInfo.Arguments = string.Format("\"{0}\" \"{1}\"", psFilename, txtFilename);
//        //        p.StartInfo.CreateNoWindow = true;
//        //        p.EnableRaisingEvents = true;
//        //        p.Exited += new EventHandler(p_Exited);
//        //        p.Start();

//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        Console.WriteLine("An error occurred trying to print \"{0}\":" + "\n" + ex.Message, fileName);
//        //        return;
//        //    }

//        //    // Wait for Exited event, but not more than 30 seconds.
//        //    const int SLEEP_AMOUNT = 100;
//        //    while (!eventHandled)
//        //    {
//        //        elapsedTime += SLEEP_AMOUNT;
//        //        if (elapsedTime > 30000)
//        //        {
//        //            break;
//        //        }
//        //        Thread.Sleep(SLEEP_AMOUNT);
//        //    }
//        //}
//        //// Handle Exited event and display process information.
//        //private void p_Exited(object sender, System.EventArgs e)
//        //{
//        //    eventHandled = true;
//        //}

//        public static string PSToTxt(string psFilename)
//        {
//            var retVal = string.Empty;
//            var errorMessage = string.Empty;

//            var command = "C:\\PrinterPlusPlus\\Converters\\gs\\gswin32c";
//            var args = string.Format("-q -dNODISPLAY -P- -dSAFER -dDELAYBIND -dWRITESYSTEMDICT -dSIMPLE \"c:\\PrinterPlusPlus\\Converters\\gs\\ps2ascii.ps\" \"{0}\" -c quit", psFilename);
//            retVal = Shell.ExecuteShellCommand(command, args, ref errorMessage);
//            return retVal;
//        }

        
        



//    }
//}
