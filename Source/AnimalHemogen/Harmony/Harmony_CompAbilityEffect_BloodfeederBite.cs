using HarmonyLib;
using RimWorld;
using Verse;

namespace AnimalHemogen.Harmony
{
    /// <summary>
    /// Harmony_CompAbilityEffect_BloodfeederBite_Valid patches bloodfeed to work with animals.
    /// </summary>
    [HarmonyPatch(typeof(CompAbilityEffect_BloodfeederBite), nameof(CompAbilityEffect_BloodfeederBite.Valid))]
    internal static class Harmony_CompAbilityEffect_BloodfeederBite_Valid
    {
        /// <summary>
        /// Prefix patches bloodfeed to count animals as valid.
        /// </summary>
        /// <param name="__instance"></param>
        internal static bool Prefix(CompAbilityEffect_BloodfeederBite __instance, ref bool __result, LocalTargetInfo target, bool throwMessages = false)
        {
            Pawn pawn = target.Pawn;
            if (pawn == null || !pawn.RaceProps.Animal) { return true; }

            if (pawn.InMentalState)
            {
                if (throwMessages)
                {
                    Messages.Message("MessageCantUseOnResistingPerson".Translate(__instance.parent.def.Named("ABILITY")), pawn, MessageTypeDefOf.RejectInput, false);
                }
                __result = false;
                return false;
            }

            __result = true;
            return false;
        }
    }
}
