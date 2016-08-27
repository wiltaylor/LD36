namespace Assets.Scripts.TileMap.Data
{
    public class TreeDecorator : ITileDecorator
    {
        public DecoratorType Type => DecoratorType.Resource;
        public DecalType DecalType => DecalType.Tree;
        public bool Block => true;
    }
}
