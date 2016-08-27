using Assets.Scripts.TileMap.Data;
using Random = UnityEngine.Random;

namespace Assets.Scripts.TileMap.LevelGenerator
{
    public class ObjectDropperGenerator : IGenerator
    {
        public ITileDecorator Decorator;
        public int Qty;

        public void Generate(GameMap map)
        {
            while (Qty >= 0)
            {
                var x = Random.Range(0, map.MapWidth);
                var y = Random.Range(0, map.MapHeight);

                if(map.Map[x,y].Blocked)
                    continue;

                map.Map[x,y].Decorators.Add(Decorator);

                Qty--;

            }
        }
    }
}
