using System;
using System.Collections.Generic;
using Action;

namespace Inventory {
    [Serializable]
    public class Onesie: Item {
        public readonly string BaseSpriteName;
        
        public readonly int HitPointsMod;
        public readonly int ArmorMod;
        public readonly int PoisonResistMod;
        public readonly int MentalResistMod;
        public readonly int CritChance;
        public readonly int CritDamage;

        public readonly string OnHitSoundEventName;

        public readonly Attack[] Attacks;

        /// <summary>
        /// All the effects the onsie makes the entitiy immune to.
        /// </summary>
        public readonly HashSet<ActionType> Immunities = new HashSet<ActionType>();
        /// <summary>
        /// Per-effect multipliers for taking damage/etc.
        /// </summary>
        // ReSharper disable once CollectionNeverUpdated.Global
        public readonly Dictionary<ActionType, float> EffectModifiers = new Dictionary<ActionType, float>();

        public Onesie(string baseName, Attack[] attacks, int hitPointsMod = 0, int armorMod = 0, int poisonResistMod = 0,
            int mentalResistMod = 0, int critChanceMod = 0, int critDamageMod = 0, string onHitSoundEventName = null)
        {
            BaseSpriteName = baseName;
            if (attacks == null)
                throw new ArgumentNullException(nameof(attacks), "Attacks array is null.");
            if (attacks.Length != 4)
                throw new ArgumentOutOfRangeException(nameof(attacks), "Attacks array not of length 4.");
            Attacks = attacks;
            HitPointsMod = hitPointsMod;
            ArmorMod = armorMod;
            PoisonResistMod = poisonResistMod;
            MentalResistMod = mentalResistMod;
            CritChance = critChanceMod;
            CritDamage = critDamageMod;
            OnHitSoundEventName = onHitSoundEventName;
        }
    }
}
