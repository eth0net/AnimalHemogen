using RimWorld;
using Verse;

namespace AnimalHemogen
{
    /// <summary>
    /// A thought worker for animal bloodfeeder present thoughts.
    /// </summary>
    public class ThoughtWorker_Precept_BloodfeederPresent_AnimalOnly : ThoughtWorker_Precept
    {
        /// <summary>
        /// Check if there is any bloodfeeder present and set thought accordingly.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        protected override ThoughtState ShouldHaveThought(Pawn p)
        {
            ThoughtState state = ThoughtState.Inactive;

            foreach (Pawn pawn in p.MapHeld.mapPawns.AllPawnsSpawned)
            {
                if (!pawn.IsBloodfeeder() || (!pawn.IsPrisonerOfColony && !pawn.IsSlaveOfColony && !pawn.IsColonist)) continue;

                CompBloodfeederDiet diet = p.GetComp<CompBloodfeederDiet>();
                if (diet == null) continue;

                if (diet.IsHumanBlood) { state = ThoughtState.ActiveAtStage(1); break; }

                state = ThoughtState.ActiveAtStage(0);
            }

            return state;
        }
    }
}
