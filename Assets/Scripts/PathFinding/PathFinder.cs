using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.PathFinding
{
    public class PathFinder
    {
        public Func<int, int, bool> CheckNode;
        public int MoveCost = 10;
        public int DiaginalMoveCost = 14;
        public int TargetX;
        public int TargetY;
        public int StartX;
        public int StartY;
        public Action<PathFindingResult> ResultFound;
        public int IterationsPerCycle = 5;

        private readonly List<PathFindingNode> _openList = new List<PathFindingNode>();
        private readonly List<PathFindingNode> _closedList = new List<PathFindingNode>();

        public void FindPath(int startx, int starty, int endx, int endy, MonoBehaviour behaviour)
        {
            behaviour.StopAllCoroutines();

            StartX = startx;
            StartY = starty;
            TargetX = endx;
            TargetY = endy;
            _openList.Clear();
            _closedList.Clear();

            behaviour.StartCoroutine(FindPath());
        }

        public IEnumerator FindPath()
        {
            var startnode = new PathFindingNode
            {
                GCost = 0,
                HCost = CalculateH(StartX, StartY, TargetX, TargetY),
                X = StartX,
                Y = StartY
            };

            _openList.Add(startnode);

            var currentnode = default(PathFindingNode);
            var currentIterations = IterationsPerCycle;

            while (true)
            {
                currentnode = _openList.OrderBy(f => f.FScore).FirstOrDefault();


                if (currentnode == null)
                {
                    ResultFound(null);
                    yield break;
                }


                if (currentnode.X == TargetX && currentnode.Y == TargetY)
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

                currentIterations--;

                if (currentIterations < 0)
                {
                    currentIterations = IterationsPerCycle;
                    yield return null;
                }
                

            }

            var result = new PathFindingResult
            {
                X = currentnode.X,
                Y = currentnode.Y,
                Next = null
            };
            
            while (true)
            {
                if (currentnode.Parent == null)
                    break;

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

            ResultFound(result);
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

            if(CheckNode == null)
                Debug.Log("fark");

            if (CheckNode(x, y) || (x == TargetX && y == TargetY))
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
