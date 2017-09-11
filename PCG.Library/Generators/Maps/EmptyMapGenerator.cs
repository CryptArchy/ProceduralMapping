using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCG.Library.Collections;

namespace PCG.Library.Generators
{
    public class EmptyMapGenerator : IMapGenerator<int>
    {
        private Direction _openBorders;
        private bool _isHollow;

        public EmptyMapGenerator() : this(Direction.Center, true) { }

        public EmptyMapGenerator(Direction openBorders, bool makeHollow)
        {
            _openBorders = openBorders;
            _isHollow = makeHollow;
        }

        public CellMap<int> Generate(Size3 size, IRandom rng, Configuration config)
        {
            return Generate(size, rng);
        }

        public CellMap<int> Generate(Size3 size, IRandom rng)
        {
            var map = new CellMap<int>(size, c => { 
                c.Value = GetValue(c.Position, size); 
                c.Edges = GetEdges(c.Position, size); 
            });
            return map;
        }

        public Configuration GetDefaultConfig()
        {
            var config = new Configuration();
            return config;
        }

        private Direction GetEdges(Point3 p, Size3 s)
        {
            var openEdges = Direction.Center;

            if (p.X != 0) openEdges |= Direction.West;
            if (p.X != s.Width - 1) openEdges |= Direction.East;
            if (p.Y != 0) openEdges |= Direction.North;
            if (p.Y != s.Height - 1) openEdges |= Direction.South;
            if (_isHollow)
            {
                if (p.Z != 0) openEdges |= Direction.Up;
                if (p.Z != s.Depth - 1) openEdges |= Direction.Down;
            }

            return openEdges | _openBorders;
        }

        private int GetValue(Point3 p, Size3 s)
        {
            return 200;
        }
    }
}
