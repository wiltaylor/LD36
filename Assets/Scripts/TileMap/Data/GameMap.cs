using System;
using System.Security.Cryptography.X509Certificates;
using Assets.Scripts.TileMap.LevelGenerator;
using ModestTree.Util;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.TileMap.Data
{
    public class GameMap : IInitializable
    {
        public TileInfo[,] Map;

        [Inject(Id = "MapWidth")]
        public int MapWidth;

        [Inject(Id = "MapHeight")]
        public int MapHeight;

        [Inject]
        private WorldGenerator _generator;

        [Inject]
        private TileUpdateCommand _updateTile;

        [Inject] private DecalUpdateCommand _updateDecal;

        [Inject] private ApplyTileMapCommand _applyCommand;
        
        public void Initialize()
        {
            Map = new TileInfo[MapWidth,MapHeight];

            _generator.GenerateMap(this);

            for(var x = 0; x < MapWidth; x++)
                for (var y = 0; y < MapHeight; y++)
                    RebuildTile(x, y);

            _applyCommand.Execute();
        }

        public void RebuildTile(int x, int y)
        {
            var tile = Map[x, y];
            
            _updateTile.Execute(x,y, tile.Type);

            foreach (var d in tile.Decorators)
            {
                if(d.DecalType == DecalType.None)
                    continue;

                _updateDecal.Execute(x,y, d.DecalType);
            }
        }

        public void Apply()
        {
            _applyCommand.Execute();
        }

        public Tuple<int, int> FindNextFreeTile(int x, int y)
        {
            int ringlevel = 1;

            Func<int, int, bool> check = (cx, cy) =>
            {
                if (cx < 0)
                    return false;
                if(cx > MapWidth)
                    return false;
                if (cy < 0)
                    return false;
                if (cy > MapHeight)
                    return false;

                return !Map[cx, cy].Blocked;

            };

            while (ringlevel < 128)
            {
                if(check(x + ringlevel, y))
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

            throw new UnityException("Failed to find free block!");
        }
        
    }
}
