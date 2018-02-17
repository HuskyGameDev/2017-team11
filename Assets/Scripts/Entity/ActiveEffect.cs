namespace Entity {
    /// <summary>
    /// Container for an effect type and duration.
    /// </summary>
    public struct ActiveEffect {
        public ActiveEffectType Type;
        public int Duration;

        public ActiveEffect(ActiveEffectType type, int duration) {
            Type = type;
            Duration = duration;
        }
    }
}