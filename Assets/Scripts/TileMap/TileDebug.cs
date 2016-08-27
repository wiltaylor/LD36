using Assets.Scripts.Actors;
using Assets.Scripts.PathFinding;
using Assets.Scripts.TileMap.Data;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.TileMap
{
    public class TileDebug
    {
        private TileBlockController _blockController;

        [Inject]
        public void Construct(GameMap map, TileClickSignal clicksignal)
        {
            clicksignal.Event += (x, y) =>
            {
                var npc = GameObject.FindGameObjectWithTag("Debug").GetComponent<PathFinderFollower>();
                npc.Map = map;
                npc.Pathfinder = new PathFinder();
                npc.Initialize();

                npc.MoveTo(x, y);
                
            };
        }
    }
}
