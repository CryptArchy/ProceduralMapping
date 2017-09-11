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
    //Whoops, this version has a critical flaw that allows for loops.  Looks neat though, so I'm keeping it.
    public class KruskalsLoopyMapGenerator : IMapGenerator<int>
    {
        private struct Edge
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

            foreach (var edge in edges)
            {
                var cc = map[edge.CP];
                var nc = map[edge.NP];
                if (cc.Edges.Count() < 2 && nc.Edges.Count() < 2)
                    map.AddBorder(edge.CP, edge.NP, edge.D);
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
