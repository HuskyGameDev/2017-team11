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
        public int Current;
        /// <summary>
        /// The maximum value (less temporary).
        /// </summary>
        public int Maximum;
        /// <summary>
        /// The temporary bonus (added to maximum in checking).
        /// </summary>
        public int Temporary;

        /// <summary>
        /// Partial damage float (clamped to 1/16)
        /// </summary>
        private float _delta;

        public Attribute(int value) : this(value, value, 0) {}
        public Attribute(int current, int maximum) : this(current, maximum, 0) {}
        public Attribute(int current, int maximum, int temporary)
        {
            Maximum = maximum;
            Temporary = temporary;
            Current = 0;
            _delta = 0.0f;
            CurrentProperty = current;
        }
        
        /// <summary>
        /// Wraps <see cref="Current"/> with setter to make sure we don't exceed the maximum + temporary limit.
        /// </summary>
        public int CurrentProperty
        {
            get { return Current; }
            private set
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

        public int Absent => Maximum + Temporary - Current;

        /// <summary>
        /// Damages the current effect. This checks we don't go out of [0, Max + Temp].
        /// </summary>
        /// <param name="amount">The amount of damage to take, or heal (if negative).</param>
        public void Damage(float amount) {
            amount += _delta;
            if(amount > Current)
                Current = 0;
            else {
                var take = (int) (amount + 0.5f);
                _delta = amount - take;
                CurrentProperty -= take;
            }
        }
    }
}
