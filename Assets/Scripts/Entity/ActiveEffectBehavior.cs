namespace Entity
{
    /// <summary>
    /// Behaviors of effects.
    /// </summary>
    internal enum ActiveEffectBehavior
    {
        /// <summary>
        /// Additional applications will reset the duration to the maximum of the remaining durations.
        /// </summary>
        Capping = 0,
        /// <summary>
        /// Additional applications will add another entry to the list, increasing the strength of the effect.
        /// </summary>
        Intensity = 1,
        /// <summary>
        /// Additional applications will increase the remaining duration of the effect.
        /// </summary>
        Duration = 2
    }
}