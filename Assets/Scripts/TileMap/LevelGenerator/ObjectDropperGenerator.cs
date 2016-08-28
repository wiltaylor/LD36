using Assets.Scripts.TileMap.Data;
using Random = UnityEngine.Random;

namespace Assets.Scripts.TileMap.LevelGenerator
{
    public class ObjectDropperGenerator : IGenerator
    {
        public ITileDecorator Decorator;
        public int Qty;

        public WorldGenerator WorldGenerator { get; set; }

        public void Generate(GameMap map)
        {
            var retry = 100;
            while (Qty >= 0)
            {
                if (retry <= 0)
                    break;

                retry--;

                var x = Random.Range(0, map.MapWidth);
                var y = Random.Range(0, map.MapHeight);

                if(map.Map[x,y].Blocked)
                    continue;

                retry = 100;

                map.Map[x,y].Decorators.Add(Decorator);

                Qty--;

            }
        }
    }
}
