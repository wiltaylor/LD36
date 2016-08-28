using UnityEngine;
using Zenject;

namespace Assets.Scripts.Actions
{
    [CreateAssetMenu(fileName = "ActionInstaller", menuName = "Installers/ActionSystem")]
    public class ActionInstaller : ScriptableObjectInstaller
    {
        public UIAction[] Actions;

        public override void InstallBindings()
        {
            Container.Bind<UIAction[]>().FromInstance(Actions).AsSingle();
            Container.Bind<ActionManager>().AsSingle();
        }
    }
}
