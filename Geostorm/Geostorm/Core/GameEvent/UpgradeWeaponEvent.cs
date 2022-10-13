namespace Geostorm.Core
{
    public class UpgradeWeaponEvent : GameEvent
    {
        public Weapon weapon;
        public int cost;

        public UpgradeWeaponEvent(Weapon weapon, int cost)
        {
            this.weapon = weapon;
            this.cost = cost;
        }
    }
}
