using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCG.Library.Collections;

namespace PCG.Library.Generators
{
    public class RecursiveBacktrackingMapGenerator : IMapGenerator<int>
    {
        public CellMap<int> Generate(Size3 size, IRandom rng, Configuration config)
        {
            return Generate(size, rng);
        }

        public CellMap<int> Generate(Size3 size, IRandom rng)
        {
            var map = new CellMap<int>(size);
            CarvePassagesFrom(new Point3(0, 0, 0), map, rng);
            return map;
        }

        public Configuration GetDefaultConfig()
        {
            var config = new Configuration();
            return config;
        }

        private void CarvePassagesFrom(Point3 cur, CellMap<int> map, IRandom rng)
        {
            foreach (var dir in rng.NextDirectionsXY())
            {
                Point3 next = cur.Move(dir);

                if (map.IsInBounds(next) && (map[next].Edges == Direction.Center))
                {
                    if (map[cur].Edges.Count() <= 1)
                        map.AddBorder(cur, dir);
                    else
                        map.AddBorder(cur, dir);

                    CarvePassagesFrom(next, map, rng);
                }
            }
        }
    }
}
