using System.Collections.Generic;
using System.Numerics;

namespace Geostorm.Core
{
    public class Bullet : Entity
    {
        public Vector2 direction;

        public Bullet(Vector2 position, Vector2 direction, float rotation = 0f, float collisionRadius = 8f, float speed = 7f)
            : base(position, rotation, collisionRadius)
        {
            this.direction = direction * speed;
        }

        public override void Update(in GameInputs gameInputs, GameData gameData, List<GameEvent> events)
        {
            // Deplacement
            if (!CheckBulletOutsidMap(gameInputs))
                position += direction;
            else
                isDead = true;
        }

        public override void Draw(Renderer.Graphics graphics)
        {
            graphics.DrawBullet(position, rotation);
        }

        public bool CheckBulletOutsidMap(in GameInputs inputs)
        {
            if (position.X < 0f
             && position.X > inputs.screenSize.X
             && position.Y < 0f
             && position.Y > inputs.screenSize.Y)
                return true;
            return false;
        }
    }

}