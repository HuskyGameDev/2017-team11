using System.Collections.Generic;
using Action;
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

        private void OnGUI()
        {
            if (_turnOver)
                return;
            if (!GUILayout.Button("DoPlayerAttack"))
                return;
            _moves.Add(new Move(new Attack(2)));
            _turnOver = true;
        }
    }
}