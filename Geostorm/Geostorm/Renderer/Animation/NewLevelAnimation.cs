using Raylib_cs;

namespace Geostorm.Renderer
{
    public class NewLevelAnimation : Animation
    {
        private int level;

        public NewLevelAnimation(int level = 1, int animationTime = 30)
            :base(animationTime)
        {
            this.level = level;
        }

        public override void Update()
        {
            Raylib.DrawText("Level " + level, 550, 300, 50, Color.WHITE);
            timer++;
            if (timer > animationTime)
                isFinished = true;
        }
    }
}
