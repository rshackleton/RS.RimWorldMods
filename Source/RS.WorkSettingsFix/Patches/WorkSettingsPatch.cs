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
            if (__instance.workSettings != null)
            {
                return;
            }

            if (__instance.Dead)
            {
                return;
            }

            if (__instance.AnimalOrWildMan())
            {
                return;
            }

            if (!(__instance.IsColonist || __instance.IsPrisonerOfColony))
            {
                return;
            }
                
            Log.Message($"[RS] WorkSettingsFix: Fixing missing work settings for {__instance.Name}");

            // Force-initialise work settings for colonists and prisoners.
            __instance.workSettings = new Pawn_WorkSettings(__instance);
            __instance.workSettings.EnableAndInitialize();
        }
    }
}