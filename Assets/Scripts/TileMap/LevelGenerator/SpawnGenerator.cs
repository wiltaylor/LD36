using System.Linq;
using Assets.Scripts.TileMap.Data;
using Random = UnityEngine.Random;

namespace Assets.Scripts.TileMap.LevelGenerator
{
    public class SpawnGenerator : IGenerator
    {
        public TileTypes Tile;
        public TileTypes[] BannedTypes;
        public int Range;

        public void Generate(GameMap map)
        {
            while (true)
            {
                var sx = Random.Range(0, map.MapWidth);
                var sy = Random.Range(0, map.MapHeight);
                var ex = sx + Range;
                var ey = sy + Range;

                if (ex > map.MapWidth)
                    continue;
                if (ey > map.MapHeight)
                    continue;
                var banned = false;

                for (var x = sx; x < ex; x++)
                    for (var y = sy; y < ey; y++)
                    {
                        if(BannedTypes.Any(b => b == map.Map[x,y].Type))
                            banned = true;
                    }

                if(banned)
                    continue;

                for (var x = sx; x < ex; x++)
                    for (var y = sy; y < ey; y++)
                    {
                        map.Map[x, y].Type = Tile;
                        map.Map[x, y].Decorators.Clear();
                    }

                break;
            }

        }
    }
}
