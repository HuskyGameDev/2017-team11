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
                if (GUILayout.Button("DoPlayerAttack"))
                {
                    _moves.Add(new Move(PlayerController.Instance.Cats[0], RoundController.Instance.EnemyEntities[0],
                        new Attack(0, new Action.Action(ActionType.Bleeding, 1))));
                    _turnOver = true;
                }
            }
        }
    }
}