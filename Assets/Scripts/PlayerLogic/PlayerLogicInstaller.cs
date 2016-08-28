using UnityEngine;
using Zenject;

namespace Assets.Scripts.PlayerLogic
{
    [CreateAssetMenu(fileName = "PlayerLogicSystemInstaller", menuName = "Installers/PlayerLogicSystem", order = 5)]
    public class PlayerLogicInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<PlayerManager>().AsSingle();
        }
    }
}
