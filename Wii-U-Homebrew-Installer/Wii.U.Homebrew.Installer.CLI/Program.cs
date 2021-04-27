using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Reflection;
using Wii.U.Homebrew.Installer.CLI.Classes;
using Wii.U.Homebrew.Installer.CLI.Properties;
using System.Threading.Tasks;

namespace Wii.U.Homebrew.Installer.CLI
{
    class Program
    {
        static void Main() =>
            MainAsync().GetAwaiter().GetResult();

        static async Task MainAsync()
        {
            if (!WebCheck.CheckForInternetConnection())
                await File.WriteAllBytesAsync("Wii-u.Zip", Resources.Wii_U);
            else
                using (var client = new WebClient())
                    client.DownloadFile("https://lord-giganticus.github.io/Homebrew-Installer-Csharp/files/Wii-U.zip", "Wii-U.zip");
            ZipFile.ExtractToDirectory("Wii-U.zip", Directory.GetCurrentDirectory());
            if (!Directory.Exists("Copy"))
                Directory.CreateDirectory("Copy");
            Directory.Move("wiiu", @"Copy\wiiu");
            Directory.Move("haxchi", @"Copy\haxchi");
            Directory.Move("cbhc", @"Copy\cbhc");
            Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
            Directory.SetCurrentDirectory("Copy");
            string Copy_Directory = Directory.GetCurrentDirectory();
            string sd_card = "";
            var types = new List<DriveType>();
            foreach (var d in DriveInfo.GetDrives())
                types.Add(d.DriveType);
            while (!types.Contains(DriveType.Removable))
            {
                Console.WriteLine("Please insert your sd card and press any key when it's inserted.");
                Console.ReadKey();
                var t = new Dictionary<DriveType,DriveInfo>();
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
            Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
            Directory.Delete(Copy_Directory, true);
            File.Delete("Wii-U.zip");
            Console.WriteLine("Complete!");
        }
    }
}
