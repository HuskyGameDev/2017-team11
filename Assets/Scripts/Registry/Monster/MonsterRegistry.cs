using System;
using System.Collections.Generic;

namespace Registry.Monster
{
    public static class MonsterRegistry
    {
        public static Entity.Entity GetRandomMonsterEntity(Region region, Rarity rarity, bool unique = true)
        {
            var list = Registry[region][rarity];
            if (list.Count == 0)
                list = Registry[region][Rarity.Common];
            if (list.Count == 0)
                list = Registry[region][Rarity.Common];
            if (list.Count == 0)
                return unique ? BoxMonster01.Clone() : BoxMonster01;
            return unique ? list[GameRegistry.Random.Next(list.Count)].Clone() : list[GameRegistry.Random.Next(list.Count)];
        }

        private static readonly Dictionary<Region, Dictionary<Rarity, List<Entity.Entity>>> Registry =
            new Dictionary<Region, Dictionary<Rarity, List<Entity.Entity>>>();

        public static readonly Entity.Entity BoxMonster01 = new Entity.Entity(armor: 15, health: 50, onesie: OnesieRegistry.BoxOnesie);

        static MonsterRegistry()
        {
            var regionList = (Region[]) Enum.GetValues(typeof(Region));
            var rarityList = (Rarity[]) Enum.GetValues(typeof(Rarity));
            foreach (var region in regionList)
            {
                Registry[region] = new Dictionary<Rarity, List<Entity.Entity>>();
                foreach (var rarity in rarityList)
                    Registry[region][rarity] = new List<Entity.Entity>();
            }

            Registry[Region.City][Rarity.Common].Add(BoxMonster01);
        }
    }
}