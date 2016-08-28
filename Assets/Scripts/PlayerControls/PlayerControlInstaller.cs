using UnityEngine;
using Zenject;

namespace Assets.Scripts.PlayerControls
{
    [CreateAssetMenu(fileName = "PlayerControlSystemInstaller", menuName = "Installers/PlayerControlSystem", order = 4)]
    public class PlayerControlInstaller : ScriptableObjectInstaller
    {
        public Sprite BuildingBlock;
        public GameObject UI;

        public override void InstallBindings()
        {
            Container.Bind<SpriteRenderer>();

            Container.Bind<Sprite>().WithId("BuildingBlock").FromInstance(BuildingBlock);
            Container.Bind<PlayerSessionModifiers>().AsSingle();
            Container.Bind<PlayerSelectionManager>().AsSingle().NonLazy();
            Container.Bind<SelectionBoxDrawer>().FromGameObject().AsSingle().NonLazy();
            Container.Bind<PlayerBuildModeDrawer>().FromGameObject().AsSingle().NonLazy();
        }
    }
}
