using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Assets.Scripts.TileMap.Blocks
{
    [Serializable]
    public class Dirt : ITile
    {
        public TileTypes TileType => TileTypes.Dirt;

        [Inject]
        public Sprite[] Tiles;

        public Sprite GetTileSprite()
        {
            return Tiles[Random.Range(0, Tiles.Length - 1)];
        }
    }
}
