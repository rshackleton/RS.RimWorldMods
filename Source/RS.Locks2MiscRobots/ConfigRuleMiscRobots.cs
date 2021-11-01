using Locks2.Core;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Verse;

namespace Locks2MiscRobots
{
    public class ConfigRuleMiscRobots : LockConfig.IConfigRule
    {
        public bool enabled = true;

        public override float Height => 54;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Allows(Pawn pawn)
        {
            if (!enabled) return false;

            // @note: Works, might be a better way of doing this though. Couldn't figure out how to check pawn race parent...
            return pawn.kindDef.defName.StartsWith("AIRobot_", StringComparison.OrdinalIgnoreCase);
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
