namespace Geostorm.Renderer
{
    public abstract class Animation
    {
        public bool isFinished;
        protected int animationTime;
        protected int timer;

        protected Animation(int animationTime = 10)
        {
            isFinished = false;
            this.animationTime = animationTime;
            this.timer = 0;
        }

        public abstract void Update();
    }
}
