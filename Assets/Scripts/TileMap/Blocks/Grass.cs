using System;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Assets.Scripts.TileMap.Blocks
{
    [Serializable]
    public class Grass : ITile
    {
        public TileTypes TileType => TileTypes.Grass;

        [Inject]
        public Sprite[] Tiles;

        public Sprite GetTileSprite()
        {
            return Tiles[Random.Range(0, Tiles.Length - 1)];
        }
    }
}
