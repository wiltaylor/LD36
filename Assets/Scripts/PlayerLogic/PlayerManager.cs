using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.PlayerLogic
{
    public class PlayerManager
    {
        public List<PlayerInfo> Players = new List<PlayerInfo>();

        public PlayerInfo HumanPlayer => Players.FirstOrDefault(p => p.Type == PlayerType.Human);

        public PlayerInfo AddNullPlayer(int wood, int food, int stone)
        {
            var result = new PlayerInfo
            {
                ID = Players.Count,
                Type = PlayerType.Null,
                Wood = wood,
                Food = food,
                Home = 0,
                Stone = stone
            };

            Players.Add(result);

            return result;
        }
    
    }
}
