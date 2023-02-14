﻿using Data;
using Infrastructure.Services.Progress;

namespace Domain.Units.Specific.Character.Progress
{
    public class CharacterStatsProgress : ISavedProgressWriter
    {
        private readonly Health.Health _health;

        public CharacterStatsProgress(Health.Health health)
        {
            _health = health;
        }
        
        public void LoadProgress(PlayerProgress progress)
        {
            CharacterStatsData statsData = progress.CharacterStats;
            
            _health.Init(statsData.Health.CurrentValue, statsData.Health.MaxValue);
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            CharacterStatsData statsData = progress.CharacterStats;
            
            statsData.Health.CurrentValue = _health.CurrentValue();
            statsData.Health.MaxValue = _health.MaxValue();
        }
    }
}