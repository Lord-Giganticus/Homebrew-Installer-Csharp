using System.Net;
using System.Diagnostics;
using System.IO;
using System;


namespace Wii_or_VWii_Homebrew_Installer
{
    class Wii
    {
        public static void Make_Text()
        {
            using (StreamWriter sw = File.CreateText("wii.txt"))
            {
                sw.WriteLine("Generated for Wii Homebrew.");
                sw.Close();
            }
        }
        public static void Download()
        {
            
            using (var client = new WebClient())
            {
                client.DownloadFile("https://wii.guide/assets/files/homebrew_browser_v0.3.9e.zip", "homebrew_browser.zip");
                client.DownloadFile("https://wii.guide/assets/files/Priiloader_v0_9.zip", "Priiloader.zip");
                client.DownloadFile("https://mattamech.github.io/Wii-or-VWii-Homebrew-Installer/hackmii_installer_v1.2.1.zip", "hackmii_installer_v1.2.zip");
                client.DownloadFile("http://stahlworks.com/dev/unzip.exe", "unzip.exe");
            }
            return;
        }
        public static void Extract()
        {
            Environment.CurrentDirectory = Directory.GetCurrentDirectory();
            Process.Start("CMD.exe", "/c unzip *.zip & del /f *.zip & del /f *.txt & del /f unzip.exe & exit").WaitForExit();
            return;
        }
        public static void Move()
        {
            Environment.CurrentDirectory = Directory.GetCurrentDirectory();
            Process.Start("CMD.exe", "/c mkdir Copy_to_SD & robocopy /E homebrew_browser apps & RD /S /Q homebrew_browser & RD /S /Q \"Homebrew Browser Guide and Help\" & move boot.elf Copy_to_SD & move bootmini.elf Copy_to_SD & move apps Copy_to_SD & move extra Copy_to_SD & exit").WaitForExit();
        }
    }
}
