﻿using System;
using System.Collections.Generic;
using System.Numerics;

namespace Geostorm.Core
{
    public class EnemySpawnSystem : ISystem
    {
        private int timer;
        private int spawnFrequency;

        public EnemySpawnSystem(int spawnFrequency = 16)
        {
            this.timer = 0;
            this.spawnFrequency = spawnFrequency;
        }

        public void Update(GameData gameData, GameInputs inputs, List<GameEvent> events)
        {
            timer++;
            if(timer > spawnFrequency)
            {
                Random random = new Random();
                Vector2 positionEnemy = new Vector2(random.Next((int)inputs.screenSize.X), random.Next((int)inputs.screenSize.Y));
                gameData.AddEnemyDelayed(new Grunt(positionEnemy));
                timer = 0;
            }
        }

        public void LevelUp()
        {
            if(spawnFrequency != 5)
                spawnFrequency--;
        }
    }
}
