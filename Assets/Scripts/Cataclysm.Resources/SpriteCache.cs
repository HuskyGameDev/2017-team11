using System.Collections.Generic;
using UnityEngine;

namespace Cataclysm.Resources {
    public static class SpriteCache {
        private static readonly Dictionary<string, Sprite> cache = new Dictionary<string, Sprite>();
        public static Sprite GetSprite(string name) {
            Debug.Log($"Looking for sprite {name}...");
            if(!cache.ContainsKey(name)) {
                cache[name] = UnityEngine.Resources.Load<Sprite>(name);
                Debug.Log($"Loaded sprite for {name}");
            }

            return cache[name];
        }
    }
}
