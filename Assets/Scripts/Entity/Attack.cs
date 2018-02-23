namespace Entity {
    /// <summary>
    /// Container for attack data.
    /// </summary>
    public struct Attack {
        public int PhysicalStrength;
        public Action[] Actions;

        public Attack(int physicalStrength, params Action[] actions) {
            PhysicalStrength = physicalStrength;
            Actions = actions;
        }
    }
}
