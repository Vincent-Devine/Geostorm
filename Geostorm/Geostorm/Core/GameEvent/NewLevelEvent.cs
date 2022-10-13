namespace Geostorm.Core
{
    class NewLevelEvent : GameEvent
    {
        public int level;

        public NewLevelEvent(int level = 1)
        {
            this.level = level;
        }
    }
}
