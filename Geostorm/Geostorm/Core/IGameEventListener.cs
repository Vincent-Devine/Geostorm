using System.Collections.Generic;

namespace Geostorm.Core
{
    public interface IGameEventListener
    {
        public void HandleEvents(IEnumerable<GameEvent> gameEvents, GameData gameData);
    }
}
