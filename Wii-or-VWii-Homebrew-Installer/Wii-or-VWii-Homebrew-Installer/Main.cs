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
                if (Copier.Checker("wii.txt") == true)
                {
                    Console.WriteLine("This folder was made for Wii Homebrew. If this is correct enter 1. If not enter 2.");
                    Question();
                }
                else if (Copier.Checker("vwii.txt") == true)
                {
                    Console.WriteLine("This folder was made for VWii Homebrew. If this is correct enter 1. If not enter 2.");
                    Question();
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
                //pass
            }
            Console.WriteLine("Chose which Wii variant you want to ready your sd card for:\n[1]Wii\n[2]VWii\n");
            string Choice = Console.ReadLine();
            if (Choice == "1")
            {
                Wii.Make_Text();
                Wii.Download();
                Wii.Extract();
                Wii.Move();
                goto Copier;
            } else if (Choice == "2")
            {
                VWii.Make_Text();
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
