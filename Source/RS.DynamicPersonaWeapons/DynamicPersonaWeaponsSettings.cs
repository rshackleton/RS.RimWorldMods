using System.Collections.Generic;
using Verse;

namespace RS.DynamicPersonaWeapons
{
    public class DynamicPersonaWeaponsSettings : ModSettings
    {
        public HashSet<string> allowlist = new HashSet<string>();

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Collections.Look(ref allowlist, "allowlist", LookMode.Value);

            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                if (allowlist is null)
                {
                    allowlist = new HashSet<string>();
                }
            }
        }
    }
}
