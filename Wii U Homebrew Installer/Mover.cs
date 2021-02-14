using System;
using System.Diagnostics;
using System.IO;

namespace Wii_U_Homebrew_Installer
{
    class Mover
    {
        public static void Move()
        {
            Environment.CurrentDirectory = Directory.GetCurrentDirectory();
            Process process = Process.Start("CMD.exe", "/c move wiiu Copy_to_SD & move cbhc Copy_to_SD & tar -xf Haxchi-Installer.tar.gz & del /f *.gz & del /f tar.exe & move haxchi Copy_to_SD & exit");
            process.WaitForExit();
            return;
        }
    }
}
