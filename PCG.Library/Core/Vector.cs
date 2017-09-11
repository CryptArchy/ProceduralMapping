using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonExtensions.NumericExtensions;

namespace PCG.Library
{
    public struct Vector3
    {
        private int x;
        public int X { get { return x; } }

        private int y;
        public int Y { get { return y; } }

        private int z;
        public int Z { get { return z; } }

        public Vector3(int x, int y, int z)
            : this()
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public override bool Equals(object obj)
        {
            if (obj is Vector3)
            {
                var that = (Vector3)obj;
                return this == that;
            }
            else { return false; }
        }

        public override int GetHashCode()
        {
            return (this.X ^ this.Y.RotateLeft(10) ^ this.Z.RotateLeft(20)).Scramble();
        }

        public override string ToString()
        {
            return this.X.ToString() + "," + this.Y.ToString() + "," + this.Z.ToString();
        }

        public static Vector3 Parse(string source)
        {
            throw new NotImplementedException();
        }

        public static bool operator ==(Vector3 a, Vector3 b)
        {
            return a.X == b.X && a.Y == b.Y && a.Z == b.Z;
        }

        public static bool operator !=(Vector3 a, Vector3 b)
        {
            return !(a == b);
        }

        public static double AngleBetween(Vector3 v1, Vector3 v2)
        {
            double cos_theta = (v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z)
                / (v1.Length * v2.Length);

            return Math.Acos(cos_theta);// / Math.PI * 180;
        }

        public static Vector3 CrossProduct(Vector3 v1, Vector3 v2)
        {
            //return v1.X * v2.Y - v1.Y * v2.X;
            return new Vector3(v1.Y * v2.Z - v1.Z * v2.Y, v1.Z * v2.X - v2.Z * v1.X, v1.X * v2.Y - v2.X * v1.Y);
        }

        public static double Determinant(Vector3 v1, Vector3 v2)
        {
            // same as CrossProduct, it appears.
            return v1.X * v2.Y - v1.Y * v2.X;
        }

        public static Vector3 Divide(Vector3 Vector3, int scalar)
        {
            return new Vector3(Vector3.X / scalar, Vector3.Y / scalar, Vector3.Z / scalar);
        }

        public static int Multiply(Vector3 v1, Vector3 v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }

        //public static Vector3 Multiply(Vector3 v, Matrix m)
        //{
        //    return new Vector3(v.X * m.M11 + v.Y * m.M21,
        //               v.X * m.M12 + v.Y * m.M22);
        //}

        public static Vector3 Multiply(Vector3 v, int s)
        {
            return new Vector3(s * v.X, s * v.Y, s * v.Z);
        }

        public void Negate()
        {
            x = -x;
            y = -y;
        }



        public static Vector3 Subtract(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }


        public double Length
        {
            get { return Math.Sqrt(LengthSquared); }
        }

        private double LengthSquared
        {
            get { return x * x + y * y + z * z; }
        }
    }
}
