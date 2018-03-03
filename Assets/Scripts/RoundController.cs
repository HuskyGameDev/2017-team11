using System;
using System.Runtime.InteropServices;
using AI;
using Entity;
using Inventory;
using UnityEngine;

/// <inheritdoc />
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
    public IEntityTurnController PlayerAiController, EnemyAiController;

    private void Awake() => Instance = this;
    private void OnDestroy() => Instance = null;

    private void Start()
    {
        PlayerAiController = PlayerController.Instance.GetComponent<PlayerAi>();
        EnemyAiController = GetComponent<EnemyAi>();
        EnemyEntities[0].EquipOnesie(new Onesie("Box"));
    }

    private void Update()
    {
        if (IsPlayerTurn)
        {
            if (IsNewTurn)
            {
                for (var i = 0; i < PlayerController.Instance.Cats.Length; i++)
                    // tick effects on each cat.
                    PlayerController.Instance.Cats[i].MyEntity.ProcessEffects();
                PlayerAiController.BeginTurn();

                IsNewTurn = false;
            }

            if (PlayerAiController.IsMoveAvailable())
            {
                // perform available moves.
                var moveList = PlayerAiController.GetMoves();
                for (var i = 0; i < moveList.Count; i++)
                    moveList[i].Perform();
                PlayerAiController.DoneWithMoves();
            }

            if (PlayerAiController.IsTurnOver())
            {
                IsPlayerTurn = false;
                IsNewTurn = true;
            }
        }
        else // monster turn
        {
            if (IsNewTurn)
            {
                for (var i = 0; i < EnemyEntities.Length; i++)
                    // tick effects on each monster.
                    EnemyEntities[i].MyEntity.ProcessEffects();
                EnemyAiController.BeginTurn();

                IsNewTurn = false;
            }

            if (EnemyAiController.IsMoveAvailable())
            {
                // perform available moves.
                var moveList = EnemyAiController.GetMoves();
                for (var i = 0; i < moveList.Count; i++)
                    moveList[i].Perform();
                EnemyAiController.DoneWithMoves();
            }

            if (EnemyAiController.IsTurnOver())
            {
                IsPlayerTurn = true;
                IsNewTurn = true;
            }
        }
    }
}