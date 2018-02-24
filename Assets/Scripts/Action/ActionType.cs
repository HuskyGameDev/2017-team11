using System;

namespace Action
{
    /// <summary>
    /// An active effect; this can be either a boon or a condition, or something else just for tracking.
    /// Each effect behaves differently; the default behavior is ActiveEffectKind.CappingDurationCondition.
    /// <seealso cref="ActionDescription"/>
    /// </summary>
    [Serializable]
    public enum ActionType
    {
        Physical,
        Burning,
        Bleeding
    }
}