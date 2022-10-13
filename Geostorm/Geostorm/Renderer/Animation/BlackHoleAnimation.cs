using Raylib_cs;

namespace Geostorm.Renderer
{
    class BlackHoleAnimation : Animation
    {
        private Core.BlackHole blackHole;

        public BlackHoleAnimation(Core.BlackHole blackHole)
        {
            this.blackHole = blackHole;
            timer = 0;
        }

        public override void Update()
        {
            timer++;
            timer = timer % 5;

            Raylib.DrawCircleLines((int)blackHole.position.X, (int)blackHole.position.Y, (int)blackHole.collisionRadius + timer, Color.PURPLE);
            Raylib.DrawCircleLines((int)blackHole.position.X, (int)blackHole.position.Y, (int)blackHole.collisionRadius - timer, Color.PURPLE);

            if (blackHole.isDead)
                isFinished = true;
        }
    }
}
