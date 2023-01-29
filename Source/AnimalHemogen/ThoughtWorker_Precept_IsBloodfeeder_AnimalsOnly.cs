using RimWorld;
using Verse;

namespace AnimalHemogen
{
    /// <summary>
    /// A thought worker for animal bloodfeeder thoughts.
    /// </summary>
    public class ThoughtWorker_Precept_IsBloodfeeder_AnimalsOnly : ThoughtWorker_Precept
    {
        /// <summary>
        /// Check if the pawn is a bloodfeeder and set thought accordingly.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        protected override ThoughtState ShouldHaveThought(Pawn p)
        {
            if (!p.IsBloodfeeder()) return ThoughtState.Inactive;

            CompBloodfeederDiet diet = p.GetComp<CompBloodfeederDiet>();
            if (diet == null) return ThoughtState.Inactive;

            if (diet.IsHumanBlood) return ThoughtState.ActiveAtStage(1);

            return ThoughtState.ActiveAtStage(0);
        }
    }
}
