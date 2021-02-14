using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Wii_or_VWii_Homebrew_Installer
{
    class Program
    {
        static void Main()
        {
            if (Directory.Exists("Copy_to_SD"))
            {
                Console.WriteLine("Copy_to_SD folder dectected. Jumping to Copier code.");
                Thread.Sleep(2000);
                goto Copier;
            }
            else
            {
                //pass
            }
            Console.WriteLine("Chose which Wii Variant you want to ready your sd card for:\n[1]Wii\n[2]VWii\n");
            string Choice = Console.ReadLine();
            if (Choice == "1")
            {
                Wii.Download();
                Wii.Extract();
                Wii.Move();
                goto Copier;
            } else if (Choice == "2")
            {
                VWii.Download();
                VWii.Extract();
                VWii.Move();
                goto Copier;
            }
        Copier:
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
