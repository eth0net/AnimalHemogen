using Verse;


namespace AnimalHemogen
{
    public class AnimalHemogenSettings : ModSettings
    {
        /// <summary>
        /// Animal hemogen rate.
        /// </summary>
        public float animalHemogenRate = 0.5f;

        /// <summary>
        /// ExposeData saves and loads the settings.
        /// </summary>
        public override void ExposeData()
        {
            Scribe_Values.Look(ref animalHemogenRate, "animalHemogenRate", 0.5f, false);
            base.ExposeData();
        }
    }
}
