using Verse;

namespace AnimalHemogen
{
    /// <summary>
    /// Settings for the AnimalHemogen mod.
    /// </summary>
    public class AnimalHemogenSettings : ModSettings
    {
        /// <summary>
        /// Animal hemogen rate, compared to human hemogen.
        /// </summary>
        public float animalHemogenRate = 0.5f;

        /// <summary>
        /// Minimum number of days since human bloodfeeding.
        /// </summary>
        public int daysSinceHumanBloodThreshold = 3;

        /// <summary>
        /// Minimum number of feeds since human bloodfeeding.
        /// </summary>
        public int feedsSinceHumanBloodThreshold = 3;

        /// <summary>
        /// ExposeData saves and loads the settings.
        /// </summary>
        public override void ExposeData()
        {
            Scribe_Values.Look(ref animalHemogenRate, "animalHemogenRate", 0.5f, false);
            Scribe_Values.Look(ref daysSinceHumanBloodThreshold, "daysSinceHumanBloodThreshold", 3, false);
            Scribe_Values.Look(ref feedsSinceHumanBloodThreshold, "feedsSinceHumanBloodThreshold", 3, false);
            base.ExposeData();
        }
    }
}
