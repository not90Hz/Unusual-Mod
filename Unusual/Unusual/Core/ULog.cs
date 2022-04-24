using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnusualMod.Core
{
    internal class ULog
    {
        public static void MSG(string text)
        {
            MelonLogger.Msg(ConsoleColor.Green, string.Format("{0} {1}", "[Log]", text));
        }

        public static void WARN(string text)
        {
            MelonLogger.Msg(ConsoleColor.Yellow, string.Format("{0} {1}", "[WARN]", text));
        }

        public static void ERR(string text)
        {
            MelonLogger.Msg(ConsoleColor.Red, string.Format("{0} {1}", "[ERROR]", text));
        }
    }
}
