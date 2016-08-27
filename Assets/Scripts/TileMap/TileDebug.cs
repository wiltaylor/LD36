using UnityEngine;
using Zenject;

namespace Assets.Scripts.TileMap
{
    public class TileDebug
    {
        private TileBlockController _blockController;

        [Inject]
        public void Construct(TileMap map, TileClickSignal clicksignal)
        {

            clicksignal.Event += (x, y) =>
            {
                map.SetTile(x, y, TileTypes.Grass);
                map.Apply();
            };

            for (int x = 0; x < 128; x++)
            {
                for (int y = 0; y < 128; y++)
                {
                    map.SetTile(x,y, TileTypes.Dirt);
                }
            }

            map.Apply();
               
        }
    }
}
