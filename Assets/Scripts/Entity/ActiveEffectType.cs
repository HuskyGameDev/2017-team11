using System;

namespace Entity
{
    /// <summary>
    /// An active effect; this can be either a boon or a condition, or something else just for tracking.
    /// Each effect behaves differently; the default behavior is ActiveEffectKind.CappingDurationCondition.
    /// <seealso cref="ActiveEffectDescription"/>
    /// </summary>
    [Serializable]
    public enum ActiveEffectType
    {
        Burning,
        Bleeding
    }
}