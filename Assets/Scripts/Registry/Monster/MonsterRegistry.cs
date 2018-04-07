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

        private static readonly Dictionary<Region, Dictionary<Rarity, List<Entity.Entity>>> registry;

        public static readonly Entity.Entity BoxMonster01 = new Entity.Entity();

        static MonsterRegistry()
        {
            registry = new Dictionary<Region, Dictionary<Rarity, List<Entity.Entity>>>();
            Region[] regionList = (Region[]) Enum.GetValues(typeof(Region));
            Rarity[] rarityList = (Rarity[]) Enum.GetValues(typeof(Rarity));
            foreach (var region in regionList)
            {
                registry[region] = new Dictionary<Rarity, List<Entity.Entity>>();
                foreach (var rarity in rarityList)
                    registry[region][rarity] = new List<Entity.Entity>();
            }
            
            registry[Region.City][Rarity.Common].Add(BoxMonster01);
        }
    }
}
