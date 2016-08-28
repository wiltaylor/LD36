using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.TileMap.Data
{
    [Serializable]
    public class TileTypeData
    {
        public TileTypes Type;
        public bool Passble;
        public float WalkCost;
    }
}
