using Inventory;

namespace Action
{
    public class OnesieRegistry
    {
        public const string DefaultOnesieName = "Cat";

        public static Onesie DefaultOnesie = new Onesie(DefaultOnesieName)
        {
            ArmorMod = 5,
            CritChance = 3
        };
        
        public static Onesie BoxOnesie = new Onesie("Box");
    }
}