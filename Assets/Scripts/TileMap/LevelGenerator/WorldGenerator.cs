using System.Collections.Generic;
using System.Runtime.InteropServices;
using Assets.Scripts.Actors;
using Assets.Scripts.InputHandler;
using Assets.Scripts.PlayerLogic;
using Assets.Scripts.TileMap.Data;
using Zenject;

namespace Assets.Scripts.TileMap.LevelGenerator
{
    public class WorldGenerator
    {
        [Inject]
        public List<IGenerator> _generators;

        [Inject] public UnitFactory UnitFactory;

        [Inject] public BuildingFactory BuildingFactory;

        [Inject] public PlayerManager PlayerManager;

        [Inject] public CameraMover CameraMover;

        public void GenerateMap(GameMap map)
        {
            foreach (var gen in _generators)
            {
                gen.WorldGenerator = this;
                gen.Generate(map);
            }
                
        }
    }
}
