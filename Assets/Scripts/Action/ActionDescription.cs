namespace Action
{
    /// <summary>
    /// A description of what the effect is and how it behaves when applied more than once,
    /// and how it interacts with the various resistances. If a resistance has a value of zero,
    /// that resistance is bypassed.
    /// <seealso cref="ActionType"/>
    /// </summary>
    internal struct ActionDescription
    {
        public readonly ActionKind Kind;
        public readonly ActionBehavior Behavior;
        public readonly int BaseDamage;
        public readonly float ArmorReductionScaling;
        public readonly float PoisonResistScaling;
        public readonly float MentalResistScaling;
        public readonly float HealingFactor;

        public ActionDescription(ActionKind kind, ActionBehavior behavior, int baseDamage, float armorScaling, float poisonResistScaling, float mentalResistScaling, float healingFactor)
        {
            Kind = kind;
            Behavior = behavior;
            BaseDamage = baseDamage;
            ArmorReductionScaling = armorScaling;
            PoisonResistScaling = poisonResistScaling;
            MentalResistScaling = mentalResistScaling;
            HealingFactor = healingFactor;
        }
    }
}