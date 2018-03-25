using System.Collections.Generic;
using System.Linq.Expressions;
using Action;
using Entity;
using UnityEngine;

namespace AI
{
    /// <summary>
    /// Enemy AI.
    /// </summary>
    public class EnemyAi : MonoBehaviour, IEntityTurnController
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
            if (!GUILayout.Button("DoMonsterAttack"))
                return;
            _moves.Add(new Move(new Attack(2)));
            _turnOver = true;
        }
    }
}