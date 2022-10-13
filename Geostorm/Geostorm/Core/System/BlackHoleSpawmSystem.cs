using System;
using System.Collections.Generic;
using System.Numerics;

namespace Geostorm.Core
{
    class BlackHoleSpawmSystem : ISystem
    {
        private int timer;
        private int spawnFrequency;

        public BlackHoleSpawmSystem(int spawnFrequency = 100)
        {
            timer = 0;
            this.spawnFrequency = spawnFrequency;
        }

        public void Update(GameData gameData, GameInputs inputs, List<GameEvent> events)
        {
            timer++;
            if (timer > spawnFrequency)
            {
                Random random = new Random();
                Vector2 positionBlackHole = new Vector2(random.Next((int)inputs.screenSize.X), random.Next((int)inputs.screenSize.Y));
                gameData.AddBlackHoleDelayed(new BlackHole(positionBlackHole));
                timer = 0;
            }
        }
    }
}
