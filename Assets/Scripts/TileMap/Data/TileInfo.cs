using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;

namespace Assets.Scripts.TileMap.Data
{
    public class TileInfo
    {
        public TileTypes Type;
        public List<ITileDecorator> Decorators = new List<ITileDecorator>();

        public bool Blocked => Decorators.Any(d => d.Block);
    }
}
