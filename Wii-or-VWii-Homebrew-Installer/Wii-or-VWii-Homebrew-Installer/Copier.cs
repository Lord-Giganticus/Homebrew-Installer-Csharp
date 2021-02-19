using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Wii_or_VWii_Homebrew_Installer
{
    class Copier
    {
        public static bool Checker(string args)
        {
            string CWD = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory("Copy_to_SD");
            if (File.Exists(args) == true)
            {
                Directory.SetCurrentDirectory(CWD);
                return true;
            } else
            {
                Directory.SetCurrentDirectory(CWD);
                return false;
            }
        }
        public static void Copy()
        {
            Environment.CurrentDirectory = Directory.GetCurrentDirectory();
            Console.WriteLine("Enter the drive you want to copy the files to:");
            string drive = Console.ReadLine();
            Process process = Process.Start("CMD.exe", "/c robocopy /E Copy_to_SD \"" + drive + "\"");
            process.WaitForExit();
            Console.WriteLine("Complete. Exiting.");
            Thread.Sleep(5000);
            Environment.Exit(0);
        }
    }
}
