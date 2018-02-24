using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Action;
using Inventory;
using UnityEngine;
using UnityEngine.VR.WSA;

namespace Entity {
    /// <summary>
    /// A friendly or enemy entity w/ attributes, active effects, and inventory.
    /// </summary>
    [Serializable] public class Entity {
        public bool IsPlayer;

        public Attribute MentalResist = new Attribute(0);
        public Attribute Armor = new Attribute(0);
        public Attribute PoisonResist = new Attribute(0);
        public Attribute HitPoints = new Attribute(1);

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
        public bool IsImmuneTo(ActionType type) {return Immunities.Contains(type);}

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
                Damage(attack.PhysicalStrength, Registry.PhysicalDamageArmorReductionScaling, 0.0f, 0.0f, 0.0f);
        }

        /// <summary>
        /// Applies the damage from all persisting effects and ticks down the stacks.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void ProcessEffects() {
            for(var i = 0; i < EffectList.Count;) {
                var type = EffectList[i];
                var stacks = Effects[type];
                var descriptor = Registry.ActionDescriptors[type];
                Debug.Log($"Taking {type} of damage {descriptor.BaseDamage} of stacks {stacks.Count}");
                Damage(descriptor.BaseDamage * stacks.Count,
                       descriptor.ArmorReductionScaling,
                       descriptor.PoisonResistScaling,
                       descriptor.MentalResistScaling,
                       descriptor.HealingFactor);

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

        /// <summary>
        /// Damages an entity. Order of damage is: Mental, Armor, Poison, Health.
        /// </summary>
        /// <param name="damage">Base damage to take, if there are no resistances, directly against health.</param>
        /// <param name="armorScaling">By what factor armor affects damage.</param>
        /// <param name="poisonScaling">By what factor poison resist affects damage.</param>
        /// <param name="mentalScaling">By what factor mental resist affects damage.</param>
        /// <param name="healingFactor">If damage is negative, by what factor does healing scale off that damage?</param>
        private void Damage(int damage, float armorScaling, float poisonScaling, float mentalScaling, float healingFactor) {
            if(damage < 0) {
                if(mentalScaling > 0 && MentalResist.Absent > 0)
                    MentalResist.Damage(damage * mentalScaling);

                if(armorScaling > 0 && Armor.Absent > 0)
                    Armor.Damage(damage * armorScaling);

                if(poisonScaling > 0 && PoisonResist.Absent > 0)
                    PoisonResist.Damage(damage * poisonScaling);

                if(healingFactor > 0 && HitPoints.Absent > 0)
                    HitPoints.Damage(damage * healingFactor);
            } else {
                if(MentalResist.Current > 0 && mentalScaling > 0) {
                    // The half comes from https://stackoverflow.com/questions/904910/how-do-i-round-a-float-up-to-the-nearest-int-in-c#comment38709089_904925
                    var resisted = (int) (damage / mentalScaling + 0.5f);
                    Debug.Log($"Resisted {resisted} Mental Damage");
                    if(resisted < MentalResist.Current) {
                        MentalResist.Damage(resisted);
                        return;
                    }

                    MentalResist.Current = 0;
                    damage -= resisted;
                }

                if(Armor.Current > 0 && armorScaling > 0) {
                    var resisted = (int) (damage / armorScaling + 0.5f);
                    Debug.Log($"Resisted {resisted} Armor Damage");
                    if(resisted < Armor.Current) {
                        Armor.Damage(resisted);
                        return;
                    }

                    Armor.Current = 0;
                    damage -= resisted;
                }

                if(PoisonResist.Current > 0 && poisonScaling > 0) {
                    var resisted = (int) (damage / poisonScaling + 0.5f);
                    Debug.Log($"Resisted {resisted} Poison Damage");
                    if(resisted < PoisonResist.Current) {
                        PoisonResist.Damage(resisted);
                        return;
                    }

                    PoisonResist.Current = 0;
                    damage -= resisted;
                }

                Debug.Log($"Taking {damage} damage.");
                if(damage > 0)
                    HitPoints.Damage(damage);
            }
        }
    }
}
