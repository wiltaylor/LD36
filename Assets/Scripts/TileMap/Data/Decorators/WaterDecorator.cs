namespace Assets.Scripts.TileMap.Data.Decorators
{
    public class WaterDecorator : ITileDecorator
    {
        public DecoratorType Type => DecoratorType.None;
        public DecalType DecalType => DecalType.None;
        public bool Block => true;
    }
}
