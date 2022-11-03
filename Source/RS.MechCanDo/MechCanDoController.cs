using HugsLib;
using HugsLib.Settings;
using RimWorld;
using System.Collections.Generic;
using Verse;

namespace RS.MechCanDo
{
    [EarlyInit]
    public class MechCanDoController : ModBase
    {
        private readonly Dictionary<string, SettingHandle<bool>> workGiverToggleHandles = new Dictionary<string, SettingHandle<bool>>();

        public override string ModIdentifier
        {
            get { return "RS.MechCanDo"; }
        }

        public override void DefsLoaded()
        {
            ToggleWorkGivers();
        }

        private void ToggleWorkGivers()
        {
            if (!ModIsActive)
            {
                return;
            }

            foreach (var def in DefDatabase<WorkGiverDef>.AllDefs)
            {
                // Skip defs that can already be done.
                if (def.canBeDoneByMechs)
                {
                    continue;
                }

                // Update def value.
                def.canBeDoneByMechs = IsWorkGiverEnabled(def);
            }
        }

        private bool IsWorkGiverEnabled(WorkGiverDef def)
        {
            var handleName = $"MechCanDo_{def.defName}_Toggle".ToLowerInvariant();

            // Return existing value.
            if (workGiverToggleHandles.ContainsKey(handleName))
            {
                return workGiverToggleHandles[handleName].Value;
            }

            // Or define new setting and store handle.
            var label = def.defName;
            var description = $"{def.label} ({def.workType.defName})";

            var handle = Settings.GetHandle(handleName, label, description, false);

            workGiverToggleHandles[handleName] = handle;

            return handle.Value;
        }
    }
}
