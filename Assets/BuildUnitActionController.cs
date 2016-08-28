using UnityEngine;
using System.Collections;
using Assets.Scripts.Actors;
using Assets.Scripts.PlayerControls;
using Assets.Scripts.PlayerLogic;
using UnityEngine.UI;
using Zenject;

public class BuildUnitActionController : MonoBehaviour
{

    [Inject] private PlayerSelectionManager _selectionManager;

    [Inject] private PlayerManager _playerManager;

    public UnitType Type;
    public Text Text;
    public int Wood;
    public int Food;

    private Button _button;

    void Start()
    {
        _button = GetComponent<Button>();
    }

    void Update()
    {
        if (_selectionManager.SelectedBuilding == null)
            return;

        if (_playerManager.HumanPlayer == null)
            return;

        if (_playerManager.HumanPlayer.Wood < Wood || _playerManager.HumanPlayer.Food < Food)
            _button.interactable = false;
        else
            _button.interactable = true;

        _button.interactable = _selectionManager.SelectedBuilding.CurrentQueuedItem == QueueType.None;

        if (_selectionManager.SelectedBuilding.CurrentQueuedItem == QueueType.Unit &&
            _selectionManager.SelectedBuilding.QueuedUnit == Type)
        {
            Text.text = ((int) _selectionManager.SelectedBuilding.QueueTimeLeft).ToString();
        }
        else
        {
            Text.text = "";
        }
        
    }

	public void Click()
	{
	    _playerManager.HumanPlayer.Wood -= Wood;
        _playerManager.HumanPlayer.Food -= Food;


        _selectionManager.SelectedBuilding.QueueTimeLeft = 10f;
        _selectionManager.SelectedBuilding.CurrentQueuedItem = QueueType.Unit;
        _selectionManager.SelectedBuilding.QueuedUnit = Type;
        
	}
}
