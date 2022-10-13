using System.Collections.Generic;
using System.Numerics;

namespace Geostorm.Core
{
    public abstract class Enemy : Entity
    {
        // Spawn
        protected int timer;
        protected int spawnTime;
        protected bool isSpawning;
        
        protected Vector2 direction;

        public int damage;
        public int scoreWin;

        public Enemy(Vector2 position, float rotation = 0f, float collisionRadius = 0f, int spawnTime = 20, int damage = 1, int scoreWin = 100)
            : base(position, rotation, collisionRadius)
        {
            this.timer = 0;
            this.isSpawning = true;
            this.spawnTime = spawnTime;
            
            this.damage = damage;
            this.scoreWin = scoreWin;
        }

        public override sealed void Update(in GameInputs gameInputs, GameData gameData, List<GameEvent> events)
        {
            if(!isSpawning)
            {
                DoUpdate(gameInputs, gameData, events);
            }
            else
            {
                timer++;
                if (timer > spawnTime)
                    isSpawning = false;
            }
        }

        protected abstract void DoUpdate(in GameInputs gameInputs, GameData gameData, List<GameEvent> events);

        public override void Draw(Renderer.Graphics graphics)
        {
        }

        public bool GetIsSpawning() { return isSpawning; }
    }
}