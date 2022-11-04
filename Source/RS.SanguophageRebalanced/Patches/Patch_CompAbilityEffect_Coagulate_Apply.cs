using HarmonyLib;
using RimWorld;
using Verse;

namespace RS.SanguophageRebalanced.Patches
{
    [HarmonyPatch(typeof(CompAbilityEffect_Coagulate), "Apply")]
    class Patch_CompAbilityEffect_Coagulate_Apply
    {
        static void Prefix(LocalTargetInfo target, out int __state)
        {
            __state = 0;

            var pawn = target.Pawn;

            if (pawn == null)
            {
                return;
            }

            var hediffs = pawn.health.hediffSet.hediffs;

            if (SanguophageRebalancedController.Instance.EnableHemostaticShock && hediffs.Any())
            {
                __state = hediffs.Count(hediff => (hediff is Hediff_Injury || hediff is Hediff_MissingPart) && hediff.TendableNow());

                Log.Message($"Patch_CompAbilityEffect_Coagulate_Apply.Prefix :: {__state} hediffs tended");
            }
        }

        static void Postfix(LocalTargetInfo target, int __state)
        {
            var pawn = target.Pawn;

            if (pawn == null)
            {
                return;
            }

            if (!SanguophageRebalancedController.Instance.EnableHemostaticShock)
            {
                return;
            }

            // Apply new hediff based on number of treated hediffs
            var hediffDef = DefDatabase<HediffDef>.GetNamed("HemostaticShock");

            if (hediffDef != null)
            {
                var ticksToDisappear = __state * SanguophageRebalancedController.Instance.HemostaticShockTicksPerWound;

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