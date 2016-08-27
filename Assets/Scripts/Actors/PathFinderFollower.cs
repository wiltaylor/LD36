using Assets.Scripts.PathFinding;
using Assets.Scripts.TileMap.Data;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Actors
{
    public class PathFinderFollower : MonoBehaviour, IInitializable
    {
        //[Inject]
        public PathFinder Pathfinder;

        //[Inject]
        public GameMap Map;

        private PathFindingResult _currentpath;

        private float _distanceBeforeNext = 0.15f;

        private Vector3 _nextworldpos = default(Vector3);

        public float Speed = 1f;

        public void MoveTo(int x, int y)
        {
            if (Map.Map[x, y].Blocked)
            {
                _currentpath = null;
                return;
            }
                

            var position = CordUtil.WorldToTile(transform.position);
            var currentx = position.First;
            var currenty = position.Second;
            _currentpath = Pathfinder.FindPath(currentx, currenty, x, y);

            _nextworldpos = CordUtil.TileToWorld(_currentpath.X, _currentpath.Y);

        }

        void FixedUpdate()
        {
            if (_currentpath == null)
                return;

            if (Vector3.Distance(_nextworldpos, transform.position) <= _distanceBeforeNext)
            {
                _currentpath = _currentpath.Next;

                if (_currentpath == null)
                    return;

                _nextworldpos = CordUtil.TileToWorld(_currentpath.X, _currentpath.Y);
            }

            transform.position = Vector3.MoveTowards(transform.position, _nextworldpos, Speed * Time.fixedDeltaTime);
        }

        public void Initialize()
        {
            Pathfinder.CheckNode = (x, y) =>
            {
                if (x < 0)
                    return false;
                if (x > Map.MapWidth -1)
                    return false;
                if (y < 0)
                    return false;
                if (y > Map.MapHeight -1 )
                    return false;
                return !Map.Map[x, y].Blocked;
            };
        }
    }
}
