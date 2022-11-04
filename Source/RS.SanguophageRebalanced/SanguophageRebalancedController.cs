using HugsLib;
using HugsLib.Settings;
using Verse;

namespace RS.SanguophageRebalanced
{
    [EarlyInit]
    public class SanguophageRebalancedController : ModBase
    {
        public static SanguophageRebalancedController Instance { get; private set; }

        public override string ModIdentifier
        {
            get { return "RS.SanguophageRebalanced"; }
        }

        public SettingHandle<bool> EnableHemostaticShock;
        public SettingHandle<int> HemostaticShockTicksPerWound;

        private SanguophageRebalancedController()
        {
            Instance = this;
        }

        public override void DefsLoaded()
        {
            EnableHemostaticShock = Settings.GetHandle($"MechCanDo_{nameof(EnableHemostaticShock)}", nameof(EnableHemostaticShock).Translate(), "", defaultValue: true);
            HemostaticShockTicksPerWound = Settings.GetHandle($"MechCanDo_{nameof(HemostaticShockTicksPerWound)}", nameof(HemostaticShockTicksPerWound).Translate(), "", defaultValue: 10000);
        }
    }
}
