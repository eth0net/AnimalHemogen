using HarmonyLib;
using System.Collections.Generic;
using Verse;

namespace AnimalHemogen.Harmony
{
    internal class Harmony_HemogenExtractor
    {
        [HarmonyPatch("HemogenExtractorPatches", "Prefix_AddHumanlikeOrders")]
        internal static class Harmony_HemogenExtractorPatches_Prefix_AddHumanlikeOrders
        {
            internal static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions) => instructions.MethodReplacer(
                AccessTools.PropertyGetter(typeof(RaceProperties), "Humanlike"),
                AccessTools.Method(typeof(Harmony_HemogenExtractorPatches_Prefix_AddHumanlikeOrders), "HumanlikeOrAnimal")
            );
            internal static bool HumanlikeOrAnimal(RaceProperties raceProps) => raceProps.Humanlike || raceProps.Animal;
        }
    }
}
