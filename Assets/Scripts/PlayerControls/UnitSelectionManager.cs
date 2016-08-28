using System.Collections.Generic;
using Assets.Scripts.Actors;
using Assets.Scripts.InputHandler;
using Assets.Scripts.TileMap;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.PlayerControls
{
    public class UnitSelectionManager
    {
        private readonly List<UnitController> _selected = new List<UnitController>();

        [Inject]
        private Camera _camera;

        public UnitSelectionManager(UnitClickSignal unitClickSignal, TileClickSignal tileClickSignal, PlayerSessionModifiers modifiers, MouseReleaseSignal mouseRelease)
        {
            mouseRelease.Event += btn =>
            {
                if (btn == 0 && modifiers.Dragging)
                {
                    modifiers.Dragging = false;

                    foreach (var s in _selected)
                        s.Deselect();
                    _selected.Clear();

                    var startpoint = _camera.ScreenToWorldPoint(modifiers.DragStart);
                    var endpoint = _camera.ScreenToWorldPoint(modifiers.DragCurrent);
                    var size = new Vector2(endpoint.x - startpoint.x, endpoint.y - startpoint.y);
                    var mask = LayerMask.NameToLayer("Units");

                    Debug.Log($"[{startpoint.x}/{startpoint.y}] - [{endpoint.x}/{endpoint.y}]");

                    //var allunits = Physics2D.OverlapBoxAll(startpoint, size, 0f, mask, -10, 10);

                    var allunits = Physics2D.OverlapAreaAll(startpoint, endpoint);

                    foreach (var unit in allunits)
                    {

                        if(unit.tag != "Debug")
                            continue;
                        
                        var ctrl = unit.GetComponent<UnitController>();

                        ctrl.Select();
                        _selected.Add(ctrl);
                    }
                }
            };
            

            unitClickSignal.Event += (btn, ctrl) =>
            {
                if (btn == 0)
                {
                    if (!modifiers.MultiSelectDown)
                    {
                        foreach (var s in _selected)
                            s.Deselect();
                        _selected.Clear();
                    }
                    else
                    {
                        if (_selected.Contains(ctrl))
                        {
                            _selected.Remove(ctrl);
                            ctrl.Deselect();
                            return;
                        }
                    }

                    _selected.Add(ctrl);
                    ctrl.Select();
                }
                else
                {
                    _selected.Remove(ctrl);
                    ctrl.Deselect();
                }
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
                    foreach (var unit in _selected)
                        unit.PathFinderFollower.MoveTo(x, y);
                    
                }
            };
        }
    }
}
