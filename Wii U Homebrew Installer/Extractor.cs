using System.Diagnostics;
using System.IO;
using System;

namespace Wii_U_Homebrew_Installer
{
    class Extractor
    {
        public static void Extract()
        {
            Environment.CurrentDirectory = Directory.GetCurrentDirectory();
            Process process = Process.Start("CMD.exe", "/c unzip *zip & del /f *.zip & del /f *.install & del /f *.png & del /f *.json & del /f unzip.exe & RD /S /Q haxchi & exit");
            process.WaitForExit();
            return;
        }
    }
}
