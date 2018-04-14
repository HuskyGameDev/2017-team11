using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace AI
{
    /// <summary>
    /// Player controls; UI updates this, and turn ends when button is clicked?
    /// </summary>
    /// <inheritdoc cref="IEntityTurnController" />
    public class PlayerAi : MonoBehaviour, IEntityTurnController
    {
        public static PlayerAi Instance;
        public GameObject[] Buttons;
        private Button[] _buttons;

        private void Awake()
        {
            Instance = this;
            _buttons = new Button[Buttons.Length];
            for (var i = 0; i < Buttons.Length; i += 1)
                _buttons[i] = Buttons[i].GetComponent<Button>();
        }
        private void OnDestroy() => Instance = null;

        private bool _turnOver = true;
        private readonly List<Move> _moves = new List<Move>();

        public void BeginTurn()
        {
            Debug.Log("Beginning player turn");
            _turnOver = false;
            _buttons[0].interactable = true;
            SetButtonsEnabled(true);
        }
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
            SetButtonsEnabled(false);
        }

        /// <summary>
        /// Enable or disable UI buttons.
        /// </summary>
        /// <param name="on"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetButtonsEnabled(bool on)
        {
            for (var i = 0; i < _buttons.Length; i += 1)
                _buttons[i].interactable = on;
        }

        #region UIFunctions

        public void ExecuteAttack0() => ExecuteAttack(0);
        public void ExecuteAttack1() => ExecuteAttack(1);
        public void ExecuteAttack2() => ExecuteAttack(2);
        public void ExecuteAttack3() => ExecuteAttack(3);

        #endregion
    }
}