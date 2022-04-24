﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using Il2CppSystem.Collections;
using UnityEngine;
using VRC;
using VRC.Core;
using VRC.Management;
using VRC.UI;
using VRC.UI.Elements;

namespace VRChatUtilityKit.Utilities
{
    // Join/leave events stolen from knah's JoinNotifier (https://github.com/knah/VRCMods/tree/master/JoinNotifier)
    /// <summary>
    /// A set of events pertaining to VRChat's network.
    /// </summary>
    [MelonLoaderEvents]
    public static class NetworkEvents
    {
        /// <summary>
        /// Calls when a player has left the instance.
        /// This will also call as every player is removed after leaving an instance.
        /// </summary>
        public static event Action<Player> OnPlayerLeft;

        /// <summary>
        /// Calls when a player joins the instance.
        /// The first call of this may contain a null APIUser.
        /// </summary>
        public static event Action<Player> OnPlayerJoined;

        /// <summary>
        /// Calls when the local user leaves an room.
        /// </summary>
        public static event Action OnRoomLeft;

        /// <summary>
        /// Calls when the local user joins an room.
        /// </summary>
        public static event Action OnRoomJoined;

        /// <summary>
        /// Calls when a new friend is added.
        /// Whether they accepted your friend request or you accepted theirs does not matter.
        /// </summary>
        public static event Action<APIUser> OnFriended;

        /// <summary>
        /// Calls when a friend is removed.
        /// Whether you removed them or they removed you does not matter.
        /// </summary>
        public static event Action<string> OnUnfriended;

        /// <summary>
        /// Calls during the loading screen into a new instance.
        /// Does not call if there was a desync while joining the instance.
        /// </summary>
        public static event Action<ApiWorld, ApiWorldInstance> OnInstanceChanged;

        /// <summary>
        /// Calls when the master of the instance changes.
        /// Does not call upon initialy receiving the master after joining the instance.
        /// </summary>
        public static event Action<Photon.Realtime.Player> OnMasterChanged;

        /*
        /// <summary>
        /// Called when a player switches avatars.
        /// </summary>
        public static event Action<VRCPlayer, GameObject> OnCustomAvatarChanged;
        */

        /// <summary>
        /// Calls when the internal value of the avatar in an instance of VRCAvatarManager is changed.
        /// It will call for every GameObject change. This is not limited to, but includes the error bot and platform substitute.
        /// </summary>
        public static event Action<VRCAvatarManager, GameObject> OnAvatarChanged;

        /// <summary>
        /// Calls when an avatar is instantiated
        /// </summary>
        public static event Action<VRCAvatarManager, ApiAvatar, GameObject> OnAvatarInstantiated;

        /// <summary>
        /// Calls when an avatar download is progressing. 
        /// The float is percentage and long is total size.
        /// </summary>
        public static event Action<AvatarLoadingBar, float, long> OnAvatarDownloadProgressed;

        /// <summary>
        /// Calls when the setup flags of a VRCPlayer is received from photon.
        /// The int is actually the enum "VRCPlayer.EnumNPrivateSealedvaViMoVRStShAvUsFa9vUnique" at the time of writing.
        /// </summary>
        public static event Action<VRCPlayer, Hashtable> OnSetupFlagsReceived;

        /// <summary>
        /// Calls when the local user changes whether to show their social rank.
        /// </summary>
        public static event Action OnShowSocialRankChanged;

        /// <summary>
        /// Calls when a player moderation is sent from the local user.
        /// </summary>
        public static event Action<string, ApiPlayerModeration.ModerationType> OnPlayerModerationSent;

        /// <summary>
        /// Calls when a player moderation is removed by the local user.
        /// </summary>
        public static event Action<string, ApiPlayerModeration.ModerationType> OnPlayerModerationRemoved;

        private static void OnRoomLeave() => OnRoomLeft?.DelegateSafeInvoke();

        private static void OnRoomJoin() => OnRoomJoined?.DelegateSafeInvoke();

        private static void OnFriend(APIUser __0)
        {
            if (__0 == null) return;

            OnFriended?.DelegateSafeInvoke(__0);
        }
        private static void OnUnfriend(string __0)
        {
            if (string.IsNullOrEmpty(__0)) return;

            OnUnfriended?.DelegateSafeInvoke(__0);
        }
        private static void OnInstanceChange(ApiWorld __0, ApiWorldInstance __1)
        {
            if (__0 == null || __1 == null) return;

            OnInstanceChanged?.DelegateSafeInvoke(__0, __1);
        }
        private static void OnMasterChange(Photon.Realtime.Player __0)
        {
            if (__0 == null) return;

            OnMasterChanged?.DelegateSafeInvoke(__0);
        }
        private static void OnAvatarChange(VRCAvatarManager __instance)
        {
            OnAvatarChanged?.DelegateSafeInvoke(__instance, __instance.prop_GameObject_0);
        }
        private static void OnPlayerAwake(VRCPlayer __instance)
        {
            __instance.Method_Public_add_Void_OnAvatarIsReady_0(new Action(()
                => OnAvatarInstantiate(__instance.prop_VRCAvatarManager_0, __instance.field_Private_ApiAvatar_0, __instance.field_Internal_GameObject_0))
            );
        }
        private static void OnAvatarInstantiate(VRCAvatarManager manager, ApiAvatar apiAvatar, GameObject avatar)
        {
            if (manager == null || apiAvatar == null || avatar == null)
                return;

            OnAvatarInstantiated.DelegateSafeInvoke(manager, apiAvatar, avatar);
        }
        private static void OnAvatarDownloadProgress(AvatarLoadingBar __instance, float __0, long __1)
        {
            OnAvatarDownloadProgressed?.DelegateSafeInvoke(__instance, __0, __1);
        }
        private static void OnSetupFlagsReceive(VRCPlayer __instance, Hashtable param_1)
        {
            if (param_1 == null)
                return;

            //VRChatUtilityKitMod.Instance.LoggerInstance.Msg(param_1["showSocialRank"].ToString());

            OnSetupFlagsReceived?.DelegateSafeInvoke(__instance, param_1);
        }
        private static void OnShowSocialRankChange()
        {
            OnShowSocialRankChanged?.DelegateSafeInvoke();
        }
        private static void OnPlayerModerationSend1(string __1, ApiPlayerModeration.ModerationType __2)
        {
            if (__1 == null) return;

            OnPlayerModerationSent?.DelegateSafeInvoke(__1, __2);
        }
        private static void OnPlayerModerationSend2(string __0, ApiPlayerModeration.ModerationType __1)
        {
            if (__0 == null) return;

            OnPlayerModerationSent?.DelegateSafeInvoke(__0, __1);
        }
        private static void OnPlayerModerationRemove(string __0, ApiPlayerModeration.ModerationType __1)
        {
            if (__0 == null) return;

            OnPlayerModerationRemoved?.DelegateSafeInvoke(__0, __1);
        }

        private static void OnUiManagerInit()
        {
            var field0 = NetworkManager.field_Internal_Static_NetworkManager_0.field_Internal_VRCEventDelegate_1_Player_0;
            var field1 = NetworkManager.field_Internal_Static_NetworkManager_0.field_Internal_VRCEventDelegate_1_Player_1;

            field0.field_Private_HashSet_1_UnityAction_1_T_0.Add(new Action<Player>((player) => { if (player != null) OnPlayerJoined?.DelegateSafeInvoke(player); }));
            field1.field_Private_HashSet_1_UnityAction_1_T_0.Add(new Action<Player>((player) => { if (player != null) OnPlayerLeft?.DelegateSafeInvoke(player); }));

            List<Exception> exceptions = new();

            // Passing the original as a function so that exceptions when getting it get handled here too.
            void prefixMultiple(Func<IEnumerable<MethodBase>> getOriginals, string staticMethodName)
            {
                try
                {
                    HarmonyMethod prefix = new(typeof(NetworkEvents).GetMethod(staticMethodName, BindingFlags.NonPublic | BindingFlags.Static));
                    foreach (MethodBase method in getOriginals())
                    {
                        VRChatUtilityKitMod.Instance.HarmonyInstance.Patch(method, prefix: prefix);
                    }
                }
                catch (Exception ex)
                {
                    exceptions.Add(new Exception($"Prefix for {staticMethodName} failed: ", ex));
                }
            }

            void prefix(Func<MethodBase> getOriginal, string staticMethodName)
            {
                prefixMultiple(() => new[] { getOriginal() }, staticMethodName);
            }

            void postfixMultiple(Func<IEnumerable<MethodBase>> getOriginals, string staticMethodName)
            {
                try
                {
                    HarmonyMethod postfix = new(typeof(NetworkEvents).GetMethod(staticMethodName, BindingFlags.NonPublic | BindingFlags.Static));
                    foreach (MethodBase method in getOriginals())
                    {
                        VRChatUtilityKitMod.Instance.HarmonyInstance.Patch(method, postfix: postfix);
                    }
                }
                catch (Exception ex)
                {
                    exceptions.Add(new Exception($"Postfix for {staticMethodName} failed: ", ex));
                }
            }

            void postfix(Func<MethodBase> getOriginal, string staticMethodName)
            {
                postfixMultiple(() => new[] { getOriginal() }, staticMethodName);
            }

            postfix(() => typeof(APIUser).GetMethod("LocalAddFriend"), nameof(OnFriend));
            postfix(() => typeof(APIUser).GetMethod("UnfriendUser"), nameof(OnUnfriend));
            postfix(
                () => typeof(RoomManager).GetMethod("Method_Public_Static_Boolean_ApiWorld_ApiWorldInstance_String_Int32_0"),
                nameof(OnInstanceChange)
            );
            prefix(() => typeof(NetworkManager).GetMethod("OnMasterClientSwitched"), nameof(OnMasterChange));
            prefix(() => typeof(NetworkManager).GetMethod("OnLeftRoom"), nameof(OnRoomLeave));
            prefix(() => typeof(NetworkManager).GetMethod("OnJoinedRoom"), nameof(OnRoomJoin));
            postfix(
                () => typeof(VRCAvatarManager).GetMethods().First(mb => mb.Name.StartsWith("Method_Private_Boolean_GameObject_String_Single_")),
                nameof(OnAvatarChange)
            );
            postfix(() => typeof(VRCPlayer).GetMethods().First(mb => mb.Name.StartsWith("Awake")), nameof(OnPlayerAwake));
            prefixMultiple(
                () => typeof(ModerationManager).GetMethods().Where(mb => mb.Name.StartsWith("Method_Private_ApiPlayerModeration_String_String_ModerationType_")),
                nameof(OnPlayerModerationSend1)
            );
            prefixMultiple(
                () => typeof(ModerationManager).GetMethods().Where(mb => mb.Name.StartsWith("Method_Private_Void_String_ModerationType_Action_1_ApiPlayerModeration_Action_1_String_")),
                nameof(OnPlayerModerationSend2)
            );
            prefix(() => typeof(ModerationManager).GetMethod("Method_Private_Void_String_ModerationType_0"), nameof(OnPlayerModerationRemove));
            prefixMultiple(
                () => typeof(AvatarLoadingBar).GetMethods().Where(mb => mb.Name.Contains("Method_Public_Void_Single_Int64_")),
                nameof(OnAvatarDownloadProgress)
            );
            prefix(() => typeof(FriendsListManager).GetMethod("Method_Private_Void_String_0"), nameof(OnUnfriend));
            postfix(
                () => typeof(VRCPlayer).GetMethods().First(mi => mi.Name.StartsWith("Method_Public_Void_Hashtable_Boolean_")),
                nameof(OnSetupFlagsReceive)
            );
            postfixMultiple(
                () => typeof(ProfileWingMenu).GetMethods().Where(method => method.Name.StartsWith("Method_Private_Void_Boolean_")),
                nameof(OnShowSocialRankChange)
            );

            if (exceptions.Count == 1)
            {
                throw exceptions.First();
            }
            else if (exceptions.Count > 1)
            {
                throw new AggregateException("Multiple Errors Occured", exceptions);
            }
        }
    }
}
