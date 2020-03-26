using System;
using System.Reflection;
using HarmonyLib;
using Verse;

// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedType.Global

namespace RS.WorkSettingsFix.Patches
{
    internal static class HPatcher
    {
        public static void Init()
        {
            var harmony = new Harmony("com.rs.rimworld.worksettingsfix");
            
            try
            {
                harmony.PatchAll(Assembly.GetExecutingAssembly());
                Log.Message($"[RS] WorkSettingsFix: Applied Harmony patch.");
            }
            catch (Exception e)
            {
                Log.Error($"[RS] WorkSettingsFix: Failed to apply Harmony patch. Error: {e.Message}");
            }
        }
    }
}