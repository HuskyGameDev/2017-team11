using System;
using System.Runtime.CompilerServices;
using AI;
using Entity;
using UnityEngine;

/// <summary>
/// Handles turn-based combat.
/// </summary>
[Serializable]
[RequireComponent(typeof(EnemyAi))]
public class RoundController : MonoBehaviour {
	public bool IsPlayerTurn = true;
	public bool IsNewTurn = true;
	public GameEntity[] EnemyEntities;
	public IActionController PlayerAiController, EnemyAiController;

	private void Start() {
		PlayerAiController = PlayerController.Instance.GetComponent<PlayerAi>();
		EnemyAiController = GetComponent<EnemyAi>();
		//PlayerController.Instance.PassiveLoopEnabled = false;
	}

	private void Update() {
		if(IsNewTurn)
			BeginTurn();
		if(IsPlayerTurn && PlayerAiController.IsTurnOver() || !IsPlayerTurn && EnemyAiController.IsTurnOver())
			AdvanceTurn();
	}

	/*
	private void OnDestroy() {
		PlayerController.Instance.PassiveLoopEnabled = true;
	}
	*/

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private void BeginTurn() {
		if(IsPlayerTurn) {
			for(var i = 0; i < PlayerController.Instance.Cats.Length; i += 1) {
				PlayerController.Instance.Cats[i].MyEntity.ProcessEffects();
			}
			PlayerAiController.BeginTurn();
		} else {
			for(var i = 0; i < EnemyEntities.Length; i += 1) {
				EnemyEntities[i].MyEntity.ProcessEffects();
			}
			EnemyAiController.BeginTurn();	
		}
		IsNewTurn = false;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private void AdvanceTurn() {
		if(IsPlayerTurn) {
			var moveList = PlayerAiController.GetMoves();
			for(var i = 0; i < moveList.Count; i++) {
				moveList[i].Perform();
			}
			PlayerAiController.DoneWithMoveList();
		} else {
			var moveList = EnemyAiController.GetMoves();
			for(var i = 0; i < moveList.Count; i++) {
				moveList[i].Perform();
			}
			EnemyAiController.DoneWithMoveList();
		}
		IsPlayerTurn = !IsPlayerTurn;
		IsNewTurn = true;
	}
}
