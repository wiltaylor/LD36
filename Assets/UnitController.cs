using Assets.Scripts.Actors;
using UnityEngine;
using Zenject;

public class UnitController : MonoBehaviour
{
    public int PlayerOwner;
    public float HP;
    public float MaxHP;

    [Inject]
    private UnitClickSignal.Trigger _clickTrigger;

    public GameObject SelectionBox;

    public PathFinderFollower PathFinderFollower;
    public void Start()
    {
        PathFinderFollower = GetComponent<PathFinderFollower>();
    }

    public void FixedUpdate()
    {
        if (HP <= 0)
        {
            //Unit dies
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
