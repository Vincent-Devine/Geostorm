namespace Geostorm.Core
{
    class PlayerTakeDamageEvent : GameEvent
    {
        public Player player;
        public int damage;

        public PlayerTakeDamageEvent(Player player, int damage)
        {
            this.player = player;
            this.damage = damage;
        }
    }
}
