using Assets.Scripts.PlayerControls;
using Assets.Scripts.TileMap;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Actors
{
    public class BuildingPlacer: IInitializable
    {
        [Inject]
        private TileClickSignal _tileClick;

        [Inject] private PlayerSessionModifiers _sessionModifiers;

        [Inject] private BuildingFactory _buildingFactory;

        [Inject] private Camera _camera;

        [Inject] private PlayerSelectionManager _selectionManager;


        public void Initialize()
        {
            _tileClick.Event += (btn, x, y) =>
            {
                if (btn != 0)
                    return;

                if (!_sessionModifiers.PlacingBuilding)
                    return;

                var worldcords = _camera.ScreenToWorldPoint(Input.mousePosition);
                var tilecords = CordUtil.WorldToTile(worldcords);

                var obj = _buildingFactory.Create(_sessionModifiers.SelectedBuildingType, 0, tilecords.First, tilecords.Second);

                _selectionManager.PrimaryUnit.TargetBuilding = obj.GetComponent<BuildingController>();
                _selectionManager.PrimaryUnit.GetComponent<PathFinderFollower>().MoveTo(tilecords.First, tilecords.Second);

                _sessionModifiers.PlacingBuilding = false;
            };
        }
    }
}
