using System.Collections.Generic;
using Assets.Scripts.TileMap.Data;
using Zenject;

namespace Assets.Scripts.TileMap.LevelGenerator
{
    public class WorldGenerator
    {
        [Inject]
        public List<IGenerator> _generators;

        public void GenerateMap(GameMap map)
        {
            foreach(var gen in _generators)
                gen.Generate(map);
        }
    }
}
