using Force.DeepCloner;
using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace RS.DynamicPersonaWeapons.HarmonyPatches
{
    [HarmonyPatch(typeof(DefGenerator), "GenerateImpliedDefs_PreResolve")]
    internal static class DefGenerator_GenerateImpliedDefs_PreResolve_Patch
    {
        public static void Prefix()
        {
            Log.Message($"[RS] DynamicPersonaWeapons: DefGenerator_GenerateImpliedDefs_PreResolve_Patch");

            var allowlist = DynamicPersonaWeaponsMod.settings.allowlist;

            if (allowlist == null)
            {
                Log.Message($"[RS] DynamicPersonaWeapons: DefGenerator_GenerateImpliedDefs_PreResolve_Patch: Could not retrieve settings.");
                return;
            }

            Log.Message($"[RS] DynamicPersonaWeapons: DefGenerator_GenerateImpliedDefs_PreResolve_Patch: Processing {allowlist.Count} defs.");

            var nameMaker = DefSelectors.GetRulePackDefNamerWeaponBladelink();
            var thingCategory = DefSelectors.GetThingCategoryDefWeaponsMeleeBladelink();
            var filteredWeaponDefs = DefSelectors.GetNonPersonaWeapons(allowlist.ToArray());

            var tags = new HashSet<string>();

            foreach (var oldWeaponDef in filteredWeaponDefs)
            {
                var tag = $"{oldWeaponDef.techLevel}Bladelink";

                if (!tags.Contains(tag))
                {
                    tags.Add(tag);
                }

                var newWeaponDef = new ThingDef();

                oldWeaponDef.ShallowCloneTo(newWeaponDef);

                newWeaponDef.defName = $"{oldWeaponDef.defName}Bladelink";
                newWeaponDef.tradeNeverStack = true;

                if (newWeaponDef.HasComp(typeof(CompStyleable)))
                {
                    newWeaponDef.relicChance = 3;
                }

                newWeaponDef.comps = new List<CompProperties>(oldWeaponDef.comps)
                {
                    new CompProperties_BladelinkWeapon
                    {
                        biocodeOnEquip = true
                    },

                    new CompProperties_GeneratedName
                    {
                        nameMaker = nameMaker
                    }
                };

                newWeaponDef.thingCategories = new List<ThingCategoryDef>
                {
                    thingCategory
                };

                newWeaponDef.thingSetMakerTags = new List<string>
                {
                    "RewardStandardMidFreq"
                };

                newWeaponDef.tradeTags = new List<string>
                {
                    tag
                };

                newWeaponDef.weaponTags = new List<string>
                {
                    tag
                };

                DefGenerator.AddImpliedDef(newWeaponDef);

                Log.Message($"[RS] DynamicPersonaWeapons: Added {newWeaponDef.defName}.");
            }
        }
    }
}
