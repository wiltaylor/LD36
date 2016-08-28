using System;
using System.Collections.Generic;
using Assets.Scripts.Actors;
using Assets.Scripts.InputHandler;
using Assets.Scripts.PlayerLogic;
using Assets.Scripts.TileMap;
using Assets.Scripts.TileMap.Data;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.PlayerControls
{
    public class PlayerSelectionManager
    {
        public readonly List<UnitController> SelectedUnits = new List<UnitController>();
        public BuildingController SelectedBuilding;
        public UnitController PrimaryUnit;


        [Inject]
        private Camera _camera;

        public PlayerSelectionManager(UnitClickSignal unitClickSignal, BuildingClickSignal buildingClickSignal, TileClickSignal tileClickSignal, PlayerSessionModifiers modifiers, MouseReleaseSignal mouseRelease, PlayerManager playerManager, GameMap map)
        {
            Action clearBuildings = () =>
            {
                if(SelectedBuilding != null)
                    SelectedBuilding.Deselect();

                SelectedBuilding = null;

            };

            Action clearSelection = () =>
            {
                foreach (var s in SelectedUnits)
                    s.Deselect();
                SelectedUnits.Clear();

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

                    var buildingcandidate = default(BuildingController);
                    var foundunit = false;

                    foreach (var unit in allunits)
                    {
                        if (unit.tag == "Building")
                            buildingcandidate = unit.GetComponent<BuildingController>();

                        if (unit.tag == "Unit")
                        {
                            var ctrl = unit.GetComponent<UnitController>();

                            if (ctrl.PlayerOwner != playerManager.HumanPlayer.ID)
                                continue;

                            foundunit = true;
                            ctrl.Select();
                            SelectedUnits.Add(ctrl);
                        }
                    }

                    if (!foundunit && buildingcandidate != null)
                        SelectedBuilding = buildingcandidate;
                }
            };
            

            unitClickSignal.Event += (btn, ctrl) =>
            {
                if (btn == 0)
                {
                    if (ctrl.PlayerOwner != playerManager.HumanPlayer.ID)
                        return;

                    if (!modifiers.MultiSelectDown)
                    {
                        clearSelection();
                    }
                    else
                    {
                        if (SelectedUnits.Contains(ctrl))
                        {
                            SelectedUnits.Remove(ctrl);
                            ctrl.Deselect();

                            if (PrimaryUnit == ctrl)
                                PrimaryUnit = null;

                            return;
                        }
                    }

                    clearBuildings();
                    SelectedUnits.Add(ctrl);
                    ctrl.Select();
                    PrimaryUnit = ctrl;
                }
                else
                {
                    if (PrimaryUnit != null && ctrl.PlayerOwner != 0)
                    {
                        if (PrimaryUnit.CanAttack)
                            PrimaryUnit.Attack(ctrl);
                        
                    }
                }
            };

            buildingClickSignal.Event += (btn, ctrl) =>
            {
                if (btn != 0) return;

                if (ctrl.PlayerOwner != playerManager.HumanPlayer.ID)
                    return;

                clearSelection();

                ctrl.Select();
                SelectedBuilding = ctrl;
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
                    foreach (var unit in SelectedUnits)
                    {
                        if (map.Map[x, y].Minable && unit.CanHarvest)
                        {
                            unit.Harvest(x, y);
                            return;
                        }

                        unit.GetComponent<PathFinderFollower>().MoveTo(x, y);
                        unit.TargeType = TragetType.None;
                    }
                        
                    
                }
            };
        }
    }
}
