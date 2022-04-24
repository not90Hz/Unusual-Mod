using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnusualMod.Implementations
{
    internal class ImplementationsHandler
    {
        public static void OnApplicationStart()
        {
            NetworkSanity.NetworkSanity.OnApplicationStart();
            VRChatUtilityKit.VRChatUtilityKitMod.Instance.OnApplicationStart();
        }

        public static void OnUpdate()
        {
            VRChatUtilityKit.VRChatUtilityKitMod.Instance.OnUpdate();
        }

        public static void OnLateUpdate()
        {
            VRChatUtilityKit.VRChatUtilityKitMod.Instance.OnLateUpdate();
        }

        public static void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            VRChatUtilityKit.VRChatUtilityKitMod.Instance.OnSceneWasLoaded(buildIndex, sceneName);
        }

        public static void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            VRChatUtilityKit.VRChatUtilityKitMod.Instance.OnSceneWasInitialized(buildIndex, sceneName);
        }

        public static void OnSceneWasUnloaded(int buildIndex, string sceneName)
        {
            VRChatUtilityKit.VRChatUtilityKitMod.Instance.OnSceneWasUnloaded(buildIndex, sceneName);
        }

        public static void OnApplicationQuit()
        {
            VRChatUtilityKit.VRChatUtilityKitMod.Instance.OnApplicationQuit();
        }
    }
}
