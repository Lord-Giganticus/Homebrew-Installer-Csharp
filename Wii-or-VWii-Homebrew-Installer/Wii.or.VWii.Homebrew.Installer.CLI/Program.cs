using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Wii.or.VWii.Homebrew.Installer.CLI.Classes;
using Wii.or.VWii.Homebrew.Installer.CLI.Properties;

namespace Wii.or.VWii.Homebrew.Installer.CLI
{
    class Program
    {
        internal static string Assembly_Path { get => AppContext.BaseDirectory; }

        static void Main(string[] args) =>
            MainAsync(args).GetAwaiter().GetResult();

        static async Task MainAsync(string[] args)
        {
            if (!await WebCheck.CheckForInternetConnectionAsync())
            {
                await File.WriteAllBytesAsync("Wii.zip", Resources.Wii);
                await File.WriteAllBytesAsync("VWii.zip", Resources.VWii);
            } else
            {
                using var client = new WebClient();
                client.DownloadFile("https://lord-giganticus.github.io/Homebrew-Installer-Csharp/files/VWii.zip", "VWii.zip");
                client.DownloadFile("https://lord-giganticus.github.io/Homebrew-Installer-Csharp/files/Wii.zip", "Wii.zip");
            }
            if (args is not null)
            {
                if (args[0] is "Wii" || args[0] is "wii")
                {
                    ZipFile.ExtractToDirectory("Wii.zip", Directory.GetCurrentDirectory());
                    if (!Directory.Exists("Copy"))
                        Directory.CreateDirectory("Copy");
                    File.Move("boot.elf", "Copy/boot.elf");
                    File.Move("bootmini.elf", "Copy/bootmini.elf");
                    Directory.Move("apps", "Copy");
                } else if (args[0] is "VWii" || args[0] is "vwii")
                {
                    ZipFile.ExtractToDirectory("VWii.zip", Directory.GetCurrentDirectory());
                    if (!Directory.Exists("Copy"))
                        Directory.CreateDirectory("Copy");
                    File.Move("boot.elf", "Copy/boot.elf");
                    File.Move("bootmini.elf", "Copy/bootmini.elf");
                    Directory.Move("apps", "Copy");
                    Directory.Move("wiiu", "Copy");
                }
                Directory.SetCurrentDirectory("Copy");
                string Copy_Directory = Directory.GetCurrentDirectory();
                string sd_card = "";
                var types = new List<DriveType>();
                foreach (var d in DriveInfo.GetDrives())
                    types.Add(d.DriveType);
                if (types.Contains(DriveType.Removable))
                    sd_card = DriveInfo.GetDrives()[types.IndexOf(DriveType.Removable)].RootDirectory.FullName;
                while (!types.Contains(DriveType.Removable))
                {
                    Console.WriteLine("Please insert your sd card and press any key when it's inserted.");
                    Console.ReadKey();
                    var t = new Dictionary<DriveType, DriveInfo>();
                    foreach (var d in DriveInfo.GetDrives())
                        t.Add(d.DriveType, d);
                    if (t.ContainsKey(DriveType.Removable))
                    {
                        var dtlist = new List<DriveType>();
                        var dilist = new List<DriveInfo>();
                        foreach (var d in t.Values)
                        {
                            dtlist.Add(d.DriveType);
                            dilist.Add(d);
                        }
                        var index = dtlist.IndexOf(DriveType.Removable);
                        sd_card = dilist[index].RootDirectory.FullName;
                        break;
                    }
                    else
                        continue;
                }
                await Copier.DirectoryCopyAsync(Copy_Directory, sd_card, true);
                Directory.SetCurrentDirectory(Assembly_Path);
                Directory.Delete(Copy_Directory, true);
                File.Delete("VWii.zip");
                File.Delete("Wii.zip");
                Console.WriteLine("Complete!");
            } else
            {
                Console.WriteLine("Enter the console you want to install to (Wii or VWii)");
                if (Console.ReadLine() is "Wii" || Console.ReadLine() is "wii")
                {
                    ZipFile.ExtractToDirectory("Wii.zip", Directory.GetCurrentDirectory());
                    if (!Directory.Exists("Copy"))
                        Directory.CreateDirectory("Copy");
                    File.Move("boot.elf", "Copy/boot.elf");
                    File.Move("bootmini.elf", "Copy/bootmini.elf");
                    Directory.Move("apps", "Copy");
                } else if (Console.ReadLine() is "VWii" || Console.ReadLine() is "vwii")
                {
                    ZipFile.ExtractToDirectory("VWii.zip", Directory.GetCurrentDirectory());
                    if (!Directory.Exists("Copy"))
                        Directory.CreateDirectory("Copy");
                    File.Move("boot.elf", "Copy/boot.elf");
                    File.Move("bootmini.elf", "Copy/bootmini.elf");
                    Directory.Move("apps", "Copy");
                    Directory.Move("wiiu", "Copy");
                }
                Directory.SetCurrentDirectory("Copy");
                string Copy_Directory = Directory.GetCurrentDirectory();
                string sd_card = "";
                var types = new List<DriveType>();
                foreach (var d in DriveInfo.GetDrives())
                    types.Add(d.DriveType);
                if (types.Contains(DriveType.Removable))
                    sd_card = DriveInfo.GetDrives()[types.IndexOf(DriveType.Removable)].RootDirectory.FullName;
                while (!types.Contains(DriveType.Removable))
                {
                    Console.WriteLine("Please insert your sd card and press any key when it's inserted.");
                    Console.ReadKey();
                    var t = new Dictionary<DriveType, DriveInfo>();
                    foreach (var d in DriveInfo.GetDrives())
                        t.Add(d.DriveType, d);
                    if (t.ContainsKey(DriveType.Removable))
                    {
                        var dtlist = new List<DriveType>();
                        var dilist = new List<DriveInfo>();
                        foreach (var d in t.Values)
                        {
                            dtlist.Add(d.DriveType);
                            dilist.Add(d);
                        }
                        var index = dtlist.IndexOf(DriveType.Removable);
                        sd_card = dilist[index].RootDirectory.FullName;
                        break;
                    }
                    else
                        continue;
                }
                await Copier.DirectoryCopyAsync(Copy_Directory, sd_card, true);
                Directory.SetCurrentDirectory(Assembly_Path);
                Directory.Delete(Copy_Directory, true);
                File.Delete("VWii.zip");
                File.Delete("Wii.zip");
                Console.WriteLine("Complete!");
            }
        }
    }
}
