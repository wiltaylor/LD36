using JetBrains.Annotations;

namespace Assets.Scripts.TileMap.Data
{
    public enum MinableResourceType
    {
        None,
        Food,
        Rock,
        Wood,
        Metal
    }

    public enum DecoratorType
    {
        None,
        Resource
    }

    public interface ITileDecorator
    {
        DecoratorType Type { get; }
        DecalType DecalType { get; }
        bool Block { get; }
    }
}
