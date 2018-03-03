using System.Collections.Generic;
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

        public void EndTurn()
        {
            //TODO: disable action-taking
        }

        public bool IsTurnOver() => _turnOver;
        public bool IsMoveAvailable() => _moves.Count > 0;
        public List<Move> GetMoves() => _moves;
        public void DoneWithMoves() => _moves.Clear();

        private void OnGUI()
        {
            if (!_turnOver)
            {
                if (GUILayout.Button("DoMonsterAttack"))
                {
                    _moves.Add(new Move(RoundController.Instance.EnemyEntities[0], PlayerController.Instance.Cats[0],
                        new Attack(1)));
                    _turnOver = true;
                }
            }
        }
    }
}