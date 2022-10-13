using System.Collections.Generic;
using System.Numerics;

namespace Geostorm.Core
{
    public class BlackHole : Entity
    {
        private int timer; // Use for spawn and lifespan
        
        // Spawn
        protected int spawnTime;
        private bool isSpawning;
        
        // Lifespan
        private int lifespan;

        private int damage;
        private float pull;
        private float pullDistance;

        // Animation
        private bool haveAnimation;


        public BlackHole(Vector2 position, float rotation = 0f, float collisionRadius = 50f, int lifespan = 50, int damage = 1, float pull = 4f, float pullDistance = 200f, int spawnTime = 50)
            : base(position, rotation, collisionRadius)
        {
            this.timer = 0;

            this.isSpawning = true;
            this.spawnTime = spawnTime;

            this.lifespan = lifespan + spawnTime;

            this.damage = damage;
            this.pull = pull;
            this.pullDistance = pullDistance;

            this.haveAnimation = false;
        }

        public override void Update(in GameInputs gameInputs, GameData gameData, List<GameEvent> events)
        {
            timer++;
            if (timer > lifespan)
                isDead = true;

            if (!isSpawning)
            {
                // pull player in darck hole
                Vector2 direction = position - gameData.Player.position;
                if (direction.Length() < pullDistance)
                {
                    direction = Vector2.Normalize(direction);
                    gameData.Player.position += direction * pull;
                }

                // pull enemy in black hole
                foreach (Enemy enemy in gameData.Enemies)
                {
                    direction = position - enemy.position;
                    if (!enemy.GetIsSpawning() && direction.Length() < pullDistance)
                    {
                        direction = Vector2.Normalize(direction);
                        enemy.position += direction * pull;
                    }
                }
            }
            else if (timer > spawnTime)
                isSpawning = false;
        }

        public override void Draw(Renderer.Graphics graphics)
        {
            if (!isSpawning)
            {
                if (!haveAnimation)
                {
                    graphics.allAnimation.Add(new Renderer.BlackHoleAnimation(this));
                    haveAnimation = true;
                }

                graphics.DrawBlackHole(position, (int)collisionRadius);
            }
            else
                graphics.DrawBlackHole(position, timer);
        }

        public int GetDamage() { return damage; }
        public bool GetIsSpawning() { return isSpawning; }
    }

}