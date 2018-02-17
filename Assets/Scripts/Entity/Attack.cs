namespace Entity {
    /// <summary>
    /// Container for attack data.
    /// </summary>
    public struct Attack {
        public int PhysicalDamage;
        public ActiveEffect[] ActiveEffects;

        public Attack(int physicalDamage, params ActiveEffect[] activeEffects) {
            PhysicalDamage = physicalDamage;
            ActiveEffects = activeEffects;
        }
    }
}
