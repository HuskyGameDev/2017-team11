using System.Collections.Generic;

namespace Entity
{
    /// <summary>
    /// Registries of various things.
    /// </summary>
    internal static class Registry
    {
        /// <summary>
        /// Ties <see cref="ActionType"/>s to <see cref="ActionKind"/>s and <see cref="ActionBehavior"/>s.
        /// <seealso cref="ActionBehavior"/>
        /// </summary>
        public static readonly Dictionary<ActionType, ActionDescription> ActionDescriptors =
            new Dictionary<ActionType, ActionDescription>
            {
                {ActionType.Bleeding,new ActionDescription(ActionKind.Condition, ActionBehavior.Capping, 6, 0.0f, 0.0f, 0.0f, 0.0f)},
                {ActionType.Burning, new ActionDescription(ActionKind.Condition, ActionBehavior.Capping, 10, 5.0f, 0.0f, 0.0f, 0.0f)}
            };

        /// <summary>
        /// Since physical damage varies per attack, unlikely condition damage, we can't use its descriptor.
        /// This constant is used in damage calculation, with poison and mental resist scaling set to 0.
        /// </summary>
        public const float PhysicalDamageArmorReductionScaling = 2.0f;
    }
}