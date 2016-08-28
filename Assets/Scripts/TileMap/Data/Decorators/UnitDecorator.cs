namespace Assets.Scripts.TileMap.Data.Decorators
{
    public class UnitDecorator : ITileDecorator
    {
        public DecoratorType Type => DecoratorType.None;
        public DecalType DecalType => DecalType.None;
        public MinableResourceType ResourceType => MinableResourceType.None;
        public bool Block => true;
    }
}
