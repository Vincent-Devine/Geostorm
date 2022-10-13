namespace Geostorm.Core
{
    public class EnemyKilledEvent : GameEvent
    {
        public Enemy enemy;
        public Bullet bullet;

        public EnemyKilledEvent(Enemy enemy, Bullet bullet)
        {
            this.enemy = enemy;
            this.bullet = bullet;
        }
    }
}
