namespace Action {
    /// <summary>
    /// Container for attack data.
    /// </summary>
    public class Attack {
        public int PhysicalStrength;
        public Action[] Actions;

        public Attack(int physicalStrength, params Action[] actions) {
            PhysicalStrength = physicalStrength;
            Actions = actions;
        }
    }
}
