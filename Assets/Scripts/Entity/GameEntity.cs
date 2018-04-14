using System;
using AI;
using Cataclysm.Resources;
using Inventory;
using UnityEngine;

namespace Entity {
    /// <summary>
    /// Brings an <see cref="Entity"/> into the Unity real-world. We don't store attributes directly in here
    /// to make it a bit easier to swap cats in and out.
    /// </summary>
    [Serializable]
    [RequireComponent(typeof(SpriteRenderer))]
    public class GameEntity : MonoBehaviour {
        public Entity MyEntity;
        private SpriteRenderer _renderer;

        private void Awake() {
            _renderer = GetComponent<SpriteRenderer>();
        }

        private void Start() {
            SetEntity(MyEntity);
        }

        private void OnDestroy()
        {
            if (MyEntity != null)
                MyEntity.GameEntity = null;
        }

        public void SetEntity(Entity entity)
        {
            MyEntity = entity;
            MyEntity.GameEntity = this;
            RefreshSprite();
        }

        public void RefreshSprite()
        {
            _renderer.sprite = MyEntity == null ? null : SpriteCache.GetSprite(MyEntity.SpriteName);
        }
        
        public Move GetAttackMove(byte attackIndex)
        {
            return new Move(this, RoundController.Instance.EnemyEntities[0], MyEntity.Onesie.Attacks[attackIndex]);
        }
    }
}
