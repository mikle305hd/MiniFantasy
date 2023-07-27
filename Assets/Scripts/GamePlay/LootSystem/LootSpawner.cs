using System.Collections.Generic;
using Additional.Extensions;
using Infrastructure.Services;
using StaticData;
using UniDependencyInjection.Unity;
using UnityEngine;

namespace GamePlay.LootSystem
{
    public class LootSpawner : MonoBehaviour
    {
        private IRandomizer _randomizer;
        private ILootFactory _factory;
        private IStaticDataService _staticDataService;
        private List<RandomLoot> _lootCollection;


        [Inject]
        public void Construct(
            ILootFactory factory,
            IStaticDataService staticDataService,
            IRandomizer randomizer)
        {
            _factory = factory;
            _staticDataService = staticDataService;
            _randomizer = randomizer;
        }

        public void Init(List<RandomLoot> lootCollection)
        {
            _lootCollection = lootCollection;
        }

        public void Spawn()
        {
            foreach (RandomLoot loot in _lootCollection) 
                SpawnOne(loot);
        }

        private void SpawnOne(RandomLoot randomLoot)
        {
            if (_randomizer.TryChancePercents(randomLoot.Chance) == false)
                return;

            int lootCount = _randomizer.Generate(randomLoot.MinCount, randomLoot.MaxCount);
            LootPiece lootPiece = _factory.CreateInWorld(randomLoot.LootId, transform.position.AddY(1));
            LootData lootData = _staticDataService.GetLootData(randomLoot.LootId);
            lootPiece.Init(lootData, lootCount);
        }
    }
}