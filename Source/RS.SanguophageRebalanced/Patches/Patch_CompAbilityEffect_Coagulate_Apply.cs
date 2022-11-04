using HarmonyLib;
using RimWorld;
using Verse;

namespace RS.SanguophageRebalanced.Patches
{
    [HarmonyPatch(typeof(CompAbilityEffect_Coagulate), "Apply")]
    class Patch_CompAbilityEffect_Coagulate_Apply
    {
        const int ticksPerWound = 10000;

        static void Prefix(LocalTargetInfo target, out int __state)
        {
            var pawn = target.Pawn;

            if (pawn == null)
            {
                __state = 0;
                return;
            }

            var hediffs = pawn.health.hediffSet.hediffs;

            if (!hediffs.Any())
            {
                __state = 0;
                return;
            }

            __state = hediffs.Count(hediff => (hediff is Hediff_Injury || hediff is Hediff_MissingPart) && hediff.TendableNow());

            Log.Message($"Patch_CompAbilityEffect_Coagulate_Apply.Prefix :: {__state} hediffs tended");
        }

        static void Postfix(LocalTargetInfo target, int __state)
        {
            // Apply new hediff based on number of treated hediffs
            var pawn = target.Pawn;

            if (pawn == null)
            {
                return;
            }

            var hediffDef = DefDatabase<HediffDef>.GetNamed("HemostaticShock");

            if (hediffDef != null)
            {
                var ticksToDisappear = __state * ticksPerWound;

                Log.Message($"Patch_CompAbilityEffect_Coagulate_Apply.Postfix :: {ticksToDisappear} ticks due to {__state} hediffs tended");

                var hediff = HediffMaker.MakeHediff(hediffDef, pawn);

                var disappearsComp = hediff.TryGetComp<HediffComp_Disappears>();
                disappearsComp.ticksToDisappear = ticksToDisappear;
                disappearsComp.disappearsAfterTicks = ticksToDisappear;

                pawn.health.AddHediff(hediff);
            }
        }
    }
}