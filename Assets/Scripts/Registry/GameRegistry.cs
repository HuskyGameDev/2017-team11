﻿using System;
using System.Collections.Generic;
using Action;

namespace Registry
{
    /// <summary>
    /// Registries of various things.
    /// </summary>
    internal static class GameRegistry
    {
        /// <summary>
        /// Ties <see cref="ActionType"/>s to <see cref="ActionKind"/>s and <see cref="ActionBehavior"/>s.
        /// <seealso cref="ActionBehavior"/>
        /// </summary>
        public static readonly Dictionary<ActionType, ActionDescription> ActionDescriptors =
            new Dictionary<ActionType, ActionDescription>
            {
                {ActionType.Physical,new ActionDescription(ActionKind.Condition, ActionBehavior.Intensity, 2.0f, 0.0f, 0.0f, 0.0f)},
                {ActionType.Bleeding,new ActionDescription(ActionKind.Condition, ActionBehavior.Capping, 0.0f, 0.0f, 0.0f, 0.0f)},
                {ActionType.Burning, new ActionDescription(ActionKind.Condition, ActionBehavior.Capping, 5.0f, 0.0f, 0.0f, 0.0f)},
                {ActionType.Poison, new ActionDescription(ActionKind.Condition, ActionBehavior.Capping, 0.0f, 0.5f, 0.0f, 0.0f)}
            };
        
        /// <summary>
        /// Base damage for <see cref="ActionType"/>s.
        /// </summary>
        public static readonly Dictionary<ActionType, int> ActionStrength =
            new Dictionary<ActionType, int> {
                {ActionType.Physical, 0},
                {ActionType.Bleeding, 6},
                {ActionType.Burning, 10}
            };
        
        /// <summary>
        /// The sole (non-Unity) RNG for the game; used for randomly selecting elements from arrays. 
        /// </summary>
        public static readonly Random Random = new Random();
    }
}