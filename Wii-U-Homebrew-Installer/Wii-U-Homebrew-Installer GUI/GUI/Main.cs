using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Reflection;
using System.IO.Compression;
using System.Net;
using Microsoft.VisualBasic.FileIO;

namespace Wii_U_Homebrew_Installer_GUI.GUI
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            if (Classes.WebCheck.CheckForInternetConnection() == false)
            {
                MessageBox.Show("There is no Internet connection! Select OK to search for the offline zip packaged from the releases.", "Waring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                OpenFileDialog ofd = new OpenFileDialog
                {
                    InitialDirectory = Directory.GetCurrentDirectory(),
                    FileName = "Wii-U",
                    DefaultExt = ".zip",
                    Filter = "ZIP file|*.zip",
                    CheckFileExists = true,
                    CheckPathExists = true,
                    Title = "Search for the offline zip file.",
                    Multiselect = false
                };
                DialogResult result = ofd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string file = ofd.FileName;
                    string directory = Directory.GetCurrentDirectory();
                    ZipFile.ExtractToDirectory(file, directory);
                    goto Extracted;
                }
            }
            else
            {
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile("https://lord-giganticus.github.io/Homebrew-Installer-Csharp/files/Wii-U.zip", "Wii-U.zip");
                }
                ZipFile.ExtractToDirectory("Wii-U.zip", Directory.GetCurrentDirectory());
                goto Extracted;
            }
        Extracted:
            if (!Directory.Exists("Copy"))
            {
                Directory.CreateDirectory("Copy");
            }
            Directory.Move("wiiu", @"Copy\wiiu");
            Directory.Move("haxchi", @"Copy\haxchi");
            Directory.Move("cbhc", @"Copy\cbhc");
            Directory.SetCurrentDirectory("Copy");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Directory.SetCurrentDirectory("Copy");
            string Copy_Directory = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            string sd_card = @"C:\";
            foreach (DriveInfo d in allDrives)
            {
                if (d.DriveType == DriveType.Removable)
                {
                    sd_card = d.Name;
                }
            }
            SaveFileDialog save = new SaveFileDialog
            {
                InitialDirectory = sd_card,
                Title = "Search for the drive to save",
                DefaultExt = ".txt",
                Filter = "Blank Text File|*.txt",
                FileName = "Blank",
                CheckPathExists = true
            };
            DialogResult result = save.ShowDialog();
            if (result == DialogResult.OK)
            {
                string folder = Path.GetDirectoryName(save.FileName);
                Copy(Copy_Directory, folder);

            }
        }
        /// <summary>
        /// Copys one dir to another by calling CopyAll. From https://stackoverflow.com/a/690980
        /// </summary>
        /// <param name="sourceDirectory">Initial Directory</param>
        /// <param name="targetDirectory">End Directory</param>
        public static void Copy(string sourceDirectory, string targetDirectory)
        {
            DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
            DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);

            CopyAll(diSource, diTarget);
        }
        /// <summary>
        /// Called by Copy to do all the work. From https://stackoverflow.com/a/690980
        /// </summary>
        /// <param name="source">Source dir</param>
        /// <param name="target">Targer dir</param>
        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            // Copy each file into the new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }
    }
}
