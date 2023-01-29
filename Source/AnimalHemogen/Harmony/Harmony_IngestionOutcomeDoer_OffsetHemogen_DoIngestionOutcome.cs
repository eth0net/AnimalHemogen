using HarmonyLib;
using RimWorld;
using Verse;

namespace AnimalHemogen.Harmony
{
    /// <summary>
    /// Harmony_CompAbilityEffect_BloodfeederBite_Valid patches bloodfeed to work with animals.
    /// </summary>
    [HarmonyPatch(typeof(IngestionOutcomeDoer_OffsetHemogen), nameof(IngestionOutcomeDoer_OffsetHemogen.DoIngestionOutcome))]
    internal static class Harmony_IngestionOutcomeDoer_OffsetHemogen_DoIngestionOutcome
    {
        /// <summary>
        /// Postfix patches ingesting hemogen packs to update the diet comp.
        /// </summary>
        internal static void Postfix(Pawn pawn, Thing ingested)
        {
            CompBloodfeederDiet diet = pawn.GetComp<CompBloodfeederDiet>();
            if (diet == null) return;

            switch (ingested.def.defName)
            {
                case "HemogenPack": diet.ConsumeHumanBlood(); break;
                case "AnimalHemogenPack": diet.ConsumeAnimalBlood(); break;
                default: diet.ConsumeHumanBlood(); break;
            };
        }
    }
}
