using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCG.Library.Collections;

namespace PCG.Library
{
    public interface IMapGenerator<T>
    {
        CellMap<T> Generate(Size3 size, IRandom rng);
        CellMap<T> Generate(Size3 size, IRandom rng, Configuration config);
        Configuration GetDefaultConfig();
    }
}
