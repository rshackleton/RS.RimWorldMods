using RS.WorkSettingsFix.Patches;
using Verse;

// ReSharper disable UnusedType.Global

namespace RS.WorkSettingsFix
{
    [StaticConstructorOnStartup]
    public static class Initialization
    {
        static Initialization()
        {
            HPatcher.Init();
        }
    }
}
