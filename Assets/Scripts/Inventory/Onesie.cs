using System;
using System.Collections.Generic;
using Action;
using UnityEngine;
using AI;

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

        /// <summary>
        /// All the effects the onsie makes the entitiy immune to.
        /// </summary>
        public readonly HashSet<ActionType> Immunities = new HashSet<ActionType>();
        /// <summary>
        /// Per-effect multipliers for taking damage/etc.
        /// </summary>
        public readonly Dictionary<ActionType, float> EffectModifiers = new Dictionary<ActionType, float>();

		// Holds the set of attacks the onsie adds to the cat
		public readonly List<Move> Moves = new List<Move> ();

		public Onesie(string baseName, List<Move> _Moves)
		{
			_baseName = baseName;
			Moves = _Moves;
		}

        public void SetSpriteName(ushort entityId) {
            SpriteName = $"Onesie/{_baseName}{entityId:x4}";
            Debug.Log($"Set sprite name to \"{SpriteName}\" from ID {entityId}");
        }
    }
}
