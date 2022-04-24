using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using UnityEngine;
using UnusualMod;
using UnusualMod.Core;
using UnusualMod.Functions;
using BuildInfo = UnusualMod.Melon.BuildInfo;
using UnusualMod.Implementations;
using System.IO;

[assembly: MelonInfo(typeof(Unusual), BuildInfo.Name, BuildInfo.Version, BuildInfo.Author, BuildInfo.DownloadLink)]
[assembly: MelonGame("VRChat", "VRChat")]
[assembly: MelonColor(ConsoleColor.DarkMagenta)]

namespace UnusualMod
{
    public class Unusual : MelonMod
    {
        public new static HarmonyLib.Harmony Harmony { get; private set; }

        public override void OnApplicationStart()
        {
            Harmony = HarmonyInstance;
            
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).Replace("Roaming", "LocalLow") + "/UnusualMod";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                Config.ConfigPath = path + "/Settings.txt";
            }
            else
            {
                Config.ConfigPath = path + "/Settings.txt";
            }

            ImplementationsHandler.OnApplicationStart();
            Settings.Load();

        }

        public override void OnApplicationQuit()
        {
            ImplementationsHandler.OnApplicationQuit();
            Settings.Save();
            Application.Quit();
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            ImplementationsHandler.OnSceneWasLoaded(buildIndex, sceneName);
        }

        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            ImplementationsHandler.OnSceneWasInitialized(buildIndex, sceneName);
        }

        public override void OnSceneWasUnloaded(int buildIndex, string sceneName)
        {
            ImplementationsHandler.OnSceneWasUnloaded(buildIndex, sceneName);
        }

        public override void OnUpdate()
        {
            ImplementationsHandler.OnUpdate();
            FunctionsHandler.Update();
        }
    }
}
