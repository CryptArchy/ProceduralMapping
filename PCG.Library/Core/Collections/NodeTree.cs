using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCG.Library.Collections
{
    class NodeTree<T>
    {
        public List<Sector<T>> Sectors { get; set; }
    }

    //Maybe should be an Octree or derived from one?
    class Sector<T>
    {
        public List<Octree<T>> Zones { get; set; }
    }

    class Octree<T>
    {
        public T[] Children { get; set; }
        public Octree() { Children = new T[8]; }
    }
}
