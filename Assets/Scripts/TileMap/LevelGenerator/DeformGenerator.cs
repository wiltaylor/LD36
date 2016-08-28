using Assets.Scripts.TileMap.Data;
using Random = UnityEngine.Random;

namespace Assets.Scripts.TileMap.LevelGenerator
{
    public class DeformGenerator : IGenerator
    {
        public int Qty;

        public WorldGenerator WorldGenerator { get; set; }

        public void Generate(GameMap map)
        {
            while (Qty >= 0)
            {
                var x = Random.Range(0, map.MapWidth);
                var y = Random.Range(0, map.MapHeight);

                var dir = Random.Range(0, 4);
                var type = map.Map[x, y].Type;
                var decorators = map.Map[x, y].Decorators.ToArray();

                switch (dir)
                {
                    case 0: //right
                        if(x + 1 >= map.MapWidth)
                            continue;

                        map.Map[x + 1, y].Type = type;
                        map.Map[x + 1, y].Decorators.Clear();
                        map.Map[x + 1, y].Decorators.AddRange(decorators);
                        break;
                    case 1: //left
                        if (x - 1 < 0)
                            continue;

                        map.Map[x - 1, y].Type = type;
                        map.Map[x - 1, y].Decorators.Clear();
                        map.Map[x - 1, y].Decorators.AddRange(decorators);
                        break;
                    case 2: //up
                        if (y + 1 >= map.MapWidth)
                            continue;

                        map.Map[x, y + 1].Type = type;
                        map.Map[x, y + 1].Decorators.Clear();
                        map.Map[x, y + 1].Decorators.AddRange(decorators);

                        break;
                    case 3: //down
                        if (y - 1 < 0)
                            continue;

                        map.Map[x, y - 1].Type = type;
                        map.Map[x, y - 1].Decorators.Clear();
                        map.Map[x, y - 1].Decorators.AddRange(decorators);

                        break;
                }


                Qty--;
            }
        }
    }
}
