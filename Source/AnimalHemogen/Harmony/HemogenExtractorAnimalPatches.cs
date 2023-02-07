using HarmonyLib;
using HemogenExtractor;
using System.Collections.Generic;
using System.Reflection;
using Verse;

namespace AnimalHemogen.Harmony
{
    /// <summary>
    /// HemogenExtractorAnimalPatches patches the hemogen extractor mod to support animal hemogen.
    /// </summary>
    internal class HemogenExtractorAnimalPatches
    {
        /// <summary>
        /// Patch applies the animal hemogen patches to hemogen extractor.
        /// </summary>
        /// <param name="harmony"></param>
        public void Patch(HarmonyLib.Harmony harmony)
        {
            MethodInfo humanlike = AccessTools.Method(nameof(HemogenExtractorPatches.Prefix_AddHumanlikeOrders));
            HarmonyMethod humanlikeOrAnimal = new(typeof(HemogenExtractorAnimalPatches), nameof(AllowHumanlikeOrAnimal));
            harmony.Patch(humanlike, transpiler: humanlikeOrAnimal);

            MethodInfo spawn = AccessTools.Method(nameof(CompSpawnerHemogen.TryDoSpawn));
            HarmonyMethod selectHemogen = new(typeof(HemogenExtractorAnimalPatches), nameof(SelectHemogen));
        }

        /// <summary>
        /// AllowHumanlikeOrAnimal patches hemogen extractor to replace calls to RaceProperties.Humanlike with HumanlikeOrAnimal.
        /// </summary>
        /// <param name="instructions"></param>
        /// <returns></returns>
        internal static IEnumerable<CodeInstruction> AllowHumanlikeOrAnimal(IEnumerable<CodeInstruction> instructions) => instructions.MethodReplacer(
            AccessTools.PropertyGetter(nameof(RaceProperties.Humanlike)),
            AccessTools.Method(nameof(HemogenExtractorAnimalPatches.HumanlikeOrAnimal))
        );

        /// <summary>
        /// SelectHemogen patches CompSpawnerHemogen.TryDoSpawn to switch the hemogen type spawned by the extractor.
        /// </summary>
        /// <param name="__instance"></param>
        internal static void SelectHemogen(CompSpawnerHemogen __instance)
        {
            __instance.PropsSpawner.thingToSpawn = hemogenPack;

            Building_HemogenExtractor extractor = __instance.parent as Building_HemogenExtractor;
            if (extractor == null) return;

            Pawn victim = extractor.InnerPawn;
            if (victim == null || !victim.RaceProps.Animal) return;

            __instance.PropsSpawner.thingToSpawn = animalHemogenPack;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="raceProps"></param>
        /// <returns></returns>
        static bool HumanlikeOrAnimal(RaceProperties raceProps) => raceProps.Humanlike || raceProps.Animal;

        static readonly ThingDef hemogenPack = ThingDef.Named("HemogenPack");
        static readonly ThingDef animalHemogenPack = ThingDef.Named("AnimalHemogenPack");
    }
}
