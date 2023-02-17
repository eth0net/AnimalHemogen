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
            MethodInfo humanlike = AccessTools.Method(typeof(HemogenExtractorPatches), nameof(HemogenExtractorPatches.Prefix_AddHumanlikeOrders));
            HarmonyMethod humanlikeOrAnimal = new(typeof(HemogenExtractorAnimalPatches), nameof(AllowHumanlikeOrAnimal));
            harmony.Patch(humanlike, transpiler: humanlikeOrAnimal);

            MethodInfo spawn = AccessTools.Method(typeof(CompSpawnerHemogen), nameof(CompSpawnerHemogen.TryDoSpawn));
            HarmonyMethod selectHemogen = new(typeof(HemogenExtractorAnimalPatches), nameof(SelectHemogen));
            harmony.Patch(spawn, prefix: selectHemogen);
        }

        /// <summary>
        /// AllowHumanlikeOrAnimal patches hemogen extractor to replace calls to RaceProperties.Humanlike with HumanlikeOrAnimal.
        /// </summary>
        /// <param name="instructions"></param>
        /// <returns></returns>
        internal static IEnumerable<CodeInstruction> AllowHumanlikeOrAnimal(IEnumerable<CodeInstruction> instructions) => instructions.MethodReplacer(
            AccessTools.PropertyGetter(typeof(RaceProperties), nameof(RaceProperties.Humanlike)),
            AccessTools.Method(typeof(HemogenExtractorAnimalPatches), nameof(HemogenExtractorAnimalPatches.HumanlikeOrAnimal))
        );

        /// <summary>
        /// SelectHemogen patches CompSpawnerHemogen.TryDoSpawn to switch the hemogen type spawned by the extractor.
        /// </summary>
        /// <param name="__instance"></param>
        internal static void SelectHemogen(ThingComp __instance)
        {
            if (__instance is not CompSpawnerHemogen comp) return;

            comp.PropsSpawner.thingToSpawn = hemogenPack;

            if (comp.parent is not Building_HemogenExtractor extractor) return;

            Pawn victim = extractor.InnerPawn;
            if (victim == null || !victim.RaceProps.Animal) return;

            comp.PropsSpawner.thingToSpawn = animalHemogenPack;
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
