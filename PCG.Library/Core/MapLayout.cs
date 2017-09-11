using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCG.Library.Core
{
    /// <summary>
    /// Describes how the map should be treated in terms of dimensions and structure.
    /// </summary>
    public enum MapLayout
    {
        /// <summary>2D map which can have multiple floors, but each is treated individually.</summary>
        Layer,
        /// <summary>2D map, where the second layer is treated as "weaving" under the top layer.</summary>
        Weave,
        /// <summary>3D map, where all paths can move in full 3 dimensions.</summary>
        Open
    }
}
