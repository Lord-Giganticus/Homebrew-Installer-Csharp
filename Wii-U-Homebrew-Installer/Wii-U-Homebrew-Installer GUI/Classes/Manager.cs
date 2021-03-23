using System.IO;

namespace Wii_U_Homebrew_Installer_GUI.Classes
{
    class Manager
    {
        public void ExtractResource(string name, byte[] array)
        {
            File.WriteAllBytes(name, array);
        }
    }
}
