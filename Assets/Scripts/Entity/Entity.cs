using System;
using System.Collections.Generic;
using Inventory;
using UnityEditor;

namespace Entity
{
    /// <summary>
    /// A friendly or enemy entity w/ attributes, active effects, and inventory.
    /// </summary>
    [Serializable]
    public class Entity
    {
        public bool IsPlayer;
        public Attribute HitPoints = new Attribute(1);
        public Attribute Armor = new Attribute(0);
        public Attribute PhysicalResist = new Attribute(0);
        public Attribute MentalResist = new Attribute(0);

        /// <summary>
        /// Effects to a list of durations.
        /// </summary>
        public readonly Dictionary<ActiveEffectType, List<int>> Effects = new Dictionary<ActiveEffectType, List<int>>();

        public readonly Dictionary<ActiveEffectType, bool> Immunities = new Dictionary<ActiveEffectType, bool>();

        public List<Item> EquippedInventory = new List<Item>();

        public bool IsDead
        {
            get { return HitPoints.Current == 0; }
        }

        public bool IsImmuneTo(ActiveEffectType type)
        {
            return Immunities.ContainsKey(type) && Immunities[type];
        }

        public bool HasEffect(ActiveEffectType type)
        {
            return Effects.ContainsKey(type);
        }

        public void ApplyAttack(int physicalDamage, params ActiveEffect[] activeEffects)
        {
            foreach (var effect in activeEffects)
            {
                if (IsImmuneTo(effect.Type)) continue;
                var description = Registry.ActiveEffectKinds[effect.Type];
                if (!HasEffect(effect.Type))
                {
                    Effects[effect.Type] = new List<int>(effect.Duration);
                }
                else
                    switch (description.Behavior)
                    {
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

            if (physicalDamage > 0)
            {
                var blockedByArmor = (int) Armor.Current * 2;
                if (physicalDamage < blockedByArmor)
                {
                    Armor.Damage(physicalDamage / 2);
                    return;
                }

                Armor.Current = 0;
                HitPoints.Damage(physicalDamage - blockedByArmor);
            }
        }

        public void ProcessEffects()
        {
            var remove = new List<ActiveEffectType>();
            foreach (var effect in Effects)
            {
                switch (effect.Key)
                {
                    case ActiveEffectType.Burning:
                        var damageToTake = 10 * effect.Value.Count;
                        if (damageToTake > 0)
                        {
                            var blockedByArmor = (int) Armor.Current * 5;
                            if (damageToTake < blockedByArmor)
                            {
                                Armor.Damage(damageToTake / 5);
                                continue;
                            }

                            Armor.Current = 0;
                            HitPoints.Damage(damageToTake - blockedByArmor);
                        }
                        break;
                    case ActiveEffectType.Bleeding:
                        HitPoints.Damage(6);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                for (var i = 0; i < effect.Value.Count;)
                {
                    effect.Value[i] -= 1;
                    if (effect.Value[i] == 0)
                    {
                        effect.Value.RemoveAt(i);
                        continue;
                    }

                    i += 1;
                }

                if (effect.Value.Count == 0)
                {
                    remove.Add(effect.Key);
                }
            }

            foreach (var effectType in remove)
            {
                Effects.Remove(effectType);
            }
        }
    }

    public struct ActiveEffect
    {
        public ActiveEffectType Type;
        public int Duration;
    }
}