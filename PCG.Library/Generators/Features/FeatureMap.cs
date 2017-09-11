using PCG.Library.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCG.Library.Generators.Features
{
    public class FeatureMap<T>
    {
        private CellMap<T> basemap;
        private Dictionary<Point3, List<T>> features;
        
    }
}
