using System;
using System.Collections.Generic;
using System.Numerics;

namespace Geostorm.Core
{
    public enum Level : int
    {
        level1,
        level2,
        level3,
        level4,
        level5,
    }

    public class Weapon
    {
        //Shoot
        private int frequency;
        private int timer;
        private float speed;

        private float radius;

        // level
        private Level level;
        private int[] costUpgrade;
        
        public Weapon(Level level = Level.level1, int frequency = 10, int timer = 0, float speed = 15f, float radius = 30f)
        {
            this.frequency = frequency;
            this.timer = timer;
            this.speed = speed;

            this.radius = radius;

            this.level = level;
            costUpgrade = new int[4];
            costUpgrade[0] = 500;   // Level 1 -> 2
            costUpgrade[1] = 1000;  // Level 2 -> 3
            costUpgrade[2] = 1500;  // Level 3 -> 4
            costUpgrade[3] = 2000;  // Level 4 -> 5
        }

        public void Update(GameInputs gameInputs, GameData gameData, IList<GameEvent> gameEvents)
        {
            Shoot(gameInputs, gameData, gameEvents);
            Upgrade(gameInputs, gameData, gameEvents);

            timer++;
        }

        private void Shoot(GameInputs gameInputs, GameData gameData, IList<GameEvent> gameEvents)
        {
            if (gameInputs.shoot && timer > frequency)
            {
                Vector2 direction = Vector2.Normalize(gameInputs.shootTarget - gameData.Player.position);
                float rotation = MathF.Atan2(direction.Y, direction.X);
                Vector2 position = new Vector2(gameData.Player.position.X + MathF.Cos(rotation) * radius, gameData.Player.position.Y + MathF.Sin(rotation) * radius);

                switch (level)
                {
                    case Level.level1:
                    case Level.level2:
                        {
                            Bullet bullet = new Bullet(position, direction, rotation, 8f, speed);
                            gameData.AddBulletDelayed(bullet);
                            gameEvents.Add(new BulletShootEvent(bullet));
                        }
                        break;

                    case Level.level3:
                        {
                            // Bullet 1
                            Bullet bullet1 = new Bullet(position, direction, rotation, 8f, speed);
                            bullet1.direction.X += MathF.PI / 3;
                            bullet1.direction.Y += MathF.PI / 3;
                            gameData.AddBulletDelayed(bullet1);
                            gameEvents.Add(new BulletShootEvent(bullet1));
                            // Bullet 2
                            Bullet bullet2 = new Bullet(position, direction, rotation, 8f, speed);
                            bullet2.direction.X -= MathF.PI / 3;
                            bullet2.direction.Y -= MathF.PI / 3;
                            gameData.AddBulletDelayed(bullet2);
                            gameEvents.Add(new BulletShootEvent(bullet2));
                        }
                        break;

                    case Level.level4:
                        {
                            // Bullet 1
                            Bullet bullet1 = new Bullet(position, direction, rotation, 8f, speed);
                            bullet1.direction.X += MathF.PI / 4;
                            bullet1.direction.Y += MathF.PI / 4;
                            gameData.AddBulletDelayed(bullet1);
                            gameEvents.Add(new BulletShootEvent(bullet1));
                            // Bullet 2
                            Bullet bullet2 = new Bullet(position, direction, rotation, 8f, speed);
                            gameData.AddBulletDelayed(bullet2);
                            gameEvents.Add(new BulletShootEvent(bullet2));
                            // Bullet 3
                            Bullet bullet3 = new Bullet(position, direction, rotation, 8f, speed);
                            bullet3.direction.X -= MathF.PI / 4;
                            bullet3.direction.Y -= MathF.PI / 4;
                            gameData.AddBulletDelayed(bullet3);
                            gameEvents.Add(new BulletShootEvent(bullet3));
                        }
                        break;

                    case Level.level5:
                        {
                            // Bullet 1
                            Bullet bullet1 = new Bullet(position, direction, rotation, 8f, speed);
                            bullet1.direction.X += MathF.PI / 4;
                            bullet1.direction.Y += MathF.PI / 4;
                            gameData.AddBulletDelayed(bullet1);
                            gameEvents.Add(new BulletShootEvent(bullet1));
                            // Bullet 2
                            Bullet bullet2 = new Bullet(position, direction, rotation, 8f, speed);
                            gameData.AddBulletDelayed(bullet2);
                            gameEvents.Add(new BulletShootEvent(bullet2));
                            // Bullet 3
                            Bullet bullet3 = new Bullet(position, direction, rotation, 8f, speed);
                            bullet3.direction.X -= MathF.PI / 4;
                            bullet3.direction.Y -= MathF.PI / 4;
                            gameData.AddBulletDelayed(bullet3);
                            gameEvents.Add(new BulletShootEvent(bullet3));
                            // Bullet 4
                            Bullet bullet4 = new Bullet(position, -direction, MathF.Atan2(-direction.Y, -direction.X), 8f, speed);
                            gameData.AddBulletDelayed(bullet4);
                            gameEvents.Add(new BulletShootEvent(bullet4));
                        }
                        break;
                }

                timer = 0;
            }
        }

        // For level 3,4 and 5. Parametters don't change but we shoot more bullet
        private void Upgrade(GameInputs gameInputs, GameData gameData, IList<GameEvent> gameEvents)
        {
            if (gameInputs.buyUpgradeWeapon && (int)level < costUpgrade.Length && gameData.Player.GetMoney() >= costUpgrade[(int)level])
            {
                // Player is listener, player -> pay cost
                gameEvents.Add(new UpgradeWeaponEvent(this, costUpgrade[(int)level]));

                level++;
                if(level == Level.level2)
                {
                    frequency = 8;
                    speed = 20f;
                }
            }
        }

        public void Draw(Renderer.Graphics graphics, int moneyPlayer)
        {
            if((int)level < costUpgrade.Length)
                graphics.DrawWeaponIU(1 + (int)level, costUpgrade[(int)level], frequency, speed, moneyPlayer);
            else
                graphics.DrawWeaponIU(1 + (int)level, 0, frequency, speed, 0, true);
        }
    }
}
