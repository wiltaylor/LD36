using System;
using Assets.Scripts.TileMap.Data;
using JetBrains.Annotations;

namespace Assets.Scripts.TileMap.LevelGenerator
{
    public interface IGenerator
    {
        void Generate(GameMap map);
    }
}
