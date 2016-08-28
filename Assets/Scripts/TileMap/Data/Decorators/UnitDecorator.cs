namespace Assets.Scripts.TileMap.Data.Decorators
{
    class UnitDecorator : ITileDecorator
    {
        public DecoratorType Type => DecoratorType.None;
        public DecalType DecalType => DecalType.None;
        public bool Block => true;
    }
}
