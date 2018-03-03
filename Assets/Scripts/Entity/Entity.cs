using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Action;
using Inventory;
using UnityEngine;
using UnityEngine.VR.WSA;
using Random = UnityEngine.Random;

namespace Entity {
    /// <summary>
    /// A friendly or enemy entity w/ attributes, active effects, and inventory.
    /// </summary>
    [Serializable] public class Entity {
        public ushort EntityId = 0;

        public Attribute MentalResist = new Attribute(0);
        public Attribute Armor = new Attribute(0);
        public Attribute PoisonResist = new Attribute(0);
        public Attribute HitPoints = new Attribute(1);
        // Crit chance is / 100.0f when applied
        public Attribute CritChance = new Attribute(10);
        // Crit damage is / 100.0f when applied
        public Attribute CritDamage = new Attribute(50);

        public Entity() {
            Onesie.SetSpriteName(EntityId);
        }

        #region Inventory
        /// <summary>
        /// The equipped onesie.
        /// </summary>
        public Onesie Onesie = new Onesie(Registry.DefaultOnesieName);
        
        /// <summary>
        /// Equipped items: probably just onesies TODO
        /// </summary>
        public List<Item> EquippedInventory = new List<Item>();

        public Onesie EquipOnesie(Onesie onesie) {
            if(onesie == null)
                onesie = new Onesie(Registry.DefaultOnesieName);
            var oldOnesie = Onesie;
            Onesie = onesie;
            Onesie.SetSpriteName(EntityId);

            MentalResist.Temporary = MentalResist.Temporary - oldOnesie.MentalResistMod + Onesie.MentalResistMod;
            Armor.Temporary = Armor.Temporary - oldOnesie.ArmorMod + Onesie.ArmorMod;
            PoisonResist.Temporary = PoisonResist.Temporary - oldOnesie.PoisonResistMod + Onesie.PoisonResistMod;
            HitPoints.Temporary = HitPoints.Temporary - oldOnesie.HitPointsMod + Onesie.HitPointsMod;
            
            return oldOnesie;
        }
        #endregion

        #region Combat
        /// <summary>
        /// Effects that are active on this entity.
        /// </summary>
        public readonly List<ActionType> EffectList = new List<ActionType>();

        /// <summary>
        /// Effects to a list of durations.
        /// </summary>
        public readonly Dictionary<ActionType, List<int>> Effects = new Dictionary<ActionType, List<int>>();

        /// <summary>
        /// All the effects the entity is immune to.
        /// </summary>
        public readonly HashSet<ActionType> Immunities = new HashSet<ActionType>();
        
        /// <summary>
        /// Per-effect multipliers for taking damage/etc.
        /// </summary>
        public readonly Dictionary<ActionType, float> EffectModifiers = new Dictionary<ActionType, float>();
        
        public bool IsDead => HitPoints.CurrentProperty <= 0;

        /// <summary>
        /// Checks if the entity has an effect on them currently.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasEffect(ActionType type) {return Effects.ContainsKey(type);}

        /// <summary>
        /// Applies physical damage and inserts/updates effects into the list/dictionary.
        /// </summary>
        /// <param name="attack">An attack</param>
        public void ApplyAttack(Attack attack) {
            for(var i = 0; i < attack.Actions.Length; i++) {
                var type = attack.Actions[i].Type;
                var duration = attack.Actions[i].Duration;
                if(Immunities.Contains(type))
                    continue;
                var description = Registry.ActionDescriptors[type];
                if(!HasEffect(type)) {
                    EffectList.Add(type);
                    Effects[type] = new List<int>{ duration };
                } else {
                    switch(description.Behavior) {
                        case ActionBehavior.Capping:
                            Effects[type][0] = Math.Max(Effects[type][0], duration);
                            break;
                        case ActionBehavior.Duration:
                            Effects[type][0] += duration;
                            break;
                        case ActionBehavior.Intensity:
                            Effects[type].Add(duration);
                            break;
                    }
                }
            }

            // if physical damage, apply it.
            if(attack.PhysicalStrength > 0)
                Damage(ActionType.Physical, attack.PhysicalStrength);
        }

        /// <summary>
        /// Applies the damage from all persisting effects and ticks down the stacks.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void ProcessEffects() {
            for(var i = 0; i < EffectList.Count;) {
                var type = EffectList[i];
                var stacks = Effects[type];
                Damage(type, Registry.ActionStrength[type] * stacks.Count);

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

        /// <summary>Returns true if hit was critical. We can do deciding for stuff on here.</summary>
        /// <returns>True if the hit was critical (random &lt;= crit chance)</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool CriticalHit()
        {
            var it = Random.value <= CritChance.Current / 100.0f;
            if (it)
            {
                Debug.Log("Critical hit!");
            }
            else
            {
                Debug.Log("Normal hit!");
            }

            return it;
        }

        /// <summary>
        /// Damages an entity. Order of damage is: Mental, Armor, Poison, Health.
        /// </summary>
        /// <param name="type">Type of the attack. <see cref="ActionType"/></param>
        /// <param name="damage">Base damage to take, if there are no resistances, directly against health. <see cref="Registry"/></param>
        private void Damage(ActionType type, float damage) {
            var descriptor = Registry.ActionDescriptors[type];
            if (type == ActionType.Physical && CriticalHit())
            {
                damage *= 1 + CritDamage.Current / 100.0f;
            }
            if(damage < 0) {
                if(Onesie.EffectModifiers.ContainsKey(type))
                    damage /= Onesie.EffectModifiers[type];
                if(EffectModifiers.ContainsKey(type))
                    damage /= EffectModifiers[type];
                if(descriptor.MentalResistScaling > 0 && MentalResist.Absent > 0)
                    MentalResist.Damage(damage * descriptor.MentalResistScaling);

                if(descriptor.ArmorReductionScaling > 0 && Armor.Absent > 0)
                    Armor.Damage(damage * descriptor.ArmorReductionScaling);

                if(descriptor.PoisonResistScaling > 0 && PoisonResist.Absent > 0)
                    PoisonResist.Damage(damage * descriptor.PoisonResistScaling);

                if(descriptor.HealingFactor > 0 && HitPoints.Absent > 0)
                    HitPoints.Damage(damage * descriptor.HealingFactor);
            } else {
                if(Onesie.EffectModifiers.ContainsKey(type))
                    damage *= Onesie.EffectModifiers[type];
                if(EffectModifiers.ContainsKey(type))
                    damage *= EffectModifiers[type];
                
                if(MentalResist.Current > 0 && descriptor.MentalResistScaling > 0) {
                    // The half comes from https://stackoverflow.com/questions/904910/how-do-i-round-a-float-up-to-the-nearest-int-in-c#comment38709089_904925
                    var resisted = (int) (damage / descriptor.MentalResistScaling + 0.5f);
                    if(resisted < MentalResist.Current) {
                        MentalResist.Damage(resisted);
                        return;
                    }

                    MentalResist.Current = 0;
                    damage -= resisted;
                }

                if(Armor.Current > 0 && descriptor.ArmorReductionScaling > 0) {
                    var resisted = (int) (damage / descriptor.ArmorReductionScaling + 0.5f);
                    if(resisted < Armor.Current) {
                        Armor.Damage(resisted);
                        return;
                    }

                    Armor.Current = 0;
                    damage -= resisted;
                }

                if(PoisonResist.Current > 0 && descriptor.PoisonResistScaling > 0) {
                    var resisted = (int) (damage / descriptor.PoisonResistScaling + 0.5f);
                    if(resisted < PoisonResist.Current) {
                        PoisonResist.Damage(resisted);
                        return;
                    }

                    PoisonResist.Current = 0;
                    damage -= resisted;
                }

                if(damage > 0)
                    HitPoints.Damage(damage);
            }
        }
        #endregion
    }
}
