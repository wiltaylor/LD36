using System;
using Assets.Scripts.AI;
using Assets.Scripts.TileMap.Data;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Actors
{
    public enum UnitType
    {
        Worker,
        Warrior
    }

    public class UnitFactory
    {
        [Inject]
        protected readonly DiContainer Container;
       
        [Inject(Id = "TeamBodies")]
        private Sprite[] _teamBodies;

        [Inject(Id = "UnitHeads")]
        private Sprite[] _heads;

        [Inject(Id = "SelectionSprite")]
        private Sprite _selectionSprite;

        [Inject(Id = "SpearSprite")] private Sprite _spearSprite;

        [Inject] private GameMap _map;

        public GameObject Create(UnitType type, int team, int x, int y)
        {
            var unit = Container.CreateEmptyGameObject("Unit");
            var head = Container.CreateEmptyGameObject("Head");
            var body = Container.CreateEmptyGameObject("Body");
            var selectionBox = Container.CreateEmptyGameObject("Selection");
            
            unit.tag = "Unit";

            head.transform.parent = unit.transform;
            body.transform.parent = unit.transform;
            selectionBox.transform.parent = unit.transform;

            head.transform.position = new Vector3(0, 0.148f, 0);

            var col = unit.AddComponent<BoxCollider2D>();
            col.offset = new Vector2(-0.0025f, 0.0126f);
            col.size = new Vector2(0.2365f, 0.5196f);
            col.isTrigger = true;
            
            var controller = Container.InstantiateComponent<UnitController>(unit);
            var pathfinder = Container.InstantiateComponent<PathFinderFollower>(unit);

            controller.SelectionBox = selectionBox;
            controller.PlayerOwner = team;
            controller.HP = 1;
            controller.MaxHP = 1;
            controller.PrimaryActionList = GetPrimaryActions(type);

            if (team != 0 && type == UnitType.Worker)
                Container.InstantiateComponent<HarvestAIController>(unit);

            if (type == UnitType.Warrior)
            {
                controller.CanAttack = true;
                var attacksight = Container.CreateEmptyGameObject("Sight");
                attacksight.transform.SetParent(unit.transform);
                var sightcol = Container.InstantiateComponent<CircleCollider2D>(attacksight);
                Container.InstantiateComponent<AttackSight>(attacksight);
                sightcol.radius = 2.5f;
                sightcol.isTrigger = true;
                attacksight.transform.position = new Vector3(attacksight.transform.position.x, attacksight.transform.position.y, 5);

                var spear = Container.CreateEmptyGameObject("Spear");
                spear.transform.SetParent(unit.transform);
                var spearrender = Container.InstantiateComponent<SpriteRenderer>(spear);
                spearrender.sortingOrder = 5;
                spearrender.sprite = _spearSprite;
                spearrender.transform.position = new Vector3(0.06f, 0, 0);
            }

            if (type == UnitType.Worker)
                controller.CanHarvest = true;

            var headsprite = Container.InstantiateComponent<SpriteRenderer>(head);
            var bodysprite = Container.InstantiateComponent<SpriteRenderer>(body);
            var selectionsprite = Container.InstantiateComponent<SpriteRenderer>(selectionBox);
            var rbody = Container.InstantiateComponent<Rigidbody2D>(unit);
            rbody.gravityScale = 0f;
            rbody.freezeRotation = true;
            rbody.isKinematic = true;

            selectionsprite.sortingOrder = 3;
            headsprite.sortingOrder = 2;
            bodysprite.sortingOrder = 1;
            headsprite.sortingLayerName = "Units";
            bodysprite.sortingLayerName = "Units";
            selectionsprite.sortingLayerName = "Units";

            selectionsprite.sprite = _selectionSprite;
            bodysprite.sprite = _teamBodies[team];
            headsprite.sprite = _heads[0];

            selectionsprite.gameObject.SetActive(false);
            head.gameObject.SetActive(true);
            body.gameObject.SetActive(true);
            unit.gameObject.SetActive(true);

            unit.transform.position = CordUtil.TileToWorld(x, y);

            pathfinder.CurrentX = x;
            pathfinder.CurrentY = y;
            pathfinder.SwapPosition(x, y);

            return unit;
        }

        private string[] GetPrimaryActions(UnitType type)
        {
            switch (type)
            {
                case UnitType.Worker:
                    return new[]
                    {
                        "BuildTownCentre",
                        "BuildFarm",
                        "BuildBarracks"
                    };
                case UnitType.Warrior:
                    return new string[] {};
                default:
                    throw new ArgumentOutOfRangeException("type", type, null);
            }
        }
    }
}
