using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.TileMap
{
    public class TileMap
    {
        [Inject(Id = "MapWidth")]public int MapWidth;
        [Inject(Id = "MapHeight")]public int MapHeight;
        [Inject(Id = "TilesPerBlock")] public int TilesPerBlock;

        private List<TileBlockController> _blocks = new List<TileBlockController>();
        
        [Inject]
        public void Construct(TileBlockController.Factory factory)
        {
            var xTile = MapWidth - TilesPerBlock;
            var yTile = MapHeight - TilesPerBlock;

            while (yTile >= 0)
            {

                var currentblock = factory.Create();
                currentblock.BottomTileY = yTile;
                currentblock.LeftTileX = xTile;
                currentblock.transform.position = new Vector3((xTile * TilesPerBlock) * 0.01f, (yTile * TilesPerBlock) * 0.01f);

                xTile -= TilesPerBlock;
                _blocks.Add(currentblock);

                if (xTile >= 0) continue;

                xTile = MapWidth - TilesPerBlock;
                yTile -= TilesPerBlock;

                
            }
        }

        public void SetTile(int x, int y, TileTypes type)
        {
            foreach(var b in _blocks)
                b.SetTile(x,y,type);
        }

        public void Apply()
        {
            foreach (var b in _blocks)
                b.Apply();
        }

    }
}
