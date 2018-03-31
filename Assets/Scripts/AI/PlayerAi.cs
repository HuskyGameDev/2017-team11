using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    /// <summary>
    /// Player controls; UI updates this, and turn ends when button is clicked?
    /// </summary>
    public class PlayerAi : MonoBehaviour, IEntityTurnController
    {
        private bool _turnOver = true;
        private readonly List<Move> _moves = new List<Move>();

        public void BeginTurn() => _turnOver = false;
        public bool IsTurnOver() => _turnOver;
        public bool IsMoveAvailable() => _moves.Count > 0;
        public List<Move> GetMoves() => _moves;
        public void DoneWithMoves() => _moves.Clear();
        
        public void ExecuteAttack(byte attackIndex)
        {
            _moves.Add(PlayerController.Instance.Cats[0].GetAttackMove(attackIndex));
            _turnOver = true;
        }
    }
}