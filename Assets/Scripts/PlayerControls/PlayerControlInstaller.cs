using UnityEngine;
using Zenject;

namespace Assets.Scripts.PlayerControls
{
    [CreateAssetMenu(fileName = "PlayerControlSystemInstaller", menuName = "Installers/PlayerControlSystem", order = 4)]
    public class PlayerControlInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<PlayerSessionModifiers>().AsSingle();
            Container.Bind<UnitSelectionManager>().AsSingle().NonLazy();
            Container.Bind<SelectionBoxDrawer>().FromGameObject().AsSingle().NonLazy();
        }
    }
}
