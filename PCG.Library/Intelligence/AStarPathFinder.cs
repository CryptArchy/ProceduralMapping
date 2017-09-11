using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCG.Library.Collections;

namespace PCG.Library.Intelligence
{
    public class AStarPathFinder : IPathFinder
    {
        private class AStarNode
        {
            public AStarNode Parent;
            public Point3 Position;
            public int GoalDistance;
            public int PathCost;
            public int TotalCost;
            public Direction DirectionInto;

            public AStarNode(Point3 pos, int goal, int path, AStarNode parent, Direction dirIn)
            {
                Position = pos;
                GoalDistance = goal;
                PathCost = path;
                TotalCost = goal + path;
                Parent = parent;
                DirectionInto = dirIn;
            }
        }

        public List<Direction> FindRoute<T>(CellMap<T> map, Point3 start, Point3 goal)
        {
            var path = new List<Direction>();
            var open = new Dictionary<Point3, AStarNode>();
            var closed = new Dictionary<Point3, AStarNode>();

            var done = false;
            AStarNode current = null;

            open.Add(start, new AStarNode(start, GoalDistance(start, goal), 0, null, Direction.Center));

            while (!done)
            {
                current = open.Values.OrderBy(a => a.TotalCost).First();
                open.Remove(current.Position);
                closed.Add(current.Position, current);

                var cc = map[current.Position];
                foreach (var dir in Direction.ListXYZ)
                {
                    if (cc.Edges.Includes(dir))
                    {
                        var point = current.Position.Move(dir);
                        if (!closed.ContainsKey(point))
                        {
                            AStarNode openNode = null;
                            if (open.TryGetValue(point, out openNode))
                            {
                                if (current.PathCost + 1 < openNode.PathCost)
                                {
                                    openNode.Parent = current;
                                    openNode.PathCost = current.PathCost + 1;
                                    openNode.DirectionInto = dir;
                                }
                            }
                            else
                            {
                                open.Add(point, new AStarNode(point, GoalDistance(point, goal), current.PathCost + 1, current, dir));
                            }
                        }
                    }    
                }
                done = (open.Count == 0 || current.Position == goal);
            }

            if (open.Count == 0)
                return path;

            while (current != null)
            {
                path.Add(current.DirectionInto);
                current = current.Parent;
            }
            path.Reverse();
            return path;
        }

        private int GoalDistance(Point3 current, Point3 dest)
        {
            return Math.Abs(current.X - dest.X) + Math.Abs(current.Y - dest.Y) + Math.Abs(current.Z - dest.Z);
        }

        private IEnumerable<Point3> TraversableNeighbors<T>(CellMap<T> map, Point3 point)
        {
            var cc = map[point];
            foreach (var dir in Direction.ListXY)
            {
                if (cc.Edges.Includes(dir))
                    yield return point.Move(dir);
            }
        }
    }
}
