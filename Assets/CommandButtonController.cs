using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Actions;
using Assets.Scripts.PlayerControls;
using Zenject;

public enum ViewMode
{
    None,
    Unit,
    UnitBuilding,
    Building
}

public class CommandButtonController : MonoBehaviour
{
    [Inject]
    private PlayerSelectionManager _selectionManager;

    [Inject]
    private ActionManager _actionManager;


    [Inject] private DiContainer _container;

    private List<GameObject> _ActionList = new List<GameObject>();

    private string[] _currentActionList;

    public ViewMode _currentViewMode = ViewMode.None;
    
    void Update()
    {
        switch (_currentViewMode)
        {
            case ViewMode.None:
                UpdateNone();
                break;
            case ViewMode.Unit:
                UpdateUnit();
                break;
            case ViewMode.Building:
                UpdateBuilding();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void UpdateBuilding()
    {
        if (_selectionManager.SelectedBuilding == null)
        {
            _currentViewMode = ViewMode.None;
            ClearPrimary();
        }
    }

    private void UpdateUnit()
    {
        if (_selectionManager?.SelectedUnits?.Count == 0)
        {
            _currentViewMode = ViewMode.None;
            ClearPrimary();
        }
    }

    private void UpdateNone()
    {
        if (_selectionManager == null)
            return;

        if (_selectionManager?.SelectedUnits?.Count == 0 && _selectionManager.SelectedBuilding == null)
            return;

        if (_selectionManager.SelectedBuilding != null)
        {
            _currentViewMode = ViewMode.Building;
            if(_selectionManager.SelectedBuilding.ConstructionTimeLeft <= 0f)
                LoadPrimaryActionList(_selectionManager.SelectedBuilding.PrimaryActionList);
            return;
        }

        if (_selectionManager.PrimaryUnit == null)
            _selectionManager.PrimaryUnit = _selectionManager.SelectedUnits.FirstOrDefault();

        if (_selectionManager.PrimaryUnit != null)
        {
            LoadPrimaryActionList(_selectionManager.PrimaryUnit.PrimaryActionList);
            _currentViewMode = ViewMode.Unit;
        }

    }

    private void LoadPrimaryActionList(string[] names)
    {
        var index = 0;

        ClearPrimary();
        foreach (var n in names)
        {
            var x = (64f/2f) + (index + 1f)*15f + (index*64f);
            var y = 150;
            index++;
            var prefab = _actionManager.GetAction(n);
            var obj = _container.InstantiatePrefab(prefab);
            var rect = obj.GetComponent<RectTransform>();
            obj.transform.SetParent(transform);
            obj.SetActive(true);
            rect.position = new Vector3(x, y);
            
            _ActionList.Add(obj);
        }
    }

    private void ClearPrimary()
    {
        foreach(var a in _ActionList)
            Destroy(a);

        _ActionList.Clear();
    }
}
