using System.Collections.Generic;
using Registry;
using UnityEngine;

namespace AI
{
    /// <summary>
    /// Enemy AI.
    /// </summary>
    /// <inheritdoc cref="IEntityTurnController" />
    public class EnemyAi : MonoBehaviour, IEntityTurnController
    {
        /// <summary>
        /// Enemy turn time in seconds.
        /// </summary>
        private const float EnemyTurnTime = 3.0f;
        private bool _turnOver = true;
        private float _timer;
        private readonly List<Move> _moves = new List<Move>();

        public void BeginTurn()
        {
            _turnOver = false;
            _timer = 0.0f;
        }
        public bool IsTurnOver() => _turnOver;
        public bool IsMoveAvailable() => _moves.Count > 0;
        public List<Move> GetMoves() => _moves;
        public void DoneWithMoves() => _moves.Clear();

        private void Update()
        {
            if(_turnOver)
                return;
            _timer += Time.deltaTime;
            if(_timer < EnemyTurnTime)
                return;
            var thisEntity = RoundController.Instance.EnemyEntities[0];
            _moves.Add(new Move(thisEntity, RoundController.Instance.CatEntities[0], thisEntity.MyEntity.Onesie.Attacks[GameRegistry.Random.Next(thisEntity.MyEntity.Onesie.Attacks.Length)]));
            _turnOver = true;
        }
    }
}