using System;
using System.IO;
using System.Windows.Forms;
using System.Reflection;
using System.IO.Compression;
using System.Net;

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
                Classes.Manager manager = new Classes.Manager();
                manager.ExtractResource("Wii-U.zip",Properties.Resources.Wii_U_Zip);
                ZipFile.ExtractToDirectory("Wii-U.zip", Directory.GetCurrentDirectory());
                goto Extracted;
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
                Classes.Copier.DirectoryCopy(Copy_Directory,save_dir,true);
                MessageBox.Show("Complete!");
                if (!run)
                {
                    run = true;
                    Properties.Settings.Default.Run_Once = run;
                    Properties.Settings.Default.Save();
                }
                Close();
            } else
            {
                return;
            }
        }
    }
}
