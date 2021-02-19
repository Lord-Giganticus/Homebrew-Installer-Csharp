using System.Net;
using System.IO;
using System;
using System.Threading;

namespace Wii_U_Homebrew_Installer
{
    class Program
    {
        static void Main()
        {
            if (Directory.Exists("Copy_to_SD"))
            {
                Console.WriteLine("Copy_to_SD folder dectected. Checking what variant it was made for.");
                Thread.Sleep(2000);
                string cwd = Directory.GetCurrentDirectory();
                Directory.SetCurrentDirectory("Copy_to_SD");
                if (Copier.Checker("wii.txt") == true)
                {
                    Console.WriteLine("This folder was made for Wii Homebrew. Therefore it is not compatable. Exiting.");
                    Environment.Exit(1);
                }
                else if (Copier.Checker("vwii.txt") == true)
                {
                    Console.WriteLine("This folder was made for VWii Homebrew. Therefore it is not compatable. Exiting.");
                    Environment.Exit(1);
                }
                else if (Copier.Checker("wiiu.txt") == true)
                {
                    Console.WriteLine("This folder was made for Wii U Homebrew. Moving on.");
                    Thread.Sleep(2000);
                    Directory.SetCurrentDirectory(cwd);
                }
                else
                {
                    Environment.Exit(2);
                }
                goto Copier;
            }
            else
            {
                goto Download;
            }
            Download:
            Console.WriteLine("Downloading files.");
            using (var client = new WebClient())
            {
                client.DownloadFile("http://wiiubru.com/appstore/zips/homebrew_launcher.zip", "homebrew_launcher.zip");
                client.DownloadFile("https://wiiubru.com/appstore/zips/appstore.zip", "appstore.zip");
                client.DownloadFile("https://www.wiiubru.com/appstore/zips/mocha.zip", "mocha.zip");
                client.DownloadFile("https://wiiubru.com/appstore/zips/haxchi.zip", "haxchi.zip");
                client.DownloadFile("https://wiiubru.com/appstore/zips/cbhc.zip", "cbhc.zip");
                client.DownloadFile("https://mattamech.github.io/Wii-U-Homebrew-Installer/cs/Haxchi-Installer.tar.gz", "Haxchi-Installer.tar.gz");
                client.DownloadFile("http://stahlworks.com/dev/unzip.exe", "unzip.exe");
                client.DownloadFile("http://stahlworks.com/dev/tar.exe", "tar.exe");
            }
            Directory.CreateDirectory("Copy_to_SD");
            string CWD = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory("Copy_to_SD");
            using (StreamWriter sw = File.CreateText("wiiu.txt"))
            {
                sw.WriteLine("Generated for Wii U Homebrew.");
                sw.Close();
            }
            Directory.SetCurrentDirectory(CWD);
            Console.WriteLine("Running .bat files.");
            Extractor.Extract();
            Mover.Move();
        Copier:
            Copier.Copy();
        }
    }
}
