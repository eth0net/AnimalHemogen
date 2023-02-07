using HarmonyLib;
using HemogenExtractor;
using System.Collections.Generic;
using System.Reflection;
using Verse;

namespace AnimalHemogen.Harmony
{
    internal class HemogenExtractorAnimalPatches
    {
        public void Patch(HarmonyLib.Harmony harmony)
        {
            MethodInfo humanlike = AccessTools.Method(nameof(HemogenExtractorPatches.Prefix_AddHumanlikeOrders));
            HarmonyMethod humanlikeOrAnimal = new(typeof(HemogenExtractorAnimalPatches), nameof(AllowHumanlikeOrAnimal));
            harmony.Patch(humanlike, transpiler: humanlikeOrAnimal);

            MethodInfo spawn = AccessTools.Method(nameof(CompSpawnerHemogen.TryDoSpawn));
            HarmonyMethod selectHemogen = new(typeof(HemogenExtractorAnimalPatches), nameof(SelectHemogen));
        }

        internal static IEnumerable<CodeInstruction> AllowHumanlikeOrAnimal(IEnumerable<CodeInstruction> instructions) => instructions.MethodReplacer(
            AccessTools.PropertyGetter(nameof(RaceProperties.Humanlike)),
            AccessTools.Method(nameof(HemogenExtractorAnimalPatches.HumanlikeOrAnimal))
        );

        internal static void SelectHemogen(CompSpawnerHemogen __instance)
        {
            __instance.PropsSpawner.thingToSpawn = hemogenPack;

            Building_HemogenExtractor extractor = __instance.parent as Building_HemogenExtractor;
            if (extractor == null) return;

            Pawn victim = extractor.InnerPawn;
            if (victim == null || !victim.RaceProps.Animal) return;

            __instance.PropsSpawner.thingToSpawn = animalHemogenPack;
        }

        static bool HumanlikeOrAnimal(RaceProperties raceProps) => raceProps.Humanlike || raceProps.Animal;

        static readonly ThingDef hemogenPack = ThingDef.Named("HemogenPack");
        static readonly ThingDef animalHemogenPack = ThingDef.Named("AnimalHemogenPack");
    }
}
