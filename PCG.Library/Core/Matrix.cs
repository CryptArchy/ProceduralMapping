using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCG.Library
{
    public struct Matrix
    {
        private double[,] values;
        public double[,] Values { get { return values; } }

        public int Width;
        public int Height;

        public Matrix(int w, int h)
        {
            values = new double[h, w];
            Width = w;
            Height = h;
        }

        public Matrix(int s)
        {
            values = new double[s, s];
            Width = s;
            Height = s;
        }

        public Matrix(double[,] seed)
        {
            values = seed;
            Width = seed.GetLength(1);
            Height = seed.GetLength(0);
        }

        public double this[int y, int x]
        {
            get { return this.Values[y, x]; }
            set { this.Values[y, x] = value; }
        }

        private static double DegreesToRadians(double degrees) { return degrees * (180 / Math.PI); }

        public static Matrix Identity(int size)
        {
            var m = new Matrix(size);
            for (int i = 0; i < size; i++)
                m[i, i] = 1;
            return m;
        }

        public static Matrix CrossMultiply(Matrix m1, Matrix m2)
        {
            var m1x = m1.Values.GetLength(1);
            var m1y = m1.Values.GetLength(0);
            var m2x = m2.Values.GetLength(1);
            var m2y = m2.Values.GetLength(0);

            if (m1x != m2y)
                throw new ArgumentException("The column count of the first matrix must equal the row count of the second.");

            var result = new Matrix(m2x, m1y);

            for (var ry = 0; ry < m1y; ry++)
            {
                for (var rx = 0; rx < m2x; rx++)
                {
                    for (var ri = 0; ri < m1x; ri++)
                    {
                        result[ry, rx] += m1[ry, ri] * m2[ri, rx];
                    }
                }
            }

            return result;
        }

        /* Unit Test for Matrix Multiplication
           m1 = new double[,]{{1,2},{3,4},{4,3},{2,1}};
           m2 = new double[,]{{1,2,3},{3,2,1}};
           mr = {{7,6,5},{15,14,13},{13,14,15},{5,6,7}};
         */
    }
}
