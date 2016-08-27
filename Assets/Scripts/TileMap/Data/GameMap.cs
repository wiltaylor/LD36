using Assets.Scripts.TileMap.LevelGenerator;
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


    }
}
