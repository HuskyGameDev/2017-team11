using System.Collections.Generic;
using UnityEngine;

namespace AI {
    /// <summary>
    /// Enemy AI.
    /// </summary>
    public class EnemyAi: MonoBehaviour, IActionController {
        public void BeginTurn() {/*TODO*/}
        public bool IsTurnOver() {return false;}
        public List<Move> GetMoves() {return new List<Move>();}
        public void DoneWithMoveList() {/*TODO*/}
    }
}
