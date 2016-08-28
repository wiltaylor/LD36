using Assets.Scripts.TileMap.Data;
using Random = UnityEngine.Random;

namespace Assets.Scripts.TileMap.LevelGenerator
{
    public class PatchGenerator : IGenerator
    {
        public TileTypes TileType;
        public ITileDecorator[] Decorators;
        public int UpperRange;
        public int LowerRange;
        public int Qty;


        public WorldGenerator WorldGenerator { get; set; }

        public void Generate(GameMap map)
        {
            while (Qty >= 0)
            {
                var sx = UnityEngine.Random.Range(0, map.MapWidth);
                var sy = Random.Range(0, map.MapHeight);
                var range = Random.Range(LowerRange, UpperRange);
                var ex = sx + range;
                var ey = sy + range;

                if (ex > map.MapWidth)
                    ex = map.MapWidth - 1;
                if (ey > map.MapHeight)
                    ey = map.MapHeight -1;

                for(var x = sx; x < ex; x++)
                    for (var y = sy; y < ey; y++)
                    {
                        if(map.Map[x,y].Blocked)
                            continue;

                        map.Map[x, y].Type = TileType;

                        if(Decorators == null || Decorators.Length == 0)
                            continue;
                        
                        foreach(var d in Decorators)
                            map.Map[x,y].Decorators.Add(d);
                    }
                        
                Qty--;
            }
        }
    }
}
