using Locks2.Core;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Verse;

namespace Locks2MiscRobots
{
    public class ConfigRuleMiscRobots : LockConfig.IConfigRule
    {
        public bool enabled = true;

        private readonly string[] kindDefNamePrefixes = new[]
        {
            // Misc. Robots
            "AIRobot_",
            // Misc. Robots++
            "RPP_Bot_"
        };

        public override float Height => 54;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Allows(Pawn pawn)
        {
            if (!enabled) return false;

            return kindDefNamePrefixes.Any(prefix => pawn.kindDef.defName.StartsWith(prefix, StringComparison.OrdinalIgnoreCase));
        }

        public override void DoContent(IEnumerable<Pawn> pawns, Rect rect, Action notifySelectionBegan, Action notifySelectionEnded)
        {
            var before = enabled;
            Widgets.CheckboxLabeled(rect, "Locks2MiscRobotsFilter".Translate(), ref enabled);
            if (before != enabled) LockConfig.Notify_Dirty();
        }

        public override LockConfig.IConfigRule Duplicate()
        {
            return new ConfigRuleMiscRobots { enabled = enabled };
        }

        public override void ExposeData()
        {
            Scribe_Values.Look(ref enabled, "enabled", true);
        }
    }
}
