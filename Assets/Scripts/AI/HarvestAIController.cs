using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using Assets.Scripts.Actors;
using Assets.Scripts.TileMap.Data;
using ModestTree.Util;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.AI
{
    public class HarvestAIController : MonoBehaviour
    {

        private UnitController _unit;

        [Inject] private GameMap _map;

        void Start()
        {
            _unit = GetComponent<UnitController>();
        }

        void FixedUpdate()
        {
            if (_unit.TargeType != TragetType.Harvest)
            {
                var pos = CordUtil.WorldToTile(transform.position);
                var tree = FindNextTree(pos.First, pos.Second);

                if (tree == null)
                {
                    enabled = false;
                    return;
                }

                _unit.Harvest(tree.First, tree.Second);
            }
        }

        public Tuple<int, int> FindNextTree(int x, int y)
        {
            int ringlevel = 1;

            Func<int, int, bool> check = (cx, cy) =>
            {
                if (cx < 0)
                    return false;
                if (cx >= _map.MapWidth)
                    return false;
                if (cy < 0)
                    return false;
                if (cy >= _map.MapHeight)
                    return false;

                return _map.Map[cx, cy].Minable;

            };

            while (ringlevel < 128)
            {
                if (check(x + ringlevel, y))
                    return new Tuple<int, int>(x + ringlevel, y);
                if (check(x - ringlevel, y))
                    return new Tuple<int, int>(x - ringlevel, y);
                if (check(x, y + ringlevel))
                    return new Tuple<int, int>(x, y + ringlevel);
                if (check(x, y - ringlevel))
                    return new Tuple<int, int>(x, y - ringlevel);
                if (check(x + ringlevel, y + ringlevel))
                    return new Tuple<int, int>(x + ringlevel, y + ringlevel);
                if (check(x - ringlevel, y - ringlevel))
                    return new Tuple<int, int>(x - ringlevel, y - ringlevel);
                if (check(x + ringlevel, y - ringlevel))
                    return new Tuple<int, int>(x + ringlevel, y - ringlevel);
                if (check(x - ringlevel, y + ringlevel))
                    return new Tuple<int, int>(x - ringlevel, y + ringlevel);

                ringlevel++;
            }

            return null;
        }
    }
}
