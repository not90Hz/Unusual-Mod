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
            ImplementationsHandler.OnStart();
            Settings.Load();
        }

        public override void OnApplicationQuit()
        {
            Settings.Save();
            Application.Quit();
        }

        public override void OnUpdate()
        {
            ImplementationsHandler.OnUpdate();
            FunctionsHandler.Update();
        }
    }
}
