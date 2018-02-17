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
public class RoundController : MonoBehaviour
{
	public static RoundController Instance;
	
	public bool IsPlayerTurn = true;
	public bool IsNewTurn = true;
	public GameEntity[] EnemyEntities;
	public IActionController PlayerAiController, EnemyAiController;

	private void Awake() { Instance = this; }
	private void OnDestroy(){ Instance = null; }

	private void Start() {
		PlayerAiController = PlayerController.Instance.GetComponent<PlayerAi>();
		EnemyAiController = GetComponent<EnemyAi>();
		//PlayerController.Instance.PassiveLoopEnabled = false;
	}

	private void Update() {
		if(IsNewTurn)
			// run the start-of-turn stuff.
			BeginTurn();
		if(IsPlayerTurn && PlayerAiController.IsTurnOver() || !IsPlayerTurn && EnemyAiController.IsTurnOver())
			AdvanceTurn();
	}

	/// <summary>
	/// Runs start-of-turn things and lets the relevant IActionController know it can  
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private void BeginTurn() {
		if(IsPlayerTurn) {
			for(var i = 0; i < PlayerController.Instance.Cats.Length; i += 1) {
				// tick effects on each cat.
				PlayerController.Instance.Cats[i].MyEntity.ProcessEffects();
			}
			PlayerAiController.BeginTurn();
		} else {
			for(var i = 0; i < EnemyEntities.Length; i += 1) {
				// tick effects on each monster.
				EnemyEntities[i].MyEntity.ProcessEffects();
			}
			EnemyAiController.BeginTurn();	
		}
		IsNewTurn = false;
	}

	/// <summary>
	/// Runs end-of-turn actions and applies all moves.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private void AdvanceTurn() {
		if(IsPlayerTurn) {
			var moveList = PlayerAiController.GetMoves();
			for(var i = 0; i < moveList.Count; i++) {
				// perform the player moves.
				moveList[i].Perform();
			}
			PlayerAiController.DoneWithMoveList();
		} else {
			var moveList = EnemyAiController.GetMoves();
			for(var i = 0; i < moveList.Count; i++) {
				// perform the AI moves.
				moveList[i].Perform();
			}
			EnemyAiController.DoneWithMoveList();
		}
		
		// start the next turn on the opposite side.
		IsPlayerTurn = !IsPlayerTurn;
		IsNewTurn = true;
	}
}
