using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wii.U.Homebrew.Installer.CLI.Classes
{
    static class Extensions
    {
        public static List<T> ToList<T>(this T[] ts)
        {
            var l = new List<T>();
            foreach (var i in ts)
                l.Add(i);
            return l;
        }
    }
}
