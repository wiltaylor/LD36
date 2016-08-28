using Assets.Scripts.TileMap;
using Assets.Scripts.TileMap.Data;
using Assets.Scripts.TileMap.Data.Decorators;
using Assets.Scripts.TileMap.LevelGenerator;
using Zenject;

namespace Assets.Scripts.LevelDecorators
{
    public class TestLands : MonoInstaller
    {
        public int MapWidth = 256;
        public int MapHeight = 256;

        private IGenerator[] _generators = {
            new FillGenerator
            {
                FillTile = TileTypes.Grass
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
                UpperRange = 25,
                LowerRange = 20,
                Qty = 1
            },
            new ObjectDropperGenerator
            {
                Decorator = new TreeDecorator(),
                Qty = 5000
            }
        };

        public override void InstallBindings()
        {
            Container.Bind<int>().WithId("LevelStartWidth").FromInstance(256);
            Container.Bind<int>().WithId("LevelStartHeight").FromInstance(256);
            Container.Bind<IGenerator[]>().WithId("LevelGenerators").FromInstance(_generators);
        }
    }
}
