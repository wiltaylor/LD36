using System;
using System.Linq;
using System.Xml.Linq;
using Assets.Scripts.PlayerLogic;
using Assets.Scripts.TileMap;
using Assets.Scripts.TileMap.Data;
using ModestTree.Util;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Actors
{
    public enum TragetType
    {
        Construct,
        Harvest,
        Combat,
        None
    }

    public class UnitController : MonoBehaviour
    {
        public int PlayerOwner;
        public float HP;
        public float MaxHP;
        public bool CanHarvest;
        public bool CanAttack;
        public bool CanBuild;
        public string[] PrimaryActionList;
        public string[] BuildActionList;
        public BuildingController TargetBuilding;
        public TragetType TargeType;
        public UnitController CombatUnitTarget;
        public float CombatDamage = 0.64f;
        public float CombatDistance = 1f;
        public float CombatCooldown = 2f;
        public int HarvestTargetX;
        public int HarvestTargetY;
        public float HarvestHP = 10f;

        private int _combatTargetX;
        private int _combatTargetY;
        private float _pathFindCheckCount = 10f;

        private float _currentcooldown;
        [Inject]
        public UnitClickSignal.Trigger ClickTrigger;

        public GameObject SelectionBox;

        private PathFinderFollower _pathFinder;

        [Inject] private GameMap _map;

        [Inject] private PlayerManager _playerManager;

        private void Start()
        {
            _pathFinder = GetComponent<PathFinderFollower>();
        }

        public void FixedUpdate()
        {
            if (_currentcooldown > 0f)
                _currentcooldown -= Time.fixedDeltaTime;

            

            if (TargeType == TragetType.Harvest)
            {
                if (HarvestHP <= 0f)
                {
                    TargeType = TragetType.None;
                    var player = _playerManager.Players.First(p => p.ID == PlayerOwner);

                    switch (_map.Map[HarvestTargetX, HarvestTargetY].ResourceType)
                    {
                        case MinableResourceType.None:
                            break;
                        case MinableResourceType.Food:
                            player.Food += 10;
                            break;
                        case MinableResourceType.Rock:
                            player.Stone += 10;
                            break;
                        case MinableResourceType.Wood:
                            player.Wood += 10;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    _map.Map[HarvestTargetX, HarvestTargetY].Decorators.Clear();
                    _map.Map[HarvestTargetX, HarvestTargetY].Type = TileTypes.Dirt;
                    _map.RebuildTile(HarvestTargetX, HarvestTargetY);
                    _map.Apply();

                    HarvestHP = 10f;
                }
                else
                {
                    var playerloc = CordUtil.WorldToTile(transform.position);
                    var xdiff = playerloc.First - HarvestTargetX;
                    var ydiff = playerloc.Second - HarvestTargetY;

                    if (xdiff == -1)
                        xdiff = 1;
                    if (ydiff == -1)
                        ydiff = 1;

                    if (xdiff <= 1 && ydiff <= 1)
                    {
                        HarvestHP -= Time.fixedDeltaTime;
                    }
                }
            }

            if (CombatUnitTarget != null && _currentcooldown <= 0f)
            {
                var loc = CordUtil.WorldToTile(CombatUnitTarget.transform.position);

                if (loc.First != _combatTargetX || loc.Second != _combatTargetY)
                    Attack(CombatUnitTarget);


                if (Vector3.Distance(transform.position, CombatUnitTarget.transform.position) < CombatDistance)
                {
                    CombatUnitTarget.Hit(CombatDamage);
                    _currentcooldown = CombatCooldown;
                }
            }

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
            ClickTrigger.Fire(0, this);
        }

        void OnRightMouseDown()
        {
            ClickTrigger.Fire(1, this);
        }

        public void ReachedBuilding(BuildingController building)
        {
            if (building == TargetBuilding)
            {
                TargetBuilding.StartBuilding(this);
                TargetBuilding = null;
            }
        }

        public void Hit(float dmg)
        {
            HP -= dmg;
        }

        public void Attack(UnitController unit)
        {
            TargeType = TragetType.Combat;
            CombatUnitTarget = unit;

            var location = CordUtil.WorldToTile(unit.transform.position);
            _pathFinder.MoveTo(location.First, location.Second, true);
            _combatTargetX = location.First;
            _combatTargetY = location.Second;
            _pathFindCheckCount = 10f;

        }

        public void Harvest(int x, int y)
        {
            TargeType = TragetType.Harvest;
            _pathFinder.MoveTo(x, y, true);
            HarvestTargetX = x;
            HarvestTargetY = y;
        }
    }
}
