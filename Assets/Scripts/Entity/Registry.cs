using System.Collections.Generic;

namespace Entity
{
    /// <summary>
    /// Registries of various things.
    /// </summary>
    internal static class Registry
    {
        /// <summary>
        /// Ties <see cref="ActiveEffectType"/>s to <see cref="ActiveEffectKind"/>s and <see cref="ActiveEffectBehavior"/>s.
        /// <seealso cref="ActiveEffectBehavior"/>
        /// </summary>
        public static readonly Dictionary<ActiveEffectType, ActiveEffectDescription> ActiveEffectKinds =
            new Dictionary<ActiveEffectType, ActiveEffectDescription>
            {
                {ActiveEffectType.Bleeding,new ActiveEffectDescription(ActiveEffectKind.Condition, ActiveEffectBehavior.Capping)},
                {ActiveEffectType.Burning, new ActiveEffectDescription(ActiveEffectKind.Condition, ActiveEffectBehavior.Capping)}
            };
    }
}