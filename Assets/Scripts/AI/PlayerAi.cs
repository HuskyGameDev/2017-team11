using System.Collections.Generic;
using UnityEngine;

namespace AI {
    /// <summary>
    /// Player controls; UI updates this, and turn ends when button is clicked?
    /// </summary>
    public class PlayerAi : MonoBehaviour, IActionController {
        public void BeginTurn() {/*TODO*/}
        public bool IsTurnOver() {return false;}
        public List<Move> GetMoves() {return new List<Move>();}
        public void DoneWithMoveList() {/*TODO*/}
    }
}
