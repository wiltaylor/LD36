using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.TileMap
{
    [Serializable]
    public class TileSet
    {
        [Inject]
        public List<ITile> Tiles;

        public Sprite GetTile(TileTypes type)
        {
            return Tiles.First(t => t.TileType == type).GetTileSprite();
        }
    }
}
