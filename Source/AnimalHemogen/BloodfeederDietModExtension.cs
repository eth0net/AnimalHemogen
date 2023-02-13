using System.Runtime.Serialization;
using Verse;

namespace AnimalHemogen
{
    public class BloodfeederDietModExtension : DefModExtension
    {
        /// <summary>
        /// diet represents what kind of diet the def counts towards.
        /// Should be human or animal.
        /// </summary>
        public string diet = "human";
    }
}
