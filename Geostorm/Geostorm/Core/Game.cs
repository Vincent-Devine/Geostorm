using System.Collections.Generic;

namespace Geostorm.Core
{
    class Game
    {
        private GameData gameData;
        
        private List<IGameEventListener> eventListeners = new List<IGameEventListener>();
        
        private EnemySpawnSystem enemySpawnSystem = new EnemySpawnSystem();
        private List<ISystem> allSystems = new List<ISystem>();

        public Game(GameInputs inputs)
        {
            // System
            allSystems.Add(enemySpawnSystem);
            allSystems.Add(new BlackHoleSpawmSystem());
            allSystems.Add(new CollisionSystem());
            allSystems.Add(new LevelSystem(enemySpawnSystem));
            
            // Data
            gameData = new GameData(inputs);
            
            // Event
            AddEventListener(gameData);
            AddEventListener(gameData.Player);
        }

        public void Update(GameInputs inputs)
        {
            if(!gameData.Player.isDead)
            {
                List<GameEvent> gameEvents = new List<GameEvent>();

                // Update entity
                foreach (Entity entity in gameData.Entities)
                    entity.Update(inputs, gameData, gameEvents);

                // Update system
                foreach (ISystem system in allSystems)
                    system.Update(gameData, inputs, gameEvents);

                // Update event
                foreach (IGameEventListener eventListener in eventListeners)
                    eventListener.HandleEvents(gameEvents, gameData);

                gameData.Update();

                inputs.Initialise();
            }
        }

        public void Render(Renderer.Graphics graphics)
        {
            graphics.DrawLevelUI(gameData.timer, gameData.level);

            foreach (Entity entity in gameData.Entities)
                entity.Draw(graphics);

            graphics.PlayAnimation();

            if (gameData.Player.isDead)
                graphics.DrawGameOver(gameData.Player.GetScore());
        }

        public void AddEventListener(IGameEventListener gameEvent)
        {
            eventListeners.Add(gameEvent);
        }
    }
}
