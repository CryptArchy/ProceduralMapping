using PCG.Library.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCG.Library.Intelligence
{
    public class RandomWalkPathFinder: IPathFinder
    {
        private IRandom Rng { get; set; }

        public RandomWalkPathFinder(IRandom rng)
        {
            Rng = rng;
        }

        public List<Direction> FindRoute<T>(CellMap<T> map, Point3 start, Point3 goal)
        {
            var path = new List<Direction>();
            
            Point3 cp = start;
            Direction dir;

            while (cp != goal)
            {
                var cc = map[cp];

                if (map.Depth > 1)
                    dir = Rng.NextDirection();
                else
                    dir = Rng.NextDirectionXY();

                if (cc.Edges.Includes(dir))
                {
                    cp = cp.Move(dir);
                    path.Add(dir);
                }
            }
            
            return path;
        }
    }
}
