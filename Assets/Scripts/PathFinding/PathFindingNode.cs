using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class PathFindingNode
    {
        public int X;
        public int Y;
        public PathFindingNode Parent;
        public int GCost;
        public int HCost;
        public int FScore => GCost + HCost;

    }
}
