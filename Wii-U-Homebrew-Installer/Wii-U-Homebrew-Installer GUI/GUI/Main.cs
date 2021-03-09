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
        public bool run = Properties.Settings.Default.Run_Once;
        private void Main_Load(object sender, EventArgs e)
        {
            if (run)
            {
                if (!Directory.Exists("Copy"))
                {
                    goto Start;
                } else
                {
                    return;
                }
            } else
            {
                goto Start;
            }
        Start:
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
            Directory.SetCurrentDirectory("Copy");
            string Copy_Directory = Directory.GetCurrentDirectory();
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            string sd_card = @"C:\";
            foreach (DriveInfo d in allDrives)
            {
                if (d.DriveType == DriveType.Removable)
                {
                    if (!Directory.Exists(d.Name))
                    {
                        continue;
                    } else
                    {
                        sd_card = d.Name;
                        break;
                    }
                }
            }
            FolderBrowserDialog dialog = new FolderBrowserDialog
            {
                SelectedPath = sd_card,
                Description = "Search for the drive you want to save to.",
                UseDescriptionForTitle = true,
                AutoUpgradeEnabled = true,
                RootFolder = Environment.SpecialFolder.Desktop
            };
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string save_dir = dialog.SelectedPath;
                Copy(Copy_Directory,save_dir);
                MessageBox.Show("Complete!");
                run = true;
                Properties.Settings.Default.Run_Once = run;
                Properties.Settings.Default.Save();
                Close();
            } else
            {
                return;
            }
        }
    }
}
