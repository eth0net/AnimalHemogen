using HarmonyLib;
using RimWorld;
using Verse;

namespace AnimalHemogen.Harmony
{
    /// <summary>
    /// Harmony_SanguophageUtility_DoBite patches dobite to work with animals.
    /// </summary>
    [HarmonyPatch(typeof(SanguophageUtility), nameof(SanguophageUtility.DoBite))]
    internal static class Harmony_SanguophageUtility_DoBite
    {
        /// <summary>
        /// Prefix patches bloodfeed to count animals as valid.
        /// </summary>
        /// <param name="__instance"></param>
        internal static void Prefix(Pawn victim, ref float targetHemogenGain)
        {
            if (victim == null || !victim.RaceProps.Animal) return;

            targetHemogenGain *= 0.5f;
        }
    }
}
