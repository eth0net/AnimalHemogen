using HarmonyLib;
using RimWorld;
using Verse;

namespace AnimalHemogen.Harmony
{
    /// <summary>
    /// Harmony_SanguophageUtility_DoBite patches dobite to work with animals and the diet comp.
    /// </summary>
    [HarmonyPatch(typeof(SanguophageUtility), nameof(SanguophageUtility.DoBite))]
    internal static class Harmony_SanguophageUtility_DoBite
    {
        /// <summary>
        /// Load a reference to mod settings.
        /// </summary>
        static readonly AnimalHemogenSettings settings = LoadedModManager.GetMod<AnimalHemogen>().GetSettings<AnimalHemogenSettings>();

        /// <summary>
        /// Prefix patches bloodfeed to modify hemogen value for animals.
        /// </summary>
        /// <param name="__instance"></param>
        internal static void Prefix(Pawn victim, ref float targetHemogenGain)
        {
            if (!victim.RaceProps.Animal) return;

            targetHemogenGain *= settings.animalHemogenRate;
        }

        /// <summary>
        /// Postfix patches bloodfeed to update the diet comp.
        /// </summary>
        internal static void Postfix(Pawn biter, Pawn victim)
        {
            CompBloodfeederDiet diet = biter.GetComp<CompBloodfeederDiet>();
            if (diet == null) return;

            switch (victim.RaceProps.Animal)
            {
                case true: diet.ConsumeAnimalBlood(); break;
                case false: diet.ConsumeHumanBlood(); break;
            }
        }
    }
}
