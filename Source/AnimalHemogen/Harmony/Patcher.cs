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

            if (LoadedModManager.RunningMods.Any(pack => pack.PackageId == "Uveren.HemogenExtractor"))
            {
                hemogenExtractorPatch.Value.Patch(harmony);
            }
        }
    }
}

// TODO: patch bloodfeed and hemogen consume calls to implement CompBloodfeederDiet methods
