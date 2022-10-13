using System;
using System.Collections.Generic;
using System.Numerics;

namespace Geostorm.Core
{
    public class Grunt : Enemy
    {
        public Grunt(Vector2 position, float rotation = 0f, float collisionRadius = 17f, int spawnTime = 30, int damage = 1, int scoreWin = 100)
            : base(position, rotation, collisionRadius, spawnTime, damage, scoreWin)
        {
        }

        protected override void DoUpdate(in GameInputs gameInputs, GameData gameData, List<GameEvent> events)
        {
            // Movement
            float speed = 2f;
            direction = gameData.Player.position - position;
            direction /= direction.Length();
            position += direction * speed;

            // Rotation
            Vector2 n = Vector2.Normalize(direction);
            rotation = MathF.Atan2(n.Y, n.X);
        }

        public override void Draw(Renderer.Graphics graphics)
        {
            if (isSpawning)
                graphics.DrawGruntSpawn(position);
            else
                graphics.DrawGrunt(position, rotation);
        }
    }

}