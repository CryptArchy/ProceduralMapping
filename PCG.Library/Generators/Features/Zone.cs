using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCG.Library.Generators.Features
{
    public class Zone
    {
        public List<Point3> Cells { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
