using System;
using System.ComponentModel;
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
        public EntityPosition EntityPosition;
        private SpriteRenderer _renderer;
        private int _wCache, _hCache;
        private Camera _camera;

        private void Awake() {
            _renderer = GetComponent<SpriteRenderer>();
            _camera = Camera.main;
        }

        private void Start() {
            SetEntity(MyEntity);
        }

        /// <summary>
        /// Recalculate sprite position.
        /// </summary>
        private void Update()
        {
            if (_renderer.sprite == null || Screen.width == _wCache && Screen.height == _hCache)
                return;
            _wCache = Screen.width;
            _hCache = Screen.height;
            switch (EntityPosition)
            {
                case EntityPosition.Enemy0:
                    transform.position = _camera.ViewportToWorldPoint(new Vector3(0.67f, 0.67f, -_camera.gameObject.transform.position.z));
                    break;
                case EntityPosition.Player0:
                    transform.position = _camera.ViewportToWorldPoint(new Vector3(0.33f, 0.33f, -_camera.gameObject.transform.position.z));
                    break;
                default:
                    throw new InvalidEnumArgumentException();
            }
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
            if (newSprite == null)
                return;
            _renderer.sprite = newSprite;
            _wCache = 0; // force a re-position
        }
        
        public Move GetAttackMove(byte attackIndex)
        {
            return new Move(this, RoundController.Instance.EnemyEntities[0], MyEntity.Onesie.Attacks[attackIndex]);
        }
    }
}
