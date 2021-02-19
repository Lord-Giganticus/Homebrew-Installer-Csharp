using System;
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
                Console.WriteLine("Copy_to_SD folder dectected. Checking what variant it was made for.");
                Thread.Sleep(2000);
                string CWD = Directory.GetCurrentDirectory();
                Directory.SetCurrentDirectory("Copy_to_SD");
                if (Copier.Checker("wii.txt") == true)
                {
                    Console.WriteLine("This folder was made for Wii Homebrew. If this is correct enter 1. If not enter 2.");
                    Question();
                    Directory.SetCurrentDirectory(CWD);
                }
                else if (Copier.Checker("vwii.txt") == true)
                {
                    Console.WriteLine("This folder was made for VWii Homebrew. If this is correct enter 1. If not enter 2.");
                    Question();
                    Directory.SetCurrentDirectory(CWD);
                }
                else if (Copier.Checker("wiiu.txt") == true)
                {
                    Console.WriteLine("This folder was made for Wii U Homebrew! This program is not compatable with this. Exiting.");
                    Thread.Sleep(2000);
                    Environment.Exit(1);
                }
                else
                {
                    Environment.Exit(2);
                }
                goto Copier;
            }
            else
            {
                goto Start;
            }
            Start:
            Console.WriteLine("Chose which Wii variant you want to ready your sd card for:\n[1]Wii\n[2]VWii\n");
            string Choice = Console.ReadLine();
            if (Choice == "1")
            {
                string CWD = Directory.GetCurrentDirectory();
                Directory.SetCurrentDirectory("Copy_to_SD");
                Wii.Make_Text();
                Directory.SetCurrentDirectory(CWD);
                Wii.Download();
                Wii.Extract();
                Wii.Move();
                goto Copier;
            } else if (Choice == "2")
            {
                string CWD = Directory.GetCurrentDirectory();
                Directory.SetCurrentDirectory("Copy_to_SD");
                VWii.Make_Text();
                Directory.SetCurrentDirectory(CWD);
                VWii.Download();
                VWii.Extract();
                VWii.Move();
                goto Copier;
            }
            else
            {
                Console.WriteLine("Improper Input. Exiting.");
                Environment.Exit(1);
            }
        Copier:
            Copier.Copy();
        }
        private static void Question()
        {
            string v = Console.ReadLine();
            if (v == "1")
            {
                Thread.Sleep(1000);
                return;
            }
            else if (v == "2")
            {
                Environment.Exit(1);
            }
            else
            {
                Environment.Exit(2);
            }
        }
    }
}
