using RimWorld;
using System.Collections.Generic;
using Verse;

namespace AnimalHemogen
{
    internal class Recipe_ExtractAnimalHemogen : Recipe_ExtractHemogen
    {
        protected override void OnSurgerySuccess(Pawn pawn, BodyPartRecord part, Pawn billDoer, List<Thing> ingredients, Bill bill)
        {
            if (!GenPlace.TryPlaceThing(ThingMaker.MakeThing(ThingDef.Named("AnimalHemogenPack"), null), pawn.PositionHeld, pawn.MapHeld, ThingPlaceMode.Near, null, null, default(Rot4)))
            {
                Log.Error("Could not drop animal hemogen pack near " + pawn.PositionHeld);
            }
        }
    }
}
