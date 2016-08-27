using Assets.Scripts.TileMap;
using Assets.Scripts.TileMap.Data;
using Assets.Scripts.TileMap.Data.Decorators;
using Assets.Scripts.TileMap.LevelGenerator;
using Zenject;

namespace Assets.Scripts.LevelDecorators
{
    public class TestLandSmallSand : MonoInstaller
    {
        public int MapWidth = 64;
        public int MapHeight = 64;

        private IGenerator[] _generators = {
        new FillGenerator
        {
            FillTile = TileTypes.Sand
        },
        new PatchGenerator
        {
            TileType = TileTypes.Rocks,
            Decorators = new ITileDecorator[] { new RockDecorator()},
            UpperRange = 30,
            LowerRange = 15,
            Qty = 2
        },
        new PatchGenerator
        {
            TileType = TileTypes.Water,
            Decorators = new ITileDecorator[] { new WaterDecorator()},
            UpperRange = 10,
            LowerRange = 3,
            Qty = 5
        },
        new PatchGenerator
        {
            TileType = TileTypes.Dirt,
            Decorators = new ITileDecorator[] { },
            UpperRange = 10,
            LowerRange = 3,
            Qty = 5
        },
        new DeformGenerator
        {
            Qty  = 10000
        },
        new ObjectDropperGenerator
        {
            Decorator = new TreeDecorator(),
            Qty = 5
        },
        new SpawnGenerator
        {
            Range = 10,
            Tile = TileTypes.Sand,
            BannedTypes = new[] { TileTypes.Water, TileTypes.Rocks}
        },
        new SpawnGenerator
        {
            Range = 10,
            Tile = TileTypes.Sand,
            BannedTypes = new[] { TileTypes.Water, TileTypes.Rocks}
        }
    };

        public override void InstallBindings()
        {
            Container.Bind<int>().WithId("LevelStartWidth").FromInstance(MapWidth);
            Container.Bind<int>().WithId("LevelStartHeight").FromInstance(MapHeight);

            Container.Bind<IGenerator[]>().WithId("LevelGenerators").FromInstance(_generators);
        }
    }
}
