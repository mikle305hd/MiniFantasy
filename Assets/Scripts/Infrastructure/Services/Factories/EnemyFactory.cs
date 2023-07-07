﻿using GamePlay.Units;
using UnityEngine;

namespace Infrastructure.Services
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;

        public EnemyFactory(IAssetProvider assetProvider, IStaticDataService staticDataService)
        {
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
        }
        
        public GameObject Create(EnemyId enemyId, Vector3 position, Transform parent)
        {
            string prefabPath = _staticDataService.FindEnemyData(enemyId).PrefabPath;
            return _assetProvider.Instantiate<GameObject>(prefabPath, position, parent);
        }
    }
}