using PCG.Library.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCG.Library.Extensions;

namespace PCG.Library.Generators
{
    public class BinaryDivisionMapGenerator : IMapGenerator<int>
    {
        private int _maxRegSz = 6;

        public BinaryDivisionMapGenerator() 
        { }

        private struct Region
        {
            public Point3 Origin;
            public Size3 Size;
            public Region(Point3 origin, Size3 size) { Origin = origin; Size = size; }
            public Region(int x, int y, int z, int sx, int sy, int sz)
            {
                Origin = new Point3(x, y, z);
                Size = new Size3(sx, sy, sz);
            }
            public override string ToString()
            {
                return string.Format("{0},{1} - {2},{3}",Origin.X,Origin.Y,Size.Width,Size.Height);
            }
        }

        public CellMap<int> Generate(Size3 size, IRandom rng, Configuration config)
        {
            _maxRegSz = config.Extract<int>("MaxRegionSize", 1);
            return Generate(size, rng);
        }

        public CellMap<int> Generate(Size3 size, IRandom rng)
        {
            var map = new EmptyMapGenerator(Direction.Center, (size.Depth > 1)).Generate(size, rng); //new CellMap<int>(size);
            var regions = new Stack<Region>();
            regions.Push(new Region(0,0,0,size.Width,size.Height,size.Depth));

            while (regions.Count > 0)
            {
                Direction direction = rng.NextPlane();
                if (size.Depth <= 1 && direction.Includes(Direction.XY)) continue;                

                Region reg = regions.Pop();
                Size3 sz1, sz2;
                Point3 splitter = rng.NextPoint3(
                    reg.Origin.X+1, reg.Origin.X + reg.Size.Width,
                    reg.Origin.Y+1, reg.Origin.Y + reg.Size.Height,
                    reg.Origin.Z+1, reg.Origin.Z + reg.Size.Depth);

                if (size.Depth > 1 && direction.Includes(Direction.XY))
                {
                    sz1 = new Size3(reg.Size.Width, reg.Size.Height, splitter.Z - reg.Origin.Z);
                    sz2 = new Size3(reg.Size.Width, reg.Size.Height, reg.Size.Depth - sz1.Depth);

                    if (sz1.Depth > _maxRegSz)
                        regions.Push(new Region(reg.Origin, sz1));
                    if (sz2.Depth > _maxRegSz)
                        regions.Push(new Region(new Point3(reg.Origin.X, reg.Origin.Y, splitter.Z), sz2));

                    for (int y = reg.Origin.Y; y < reg.Origin.Y + reg.Size.Height; y++)
                        for (int x = reg.Origin.X; x < reg.Origin.X + reg.Size.Width; x++)
                            map.RemoveBorder(new Point3(x, y, splitter.Z), Direction.Up);

                    map.AddBorder(new Point3(splitter.X - 1, splitter.Y - 1, splitter.Z), Direction.Up);
                }
                else if (direction.Includes(Direction.XZ))
                {
                    sz1 = new Size3(reg.Size.Width, splitter.Y - reg.Origin.Y , reg.Size.Depth);
                    sz2 = new Size3(reg.Size.Width, reg.Size.Height - sz1.Height, reg.Size.Depth);

                    if (sz1.Height > _maxRegSz) 
                        regions.Push(new Region(reg.Origin, sz1));
                    if (sz2.Height > _maxRegSz) 
                        regions.Push(new Region(new Point3(reg.Origin.X, splitter.Y, reg.Origin.Z), sz2));

                    for (int z = reg.Origin.Z; z < reg.Origin.Z + reg.Size.Depth; z++)
                        for (int x = reg.Origin.X; x < reg.Origin.X + reg.Size.Width; x++)
                            map.RemoveBorder(new Point3(x, splitter.Y, z), Direction.North);

                    map.AddBorder(new Point3(splitter.X - 1, splitter.Y, splitter.Z - 1), Direction.North);
                }
                else if (direction.Includes(Direction.YZ))
                {
                    sz1 = new Size3(splitter.X - reg.Origin.X, reg.Size.Height, reg.Size.Depth);
                    sz2 = new Size3(reg.Size.Width - sz1.Width, reg.Size.Height, reg.Size.Depth);

                    if (sz1.Width > _maxRegSz)
                        regions.Push(new Region(reg.Origin, sz1));
                    if (sz2.Width > _maxRegSz)
                        regions.Push(new Region(new Point3(splitter.X, reg.Origin.Y, reg.Origin.Z), sz2));

                    for (int z = reg.Origin.Z; z < reg.Origin.Z + reg.Size.Depth; z++)
                        for (int y = reg.Origin.Y; y < reg.Origin.Y + reg.Size.Height; y++)
                            map.RemoveBorder(new Point3(splitter.X, y, z), Direction.West);

                    map.AddBorder(new Point3(splitter.X, splitter.Y - 1, splitter.Z - 1), Direction.West);
                }
            }

            return map;
        }

        public Configuration GetDefaultConfig()
        {
            var config = new Configuration();
            config.Add("MaxRegionSize", 1);
            return config;
        }
    }
}
