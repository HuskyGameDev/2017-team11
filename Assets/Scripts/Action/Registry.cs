using System.Collections.Generic;
using Inventory;
using AI;

namespace Action
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
                {ActionType.Physical,new ActionDescription(ActionKind.Condition, ActionBehavior.Intensity, 2.0f, 0.0f, 0.0f, 0.0f)},
                {ActionType.Bleeding,new ActionDescription(ActionKind.Condition, ActionBehavior.Capping, 0.0f, 0.0f, 0.0f, 0.0f)},
                {ActionType.Burning, new ActionDescription(ActionKind.Condition, ActionBehavior.Capping, 5.0f, 0.0f, 0.0f, 0.0f)}
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

        public const string DefaultOnesieName = "Cat";


		public static readonly Dictionary<int, Onesie> OnesieList =
			new Dictionary<int, Onesie>
		{
			{0, new Onesie("cat", new List<Move> {new Move(new Attack(2))})},
			{1, new Onesie("box", new List<Move> {new Move(new Attack(2))})}
		};
    }
}