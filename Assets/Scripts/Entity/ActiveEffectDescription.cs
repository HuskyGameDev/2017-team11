namespace Entity
{
    /// <summary>
    /// A description of what the effect is and how it behaves when applied more than once.
    /// <seealso cref="ActiveEffectType"/>
    /// </summary>
    internal struct ActiveEffectDescription
    {
        public readonly ActiveEffectKind Kind;
        public readonly ActiveEffectBehavior Behavior;

        public ActiveEffectDescription(ActiveEffectKind kind, ActiveEffectBehavior behavior)
        {
            Kind = kind;
            Behavior = behavior;
        }
    }
}