using PCG.Library.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCG.Library.Extensions;
using CommonExtensions.EnumerableExtensions;

namespace PCG.Library.Generators
{
    public class KruskalsMapGenerator : IMapGenerator<int>
    {
        private class Edge
        {
            public Point3 Point;
            public Direction Direction;
            public Edge(Point3 p, Direction d) { Point = p; Direction = d; }
        }

        public CellMap<int> Generate(Size3 size, IRandom rng, Configuration config)
        {
            return Generate(size, rng);
        }

        public CellMap<int> Generate(Size3 size, IRandom rng)
        {
            var map = new CellMap<int>(size);
            var dirUNW = new Direction[] { Direction.North, Direction.West, Direction.Up };
            var edges = map.SelectMany(c =>
                    dirUNW.Where(d => (d == Direction.North && c.Position.Y > 0)
                                   || (d == Direction.West && c.Position.X > 0)
                                   || (d == Direction.Up && c.Position.Z > 0))
                    .Select(d => new { CP = c.Position, NP = c.Position.Move(d), D = d })
                ).Shuffle(rng);

            var paths = new Dictionary<Point3, ConsList<Point3>>();

            foreach (var edge in edges)
            {
                ConsList<Point3> cs, ns;
                if (!paths.TryGetValue(edge.CP, out cs))
                {
                    cs = new ConsList<Point3>();
                    cs.Add(edge.CP);
                    paths[edge.CP] = cs;
                }

                if (!paths.TryGetValue(edge.NP, out ns))
                {
                    ns = new ConsList<Point3>();
                    ns.Add(edge.NP);
                    paths[edge.NP] = ns;
                }

                if (cs.First != ns.First)
                {
                    cs.Append(ns);
                    paths[edge.NP] = cs;
                    map.AddBorder(edge.CP, edge.NP, edge.D);
                }
            }

            return map;
        }

        public Configuration GetDefaultConfig()
        {
            var config = new Configuration();
            return config;
        }
    }
}
