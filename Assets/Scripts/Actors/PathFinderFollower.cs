using Assets.Scripts.PathFinding;
using Assets.Scripts.TileMap.Data;
using Assets.Scripts.TileMap.Data.Decorators;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Actors
{
    public class PathFinderFollower : MonoBehaviour
    {
        [Inject]
        public PathFinder Pathfinder;

        [Inject]
        public GameMap Map;

        [Inject]
        public UnitDecorator TileDecorator;

        private PathFindingResult _currentpath;

        private float _distanceBeforeNext = 0.15f;

        private Vector3 _nextworldpos = default(Vector3);

        public float Speed = 1f;

        public int TargetX;
        public int TargetY;

        public int CurrentX;
        public int CurrentY;



        public void MoveTo(int x, int y, bool ignoreblocking = false)
        {
            SetupPathFinder();

            if (Map.Map[x, y].Blocked && !ignoreblocking)
            {
                _currentpath = null;
                return;
            }

            TargetX = x;
            TargetY = y;
               
            var position = CordUtil.WorldToTile(transform.position);
            var currentx = position.First;
            var currenty = position.Second;

            Pathfinder.ResultFound = r =>
            {
                _currentpath = r;

                if(_currentpath != null)
                    _nextworldpos = CordUtil.TileToWorld(_currentpath.X, _currentpath.Y);
            };

            _currentpath = null;
            Pathfinder.FindPath(currentx, currenty, x, y, this);
        }

        void FixedUpdate()
        {
            SetupPathFinder();

            if (_currentpath == null)
                return;

            if (Vector3.Distance(_nextworldpos, transform.position) <= _distanceBeforeNext)
            {
                if (_currentpath.Next == null)
                {
                    _currentpath = null;
                    return;
                }


                if (Map.Map[_currentpath.Next.X, _currentpath.Next.Y].Blocked)
                {
                    MoveTo(TargetX, TargetY);
                    return;
                }

                _currentpath = _currentpath.Next;

                SwapPosition(_currentpath.X, _currentpath.Y);

                if (_currentpath == null)
                    return;

                _nextworldpos = CordUtil.TileToWorld(_currentpath.X, _currentpath.Y);
            }

            transform.position = Vector3.MoveTowards(transform.position, _nextworldpos, Speed * Time.fixedDeltaTime);
        }

        public void SwapPosition(int x, int y)
        {
            Map.Map[CurrentX, CurrentY].Decorators.Remove(TileDecorator);
            Map.Map[x, y].Decorators.Add(TileDecorator);

            CurrentX = x;
            CurrentY = y;
        }

        private void SetupPathFinder()
        {
            if (Pathfinder.CheckNode != null)
                return;

            Pathfinder.CheckNode = (x, y) =>
            {
                if (Map == null)
                    return false;

                if (x < 0)
                    return false;
                if (x > Map.MapWidth - 1)
                    return false;
                if (y < 0)
                    return false;
                if (y > Map.MapHeight - 1)
                    return false;
                return !Map.Map[x, y].Blocked;
            };
        }
    }
}
