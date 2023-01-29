using RimWorld;
using Verse;

namespace AnimalHemogen
{
    /// <summary>
    /// A thought worker for animal bloodfeeder social thoughts.
    /// </summary>
    public class ThoughtWorker_Precept_Bloodfeeder_Social_AnimalsOnly : ThoughtWorker_Precept_Social
    {
        /// <summary>
        /// Check if the other pawn is a bloodfeeder and set thought accordingly.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="otherPawn"></param>
        /// <returns></returns>
        protected override ThoughtState ShouldHaveThought(Pawn p, Pawn otherPawn)
        {
            if (!otherPawn.IsBloodfeeder()) return ThoughtState.Inactive;

            CompBloodfeederDiet diet = otherPawn.GetComp<CompBloodfeederDiet>();
            if (diet == null) return ThoughtState.Inactive;

            if (diet.IsHumanBlood) return ThoughtState.ActiveAtStage(1);

            return ThoughtState.ActiveAtStage(0);
        }
    }
}
