using Assets.Scripts.TileMap.Blocks;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.TileMap
{
    [CreateAssetMenu(fileName = "TileSystemInstaller", menuName = "Installers/TileSystem", order = 1)]
    public class TileSystemInstaller : ScriptableObjectInstaller
    {
        public Sprite[] GrassTiles;
        public Sprite[] DirtTiles;

        public override void InstallBindings()
        {
            Container.Bind<Sprite>()
                .FromMethod(i =>
                {
                    var tex = new Texture2D(1024, 1024);
                    return Sprite.Create(tex, new Rect(0f, 0f, tex.width, tex.height), Vector2.zero);
                })
                .WhenInjectedInto<TileBlockController>();
            Container.Bind<TileSet>().AsSingle();
            Container.Bind<TileBlockController>().FromGameObject();
            
     

            //Bind tiles
            Container.Bind<ITile>().To<Dirt>().AsSingle();
            Container.Bind<Sprite[]>().FromInstance(DirtTiles).WhenInjectedInto<Dirt>();
            Container.Bind<ITile>().To<Grass>().AsSingle();
            Container.Bind<Sprite[]>().FromInstance(GrassTiles).WhenInjectedInto<Grass>();
            
            Container.Bind<int>().WithId("TileWidth").FromInstance(32).WhenInjectedInto<TileBlockController>();
            Container.Bind<int>().WithId("TileHeight").FromInstance(32).WhenInjectedInto<TileBlockController>();
            Container.BindFactory<TileBlockController, TileBlockController.Factory>().FromGameObject();
            
            //Camera
            Container.Bind<Camera>().FromInstance(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>()).WhenInjectedInto<TileBlockController>();

            Container.BindSignal<TileClickSignal>();
            Container.BindTrigger<TileClickSignal.Trigger>().WhenInjectedInto<TileBlockController>();

            //TileMap
            Container.Bind<TileMap>().AsSingle();
            Container.Bind<int>().WithId("MapWidth").FromInstance(128).WhenInjectedInto<TileMap>();
            Container.Bind<int>().WithId("MapHeight").FromInstance(128).WhenInjectedInto<TileMap>();
            Container.Bind<int>().WithId("TilesPerBlock").FromInstance(32).WhenInjectedInto<TileMap>();


            //Debugging
            Container.Bind<TileDebug>().NonLazy();
        }
    }
}
