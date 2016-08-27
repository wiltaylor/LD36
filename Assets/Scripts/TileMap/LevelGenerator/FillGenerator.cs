using Assets.Scripts.TileMap.Data;
using Zenject;

namespace Assets.Scripts.TileMap.LevelGenerator
{
    public class FillGenerator : IGenerator
    {
        [Inject(Optional = true, Id = "FillTileGeneratorType")]
        public TileTypes FillTile = TileTypes.Grass;

        public void Generate(GameMap map)
        {
            for(var x = 0; x < map.MapWidth; x++)
                for (var y = 0; y < map.MapHeight; y++)
                {
                    map.Map[x, y] = new TileInfo {Type = FillTile};
                }
        }
    }
}
