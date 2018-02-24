using System;
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
        public Entity MyEntity = new Entity();
        private SpriteRenderer _renderer;

        private void Awake() {
            _renderer = GetComponent<SpriteRenderer>();
        }

        private void Start() {
            SetEntity(MyEntity);
        }

        public void SetEntity(Entity entity) {
            MyEntity = entity;
            _renderer.sprite = SpriteCache.GetSprite(MyEntity.Onesie.SpriteName);
        }

        public Onesie EquipOnesie(Onesie onesie) {
            var oldOnesie = MyEntity.EquipOnesie(onesie);
            _renderer.sprite = SpriteCache.GetSprite(MyEntity.Onesie.SpriteName);
            return oldOnesie;
        }
    }
}
