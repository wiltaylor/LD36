using Assets.Scripts.Actors;
using Assets.Scripts.PlayerLogic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.AI
{
    public class UnitBuilderAI : MonoBehaviour
    {
        public float Cooldown = 30f;
        public UnitType Type;

        private float _current;
        private int _x;
        private int _y;

        [Inject] private UnitFactory _unitFactory;
        [Inject] private PlayerManager _playerManager;

        private BuildingController _controller;

        void Start()
        {
            _current = Cooldown;

            var pos = CordUtil.WorldToTile(transform.position);
            _x = pos.First;
            _y = pos.Second - 2;

            if (_y < 0)
                _y = pos.Second + 2;

            _controller = GetComponent<BuildingController>();
        }

        void FixedUpdate()
        {
            _current -= Time.fixedDeltaTime;

            if (_current <= 0f)
            {
                var unit = _unitFactory.Create(Type, _controller.PlayerOwner, _x, _y);
                var pathfinder = unit.GetComponent<PathFinderFollower>();

                try
                {
                    pathfinder.MoveTo(_playerManager.HumanPlayer.StartSpawnX, _playerManager.HumanPlayer.StartSpawnY, true);
                    //pathfinder.MoveTo(0, 0, true);
                }
                catch {  /* ignore */}

                _current = Cooldown;
            }
        }
    }
}
