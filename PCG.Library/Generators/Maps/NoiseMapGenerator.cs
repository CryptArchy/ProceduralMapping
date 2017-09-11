using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graphics.Tools.Noise;
using Graphics.Tools.Noise.Builder;
using Graphics.Tools.Noise.Filter;
using Graphics.Tools.Noise.Primitive;
using PCG.Library.Collections;

namespace PCG.Library.Generators
{
    public class NoiseMapGenerator : IMapGenerator<int>
    {
        public CellMap<int> Generate(Size3 size, IRandom rng, Configuration config)
        {
            return Generate(size, rng);
        }

        public CellMap<int> Generate(Size3 size, IRandom rng)
        {
            PrimitiveModule pMod = new SimplexPerlin()
            {
                Seed = rng.Next(),
                Quality = NoiseQuality.Standard
            };

            FilterModule fMod = new SumFractal()
            {
                Frequency = FilterModule.DEFAULT_FREQUENCY,
                Lacunarity = FilterModule.DEFAULT_LACUNARITY,
                OctaveCount = FilterModule.DEFAULT_OCTAVE_COUNT,
                Gain = FilterModule.DEFAULT_GAIN,
                Primitive3D = (IModule3D)pMod
            };

            float bound = 2f;
            var noisemap = new NoiseMap();
            var projection = new NoiseMapBuilderPlane(bound, bound * 2, bound, bound * 2, false);

            projection.SetSize(size.Width, size.Height);
            projection.SourceModule = fMod;
            projection.NoiseMap = noisemap;
            projection.Build();

            var map = new CellMap<int>(size, c => { c.Value = GetValue(c.Position, size, noisemap); c.Edges = GetEdges(c.Position, size); });

            //var cutoff = 150;
            //foreach (var cell in map)
            //{
            //    if (cell.Value >= cutoff)
            //    {
            //        cell.Value = 0;
            //        cell.Edges = Direction.Center;
            //    }
            //    else
            //    {
            //        cell.Value = 255;
            //    }
            //}

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
            if (p.X != 0)
                openEdges |= Direction.West;
            if (p.X != s.Width - 1)
                openEdges |= Direction.East;
            if (p.Y != 0)
                openEdges |= Direction.North;
            if (p.Y != s.Height - 1)
                openEdges |= Direction.South;
            return openEdges;
        }

        private int GetValue(Point3 p, Size3 s, NoiseMap nm)
        {
            return (int)(nm.GetValue(p.X, p.Y) * 64 + 128);
        }
    }
}
