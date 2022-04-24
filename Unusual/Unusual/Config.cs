﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnusualMod
{
    internal class Config
    {
        public static bool Allowed { get; set; }

        public static bool Flight { get; set; }

        public static bool Noclip { get; set; }

        public static bool RPCEnabled { get; set; }

        public static bool ESPEnabled { get; set; }

        public static string[] ConfigStrings = new string[]
        {
            "Fly=",
            "Noclip=",
            "RPC=",
            "ESP"
        };

        public static string ConfigPath 
        { 
            get 
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).Replace("Roaming", "LocalLow") + "/UnusualMod";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                    return path + "/Settings.txt";
                }
                else
                {
                    return path + "/Settings.txt";
                }
            }
        }
    }
}
