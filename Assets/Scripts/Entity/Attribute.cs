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
        private uint _current;

        public uint Current
        {
            get { return _current; }
            set
            {
                if (value > Maximum + Temporary)
                {
                    _current = (ushort) Math.Min(ushort.MaxValue, Maximum + Temporary);
                }
                else
                {
                    _current = value;
                }
            }
        }

        public ushort Maximum;
        public ushort Temporary;

        public Attribute(ushort value) : this(value, value, 0) {}
        public Attribute(ushort current, ushort maximum) : this(current, maximum, 0) {}
        public Attribute(ushort current, ushort maximum, ushort temporary)
        {
            Maximum = maximum;
            Temporary = temporary;
            _current = 0;
            Current = current;
        }

        public void Damage(int amount)
        {
            if (amount > Current)
            {
                Current = 0;
            } else if (amount < 0)
            {
                Current += (uint) -amount;
            }
            else
            {
                Current -= (uint) amount;
            }
        }
    }
}