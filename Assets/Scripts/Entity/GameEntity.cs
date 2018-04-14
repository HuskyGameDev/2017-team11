using System;
using AI;
using Cataclysm.Resources;
using UnityEngine;

namespace Entity {
    /// <summary>
    /// Brings an <see cref="Entity"/> into the Unity real-world. We don't store attributes directly in here
    /// to make it a bit easier to swap cats in and out.
    /// </summary>
    /// <inheritdoc />
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

        /// <summary>
        /// Let the entity know its game object doesn't exist anymore.
        /// </summary>
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
            if (MyEntity == null)
            {
                _renderer.sprite = null;
                return;
            }
            
            var newSprite = SpriteCache.GetSprite(MyEntity.SpriteName);
            if (newSprite != null)
                _renderer.sprite = newSprite;
        }
        
        public Move GetAttackMove(byte attackIndex)
        {
            return new Move(this, RoundController.Instance.EnemyEntities[0], MyEntity.Onesie.Attacks[attackIndex]);
        }
    }
}
