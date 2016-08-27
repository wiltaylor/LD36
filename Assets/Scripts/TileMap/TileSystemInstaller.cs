using System.Collections.Generic;
using Assets.Scripts.TileMap.Data;
using Assets.Scripts.TileMap.LevelGenerator;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.TileMap
{
    [CreateAssetMenu(fileName = "TileSystemInstaller", menuName = "Installers/TileSystem", order = 1)]
    public class TileSystemInstaller : ScriptableObjectInstaller
    {
        public Sprite[] GrassTiles;
        public Sprite[] DirtTiles;
        public Sprite[] SandTiles;
        public Sprite[] RocksTiles;
        public Sprite[] WaterTiles;

        public Sprite[] TreeDecals;

        public TileTypeData[] TileTypeData;

        [Inject(Optional = true, Id = "LevelStartWidth")]
        private int MapWidth = 64;
        [Inject(Optional = true, Id = "LevelStartHeight")]
        private int MapHeight = 64;

        [Inject(Optional = true, Id = "LevelGenerators")] private IGenerator[] _generators = {
            new FillGenerator
            {
                FillTile = TileTypes.Grass
            },

            new ObjectDropperGenerator
            {
                Decorator = new TreeDecorator(),
                Qty = 5000
            },
            new PatchGenerator
            {
                TileType = TileTypes.Rocks,
                UpperRange = 20,
                LowerRange = 5,
                Qty = 5
            },
            new PatchGenerator
            {
                TileType = TileTypes.Water,
                UpperRange = 20,
                LowerRange = 5,
                Qty = 5
            }
        };

        public override void InstallBindings()
        {

            //Container.Bind<List<IGenerator>>().FromInstance(_generators);

            foreach (var gen in _generators)
                Container.Bind<IGenerator>().FromInstance(gen);

            Container.Bind<Sprite>()
                .FromMethod(i =>
                {
                    var tex = new Texture2D(1024, 1024);
                    return Sprite.Create(tex, new Rect(0f, 0f, tex.width, tex.height), Vector2.zero);
                })
                .WhenInjectedInto<TileBlockController>();


            Container.Bind<Sprite[]>().WithId(TileTypes.Grass).FromInstance(GrassTiles).WhenInjectedInto<TileSet>();
            Container.Bind<Sprite[]>().WithId(TileTypes.Dirt).FromInstance(DirtTiles).WhenInjectedInto<TileSet>();
            Container.Bind<Sprite[]>().WithId(TileTypes.Water).FromInstance(WaterTiles).WhenInjectedInto<TileSet>();
            Container.Bind<Sprite[]>().WithId(TileTypes.Rocks).FromInstance(RocksTiles).WhenInjectedInto<TileSet>();
            Container.Bind<Sprite[]>().WithId(TileTypes.Sand).FromInstance(SandTiles).WhenInjectedInto<TileSet>();
            Container.Bind<Sprite[]>().WithId(DecalType.Tree).FromInstance(TreeDecals).WhenInjectedInto<TileSet>();

            Container.Bind<TileTypeData[]>().FromInstance(TileTypeData).AsSingle();

            Container.Bind<TileSet>().AsSingle();
            Container.Bind<TileBlockController>().FromGameObject();

            Container.Bind<int>().WithId("TileWidth").FromInstance(32).WhenInjectedInto<TileBlockController>();
            Container.Bind<int>().WithId("TileHeight").FromInstance(32).WhenInjectedInto<TileBlockController>();
            Container.BindFactory<TileBlockController, TileBlockController.Factory>().FromGameObject();
            
            //Camera
            Container.Bind<Camera>().FromInstance(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>()).WhenInjectedInto<TileBlockController>();

            Container.BindSignal<TileClickSignal>();
            Container.BindTrigger<TileClickSignal.Trigger>().WhenInjectedInto<TileBlockController>();

            //TileMap
            Container.Bind<TileMap>().AsSingle().NonLazy();
            Container.Bind<int>().WithId("MapWidth").FromInstance(MapWidth);
            Container.Bind<int>().WithId("MapHeight").FromInstance(MapHeight);
            Container.Bind<int>().WithId("TilesPerBlock").FromInstance(32);

            Container.BindCommand<ApplyTileMapCommand>()
                .To<TileMap>(c => c.Apply).AsSingle();

            Container.BindCommand<TileUpdateCommand, int, int, TileTypes>()
                .To<TileMap>(controller => (x, y, type) => controller.SetTile(x,y,type)).AsSingle();

            Container.BindCommand<DecalUpdateCommand, int, int, DecalType>()
                .To<TileMap>(controller => (x, y, type) => controller.SetDecal(x, y, type)).AsSingle();

            Container.Bind<IInitializable>().To<GameMap>().AsSingle().NonLazy();
            Container.Bind<WorldGenerator>().ToSelf().AsSingle();

            
            
            //Debugging
            //Container.Bind<TileDebug>().NonLazy();
        }
    }
}
