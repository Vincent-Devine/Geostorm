namespace Geostorm.Core
{
    public class BulletShootEvent : GameEvent
    {
        public Bullet bullet;

        public BulletShootEvent(Bullet bullet)
        {
            this.bullet = bullet;
        }
    }
}
