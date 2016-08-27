using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.PathFinding
{
    public class PathFinder
    {
        public Func<int, int, bool> CheckNode;
        public int MoveCost = 10;
        public int DiaginalMoveCost = 14;
        public int TargetX;
        public int TargetY;

        private readonly List<PathFindingNode> _openList = new List<PathFindingNode>();
        private readonly List<PathFindingNode> _closedList = new List<PathFindingNode>();

        public PathFindingResult FindPath(int startx, int starty, int endx, int endy)
        {
            var startnode = new PathFindingNode
            {
                GCost = 0,
                HCost = CalculateH(startx, starty, endx, endy),
                X = startx,
                Y = starty
            };

            TargetX = endx;
            TargetY = endy;

            _openList.Add(startnode);

            var currentnode = default(PathFindingNode);

            while (true)
            {
                currentnode = _openList.OrderBy(f => f.FScore).FirstOrDefault();


                if (currentnode == null)
                    return null;

                if (currentnode.X == endx && currentnode.Y == endy)
                    break;

                _openList.Remove(currentnode);
                _closedList.Add(currentnode);

                AddToOpenListIfBetter(GetNode(currentnode.X - 1, currentnode.Y, currentnode, false), currentnode, false);
                AddToOpenListIfBetter(GetNode(currentnode.X + 1, currentnode.Y, currentnode, false), currentnode, false);
                AddToOpenListIfBetter(GetNode(currentnode.X, currentnode.Y - 1, currentnode, false), currentnode, false);
                AddToOpenListIfBetter(GetNode(currentnode.X, currentnode.Y + 1, currentnode, false), currentnode, false);
                AddToOpenListIfBetter(GetNode(currentnode.X - 1, currentnode.Y - 1, currentnode, true), currentnode, true);
                AddToOpenListIfBetter(GetNode(currentnode.X - 1, currentnode.Y + 1, currentnode, true), currentnode, true);
                AddToOpenListIfBetter(GetNode(currentnode.X + 1, currentnode.Y - 1, currentnode, true), currentnode, true);
                AddToOpenListIfBetter(GetNode(currentnode.X + 1, currentnode.Y + 1, currentnode, true), currentnode, true);

            }

            var result = new PathFindingResult
            {
                X = currentnode.X,
                Y = currentnode.Y,
                Next = null
            };
            
            while (true)
            {
                currentnode = currentnode.Parent;

                result = new PathFindingResult
                {
                    X = currentnode.X,
                    Y = currentnode.Y,
                    Next =  result
                };

                if (currentnode.Parent == null)
                    break;
            }

            return result;
        }

        private void AddToOpenListIfBetter(PathFindingNode node, PathFindingNode parentCandidate, bool diagonal)
        {
            if (node == null)
                return;

            if (node.Parent == parentCandidate)
            {
                _openList.Add(node);
                return;
            }

            var newG = parentCandidate.GCost + (diagonal ? DiaginalMoveCost : MoveCost);

            if (node.GCost <= newG) return;

            node.Parent = parentCandidate;
            node.GCost = newG;
            _openList.Add(node);
        }

        private PathFindingNode GetNode(int x, int y, PathFindingNode parent, bool diagonal)
        {
            if (_closedList.Any(n => n.X == x && n.Y == y))
                return null;

            var openlist = _openList.FirstOrDefault(n => n.X == x && n.Y == y);

            if (openlist != null)
                return openlist;

            if (CheckNode(x, y))
            {
                return new PathFindingNode
                {
                    GCost = parent.GCost + (diagonal ? DiaginalMoveCost : MoveCost),
                    HCost = CalculateH(x,y, TargetX, TargetY),
                    Parent = parent,
                    X = x,
                    Y = y
                };
            }

            return null;
        }

        private int CalculateH(int startx, int starty, int endx, int endy)
        {
            var score = 0;

            if (startx > endx)
                score += startx - endx;
            if (startx < endx)
                score += endx - startx;
            if (starty > endy)
                score += starty - endy;
            if (starty < endy)
                score += endy - starty;

            return score;
        }

    }
}
