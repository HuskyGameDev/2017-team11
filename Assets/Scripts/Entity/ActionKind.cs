namespace Entity
{
    /// <summary>
    /// Kinds of effects that exist.
    /// </summary>
    internal enum ActionKind
    {
        /// <summary>
        /// A negative effect.
        /// </summary>
        Condition = 0,
        /// <summary>
        /// A positive effect.
        /// </summary>
        Boon = 1,
        /// <summary>
        /// An effect with no effect.
        /// </summary>
        Tracking = 2
    }
}