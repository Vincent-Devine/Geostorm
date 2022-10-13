using System;
using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;

namespace Geostorm.Renderer
{
    public class Graphics : Core.IGameEventListener
    {
        private Vector2[] playerVertices = new Vector2[8];
        private Vector2[] sightVertices = new Vector2[3];
        private Vector2[] gruntVertices = new Vector2[4];
        private Vector2[] bulletVertices = new Vector2[4];

        public List<Animation> allAnimation = new List<Animation>();

        public Graphics()
        {
            // Player
            float preScale = 20.0f;
            playerVertices[0] = new Vector2(-0.5f, 0.0f) * preScale;                     // innerCenter
            playerVertices[1] = new Vector2(-0.2f, -0.55f) * preScale;                   // topInnerWing
            playerVertices[2] = new Vector2(0.6f, -0.3f) * preScale;                     // topGun
            playerVertices[3] = new Vector2(-0.4f, -0.8f) * preScale;                    // topOuterWing
            playerVertices[4] = new Vector2(-1.0f, 0.0f) * preScale;                     // outerCenter
            playerVertices[5] = new Vector2(playerVertices[3].X, -playerVertices[3].Y);  // bottomOuterWing
            playerVertices[6] = new Vector2(playerVertices[2].X, -playerVertices[2].Y);  // bottomGun
            playerVertices[7] = new Vector2(playerVertices[1].X, -playerVertices[1].Y);  // bottomInnerWing

            // Sight
            preScale = 10f;
            sightVertices[0] = new Vector2(-0.5f, 0f) * preScale;       // top
            sightVertices[1] = new Vector2(+0.5f, -0.5f) * preScale;    // left
            sightVertices[2] = new Vector2(+0.5f, +0.5f) * preScale;    // right

            // Grunt
            preScale = 18f;
            gruntVertices[0] = new Vector2(-1.0f, 0.0f) * preScale;     // left
            gruntVertices[1] = new Vector2(-0.0f, -1.0f) * preScale;    // top
            gruntVertices[2] = new Vector2(1.0f, 0.0f) * preScale;      // right
            gruntVertices[3] = new Vector2(-0.0f, 1.0f) * preScale;     // bottom

            // Bullet
            preScale = 15f;
            bulletVertices[0] = new Vector2(-0.3f, 0.0f) * preScale;    // left
            bulletVertices[1] = new Vector2(-0.1f, 0.2f) * preScale;    // top
            bulletVertices[2] = new Vector2(0.8f, 0.0f) * preScale;     // right
            bulletVertices[3] = new Vector2(-0.1f, -0.2f) * preScale;   // bottom
        }

        public void Load()
        {
        }

        public void Unload()
        {
        }

        public void PlayAnimation()
        {
            // Play all animation
            foreach (Animation animation in allAnimation)
                animation.Update();

            // Remove finished animation
            allAnimation.RemoveAll(animation => animation.isFinished);
        }

        // ---------------
        // --- Player ----
        // ---------------
        public void DrawPlayer(Vector2 pos, float rotation, bool isInvincibility = false, int radiusShield = 20)
        {
            // Transformation
            Vector2[] worldPlayerVertices = new Vector2[playerVertices.Length];
            for(int i = 0; i < worldPlayerVertices.Length; i++)
            {
                // Rotation
                worldPlayerVertices[i].X = playerVertices[i].X * MathF.Cos(rotation) - playerVertices[i].Y * MathF.Sin(rotation);
                worldPlayerVertices[i].Y = playerVertices[i].X * MathF.Sin(rotation) + playerVertices[i].Y * MathF.Cos(rotation);

                // Translation
                worldPlayerVertices[i] += pos;    
            }
            
            // Draw
            for(int i = 0; i < worldPlayerVertices.Length; i++)
                Raylib.DrawLineV(worldPlayerVertices[i], worldPlayerVertices[(i + 1) % worldPlayerVertices.Length], Color.WHITE);

            // Invincibility
            if (isInvincibility)
                Raylib.DrawCircleLines((int)pos.X, (int)pos.Y, radiusShield + 2, Color.SKYBLUE);
        }

        public void DrawSight(Vector2 initPos, float rotation, int radius)
        {
            // Transformation
            Vector2[] worldSightVertices = new Vector2[sightVertices.Length];
            for (int i = 0; i < worldSightVertices.Length; i++)
            {
                // Rotation
                worldSightVertices[i].X = sightVertices[i].X * MathF.Cos(rotation) - sightVertices[i].Y * MathF.Sin(rotation);
                worldSightVertices[i].Y = sightVertices[i].X * MathF.Sin(rotation) + sightVertices[i].Y * MathF.Cos(rotation);

                // Translation
                worldSightVertices[i] += initPos;

                // Second rotation (around the initPos)
                worldSightVertices[i].X -= MathF.Cos(rotation) * radius;
                worldSightVertices[i].Y -= MathF.Sin(rotation) * radius;
            }

            // Draw
            for (int i = 0; i < worldSightVertices.Length; i++)
                Raylib.DrawLineV(worldSightVertices[i], worldSightVertices[(i + 1) % worldSightVertices.Length], Color.YELLOW);
        }

        public void DrawPlayerIU(int score, int money, int life)
        {
            Raylib.DrawText("Player", 25, 15, 10, Color.WHITE);
            Raylib.DrawText("Life : " + life, 15, 30, 10, Color.WHITE);
            Raylib.DrawText("Money : " + money, 15, 45, 10, Color.WHITE);
            Raylib.DrawText("Score : " + score, 15, 60, 10, Color.WHITE);
        }

        // ---------------
        // --- Weapon ----
        // ---------------
        public void DrawWeaponIU(int level, int costUpgrade, float frequency, float speed, int moneyPlayer, bool isLevelMax = false)
        {
            Raylib.DrawText("Weapon", 1150, 15, 10, Color.WHITE);
            if(!isLevelMax)
            {
                Raylib.DrawText("Level : " + level, 1100, 30, 10, Color.WHITE);
                Raylib.DrawText("Cost upgrade : " + costUpgrade, 1100, 45, 10, Color.WHITE);
                if(moneyPlayer >= costUpgrade)
                    Raylib.DrawText("Space to upgrade", 1125, 90, 10, Color.GREEN);
                else
                    Raylib.DrawText("Space to upgrade", 1125, 90, 10, Color.WHITE);
            }
            else
            {
                Raylib.DrawText("Level : " + level, 1100, 30, 10, Color.WHITE);
                Raylib.DrawText("Cost upgrade : LEVEL MAX", 1100, 45, 10, Color.WHITE);
            }
            Raylib.DrawText("Frequency : " + frequency, 1100, 60, 10, Color.WHITE);
            Raylib.DrawText("Speed : " + speed, 1100, 75, 10, Color.WHITE);
        }

        // ---------------
        // ---- Grunt ----
        // ---------------
        public void DrawGrunt(Vector2 pos, float rotation)
        {
            // Transformation
            Vector2[] worldPGruntVertices = new Vector2[gruntVertices.Length];
            for (int i = 0; i < worldPGruntVertices.Length; i++)
            {
                // Rotation
                worldPGruntVertices[i].X = gruntVertices[i].X * MathF.Cos(rotation) - gruntVertices[i].Y * MathF.Sin(rotation);
                worldPGruntVertices[i].Y = gruntVertices[i].X * MathF.Sin(rotation) + gruntVertices[i].Y * MathF.Cos(rotation);

                // Translation
                worldPGruntVertices[i] += pos;
            }

            // Draw
            for (int i = 0; i < worldPGruntVertices.Length; i++)
                Raylib.DrawLineV(worldPGruntVertices[i], worldPGruntVertices[(i + 1) % worldPGruntVertices.Length], Color.BLUE);
        }

        public void DrawGruntSpawn(Vector2 pos)
        {
            // Transformation
            Vector2[] worldPGruntVertices = new Vector2[gruntVertices.Length];
            for (int i = 0; i < worldPGruntVertices.Length; i++)
            {
                worldPGruntVertices[i] = gruntVertices[i];
                // Translation
                worldPGruntVertices[i] += pos;
            }

            // Draw
            Color color = new Color(125, 150, 255, 255);
            for (int i = 0; i < worldPGruntVertices.Length; i++)
                Raylib.DrawLineV(worldPGruntVertices[i], worldPGruntVertices[(i + 1) % worldPGruntVertices.Length], color);
        }

        // ---------------
        // --- Bullet ----
        // ---------------
        public void DrawBullet(Vector2 pos, float rotation)
        {
            // Transformation
            Vector2[] worldPBulletVertices = new Vector2[bulletVertices.Length];
            for (int i = 0; i < worldPBulletVertices.Length; i++)
            {
                // Rotation
                worldPBulletVertices[i].X = bulletVertices[i].X * MathF.Cos(rotation) - bulletVertices[i].Y * MathF.Sin(rotation);
                worldPBulletVertices[i].Y = bulletVertices[i].X * MathF.Sin(rotation) + bulletVertices[i].Y * MathF.Cos(rotation);

                // Translation
                worldPBulletVertices[i] += pos;
            }

            // Draw
            for (int i = 0; i < worldPBulletVertices.Length; i++)
                Raylib.DrawLineV(worldPBulletVertices[i], worldPBulletVertices[(i + 1) % worldPBulletVertices.Length], Color.YELLOW);
        }

        // ---------------
        // -- BlackHole --
        // ---------------
        public void DrawBlackHole(Vector2 position, int radius)
        {
            Raylib.DrawCircleLines((int)position.X, (int)position.Y, radius, Color.DARKPURPLE);
        }

        // ---------------
        // ----- UI ------
        // ---------------
        public void DrawLevelUI(int timer, int level)
        {
            Raylib.DrawText("Level : " + level + " | time : " + timer, 580, 15, 5, Color.WHITE);
        }

        public void DrawGameOver(int score)
        {
            Raylib.DrawText("Game Over", 520, 300, 50, Color.WHITE);
            Raylib.DrawText("Score : " + score, 605, 360, 15, Color.WHITE);
            Raylib.DrawText("press ESCAPE to close the game", 520, 386, 16, Color.WHITE);
        }


        // ---------------
        // ---- Event ----
        // ---------------
        public void HandleEvents(IEnumerable<Core.GameEvent> gameEvents, Core.GameData gameData)
        {
            foreach(Core.GameEvent gameEvent in gameEvents)
            {
                switch (gameEvent)
                {
                    case Core.EnemyKilledEvent:
                        Core.EnemyKilledEvent enemyKilledEvent = gameEvent as Core.EnemyKilledEvent;
                        allAnimation.Add(new EnemyKillAnimation(enemyKilledEvent.enemy));
                        break;

                    case Core.NewLevelEvent:
                        Core.NewLevelEvent newLevelEvent = gameEvent as Core.NewLevelEvent;
                        allAnimation.Add(new NewLevelAnimation(newLevelEvent.level));
                        break;
                }
            }
        }
    }
}
