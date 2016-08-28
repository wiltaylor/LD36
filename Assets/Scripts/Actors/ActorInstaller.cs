using Assets.Scripts.PathFinding;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Actors
{
    [CreateAssetMenu(fileName = "ActorSystemInstaller", menuName = "Installers/ActorSystem", order = 3)]
    public class ActorInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<PathFinder>();
            Container.BindSignal<UnitClickSignal>();
            Container.BindTrigger<UnitClickSignal.Trigger>().WhenInjectedInto<UnitController>();
            Container.BindSignal<BuildingClickSignal>();
            Container.BindTrigger<BuildingClickSignal.Trigger>().WhenInjectedInto<BuildingController>();

            //Container.Bind<IInitializable>().To<PathFinderFollower>();
        }
    }
}
