using System.Collections.Generic;
using System.Numerics;

namespace Geostorm.Core
{
    public abstract class Entity
    {
        public bool isDead;

        public Vector2 position;
        public float rotation;
        public float collisionRadius;

        public Entity(Vector2 position, float rotation = 0f, float collisionRadius = 5f)
        {
            this.isDead = false;

            this.position = position;
            this.rotation = rotation;
            this.collisionRadius = collisionRadius;
        }

        public abstract void Update(in GameInputs gameInputs, GameData gameData, List<GameEvent> events);

        public abstract void Draw(Renderer.Graphics graphics);
    }
}
