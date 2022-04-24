using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnusualMod.Implementations
{
    internal class ImplementationsHandler
    {
        public static void OnStart()
        {
            NetworkSanity.NetworkSanity.OnApplicationStart();
        }

        public static void OnUpdate()
        {

        }
    }
}
