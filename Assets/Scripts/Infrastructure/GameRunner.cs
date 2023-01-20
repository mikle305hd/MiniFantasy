﻿using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infrastructure
{
    /// <summary>
    /// Wrapper of game bootstrapper to run game from any scene in editor
    /// </summary>
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private GameBootstrapper _gameBootstrapperPrefab;

        private void Awake()
        {
            var gameBootstrapper = FindObjectOfType<GameBootstrapper>();
            if (gameBootstrapper == null)
                Instantiate(_gameBootstrapperPrefab);
            
            Destroy(gameObject);
        }
    }
}