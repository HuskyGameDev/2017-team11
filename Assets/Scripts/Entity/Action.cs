namespace Entity {
    /// <summary>
    /// Container for an effect type and duration.
    /// </summary>
    public struct Action {
        public ActionType Type;
        public int Duration;

        public Action(ActionType type, int duration) {
            Type = type;
            Duration = duration;
        }
    }
}