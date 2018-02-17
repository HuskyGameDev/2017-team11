using System;
using UnityEngine;

namespace Entity {
    /// <summary>
    /// Brings an <see cref="Entity"/> into the Unity real-world. We don't store attributes directly in here
    /// to make it a bit easier to swap cats in and out.
    /// </summary>
    [Serializable]
    public class GameEntity : MonoBehaviour {
        public Entity MyEntity;
    }
}
