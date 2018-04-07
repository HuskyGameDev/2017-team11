using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Entity;
using UnityEngine;

namespace AI
{
    /// <summary>
    /// Player controls; UI updates this, and turn ends when button is clicked?
    /// </summary>
    public class PlayerAi : MonoBehaviour, IEntityTurnController
    {
        public static PlayerAi Instance;

        private void Awake() => Instance = this;
        private void OnDestroy() => Instance = null;

        private bool _turnOver = true;
        private readonly List<Move> _moves = new List<Move>();

        public void BeginTurn() => _turnOver = false;
        public bool IsTurnOver() => _turnOver;
        public bool IsMoveAvailable() => _moves.Count > 0;
        public List<Move> GetMoves() => _moves;
        public void DoneWithMoves() => _moves.Clear();
        
        /// <summary>
        /// Execute an attack
        /// </summary>
        /// <param name="attackIndex">Attack #</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ExecuteAttack(byte attackIndex)
        {
            Debug.Log($"Doing attack: {attackIndex}");
            _moves.Add(RoundController.Instance.CatEntities[0].GetAttackMove(attackIndex));
            _turnOver = true;
        }

        #region UIFunctions

        public void ExecuteAttack0() => ExecuteAttack(0);
        public void ExecuteAttack1() => ExecuteAttack(1);
        public void ExecuteAttack2() => ExecuteAttack(2);
        public void ExecuteAttack3() => ExecuteAttack(3);

        #endregion
    }
}