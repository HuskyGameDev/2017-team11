using Action;
using Inventory;

namespace Registry
{
    public class OnesieRegistry
    {
        public const string DefaultOnesieName = "Cat";

        public static readonly Onesie DefaultOnesie = new Onesie(DefaultOnesieName, new Attack[]
        {
            new Attack(1),
            new Attack(0, new Action.Action(ActionType.Bleeding, 4)),
            new Attack(0, new Action.Action(ActionType.Burning, 4)),
            new Attack(0, new Action.Action(ActionType.Bleeding, 2), new Action.Action(ActionType.Burning, 2))
        }, armorMod: 5, critChanceMod: 3, onHitSoundEventName: "Cat_Pain_Meow");
        
        public static readonly Onesie BoxOnesie = new Onesie("Box", new Attack[]
        {
            new Attack(1),
            new Attack(1),
            new Attack(1),
            new Attack(1) 
        });
    }
}