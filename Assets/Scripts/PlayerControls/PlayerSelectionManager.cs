using System;
using System.Collections.Generic;
using Assets.Scripts.Actors;
using Assets.Scripts.InputHandler;
using Assets.Scripts.TileMap;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.PlayerControls
{
    public class PlayerSelectionManager
    {
        private readonly List<UnitController> _selectedUnits = new List<UnitController>();
        private BuildingController _selectedBuilding;

        [Inject]
        private Camera _camera;

        public PlayerSelectionManager(UnitClickSignal unitClickSignal, BuildingClickSignal buildingClickSignal, TileClickSignal tileClickSignal, PlayerSessionModifiers modifiers, MouseReleaseSignal mouseRelease)
        {

            Action clearBuildings = () =>
            {
                if(_selectedBuilding != null)
                    _selectedBuilding.Deselect();

                _selectedBuilding = null;

            };

            Action clearSelection = () =>
            {
                foreach (var s in _selectedUnits)
                    s.Deselect();
                _selectedUnits.Clear();

                clearBuildings();
            };

            mouseRelease.Event += btn =>
            {
                if (btn == 0 && modifiers.Dragging)
                {
                    modifiers.Dragging = false;

                    clearSelection();

                    var startpoint = _camera.ScreenToWorldPoint(modifiers.DragStart);
                    var endpoint = _camera.ScreenToWorldPoint(modifiers.DragCurrent);

                    var allunits = Physics2D.OverlapAreaAll(startpoint, endpoint);

                    foreach (var unit in allunits)
                    {

                        if(unit.tag != "Debug")
                            continue;
                        
                        var ctrl = unit.GetComponent<UnitController>();

                        ctrl.Select();
                        _selectedUnits.Add(ctrl);
                    }
                }
            };
            

            unitClickSignal.Event += (btn, ctrl) =>
            {
                if (btn == 0)
                {
                    if (!modifiers.MultiSelectDown)
                    {
                        clearSelection();
                    }
                    else
                    {
                        if (_selectedUnits.Contains(ctrl))
                        {
                            _selectedUnits.Remove(ctrl);
                            ctrl.Deselect();
                            return;
                        }
                    }

                    clearBuildings();
                    _selectedUnits.Add(ctrl);
                    ctrl.Select();
                }
                else
                {
                    _selectedUnits.Remove(ctrl);
                    ctrl.Deselect();
                }
            };

            buildingClickSignal.Event += (btn, ctrl) =>
            {
                if (btn != 0) return;

                clearSelection();

                ctrl.Select();
                _selectedBuilding = ctrl;
            };

            tileClickSignal.Event += (btn, x, y) =>
            {
                if (btn == 0 && !modifiers.WorkingWithAction)
                {
                    modifiers.DragStart = Input.mousePosition;
                    modifiers.Dragging = true;
                }

                if (btn == 1)
                {
                    foreach (var unit in _selectedUnits)
                        unit.PathFinderFollower.MoveTo(x, y);
                    
                }
            };
        }
    }
}
