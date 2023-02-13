using HarmonyLib;
using RimWorld;
using Verse;

namespace AnimalHemogen.Harmony
{
    /// <summary>
    /// Harmony_IngestionOutcomeDoer_OffsetHemogen_DoIngestionOutcomeSpecial patches hemogen packs to update the diet comp.
    /// </summary>
    [HarmonyPatch(typeof(IngestionOutcomeDoer_OffsetHemogen), "DoIngestionOutcomeSpecial")]
    internal static class Harmony_IngestionOutcomeDoer_OffsetHemogen_DoIngestionOutcomeSpecial
    {
        /// <summary>
        /// Postfix patches ingesting hemogen packs to update the diet comp.
        /// </summary>
        internal static void Postfix(Pawn pawn, Thing ingested)
        {
            CompBloodfeederDiet comp = pawn.GetComp<CompBloodfeederDiet>();
            if (comp == null) return;

            switch (ingested.def.GetModExtension<BloodfeederDietModExtension>()?.diet)
            {
                case "animal": comp.ConsumeAnimalBlood(); break;
                case "human": comp.ConsumeHumanBlood(); break;
            };
        }
    }
}
