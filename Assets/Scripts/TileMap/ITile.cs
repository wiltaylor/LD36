using UnityEngine;

namespace Assets.Scripts.TileMap
{
    public enum TileTypes
    {
        Grass,
        Sand,
        Dirt,
        Rocks,
        Water
    }

    public interface ITile
    {
        TileTypes TileType { get; }
        Sprite GetTileSprite();
    }
}
