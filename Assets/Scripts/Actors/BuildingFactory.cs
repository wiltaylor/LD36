using System;
using Assets.Scripts.AI;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Actors
{
    public class BuildingFactory
    {
        [Inject] protected DiContainer Container;

        [Inject(Id = "CityCenterSprite")] private Sprite _cityCenter;
        [Inject(Id = "FarmSprite")] private Sprite _farm;
        [Inject(Id = "BarracksSprite")] private Sprite _barracks;
        [Inject(Id = "SelectionSprite")] private Sprite _SelectionSprite;
        [Inject(Id = "BuildingSprite")] private Sprite _buildingSprite;
        
        public GameObject Create(BuildingType type, int team, int x, int y)
        {
            var building = Container.CreateEmptyGameObject("Building");
            var selection = Container.CreateEmptyGameObject("SelectionBox");
            selection.transform.SetParent(building.transform);
            selection.transform.localScale = new Vector3(2,2,1);

            building.tag = "Building";


            var render = Container.InstantiateComponent<SpriteRenderer>(building);


            render.sprite = _buildingSprite;

            render.sortingOrder = 1;
            var collider = Container.InstantiateComponent<BoxCollider2D>(building);
            var controller = Container.InstantiateComponent<BuildingController>(building);
            var selectrender = Container.InstantiateComponent<SpriteRenderer>(selection);
            selectrender.sprite = _SelectionSprite;
            collider.isTrigger = true;

            building.transform.position = CordUtil.TileToWorld(x, y);

            if (team != 0 && type != BuildingType.Farm)
            {
                var unitbuilder = Container.InstantiateComponent<UnitBuilderAI>(building);
                unitbuilder.Cooldown = 30f;
                unitbuilder.Type = type == BuildingType.TownCenter ? UnitType.Worker : UnitType.Warrior;
            }

            controller.PrimaryActionList = GetPrimaryActionList(type);
            controller.HP = 1;
            controller.MaxHP = 1;
            controller.PlayerOwner = team;
            controller.SelectionBox = selection;
            controller.ConstructionTimeLeft = 10f;
            controller.CompletedSprite = type == BuildingType.TownCenter ? _cityCenter :
                            type == BuildingType.Farm ? _farm
                                                      : _barracks;
            if (team != 0)
            {
                render.sprite = controller.CompletedSprite;
            }


            building.SetActive(true);
            selection.SetActive(false);


            return building;
        }

        private string[] GetPrimaryActionList(BuildingType type)
        {
            switch (type)
            {
                case BuildingType.TownCenter:
                    return new[] { "BuildWorker" };
                case BuildingType.Farm:
                    return new string[] { };
                case BuildingType.Barracks:
                    return new string[] { "BuildWarrior"};
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}
