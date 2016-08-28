using System.Linq;
using Assets.Scripts.TileMap.Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.TileMap.LevelGenerator
{
    public class SpawnGenerator : IGenerator
    {
        public int Players;
        public TileTypes Tile;
        public TileTypes[] BannedTypes;
        public int Range;
        public int Food;
        public int Wood;

        public WorldGenerator WorldGenerator { get; set; }

        public void Generate(GameMap map)
        {
            var playersleft = Players;

            while (playersleft > 0)
            {
                var sx = Random.Range(0, map.MapWidth);
                var sy = Random.Range(0, map.MapHeight);
                var ex = sx + Range;
                var ey = sy + Range;

                if (ex > map.MapWidth)
                    continue;
                if (ey > map.MapHeight)
                    continue;
                var banned = false;

                for (var x = sx; x < ex; x++)
                    for (var y = sy; y < ey; y++)
                    {
                        if(BannedTypes.Any(b => b == map.Map[x,y].Type))
                            banned = true;
                    }

                if(banned)
                    continue;

                var toclose = false;

                foreach(var p in WorldGenerator.PlayerManager.Players)
                    if(Vector2.Distance(new Vector2(p.StartSpawnX, p.StartSpawnY), new Vector2(sx, sy)) < 20)
                        toclose = true;

                if(toclose)
                    continue;
                

                for (var x = sx; x < ex; x++)
                    for (var y = sy; y < ey; y++)
                    {
                        map.Map[x, y].Type = Tile;
                        map.Map[x, y].Decorators.Clear();
                    }

                var player = WorldGenerator.PlayerManager.AddNullPlayer(Food,Wood,0);
                player.StartSpawnX = sx;
                player.StartSpawnY = sy;
                player.EndSpawnX = ex;
                player.EndSpawnY = ey;

                playersleft--;
            }

        }
    }
}
