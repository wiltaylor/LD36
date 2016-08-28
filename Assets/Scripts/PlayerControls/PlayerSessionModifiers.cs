using UnityEngine;

namespace Assets.Scripts.PlayerControls
{
    public class PlayerSessionModifiers
    {
        public bool WorkingWithAction;
        public bool MultiSelectDown;
        public Vector2 DragStart;
        public Vector2 DragCurrent;
        public bool Dragging;
        public bool PlacingBuilding;
        public int BuildingWidth;
        public int BuildingHeight;
        public BuildingType SelectedBuildingType;
    }
}
