using Verse;

namespace AnimalHemogen.Harmony
{
    /// <summary>
    /// Patcher static class to apply harmony patches.
    /// </summary>
    [StaticConstructorOnStartup]
    public static class Patcher {
        /// <summary>
        /// Patcher constructor to patch things using harmony.
        /// </summary>
        static Patcher()
        {
            var harmony = new HarmonyLib.Harmony("eth0net.AnimalHemogen.harmony");
            harmony.PatchAll();
        }
    }
}
