using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.PlayerLogic
{
    public class PlayerManager
    {
        public List<PlayerInfo> Players;

        public PlayerInfo HumanPlayer => Players.FirstOrDefault(p => p.Type == PlayerType.Human);


    }
}
