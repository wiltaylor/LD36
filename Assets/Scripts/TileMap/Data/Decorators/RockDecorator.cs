using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.TileMap.Data.Decorators
{
    public class RockDecorator : ITileDecorator
    {
        public DecoratorType Type => DecoratorType.Resource;
        public DecalType DecalType => DecalType.None;
        public MinableResourceType ResourceType => MinableResourceType.Rock;
        public bool Block => true;
    }
}
