using HarmonyLib;
using Verse;

namespace AnimalHemogen
{
    /// <summary>
    /// AnimalHemogen static class to load up the mod.
    /// </summary>
    [StaticConstructorOnStartup]
    public static class AnimalHemogen {
        /// <summary>
        /// AnimalHemogen constructor to patch things using harmony.
        /// </summary>
        static AnimalHemogen()
        {
            var harmony = new HarmonyLib.Harmony("eth0net.AnimalHemogen.harmony");
            harmony.PatchAll();
        }
    }
}
