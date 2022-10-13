using Raylib_cs;

namespace Geostorm.Renderer
{
    public class EnemyKillAnimation : Animation
    {
        private Core.Enemy enemy;

        public EnemyKillAnimation(Core.Enemy enemy, int animationTime = 10)
            :base(animationTime)
        {
            this.enemy = enemy;
        }

        public override void Update()
        {
            GainPts(enemy);
            if (timer > animationTime)
                isFinished = true;
            timer++;
        }

        private void GainPts(Core.Enemy enemy)
        {
            Raylib.DrawText("+ " + enemy.scoreWin +" pts !", (int)enemy.position.X, (int)enemy.position.Y, 5, Color.WHITE);
        }

        // TODO : Destruction enemy
    }
}
