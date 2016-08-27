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
                map.SetDecal(x, y, DecalType.Tree);
                map.Apply();
                    

            };

            for (int x = 0; x < 1024; x++)
            {
                for (int y = 0; y < 1024; y++)
                {
                    map.SetTile(x,y, TileTypes.Dirt);
                    if(Random.Range(0,3) == 1)
                        map.SetDecal(x, y, DecalType.Tree);
                }
            }

            map.Apply();
               
        }
    }
}
