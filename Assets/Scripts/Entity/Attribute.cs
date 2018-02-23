using System;

namespace Entity
{
    /// <summary>
    /// An attribute; it has a maximum, and a current value, with a minimum of 0.
    /// Also allows for temporary bonuses.
    /// </summary>
    [Serializable]
    public struct Attribute
    {
        /// <summary>
        /// The current value.
        /// </summary>
        public uint Current;
        /// <summary>
        /// The maximum value (less temporary).
        /// </summary>
        public ushort Maximum;
        /// <summary>
        /// The temporary bonus (added to maximum in checking).
        /// </summary>
        public ushort Temporary;

        public Attribute(ushort value) : this(value, value, 0) {}
        public Attribute(ushort current, ushort maximum) : this(current, maximum, 0) {}
        public Attribute(ushort current, ushort maximum, ushort temporary)
        {
            Maximum = maximum;
            Temporary = temporary;
            Current = 0;
            CurrentProperty = current;
        }
        
        /// <summary>
        /// Wraps <see cref="Current"/> with setter to make sure we don't exceed the maximum + temporary limit.
        /// </summary>
        public uint CurrentProperty
        {
            get { return Current; }
            set
            {
                if (value > Maximum + Temporary)
                {
                    Current = (ushort) Math.Min(value, Maximum + Temporary);
                }
                else
                {
                    Current = value;
                }
            }
        }

        public uint Absent => (uint) (Maximum + Temporary) - Current;

        /// <summary>
        /// Damages the current effect. This checks we don't go out of [0, Max + Temp].
        /// </summary>
        /// <param name="amount">The amount of damage to take, or heal (if negative).</param>
        public void Damage(double amount)
        {
            if (amount > Current)
                Current = 0;
            else if (amount < 0)
                CurrentProperty = Current + (uint) -amount;
            else
                Current = Current - (uint) amount;
        }
    }
}