using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using UnusualMod;
using UnusualMod.Functions;
using BuildInfo = UnusualMod.Melon.BuildInfo;

[assembly: MelonInfo(typeof(Unusual), BuildInfo.Name, BuildInfo.Version, BuildInfo.Author, BuildInfo.DownloadLink)]
[assembly: MelonGame("VRChat", "VRChat")]
[assembly: MelonColor(ConsoleColor.DarkMagenta)]

namespace UnusualMod
{
    public class Unusual : MelonMod
    {
        public override void OnApplicationStart()
        {
            base.OnApplicationStart();
        }

        public override void OnApplicationQuit()
        {
            base.OnApplicationQuit();
        }

        public override void OnUpdate()
        {
            FunctionsHandler.Update();
        }
    }
}
