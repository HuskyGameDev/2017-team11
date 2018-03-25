using System;
using System.Collections.Generic;

namespace Registry.Monster
{
    public static class MonsterRegistry
    {
        private static Random _random = new Random();

        public static Entity.Entity GetRandomMonsterEntity(Region region, Rarity rarity, bool unique = true)
        {
            var list = registry[region][rarity];
            return unique ? list[_random.Next(list.Count)].Clone() : list[_random.Next(list.Count)];
        }

        private static Dictionary<Region, Dictionary<Rarity, List<Entity.Entity>>> registry =
            new Dictionary<Region, Dictionary<Rarity, List<Entity.Entity>>>
            {
                {Region.City,new Dictionary<Rarity, List<Entity.Entity>>
                {
                    {Rarity.Common, new List<Entity.Entity>
                    {
                        BoxMonster01
                    }}
                }}
            };

        public static readonly Entity.Entity BoxMonster01 = new Entity.Entity();
    }
}
