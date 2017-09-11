using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCG.Library
{
    public interface IRandom
    {
        IRandom Create();
        IRandom Create(int seed);
        int Next();
        int Next(int exclusiveMax);
        int Next(int inclusiveMin, int exclusiveMax);
        void NextBytes(byte[] buffer);
        double NextDouble();
    }
}
