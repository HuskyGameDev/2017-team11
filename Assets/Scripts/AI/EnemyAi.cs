using System.Collections.Generic;
using Action;
using Entity;
using UnityEngine;

namespace AI
{
    /// <summary>
    /// Enemy AI.
    /// </summary>
    public class EnemyAi : MonoBehaviour, IActionController
    {
        private bool _turnOver = true;
        private readonly List<Move> _moves = new List<Move>();

        public void BeginTurn()
        {
            _turnOver = false;
        }

        public bool IsTurnOver()
        {
            return _turnOver;
        }

        public List<Move> GetMoves()
        {
            return _moves;
        }

        public void DoneWithMoveList()
        {
            _moves.Clear();
        }

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