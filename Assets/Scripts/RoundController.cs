using System;
using AI;
using Entity;
using Registry.Monster;
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

    public bool IsCombatOver; // = false
    public bool IsPlayerTurn = true;
    public bool IsNewTurn = true;
    public GameEntity[] EnemyEntities;
    public GameEntity[] CatEntities;
    public IEntityTurnController EnemyAiController;

    private float _combatOverTimer;

    private void Awake() => Instance = this;
    private void OnDestroy() => Instance = null;

    private void Start()
    {
        EnemyAiController = GetComponent<EnemyAi>();
        EnemyEntities[0].SetEntity(MonsterRegistry.GetRandomMonsterEntity(Region.City, Rarity.Common));
        CatEntities[0].SetEntity(PlayerController.Instance.Cats[0]);
        //AkSoundEngine.PostEvent("Battle_Music", gameObject);
    }

    private void Update()
    {
        if (IsCombatOver)
        {
            _combatOverTimer += Time.deltaTime;
            if(_combatOverTimer > 10.0f)
                Application.Quit();
            return;
        }
        if (IsPlayerTurn)
        {
            if (IsNewTurn)
            {
                // ReSharper disable once ForCanBeConvertedToForeach
                for (var i = 0; i < PlayerController.Instance.Cats.Count; i++)
                    // tick effects on each cat.
                    PlayerController.Instance.Cats[i].ProcessEffects();
                PlayerAi.Instance.BeginTurn();

                IsNewTurn = false;
            }

            if (PlayerAi.Instance.IsMoveAvailable())
            {
                // perform available moves.
                var moveList = PlayerAi.Instance.GetMoves();
                for (var i = 0; i < moveList.Count; i++)
                    moveList[i].Perform();
                PlayerAi.Instance.DoneWithMoves();
            }

            if (PlayerAi.Instance.IsTurnOver())
            {
                IsPlayerTurn = false;
                IsNewTurn = true;
            }
            
            // Check if the enemies are dead and if so, end combat.
            var allEnemiesDead = true;
            for (var i = 0; i < EnemyEntities.Length && allEnemiesDead; i += 1)
                allEnemiesDead &= EnemyEntities[i].MyEntity.IsDead;
            if (!allEnemiesDead)
                return;
        }
        else // monster turn
        {
            if (IsNewTurn)
            {
                // ReSharper disable once ForCanBeConvertedToForeach
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
            // Check if all players are dead and if so, end combat.
            var allPlayersDead = true;
            for (var i = 0; i < CatEntities.Length && allPlayersDead; i += 1)
                allPlayersDead &= CatEntities[i].MyEntity.IsDead;
            if (!allPlayersDead)
                return;
        }
        
        IsCombatOver = true;
        _combatOverTimer = 0.0f;
    }
}