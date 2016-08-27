using Assets.Scripts.TileMap;
using Assets.Scripts.TileMap.Data;
using Assets.Scripts.TileMap.Data.Decorators;
using Assets.Scripts.TileMap.LevelGenerator;
using Zenject;

namespace Assets.Scripts.LevelDecorators
{
    public class TestLandsSmall : MonoInstaller
    {
        public int MapWidth = 64;
        public int MapHeight = 64;

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
            Container.Bind<int>().WithId("LevelStartWidth").FromInstance(MapWidth);
            Container.Bind<int>().WithId("LevelStartHeight").FromInstance(MapHeight);

            Container.Bind<IGenerator[]>().WithId("LevelGenerators").FromInstance(_generators);

        }
    }
}
