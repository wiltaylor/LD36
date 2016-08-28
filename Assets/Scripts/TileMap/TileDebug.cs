using Assets.Scripts.Actors;
using Assets.Scripts.PathFinding;
using Assets.Scripts.TileMap.Data;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.TileMap
{
    public class TileDebug : IInitializable
    {
        private TileBlockController _blockController;

        [Inject] private UnitFactory factory;
        
        public void Initialize()
        {
            factory.Create(UnitType.Worker, 0, 5, 5);
        }
    }
}
