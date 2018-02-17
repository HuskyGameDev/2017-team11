using System.Collections.Generic;
using Entity;
using UnityEngine;

namespace AI
{
    /// <summary>
    /// Player controls; UI updates this, and turn ends when button is clicked?
    /// </summary>
    public class PlayerAi : MonoBehaviour, IActionController
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
                if (GUILayout.Button("DoPlayerAttack"))
                {
                    _moves.Add(new Move(PlayerController.Instance.Cats[0], RoundController.Instance.EnemyEntities[0],
                        new Attack(0, new ActiveEffect(ActiveEffectType.Bleeding, 1))));
                    _turnOver = true;
                }
            }
        }
    }
}