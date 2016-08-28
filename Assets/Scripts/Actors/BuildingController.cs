using UnityEngine;
using Assets.Scripts.Actors;
using Zenject;

public class BuildingController : MonoBehaviour
{
    public GameObject SelectionBox;
    public int PlayerOwner;
    public float HP;
    public float MaxHP;

    [Inject]
    private BuildingClickSignal.Trigger _clickTrigger;

    public void FixedUpdate()
    {
        if (HP <= 0)
        {
            //Building dies
            Destroy(gameObject);
        }
    }

    public void Select()
    {
        SelectionBox.SetActive(true);
    }

    public void Deselect()
    {
        SelectionBox.SetActive(false);
    }

    void OnMouseDown()
    {
        _clickTrigger.Fire(0, this);
    }

    void OnRightMouseDown()
    {
        _clickTrigger.Fire(1, this);
    }

}
