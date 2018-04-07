using Inventory;

namespace Action
{
    public class OnesieRegistry
    {
        public const string DefaultOnesieName = "Cat";

        public static Onesie DefaultOnesie = new Onesie(DefaultOnesieName, new Attack[]
        {
            new Attack(1),
            new Attack(0, new Action(ActionType.Bleeding, 4)),
            new Attack(0, new Action(ActionType.Burning, 4)),
            new Attack(0, new Action(ActionType.Bleeding, 2), new Action(ActionType.Burning, 2)) 
        }, armorMod: 5, critChanceMod: 3);
        
        public static Onesie BoxOnesie = new Onesie("Box", new Attack[]
        {
            new Attack(1),
            new Attack(1),
            new Attack(1),
            new Attack(1) 
        });
    }
}