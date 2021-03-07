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

namespace Wii_U_Homebrew_Installer_GUI.GUI
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            progressBar1.Value = 0;
            progressBar1.Maximum = 100;
            progressBar1.Step = 100;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            if(Classes.WebCheck.CheckForInternetConnection() == false)
            {
                MessageBox.Show("There is no Internet connection! Select OK to search for the offline zip packaged from the releases.","Waring",MessageBoxButtons.OK,MessageBoxIcon.Warning);
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
                string file = ofd.FileName;
                string directory = Assembly.GetEntryAssembly().Location;
                ZipFile.ExtractToDirectory(file, directory);
            }
        }
    }
}
