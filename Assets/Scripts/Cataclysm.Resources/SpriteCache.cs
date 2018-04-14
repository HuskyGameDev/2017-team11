using System.Collections.Generic;
using UnityEngine;

namespace Cataclysm.Resources {
    /// <summary>
    /// Caches sprites and wraps resource loading.
    /// </summary>
    public static class SpriteCache {
        private static readonly Dictionary<string, Sprite> Cache = new Dictionary<string, Sprite>();
        /// <summary>
        /// Loads a new or cached sprite.
        /// </summary>
        /// <param name="name">Name of the sprite</param>
        /// <returns>A sprite</returns>
        public static Sprite GetSprite(string name) {
            if (!Cache.ContainsKey(name))
                Cache[name] = UnityEngine.Resources.Load<Sprite>(name);
            return Cache[name];
        }
    }
}
