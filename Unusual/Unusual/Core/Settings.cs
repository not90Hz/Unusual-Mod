using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnusualMod.Core
{
    internal class Settings
    {
        public static void Load()
        {
            string[] lines = File.ReadAllLines(Config.ConfigPath);
            int i = 0;
            foreach(string line in lines)
            {
                Compare(line);
            }
        }
        public static void Save()
        {
            string[] list = new string[]
            {
                string.Format("{0}{1}", Config.ConfigStrings[0], Config.Flight.ToString()),
                string.Format("{0}{1}", Config.ConfigStrings[1], Config.Noclip.ToString()),
                string.Format("{0}{1}", Config.ConfigStrings[2], Config.RPCEnabled.ToString()),
                string.Format("{0}{1}", Config.ConfigStrings[3], Config.ESPEnabled.ToString()),
            };
            File.WriteAllLines(Config.ConfigPath, list);
        }

        static void Compare(string s)
        {
            if(s.StartsWith(Config.ConfigStrings[0]))
            {
                Config.Flight = bool.Parse(s);
            }
            else if(s.StartsWith(Config.ConfigStrings[1]))
            {
                Config.Noclip = bool.Parse(s);
            }
            else if(s.StartsWith(Config.ConfigStrings[2]))
            {
                Config.RPCEnabled = bool.Parse(s);
            }
            else if (s.StartsWith(Config.ConfigStrings[3]))
            {
                Config.ESPEnabled = bool.Parse(s);
            }
        }
    }
}
