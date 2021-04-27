using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Wii.or.VWii.Homebrew.Installer.CLI.Classes
{
    class WebCheck
    {
        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://google.com/generate_204"))
                    return true;
            }
            catch
            {
                return false;
            }
        }

        public static Task<bool> CheckForInternetConnectionAsync() =>
            Task.Run(() => CheckForInternetConnection());
    }
}
