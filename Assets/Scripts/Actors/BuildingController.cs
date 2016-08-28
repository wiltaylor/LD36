using UnityEngine;
using Assets.Scripts.Actors;
using Assets.Scripts.TileMap.Data;
using Zenject;

public enum BuildingType
{
    TownCenter,
    Farm,
    Barracks
}

public enum QueueType
{
    None,
    Unit
}

public class BuildingController : MonoBehaviour
{
    public GameObject SelectionBox;
    public int PlayerOwner;
    public float HP;
    public float MaxHP;
    public string[] PrimaryActionList;
    public QueueType CurrentQueuedItem;
    public UnitType QueuedUnit;
    public float QueueTimeLeft;
    public float ConstructionTimeLeft;
    public UnitController ConstructingUnit;

    public Sprite CompletedSprite;

    [Inject]
    private BuildingClickSignal.Trigger _clickTrigger;

    [Inject] private UnitFactory _unitfactory;

    [Inject] private GameMap _map;

    public void FixedUpdate()
    {
        if (ConstructingUnit != null)
        {
            ConstructionTimeLeft -= Time.deltaTime;

            if (ConstructionTimeLeft <= 0f)
            {
                ConstructingUnit.gameObject.SetActive(true);
                ConstructingUnit = null;

                GetComponent<SpriteRenderer>().sprite = CompletedSprite;
            }
        }

        if (QueueTimeLeft > 0)
            QueueTimeLeft -= Time.fixedDeltaTime;
        else if (CurrentQueuedItem != QueueType.None)
        {
            var coords = CordUtil.WorldToTile(transform.position);
            var freespot = _map.FindNextFreeTile(coords.First, coords.Second);
            _unitfactory.Create(QueuedUnit, PlayerOwner, freespot.First, freespot.Second - 2);
            CurrentQueuedItem = QueueType.None;
        }

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

    public void StartBuilding(UnitController unit)
    {
        ConstructingUnit = unit;
        unit.gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Unit")
        {
            other.GetComponent<UnitController>().ReachedBuilding(this);
        }
    }

}
