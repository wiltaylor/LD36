using Assets.Scripts.Actors;
using Assets.Scripts.PlayerLogic;
using Assets.Scripts.TileMap.Data;

namespace Assets.Scripts.TileMap.LevelGenerator
{
    public class PlayerStartGenerator : IGenerator
    {
        public WorldGenerator WorldGenerator { get; set; }
        public void Generate(GameMap map)
        {
            WorldGenerator.PlayerManager.Players[0].Type = PlayerType.Human;

            foreach (var player in WorldGenerator.PlayerManager.Players)
            {
                WorldGenerator.UnitFactory.Create(UnitType.Worker, player.ID, player.StartSpawnX, player.StartSpawnY);

                if (player.ID != 0)
                {
                    WorldGenerator.BuildingFactory.Create(BuildingType.TownCenter, player.ID, player.StartSpawnX + 2,
                        player.StartSpawnY + 3);

                    WorldGenerator.BuildingFactory.Create(BuildingType.Barracks, player.ID, player.StartSpawnX + 2,
                        player.StartSpawnY + 7);

                    WorldGenerator.BuildingFactory.Create(BuildingType.Farm, player.ID, player.StartSpawnX + 5,
                        player.StartSpawnY + 3);

                    WorldGenerator.BuildingFactory.Create(BuildingType.Farm, player.ID, player.StartSpawnX + 5,
                        player.StartSpawnY + 7);

                    WorldGenerator.BuildingFactory.Create(BuildingType.Farm, player.ID, player.StartSpawnX + 8,
                        player.StartSpawnY + 3);
                }
                else
                {
                    WorldGenerator.CameraMover.MoveToTile(player.StartSpawnX, player.StartSpawnY);
                }
            }
        }
    }
}
