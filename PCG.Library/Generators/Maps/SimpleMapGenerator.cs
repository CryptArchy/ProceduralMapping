using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCG.Library.Extensions;
using PCG.Library.Collections;

namespace PCG.Library.Generators
{
    public interface ISimpleMapStrategy<T>
    {
        IRandom Rng { get; set; }
        CellMap<T> Map { get; set; }

        Direction GetCurrentDirection();
        Point3 GetCurrentPoint();

        Direction NextDirection(bool justAdded, Direction dir);
        bool ShouldAddPoint(Point3 target);

        //Point3 NextPoint();
        //bool HasNextPoint();
        //bool HasNextDirection();
        //void Add(Point3 point);
        //void Remove(Point3 point);
    }

    public class SimpleMapGenerator<T> : IMapGenerator<T>
    {
        private ISimpleMapStrategy<T> strategy;

        public SimpleMapGenerator(ISimpleMapStrategy<T> strategy)
        {
            this.strategy = strategy;
        }

        public static SimpleMapGenerator<T> RightHandSpiral { get { return new SimpleMapGenerator<T>(new RightHandSpiralStrategy()); } }
        public static SimpleMapGenerator<T> RightHandInOutSpiral { get { return new SimpleMapGenerator<T>(new RightHandInOutSpiralStrategy()); } }
        public static SimpleMapGenerator<T> LeftHandSpiral { get { return new SimpleMapGenerator<T>(new LeftHandSpiralStrategy()); } }
        public static SimpleMapGenerator<T> LeftHandInOutSpiral { get { return new SimpleMapGenerator<T>(new LeftHandInOutSpiralStrategy()); } }
        public static SimpleMapGenerator<T> VerticalLines { get { return new SimpleMapGenerator<T>(new VerticalLinesStrategy()); } }
        public static SimpleMapGenerator<T> HorizontalLines { get { return new SimpleMapGenerator<T>(new HorizontalLinesStrategy()); } }

        public CellMap<T> Generate(Size3 size, IRandom rng, Configuration config)
        {
            return Generate(size, rng);
        }

        public CellMap<T> Generate(Size3 size, IRandom rng)
        {
            var map = new CellMap<T>(size);
            var path = new List<Direction>();

            strategy.Rng = rng;
            strategy.Map = map;

            var count = 0;
            var dir = strategy.GetCurrentDirection();
            var cur = strategy.GetCurrentPoint();

            while (count < map.Count - 1)
            {
                var nxt = cur.Move(dir);
                var shouldAdd = strategy.ShouldAddPoint(nxt);
                if (shouldAdd)
                {
                    map.AddBorder(cur, dir);
                    count++;
                    cur = nxt;
                }
                dir = strategy.NextDirection(shouldAdd, dir);
            }

            return map;
        }

        public Configuration GetDefaultConfig()
        {
            var config = new Configuration();
            return config;
        }

        private class RightHandSpiralStrategy : ISimpleMapStrategy<T>
        {
            public IRandom Rng { get; set; }
            public CellMap<T> Map { get; set; }

            public Direction GetCurrentDirection() { return Direction.East; }

            public Point3 GetCurrentPoint() { return new Point3(0, 0, 0); }

            public Direction NextDirection(bool justAdded, Direction dir)
            {
                if (justAdded)
                    return dir;
                else
                    return dir.CycleXyCw;
            }

            public bool ShouldAddPoint(Point3 target)
            {
                return Map.IsInBounds(target) && Map[target].Edges == Direction.Center;
            }
        }

        private class LeftHandSpiralStrategy : ISimpleMapStrategy<T>
        {
            public IRandom Rng { get; set; }
            public CellMap<T> Map { get; set; }

            public Direction GetCurrentDirection() { return Direction.South; }

            public Point3 GetCurrentPoint() { return new Point3(0, 0, 0); }

            public Direction NextDirection(bool justAdded, Direction dir)
            {
                if (justAdded)
                    return dir;
                else
                    return dir.CycleXyCcw;
            }

            public bool ShouldAddPoint(Point3 target)
            {
                return Map.IsInBounds(target) && Map[target].Edges == Direction.Center;
            }
        }

        private class RightHandInOutSpiralStrategy : ISimpleMapStrategy<T>
        {
            public IRandom Rng { get; set; }
            public CellMap<T> Map { get; set; }

            public Direction GetCurrentDirection() { return Direction.East; }

            public Point3 GetCurrentPoint()
            {
                return new Point3(Map.Width / 2, Map.Height / 2, Map.Depth / 2);
            }

            public Direction NextDirection(bool justAdded, Direction dir)
            {
                if (justAdded)
                    return dir.CycleXyCw;
                else
                    return dir.CycleXyCcw;
            }

            public bool ShouldAddPoint(Point3 target)
            {
                return Map.IsInBounds(target) && Map[target].Edges == Direction.Center;
            }
        }

        private class LeftHandInOutSpiralStrategy : ISimpleMapStrategy<T>
        {
            public IRandom Rng { get; set; }
            public CellMap<T> Map { get; set; }

            public Direction GetCurrentDirection() { return Direction.South; }

            public Point3 GetCurrentPoint()
            {
                return new Point3(Map.Width / 2, Map.Height / 2, Map.Depth / 2);
            }

            public Direction NextDirection(bool justAdded, Direction dir)
            {
                if (justAdded)
                    return dir.CycleXyCcw;
                else
                    return dir.CycleXyCw;
            }

            public bool ShouldAddPoint(Point3 target)
            {
                return Map.IsInBounds(target) && Map[target].Edges == Direction.Center;
            }
        }

        private class VerticalLinesStrategy : ISimpleMapStrategy<T>
        {
            public IRandom Rng { get; set; }
            public CellMap<T> Map { get; set; }

            public Direction GetCurrentDirection() { return Direction.South; }

            public Point3 GetCurrentPoint() { return new Point3(0, 0, 0); }

            public Direction NextDirection(bool justAdded, Direction dir)
            {
                if (justAdded)
                    if (dir == Direction.North || dir == Direction.South)
                        return dir;
                    else
                        return dir.CycleXyCcw;
                else
                    return dir.CycleXyCcw;
            }

            public bool ShouldAddPoint(Point3 target)
            {
                return Map.IsInBounds(target) && Map[target].Edges == Direction.Center;
            }
        }

        private class HorizontalLinesStrategy : ISimpleMapStrategy<T>
        {
            public IRandom Rng { get; set; }
            public CellMap<T> Map { get; set; }

            public Direction GetCurrentDirection() { return Direction.East; }

            public Point3 GetCurrentPoint() { return new Point3(0, 0, 0); }

            public Direction NextDirection(bool justAdded, Direction dir)
            {
                if (justAdded)
                    if (dir == Direction.East || dir == Direction.West)
                        return dir;
                    else
                        return dir.CycleXyCw;
                else
                    return dir.CycleXyCw;
            }

            public bool ShouldAddPoint(Point3 target)
            {
                return Map.IsInBounds(target) && Map[target].Edges == Direction.Center;
            }
        }
    }
}
