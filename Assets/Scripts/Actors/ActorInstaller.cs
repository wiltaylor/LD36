using Assets.Scripts.PathFinding;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Actors
{
    [CreateAssetMenu(fileName = "ActorSystemInstaller", menuName = "Installers/ActorSystem", order = 3)]
    public class ActorInstaller : ScriptableObjectInstaller
    {
        public Sprite[] Bodies;
        public Sprite[] Heads;
        public Sprite SelectionSprite;

        public Sprite CityCentre;
        public Sprite Barracks;
        public Sprite Farm;

        public Sprite Spear;

        public Sprite Building;

        public override void InstallBindings()
        {
            Container.Bind<PathFinder>();
            Container.BindSignal<UnitClickSignal>();
            Container.BindTrigger<UnitClickSignal.Trigger>();
            Container.BindSignal<BuildingClickSignal>();
            Container.BindTrigger<BuildingClickSignal.Trigger>().WhenInjectedInto<BuildingController>();

            Container.Bind<Sprite[]>().WithId("TeamBodies").FromInstance(Bodies).WhenInjectedInto<UnitFactory>();
            Container.Bind<Sprite[]>().WithId("UnitHeads").FromInstance(Heads).WhenInjectedInto<UnitFactory>();
            Container.Bind<Sprite>().WithId("SelectionSprite").FromInstance(SelectionSprite);

            Container.Bind<Sprite>()
                .WithId("CityCenterSprite")
                .FromInstance(CityCentre)
                .WhenInjectedInto<BuildingFactory>();

            Container.Bind<Sprite>()
                .WithId("FarmSprite")
                .FromInstance(Farm)
                .WhenInjectedInto<BuildingFactory>();

            Container.Bind<Sprite>()
                .WithId("BarracksSprite")
                .FromInstance(Barracks)
                .WhenInjectedInto<BuildingFactory>();

            Container.Bind<Sprite>()
                .WithId("BuildingSprite")
                .FromInstance(Building)
                .WhenInjectedInto<BuildingFactory>();

            Container.Bind<Sprite>()
                .WithId("SpearSprite")
                .FromInstance(Spear)
                .WhenInjectedInto<UnitFactory>();


            Container.Bind<UnitFactory>();

            Container.Bind<BuildingFactory>();

            Container.Bind<IInitializable>().To<BuildingPlacer>().AsSingle();
        }
    }
}
