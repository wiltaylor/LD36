using UnityEngine;

namespace Assets.Scripts.PlayerLogic
{
    public enum PlayerType
    {
        Human,
        Null,
        AI
    }

    public class PlayerInfo
    {
        public int ID;
        public PlayerType Type;
        public int Food;
        public int Wood;
        public int Home;
        public int Stone;
        public int StartSpawnX;
        public int StartSpawnY;
        public int EndSpawnX;
        public int EndSpawnY;
    }
}
