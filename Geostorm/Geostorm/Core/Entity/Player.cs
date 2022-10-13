using System.Collections.Generic;
using System.Numerics;
using System;

namespace Geostorm.Core
{
    public class Player : Entity, IGameEventListener
    {
        private int life;
        private int score;
        private int money;

        // Invincibility after take damage
        private int timer;
        private int timeInvincibility;
        private bool isInvincibility;

        // Sight
        private float sightTarget;
        public Weapon weapon;

        public Player(Vector2 position, float rotation = 0f, float collisionRadius = 20f, int life = 5, int timeInvincibility = 60) 
            : base(position, rotation, collisionRadius)
        {
            this.life = life;
            this.money = 0;
            this.score = 0;

            this.timer = timeInvincibility;
            this.timeInvincibility = timeInvincibility;
            this.isInvincibility = false;

            weapon = new Weapon();
        }

        public override void Update(in GameInputs gameInputs, GameData gameData, List<GameEvent> events)
        {
            // Rotation
            if (gameInputs.moveAxis != new Vector2())
            {
                Vector2 n = Vector2.Normalize(gameInputs.moveAxis);
                rotation = MathF.Atan2(n.Y, n.X);
            }

            // Pos
            float speed = 6f;
            position += gameInputs.moveAxis * speed;
            PlayerStayInScreen(gameInputs);

            // Invincibility
            timer++;
            if (timeInvincibility < timer)
                isInvincibility = false;

            // Sight
            Vector2 normal = Vector2.Normalize(position - gameInputs.shootTarget);
            sightTarget = MathF.Atan2(normal.Y, normal.X);

            weapon.Update(gameInputs, gameData, events);
        }

        public void PlayerStayInScreen(in GameInputs inputs)
        {
            if (position.X < 0f)
                position.X = 0f;
            else if (position.X > inputs.screenSize.X)
                position.X = inputs.screenSize.X;
            else if (position.Y < 0f)
                position.Y = 0f;
            else if (position.Y > inputs.screenSize.Y)
                position.Y = inputs.screenSize.Y;
        }

        public void TakeDamage(int damage)
        {
            // Life
            if (damage < life)
            {
                life -= damage;

                // Invincibility
                isInvincibility = true;
                timer = 0;
            }
            else
            {
                life = 0;
                isDead = true;
            }

        }

        public void EntityKilled(int score)
        {
            this.score += score;
            this.money += score;
        }

        public void PayUpgradeWeapon(int cost)
        {
            if (money >= cost)
                money -= cost;
            else
                Console.WriteLine("[ERROR] Player - PayUpgradeWeapon(int cost) - player.money ( " + money + " ) < cost ( " + cost + " )");
        }

        public int GetMoney() { return money; }
        public int GetScore() { return score; }
        public bool GetIsInvincibility() { return isInvincibility; }


        public override void Draw(Renderer.Graphics graphics)
        {
            graphics.DrawPlayer(position, rotation, isInvincibility);
            graphics.DrawSight(position, sightTarget, (int)collisionRadius + 10);
            graphics.DrawPlayerIU(score, money, life);

            weapon.Draw(graphics, money);
        }

        public void HandleEvents(IEnumerable<GameEvent> gameEvents, GameData gameData)
        {
            foreach (GameEvent gameEvent in gameEvents)
            {
                switch (gameEvent)
                {
                    case UpgradeWeaponEvent:
                        UpgradeWeaponEvent upgradeWeaponEvent = gameEvent as UpgradeWeaponEvent;
                        PayUpgradeWeapon(upgradeWeaponEvent.cost);
                        break;
                }
            }
        }
    }
}
