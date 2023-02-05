using HarmonyLib;
using HemogenExtractor;
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

        [HarmonyPatch("CompSpawnerHemogen", "TryDoSpawn")]
        internal static class Harmony_CompSpawnerHemogen_TryDoSpawn
        {
            static readonly ThingDef hemogenPack = ThingDef.Named("HemogenPack");
            static readonly ThingDef animalHemogenPack = ThingDef.Named("AnimalHemogenPack");

            internal static void Prefix(CompSpawnerHemogen __instance)
            {
                __instance.PropsSpawner.thingToSpawn = hemogenPack;

                Building_HemogenExtractor extractor = __instance.parent as Building_HemogenExtractor;
                if (extractor == null) return;

                Pawn victim = extractor.InnerPawn;
                if (victim == null || !victim.RaceProps.Animal) return;

                __instance.PropsSpawner.thingToSpawn = animalHemogenPack;
            }
        }
    }
}
