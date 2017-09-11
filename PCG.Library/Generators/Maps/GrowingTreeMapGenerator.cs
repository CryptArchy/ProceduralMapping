using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonExtensions.EnumerableExtensions;
using PCG.Library.Extensions;
using PCG.Library.Collections;

namespace PCG.Library.Generators
{
    public interface IGrowingTreeStrategy
    {
        IRandom Rng { get; set; }
        bool HasNext();
        Point3 Next();
        void Add(Point3 pos);
        void Remove(Point3 pos);
    }

    public class GrowingTreeMazeGenerator : IMapGenerator<int>
    {
        private IGrowingTreeStrategy strategy;

        public GrowingTreeMazeGenerator(IGrowingTreeStrategy strategy)
        {
            this.strategy = strategy;
        }

        public static GrowingTreeMazeGenerator TakeNewestCellStrategy { get { return new GrowingTreeMazeGenerator(new StackStrategy()); } }
        public static GrowingTreeMazeGenerator TakeOldestCellStrategy { get { return new GrowingTreeMazeGenerator(new QueueStrategy()); } }
        public static GrowingTreeMazeGenerator TakeRandomCellStrategy { get { return new GrowingTreeMazeGenerator(new RandomListStrategy()); } }

        public CellMap<int> Generate(Size3 size, IRandom rng, Configuration config)
        {
            return Generate(size, rng);
        }

        public CellMap<int> Generate(Size3 size, IRandom rng)
        {
            strategy.Rng = rng;

            var maze = new CellMap<int>(size);
            var directions = new List<Direction>() { Direction.Up, Direction.Down };
            directions.AddRange(Direction.ListXY.Repeat(12));
            var cp = rng.NextPoint3(0, maze.Width, maze.Height, maze.Depth);
            strategy.Add(cp);
            var initialPath = true;
            do
            {
                cp = strategy.Next();
                var cc = maze[cp];
                var foundValidNeighbor = false;

                foreach (var dir in directions.Shuffle(rng))
                {
                    var np = cp.Move(dir);

                    if (maze.IsInBounds(np) && maze[np].Edges == Direction.Center)
                    {
                        var nc = maze[np];
                        maze[cp].Edges |= dir;
                        maze[cp].Value = initialPath ? 0 : 1;
                        maze[np].Edges |= dir.Opposite;
                        strategy.Add(np);
                        foundValidNeighbor = true;
                        break;
                    }
                }
                if (!foundValidNeighbor) { strategy.Remove(cp); initialPath = false; }

            } while (strategy.HasNext());
            return maze;
        }

        public Configuration GetDefaultConfig()
        {
            var config = new Configuration();
            return config;
        }

        public class StackStrategy : IGrowingTreeStrategy
        {
            private Stack<Point3> cellIndexes = new Stack<Point3>();

            public IRandom Rng { get; set; }

            public bool HasNext()
            {
                return cellIndexes.Count > 0;
            }

            public Point3 Next()
            {
                return cellIndexes.Peek();
            }

            public void Add(Point3 pos)
            {
                cellIndexes.Push(pos);
            }

            public void Remove(Point3 pos)
            {
                cellIndexes.Pop();
            }
        }

        public class QueueStrategy : IGrowingTreeStrategy
        {
            private Queue<Point3> cellIndexes = new Queue<Point3>();

            public IRandom Rng { get; set; }

            public bool HasNext()
            {
                return cellIndexes.Count > 0;
            }

            public Point3 Next()
            {
                return cellIndexes.Peek();
            }

            public void Add(Point3 pos)
            {
                cellIndexes.Enqueue(pos);
            }

            public void Remove(Point3 pos)
            {
                cellIndexes.Dequeue();
            }
        }

        public class RandomListStrategy : IGrowingTreeStrategy
        {
            private List<Point3> cellIndexes = new List<Point3>();

            public IRandom Rng { get; set; }

            public bool HasNext()
            {
                return cellIndexes.Count > 0;
            }

            public Point3 Next()
            {
                return cellIndexes[Rng.Next(0, cellIndexes.Count)];
            }

            public void Add(Point3 pos)
            {
                cellIndexes.Add(pos);
            }

            public void Remove(Point3 pos)
            {
                cellIndexes.Remove(pos);
            }
        }

        public class MixedStrategy : IGrowingTreeStrategy
        {
            private List<Point3> cellIndexes = new List<Point3>();
            private double _chanceOldest;
            private double _chanceNewest;
            private double _chanceRandom;

            public MixedStrategy(double newest = 1, double oldest = 1, double random = 1)
            {
                var total = newest + oldest + random;
                if (total > 1.0)
                {
                    newest /= total;
                    oldest /= total;
                    random /= total;
                }
                _chanceNewest = newest;
                _chanceOldest = oldest + newest;
                _chanceRandom = random + oldest + newest;
            }

            public IRandom Rng { get; set; }

            public bool HasNext()
            {
                return cellIndexes.Count > 0;
            }

            public Point3 Next()
            {
                var roll = Rng.NextDouble();

                if (roll < _chanceNewest)
                    return cellIndexes[cellIndexes.Count - 1];
                else if (roll < _chanceOldest)
                    return cellIndexes[0];
                else
                    return cellIndexes[Rng.Next(0, cellIndexes.Count)];  
            }

            public void Add(Point3 pos)
            {
                cellIndexes.Add(pos);
            }

            public void Remove(Point3 pos)
            {
                cellIndexes.Remove(pos);
            }
        }
    }
}
