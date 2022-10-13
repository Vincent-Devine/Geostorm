using System.Collections.Generic;

namespace Geostorm.Core
{
    public interface ISystem
    {
        public void Update(GameData gameData, GameInputs inputs, List<GameEvent> events);
    }
}
