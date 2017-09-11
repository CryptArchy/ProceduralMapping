using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCG.Library.Core
{
    public class SysRandom : IRandom
    {
        private Random rng;

        public SysRandom() { rng = new Random(); }
        public SysRandom(int seed) { rng = new Random(seed); }
        public SysRandom(Random rng) { this.rng = rng; }

        public IRandom Create()
        {
            return new SysRandom(rng.Next());
        }

        public IRandom Create(int seed)
        {
            return new SysRandom(seed);
        }

        public int Next()
        {
            return rng.Next();
        }

        public int Next(int exclusiveMax)
        {
            return rng.Next(exclusiveMax);
        }

        public int Next(int inclusiveMin, int exclusiveMax)
        {
            return rng.Next(inclusiveMin, exclusiveMax);
        }

        public void NextBytes(byte[] buffer)
        {
            rng.NextBytes(buffer);
        }

        public double NextDouble()
        {
            return rng.NextDouble();
        }
    }
}
