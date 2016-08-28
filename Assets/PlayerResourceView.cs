using UnityEngine;
using System.Collections;
using Assets.Scripts.PlayerLogic;
using UnityEngine.UI;
using Zenject;

public class PlayerResourceView : MonoBehaviour
{

    [Inject] public PlayerManager _PlayerManager;

    public Text WoodText;
    public Text FoodText;
    public Text HomesText;

    void Update()
    {
        var player = _PlayerManager?.HumanPlayer;

        if (player == null)
            return;

        WoodText.text = $"{player.Wood}";
        FoodText.text = $"{player.Food}";
        HomesText.text = $"{player.Home}";
    }

}
