using RimWorld;
using System.Collections.Generic;
using Verse;

namespace AnimalHemogen
{
    /// <summary>
    /// A recipe to extract hemogen from an animal.
    /// </summary>
    public class Recipe_ExtractAnimalHemogen : Recipe_ExtractHemogen
    {
        /// <summary>
        /// Drop an animal hemogen pack on the ground when the surgery is successful.
        /// </summary>
        protected override void OnSurgerySuccess(Pawn pawn, BodyPartRecord part, Pawn billDoer, List<Thing> ingredients, Bill bill)
        {
            if (!GenPlace.TryPlaceThing(ThingMaker.MakeThing(ThingDef.Named("AnimalHemogenPack"), null), pawn.PositionHeld, pawn.MapHeld, ThingPlaceMode.Near, null, null, default))
            {
                Log.Error("Could not drop animal hemogen pack near " + pawn.PositionHeld);
            }
        }
    }
}
