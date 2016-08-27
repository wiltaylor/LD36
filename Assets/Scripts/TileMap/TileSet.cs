using System;
using UnityEngine;
using Zenject;

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

    public enum DecalType
    {
        None,
        Tree
    }

    [Serializable]
    public class TileSet
    {
        [Inject(Id = TileTypes.Grass)]private Sprite[] _grass;
        [Inject(Id = TileTypes.Dirt)]private Sprite[] _dirt;
        [Inject(Id = TileTypes.Sand)]private Sprite[] _sand;
        [Inject(Id = TileTypes.Rocks)]private Sprite[] _rocks;
        [Inject(Id = TileTypes.Water)]private Sprite[] _water;
        [Inject(Id = DecalType.Tree)] private Sprite[] _tree;

        public Sprite GetTile(TileTypes type)
        {
            switch (type)
            {
                case TileTypes.Grass:
                    return _grass[0];
                case TileTypes.Sand:
                    return _sand[0];
                case TileTypes.Dirt:
                    return _dirt[0];
                case TileTypes.Rocks:
                    return _rocks[0];
                case TileTypes.Water:
                    return _water[0];
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public Sprite GetDecal(DecalType type)
        {
            switch (type)
            {
                case DecalType.Tree:
                    return _tree[0];
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}
