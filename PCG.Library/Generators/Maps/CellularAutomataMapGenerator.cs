using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PCG.Library.Collections;

namespace PCG.Library.Generators
{
    public class CellularAutomataMapGenerator<T> : IMapGenerator<T>
    {
        private int[] _born;
        private int[] _live;

        public CellularAutomataMapGenerator()
        {
        }

        public CellularAutomataMapGenerator(int[] bornWith, int[] liveWith)
        {
            _born = bornWith;
            _live = liveWith;
        }

        public CellMap<T> Generate(Size3 size, IRandom rng, Configuration config)
        {
            return Generate(size, rng);
        }

        public CellMap<T> Generate(Size3 size, IRandom rng)
        {
            var map = new CellMap<T>(size, c => c.Edges = GetEdges(c.Position, size, (rng.Next(0,2) == 0)));

            for (int i = 0; i < 10; i++)
            {
                foreach (var cell in map)
                {
                    var nn = map.TryGet(cell.Position.Move(Direction.North));
                    var sn = map.TryGet(cell.Position.Move(Direction.South));
                    var wn = map.TryGet(cell.Position.Move(Direction.West));
                    var en = map.TryGet(cell.Position.Move(Direction.East));

                    var tempEdges = cell.Edges;

                    if (ShouldMakeWall(tempEdges, Direction.North, nn, wn, en))
                        map.RemoveBorder(cell.Position, Direction.North);
                    else
                        map.AddBorder(cell.Position, Direction.North);

                    if (ShouldMakeWall(tempEdges, Direction.South, sn, en, wn))
                        map.RemoveBorder(cell.Position, Direction.South);
                    else
                        map.AddBorder(cell.Position, Direction.South);

                    if (ShouldMakeWall(tempEdges, Direction.East, en, nn, sn))
                        map.RemoveBorder(cell.Position, Direction.East);
                    else
                        map.AddBorder(cell.Position, Direction.East);

                    if (ShouldMakeWall(tempEdges, Direction.West, wn, sn, nn))
                        map.RemoveBorder(cell.Position, Direction.West);
                    else
                        map.AddBorder(cell.Position, Direction.West);
                }
            }

            return map;
        }

        public Configuration GetDefaultConfig()
        {
            var config = new Configuration();
            return config;
        }

        private Direction GetEdges(Point3 p, Size3 s, bool isAlive)
        {
            var openEdges = Direction.Center;

            if (isAlive) openEdges = Direction.XY;

            if (p.X == 0) openEdges &= ~Direction.West;
            if (p.X == s.Width - 1) openEdges &= ~Direction.East;
            if (p.Y == 0) openEdges &= ~Direction.North;
            if (p.Y == s.Height - 1) openEdges &= ~Direction.South;

            return openEdges;
        }

        private bool ShouldMakeWall(Direction edges, Direction dir, Cell<T> parent, Cell<T> sister, Cell<T> brother)
        {
            bool change = false;
            if (edges.Includes(dir))
            {
                change = (sister != null && sister.Edges.Includes(dir)) ||
                         (brother != null && brother.Edges.Includes(dir));

                if (change) return true;
                else return false;
            }
            else
            {
                change = (edges.Includes(dir.CycleXyCw) || edges.Includes(dir.CycleXyCcw)) &&
                         (parent != null && (parent.Edges.Includes(dir.CycleXyCw) || parent.Edges.Includes(dir.CycleXyCcw)));

                if (change) return false;
                else return true;
            }
        }
    }
}
