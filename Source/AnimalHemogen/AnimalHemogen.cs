using RimWorld;
using UnityEngine;
using Verse;


namespace AnimalHemogen
{
    public class AnimalHemogen : Mod
    {
        /// <summary>
        /// Animal hemogen settings reference.
        /// </summary>
        readonly AnimalHemogenSettings settings;

        /// <summary>
        /// Animal hemogen mod constructor.
        /// </summary>
        /// <param name="content"></param>
        public AnimalHemogen(ModContentPack content) : base(content)
        {
            settings = GetSettings<AnimalHemogenSettings>();
        }

        /// <summary>
        /// DoSettingsWindowContents configures the settings window.
        /// </summary>
        /// <param name="inRect"></param>
        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listing = new();

            listing.Begin(inRect);

            listing.Label("Animal hemogen rate: " + settings.animalHemogenRate.ToString("0.00") + "   Default: 0.50   (1.00 matches human hemogen)");
            settings.animalHemogenRate = listing.Slider(settings.animalHemogenRate, 0f, 2f);

            listing.End();

            base.DoSettingsWindowContents(inRect);
        }

        /// <summary>
        /// SettingsCategory returns the name of the settings category.
        /// </summary>
        /// <returns></returns>
        public override string SettingsCategory()
        {
            return "AnimalHemogen".Translate();
        }
    }
}
