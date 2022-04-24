﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using MelonLoader;
using UnhollowerBaseLib.Attributes;
using UnhollowerRuntimeLib.XrefScans;

namespace VRChatUtilityKit.Utilities
{
    /// <summary>
    /// A set of utilites for Xref scanning.
    /// </summary>
    public static class XrefUtils
    {
        internal static void Init()
        {
            _printMethod = typeof(XrefUtils).GetMethod(nameof(Print), BindingFlags.NonPublic | BindingFlags.Static).ToNewHarmonyMethod();
        }

        private static HarmonyMethod _printMethod;
        /// <summary>
        /// A method that prints the name of the original method when used in a patch.
        /// Useful when figuring out Xrefs.
        /// </summary>
        public static HarmonyMethod PrintMethod => _printMethod;
        private static void Print(MethodInfo __originalMethod)
        {
            VRChatUtilityKitMod.Instance.LoggerInstance.Msg(__originalMethod.Name);
            VRChatUtilityKitMod.Instance.LoggerInstance.Msg(__originalMethod.DeclaringType.FullName);
            VRChatUtilityKitMod.Instance.LoggerInstance.Msg("");
        }

        /// <summary>
        /// Checks the strings in the given method's body.
        /// </summary>
        /// <param name="method">The method to check</param>
        /// <param name="predicate">The predicate to check the strings against</param>
        /// <returns>true if the predicate returned true any times, otherwise false</returns>
        public static bool CheckStrings(MethodInfo method, Func<string, bool> predicate)
        {
            foreach (XrefInstance instance in XrefScanner.XrefScan(method))
                if (instance.Type == XrefType.Global && predicate.Invoke(instance.ReadAsObject().ToString()))
                    return true;
            return false;
        }

        /// <summary>
        /// Returns if a string is contained within the given method's body.
        /// </summary>
        /// <param name="method">The method to check</param>
        /// <param name="match">The string to check</param>
        /// <returns>true if the given string literal is contained within the given method, otherwise false</returns>
        public static bool CheckStrings(MethodInfo method, string match)
            => CheckStrings(method, global => global.Contains(match));

        /// <summary>
        /// Returns if a string is contained within the given method's body.
        /// </summary>
        /// <param name="method">The method to check</param>
        /// <param name="match">The string to check</param>
        /// <returns>true if the given string literal is contained within the given method, otherwise false</returns>
        [Obsolete("Use XrefUtils.CheckStrings instead.")]
        public static bool CheckMethod(MethodInfo method, string match)
            => CheckStrings(method, match);

        /// <summary>
        /// Checks the methods the given method is used by.
        /// Note: the methods passed into the predicate may be false.
        /// </summary>
        /// <param name="method">The method to check</param>
        /// <param name="predicate">The predicate to check the methods against</param>
        /// <returns>true if the predicate returned true any times, otherwise false</returns>
        public static bool CheckUsedBy(MethodBase method, Func<MethodBase, bool> predicate)
        {
            foreach (XrefInstance instance in XrefScanner.UsedBy(method))
                if (instance.Type == XrefType.Method && predicate.Invoke(instance.TryResolve()))
                    return true;
            return false;
        }

        /// <summary>
        /// Returns if the given method is called by the other given method.
        /// </summary>
        /// <param name="method">The method to check</param>
        /// <param name="methodName">The name of the method that uses the given method</param>
        /// <param name="type">The type of the method that uses the given method</param>
        /// <returns>true if any of the given method is called by a method with the given name of the given type, otherwise false</returns>
        public static bool CheckUsedBy(MethodBase method, string methodName, Type type = null)
            => CheckUsedBy(method, usedByMethod => usedByMethod != null && (type == null || usedByMethod.DeclaringType == type) && usedByMethod.Name.Contains(methodName));

        /// <summary>
        /// Checks the methods the given method uses.
        /// Note: the methods passed into the predicate may be false.
        /// </summary>
        /// <param name="method">The method to check</param>
        /// <param name="predicate">The predicate to check the methods against</param>
        /// <returns>true if the predicate returned true any times, otherwise false</returns>
        public static bool CheckUsing(MethodBase method, Func<MethodBase, bool> predicate)
        {
            foreach (XrefInstance instance in XrefScanner.XrefScan(method))
                if (instance.Type == XrefType.Method && predicate.Invoke(instance.TryResolve()))
                    return true;
            return false;
        }

        /// <summary>
        /// Returns whether the given method is using another the other given method.
        /// </summary>
        /// <param name="method">The method to check</param>
        /// <param name="methodName">The name of the method that is used by the given method</param>
        /// <param name="type">The type of the method that is used by the given method</param>
        /// <returns>true if any of the given method calls a method with the given name of the given type, otherwise false</returns>
        public static bool CheckUsing(MethodBase method, string methodName, Type type = null)
            => CheckUsing(method, usingMethod => usingMethod != null && (type == null || usingMethod.DeclaringType == type) && usingMethod.Name.Contains(methodName));

        /// <summary>
        /// Counts how many times the given method is used by.
        /// Note: the methods passed into the predicate may be false.
        /// </summary>
        /// <param name="method">The method to check</param>
        /// <param name="predicate">The predicate to check the methods against</param>
        /// <returns>the number of times the given method is called by a method with the given name of the given type</returns>
        public static int CheckUsedByCount(MethodBase method, Func<MethodBase, bool> predicate)
        {
            int methodCount = 0;
            foreach (XrefInstance instance in XrefScanner.UsedBy(method))
            {
                if (instance.Type == XrefType.Method && predicate.Invoke(instance.TryResolve()))
                    methodCount++;
            }
            return methodCount;
        }

        /// <summary>
        /// Returns the number of times the given method is called by the other given method.
        /// </summary>
        /// <param name="method">The method to check</param>
        /// <param name="methodName">The name of the method that uses the given method</param>
        /// <param name="type">The type of the method that uses the given method</param>
        /// <returns>the number of times the given method is called by a method with the given name of the given type</returns>
        public static int CheckUsedByCount(MethodBase method, string methodName, Type type = null)
            => CheckUsedByCount(method, usedByMethod => usedByMethod != null && (type == null || usedByMethod.DeclaringType == type) && usedByMethod.Name.Contains(methodName));

        /// <summary>
        /// Dumps the Xref information on a method.
        /// This is for DEBUG PURPOSES ONLY.
        /// </summary>
        /// <param name="method">The method to dump information on</param>
        public static void DumpXrefInfo(MethodBase method)
        {
            try
            {
                VRChatUtilityKitMod.Instance.LoggerInstance.Msg(ConsoleColor.Yellow, $"Scanning {method.Name}");

                VRChatUtilityKitMod.Instance.LoggerInstance.Msg(ConsoleColor.Yellow, $"Checking UsedBy");
                DumpScan(XrefScanner.UsedBy(method));

                VRChatUtilityKitMod.Instance.LoggerInstance.Msg(ConsoleColor.Green, "Checking Using");
                DumpScan(XrefScanner.XrefScan(method));
            }
            catch (Exception ex)
            {
                VRChatUtilityKitMod.Instance.LoggerInstance.Error($"Failed while dumping {method.Name}:\n{ex}");
            }
        }

        private static void DumpScan(IEnumerable<XrefInstance> scan)
        {
            foreach (XrefInstance instance in scan)
            {
                if (instance.Type == XrefType.Global)
                {
                    VRChatUtilityKitMod.Instance.LoggerInstance.Msg(instance.Type);
                    VRChatUtilityKitMod.Instance.LoggerInstance.Msg(instance.ReadAsObject().ToString());
                    VRChatUtilityKitMod.Instance.LoggerInstance.Msg("");
                    continue;
                }

                MethodBase resolvedMethod = instance.TryResolve();
                if (instance.Type == XrefType.Method)
                {
                    if (resolvedMethod == null)
                    {
                        VRChatUtilityKitMod.Instance.LoggerInstance.Msg("null");
                        VRChatUtilityKitMod.Instance.LoggerInstance.Msg("null");
                    }
                    else
                    {
                        VRChatUtilityKitMod.Instance.LoggerInstance.Msg(resolvedMethod.Name);
                        VRChatUtilityKitMod.Instance.LoggerInstance.Msg(resolvedMethod.DeclaringType.FullName);
                    }

                    VRChatUtilityKitMod.Instance.LoggerInstance.Msg("");
                }
            }
        }

        /// <summary>
        /// DO NOT call this often. 
        /// It is slow.
        /// </summary>
        /// <param name="obfuscatedName">The obfuscated name of the type</param>
        /// <returns>The Unhollower processed name</returns>
        public static Type GetTypeFromObfuscatedName(string obfuscatedName) => AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .First(type => type.GetCustomAttribute<ObfuscatedNameAttribute>() != null && type.GetCustomAttribute<ObfuscatedNameAttribute>().ObfuscatedName.Equals(obfuscatedName, StringComparison.InvariantCulture));
    }
}
