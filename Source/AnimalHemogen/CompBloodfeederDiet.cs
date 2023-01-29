using Verse;

namespace AnimalHemogen
{
    /// <summary>
    /// Component for tracking bloodfeeder diet.
    /// </summary>
    public class CompBloodfeederDiet : ThingComp
    {
        /// <summary>
        /// Load a reference to mod settings.
        /// </summary>
        static readonly AnimalHemogenSettings settings = LoadedModManager.GetMod<AnimalHemogen>().GetSettings<AnimalHemogenSettings>();

        /// <summary>
        /// Number of feeds since bloodfeeding on a human.
        /// </summary>
        public int feedsSinceHumanBlood = 0;

        /// <summary>
        /// Tick when last human bloodfeeding.
        /// </summary>
        public int lastTickHumanBlood;

        /// <summary>
        /// Whether the bloodfeeder is considered a human bloodfeeder.
        /// </summary>
        public bool IsHumanBlood => HasEverConsumedHumanBlood && !(DaysOverThreshold && FeedsOverThreshold);

        /// <summary>
        /// The number of days since feeding on human blood. Use HasEverConsumedHumanBlood to determine if they have ever fed. Will equal current game tick if they have never fed.
        /// </summary>
        private float DaysSinceHumanBlood => (Find.TickManager.TicksGame - lastTickHumanBlood) / 60000f;

        /// <summary>
        /// Whether the bloodfeeder has ever consumed human blood.
        /// </summary>
        public bool HasEverConsumedHumanBlood => lastTickHumanBlood > 0;


        /// <summary>
        /// Whether it's been long enough since feeding on human blood to be considered animal bloodfeeder.
        /// </summary>
        private bool DaysOverThreshold => DaysSinceHumanBlood > settings.daysSinceHumanBloodThreshold;

        /// <summary>
        /// True if enough animal feeds have been done since feeding on human blood.
        /// </summary>
        private bool FeedsOverThreshold => feedsSinceHumanBlood > settings.feedsSinceHumanBloodThreshold;

        /// <summary>
        /// Update the diet comp to reflect consuming human blood.
        /// </summary>
        public void ConsumeHumanBlood()
        {
            feedsSinceHumanBlood = 0;
            lastTickHumanBlood = Find.TickManager.TicksGame;
        }

        /// <summary>
        /// Update the diet comp to reflect consuming animal blood.
        /// </summary>
        public void ConsumeAnimalBlood()
        {
            feedsSinceHumanBlood++;
        }
    }
}
