using System.Runtime.CompilerServices;
using Action;
using Entity;

namespace AI {
    public struct Move {
        public readonly GameEntity Actor, Target;
        public readonly Attack[] Attacks;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Perform() {
            for(var i = 0; i < Attacks.Length; i += 1) {
                Target.MyEntity.ApplyAttack(Attacks[i]);
            }
        }

        public Move(GameEntity actor, GameEntity target, params Attack[] attacks) {
            Actor = actor;
            Target = target;
            Attacks = attacks;
        }
    }
}
