using HarmonyLib;
using RimWorld;
using Verse;

// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable UnusedType.Global

namespace RS.WorkSettingsFix.Patches
{
    [HarmonyPatch(typeof(Pawn), "TickRare")]
    public class WorkSettingsPatch
    {
        // ReSharper disable once InconsistentNaming
        static void Prefix(Pawn __instance)
        {
            // Force-initialise work settings for colonists and prisoners.
            if (__instance.workSettings == null && (__instance.IsColonist || __instance.IsPrisonerOfColony))
            {
                Log.Message($"[RS] WorkSettingsFix: Fixing missing work settings for {__instance.Name}");
                    
                __instance.workSettings = new Pawn_WorkSettings(__instance);
                __instance.workSettings.EnableAndInitialize();
            }
        }
    }
}