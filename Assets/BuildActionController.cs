using UnityEngine;
using System.Collections;
using Assets.Scripts.PlayerControls;
using Assets.Scripts.PlayerLogic;
using UnityEngine.UI;
using Zenject;

public class BuildActionController : MonoBehaviour
{
    [Inject]
    private PlayerSessionModifiers _modifiers;

    [Inject]
    private PlayerManager _playerManager;

    public BuildingType Type;
    public int Wood;

    private Button _button;

    void Start()
    {
        _button = GetComponent<Button>();
    }

    void Update()
    {
        if (_playerManager.HumanPlayer == null)
            return;

        _button.interactable = _playerManager.HumanPlayer.Wood >= Wood;
    }

    public void Click()
    {
        _playerManager.HumanPlayer.Wood -= Wood;
        _modifiers.PlacingBuilding = true;
        _modifiers.SelectedBuildingType = Type;
    }
}
