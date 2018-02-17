using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Inventory;
using UnityEngine;

namespace Entity {
    /// <summary>
    /// A friendly or enemy entity w/ attributes, active effects, and inventory.
    /// </summary>
    [Serializable] public class Entity {
        public bool IsPlayer;
        public Attribute HitPoints = new Attribute(1);
        public Attribute Armor = new Attribute(0);
        public Attribute PhysicalResist = new Attribute(0);
        public Attribute MentalResist = new Attribute(0);

        /// <summary>
        /// Effects that are active on this entity.
        /// </summary>
        public readonly List<ActiveEffectType> EffectList = new List<ActiveEffectType>();

        /// <summary>
        /// Effects to a list of durations.
        /// </summary>
        public readonly Dictionary<ActiveEffectType, List<int>> Effects = new Dictionary<ActiveEffectType, List<int>>();

        /// <summary>
        /// All the effects the entity is immune to.
        /// </summary>
        public readonly HashSet<ActiveEffectType> Immunities = new HashSet<ActiveEffectType>();

        /// <summary>
        /// Equipped items: probably just onesies TODO
        /// </summary>
        public List<Item> EquippedInventory = new List<Item>();

        public bool IsDead {
            get { return HitPoints.CurrentProperty == 0; }
        }

        /// <summary>
        /// Checks if the entity is immune to a type of effect.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsImmuneTo(ActiveEffectType type) {return Immunities.Contains(type);}

        /// <summary>
        /// Checks if the entity has an effect on them currently.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasEffect(ActiveEffectType type) {return Effects.ContainsKey(type);}

        /// <summary>
        /// Applies physical damage and inserts/updates effects into the list/dictionary.
        /// </summary>
        /// <param name="attack">An attack</param>
        public void ApplyAttack(Attack attack) {
            for(var i = 0; i < attack.ActiveEffects.Length; i++) {
                Debug.Log($"Active Effect: {i}");
                var effect = attack.ActiveEffects[i];
                if(IsImmuneTo(effect.Type))
                    continue;
                var description = Registry.ActiveEffectKinds[effect.Type];
                if(!HasEffect(effect.Type)) {
                    EffectList.Add(effect.Type);
                    Effects[effect.Type] = new List<int>(effect.Duration);
                } else
                    switch(description.Behavior) {
                        case ActiveEffectBehavior.Capping:
                            Effects[effect.Type][0] = Math.Max(Effects[effect.Type][0], effect.Duration);
                            break;
                        case ActiveEffectBehavior.Duration:
                            Effects[effect.Type][0] += effect.Duration;
                            break;
                        case ActiveEffectBehavior.Intensity:
                            Effects[effect.Type].Add(effect.Duration);
                            break;
                    }
            }

            // if no physical damage, we're done here.
            if(attack.PhysicalDamage <= 0)
                return;
            var blockedByArmor = (int) Armor.Current * 2;
            if(attack.PhysicalDamage < blockedByArmor) {
                // no health damage, just damage the armor.
                Armor.Damage(attack.PhysicalDamage / 2);
                return;
            }

            // armor gone, damage health the rest.
            Armor.CurrentProperty = 0;
            HitPoints.Damage(attack.PhysicalDamage - blockedByArmor);
        }

        /// <summary>
        /// Applies the damage from all effects and ticks down the stacks.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void ProcessEffects() {
            for(var i = 0; i < EffectList.Count;) {
                var type = EffectList[i];
                var stacks = Effects[type];
                switch(type) {
                    case ActiveEffectType.Bleeding:
                        HitPoints.Damage(6);
                        break;
                    case ActiveEffectType.Burning:
                        var damageToTake = 10 * stacks.Count;
                        if(damageToTake == 0)
                            break;
                        var blockedByArmor = (int) Armor.Current * 5;
                        if(damageToTake < blockedByArmor) {
                            Armor.Damage(damageToTake / 5);
                        } else {
                            Armor.CurrentProperty = 0;
                            HitPoints.Damage(damageToTake - blockedByArmor);
                        }

                        break;
                    default: throw new ArgumentOutOfRangeException();
                }

                for(var j = 0; j < stacks.Count;) {
                    stacks[j] -= 1;
                    if(stacks[j] == 0) {
                        stacks.RemoveAt(j);
                    } else {
                        j += 1;
                    }
                }

                if(stacks.Count == 0) {
                    EffectList.RemoveAt(i);
                    Effects.Remove(type);
                    // If we removed it, we shouldn't advance.
                } else {
                    // If we didn't remove, go to the next one.
                    i += 1;
                }
            }
        }
    }
}
