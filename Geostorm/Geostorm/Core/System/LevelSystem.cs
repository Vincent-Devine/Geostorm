using System.Collections.Generic;

namespace Geostorm.Core
{
    class LevelSystem : ISystem
    {
        private EnemySpawnSystem enemySpawnSystem;
        public LevelSystem(EnemySpawnSystem enemySpawnSystem)
        {
            this.enemySpawnSystem = enemySpawnSystem;
        }

        public void Update(GameData gameData, GameInputs inputs, List<GameEvent> events)
        {
            if (gameData.timer % gameData.newLevelTimer == 0)
            {
                gameData.level++;
                enemySpawnSystem.LevelUp();
                events.Add(new NewLevelEvent(gameData.level));
            }
        }
    }
}
