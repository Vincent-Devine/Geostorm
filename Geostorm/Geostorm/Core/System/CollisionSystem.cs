using System.Collections.Generic;
using System.Numerics;

namespace Geostorm.Core
{
    class CollisionSystem : ISystem
    {

        public void Update(GameData gameData, GameInputs inputs, List<GameEvent> events)
        {
            CheckBulletEnemy(gameData, inputs, events);
            CheckPlayerEnemy(gameData, inputs, events);
            CheckBlackHole(gameData, inputs, events);
        }

        private void CheckBulletEnemy(GameData gameData, GameInputs inputs, List<GameEvent> events)
        {
            foreach (Bullet bullet in gameData.Bullets)
            {
                foreach (Enemy enemy in gameData.Enemies)
                {
                    // Check if 'isSpawning' for 'a' is true
                    if (!enemy.GetIsSpawning() && CheckCollison(enemy, bullet))
                    {
                        enemy.isDead = true;
                        bullet.isDead = true;
                        events.Add(new EnemyKilledEvent(enemy, bullet));
                    }
                }
            }
        }

        private void CheckPlayerEnemy(GameData gameData, GameInputs inputs, List<GameEvent> events)
        {
            foreach (Enemy enemy in gameData.Enemies)
            {
                if (!enemy.GetIsSpawning() && !gameData.Player.GetIsInvincibility() && CheckCollison(enemy, gameData.Player))
                {
                    events.Add(new PlayerTakeDamageEvent(gameData.Player, enemy.damage));
                    enemy.isDead = true;
                }
            }
        }
            
        private void CheckBlackHole(GameData gameData, GameInputs inputs, List<GameEvent> events)
        {
            foreach (BlackHole blackHole in gameData.BlackHoles)
            {
                if(!blackHole.GetIsSpawning())
                {
                    // With enemy
                    foreach (Enemy enemy in gameData.Enemies)
                    {
                        if (!enemy.GetIsSpawning() && CheckCollison(enemy, blackHole))
                            enemy.isDead = true;
                    }

                    // With player
                    if (!gameData.Player.GetIsInvincibility() && CheckCollison(gameData.Player, blackHole))
                        events.Add(new PlayerTakeDamageEvent(gameData.Player, blackHole.GetDamage()));
                }
            }
        }

        private bool CheckCollison(Entity a, Entity b)
        {
            if (Vector2.Distance(a.position, b.position) < (a.collisionRadius + b.collisionRadius))
                return true;
            return false;
        }
    }
}
