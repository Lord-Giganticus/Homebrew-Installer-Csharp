using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wii_U_Homebrew_Installer_GUI
{
    public partial class Start : Form
    {
        public Start()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GUI.AboutBox about = new GUI.AboutBox();
            about.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            GUI.Main main = new GUI.Main();
            main.ShowDialog();
        }
    }
}
