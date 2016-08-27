using Assets.Scripts.Input;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.InputHandler
{
    [CreateAssetMenu(fileName = "InputSystemInstaller", menuName = "Installers/InputSystem", order = 2)]
    public class InputInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            //Camera
            Container.Bind<Camera>().FromInstance(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>());

            Container.BindSignal<ScrollSignal>();
            Container.BindTrigger<ScrollSignal.Trigger>().WhenInjectedInto<InputController>();
            Container.Bind<InputController>().AsSingle().NonLazy();
            Container.Bind<ITickable>().To<InputController>();
            Container.Bind<IInitializable>().To<CameraMover>().AsSingle().NonLazy();

        }
    }
}
