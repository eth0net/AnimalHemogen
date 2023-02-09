using System;
using System.Linq;
using Verse;

namespace AnimalHemogen.Harmony
{
    /// <summary>
    /// Patcher static class to apply harmony patches.
    /// </summary>
    [StaticConstructorOnStartup]
    public static class Patcher
    {
        private static readonly HarmonyLib.Harmony harmony = new("eth0net.AnimalHemogen.harmony");
        private static readonly Lazy<HemogenExtractorAnimalPatches> hemogenExtractorPatch = new();

        /// <summary>
        /// Patcher constructor to patch things using harmony.
        /// </summary>
        static Patcher()
        {
            harmony.PatchAll();

            if (HemogenExtractorActive) hemogenExtractorPatch.Value.Patch(harmony);
        }

        private static bool HemogenExtractorActive => ModsConfig.ActiveModsInLoadOrder.Any((mod) => mod.PackageIdPlayerFacing == "Uveren.HemogenExtractor");
    }
}
