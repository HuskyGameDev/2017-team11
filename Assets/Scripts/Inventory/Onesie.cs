using System;
using System.Collections.Generic;
using Action;
using UnityEngine;

namespace Inventory {
    [Serializable]
    public class Onesie: Item {
        private readonly string _baseName;
        public string SpriteName;
        
        public int HitPointsMod;
        public int ArmorMod;
        public int PoisonResistMod;
        public int MentalResistMod;
        public int CritChance;
        public int CritDamage;

        public string OnHitSoundEventName = null;

        public readonly Attack[] Attacks;

        /// <summary>
        /// All the effects the onsie makes the entitiy immune to.
        /// </summary>
        public readonly HashSet<ActionType> Immunities = new HashSet<ActionType>();
        /// <summary>
        /// Per-effect multipliers for taking damage/etc.
        /// </summary>
        public readonly Dictionary<ActionType, float> EffectModifiers = new Dictionary<ActionType, float>();

        public Onesie(string baseName, Attack[] attacks)
        {
            _baseName = baseName;
            if (attacks == null)
                throw new ArgumentNullException(nameof(attacks), "Attacks array is null.");
            if (attacks.Length != 4)
                throw new ArgumentOutOfRangeException(nameof(attacks), "Attacks array not of length 4.");
            Attacks = attacks;
        }

        public void SetSpriteName(Entity.Entity entity)
        {
            SpriteName = $"Onesie/{entity.SpriteType}/{_baseName}";
            Debug.Log($"Set sprite name to \"{SpriteName}\"");
        }
    }
}
