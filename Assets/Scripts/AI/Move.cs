using System.Runtime.CompilerServices;
using Action;
using Entity;

namespace AI {
    public struct Move {
        public readonly Attack[] Attacks;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Perform(GameEntity target) {
            for(var i = 0; i < Attacks.Length; i += 1) {
                target.MyEntity.ApplyAttack(Attacks[i]);
            }
        }

        public Move(params Attack[] attacks) {
            Attacks = attacks;
        }
    }
}
