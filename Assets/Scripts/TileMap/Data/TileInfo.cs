using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;

namespace Assets.Scripts.TileMap.Data
{
    public class TileInfo
    {
        public TileTypes Type;
        public List<ITileDecorator> Decorators = new List<ITileDecorator>();

        public bool Blocked => Decorators.Any(d => d.Block);

        public bool Minable => Decorators.Any(d => d.Type == DecoratorType.Resource);

        public MinableResourceType ResourceType 
        {
            get
            {
                var dec = Decorators.FirstOrDefault(d => d.ResourceType != MinableResourceType.None);

                if (dec == null)
                    return MinableResourceType.None;

                return dec.ResourceType;
            }
        }
    }
}
