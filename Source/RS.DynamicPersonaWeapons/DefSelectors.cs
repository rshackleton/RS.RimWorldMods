using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace RS.DynamicPersonaWeapons
{
    static internal class DefSelectors
    {
        static internal List<ThingDef> GetAllNonPersonaWeapons()
        {
            return DefDatabase<ThingDef>.AllDefsListForReading
                .Where(def => def.IsMeleeWeapon)
                .Where(def => !def.HasComp(typeof(CompBladelinkWeapon)))
                .ToList();
        }

        static internal List<ThingDef> GetNonPersonaWeapons(string[] allowlist)
        {
            return GetAllNonPersonaWeapons()
                .Where(def => allowlist.Contains(def.defName, StringComparer.OrdinalIgnoreCase))
                .ToList();
        }

        static internal RulePackDef GetRulePackDefNamerWeaponBladelink()
        {
            return DefDatabase<RulePackDef>.GetNamed("NamerWeaponBladelink");
        }

        static internal ThingCategoryDef GetThingCategoryDefWeaponsMeleeBladelink()
        {
            return DefDatabase<ThingCategoryDef>.GetNamed("WeaponsMeleeBladelink");
        }
    }
}
