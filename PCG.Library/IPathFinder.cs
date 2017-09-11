using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCG.Library.Collections;

namespace PCG.Library
{
    public interface IPathFinder
    {
        List<Direction> FindRoute<T>(CellMap<T> map, Point3 start, Point3 goal);
    }
}
