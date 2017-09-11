using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using CommonExtensions.NumericExtensions;

namespace PCG.Library
{
    public struct Point3
    {
        private int x;
        private int y;
        private int z;
        
        public Point3(int x, int y, int z) : this()
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public int X { get { return x; } }
        public int Y { get { return y; } }
        public int Z { get { return z; } }

        public override bool Equals(object obj)
        {
            if (obj is Point3)
            {
                var that = (Point3)obj;
                return this.IsEqualTo(that);
            }
            else { return false; }
        }

        public override int GetHashCode()
        {
            return (this.X ^ this.Y.RotateLeft(10) ^ this.Z.RotateLeft(20)).Scramble();
        }

        public override string ToString()
        {
            return X.ToString() + "," + Y.ToString() + "," + Z.ToString();
        }

        public static Point3 Parse(string value)
        {
            string[] components = value.Split(',','/','*','-','+');

            if (components.Length > 3)
                throw new ArgumentException("Argument contains wrong number of elements to make a Point3.", "value");

            return new Point3(
                Convert.ToInt32(components.Length > 0 ? components[0] : "0"),
                Convert.ToInt32(components.Length > 1 ? components[1] : "0"),
                Convert.ToInt32(components.Length > 2 ? components[2] : "0"));
        }

        public static readonly Point3 Empty = new Point3();

        public static bool operator ==(Point3 p1, Point3 p2)
        {
            return p1.IsEqualTo(p2);
        }

        public static bool operator !=(Point3 p1, Point3 p2)
        {
            return !p1.IsEqualTo(p2);
        }

        public static Point3 operator +(Point3 p1, Point3 p2)
        {
            return p1.Add(p2);
        }

        public static Point3 operator +(Point3 p, Vector3 v)
        {
            return p.Add(v);
        }

        public static Point3 operator +(Point3 p, Size3 s)
        {
            return p.Add(s);
        }

        public static Vector3 operator -(Point3 p1, Point3 p2)
        {
            return p1.Subtract(p2);;
        }

        public static Point3 operator -(Point3 p, Vector3 v)
        {
            return p.Subtract(v);
        }

        //public static Point3 operator *(Point3 point, Matrix matrix)
        //{
        //    return Multiply(point, matrix);
        //}

        public static explicit operator Size3(Point3 p)
        {
            return new Size3(p.X, p.Y, p.Z);
        }

        public static explicit operator Vector3(Point3 p)
        {
            return new Vector3(p.X, p.Y, p.Z);
        }
    }

    public struct Point2
    {
        private int x;
        private int y;

        public Point2(int x, int y)
            : this()
        {
            this.x = x;
            this.y = y;
        }

        public int X { get { return x; } }
        public int Y { get { return y; } }

        public override bool Equals(object obj)
        {
            if (obj is Point2)
            {
                var that = (Point2)obj;
                return this.X == that.X && this.Y == that.Y;
            }
            else { return false; }
        }

        public override int GetHashCode()
        {
            return (this.X ^ this.Y.RotateLeft(16)).Scramble();

        }

        public override string ToString()
        {
            return "(" + X.ToString() + "," + Y.ToString() + ")";
        }

        public static bool operator ==(Point2 x, Point2 y)
        {
            return x.X == y.X && x.Y == y.Y;
        }

        public static bool operator !=(Point2 x, Point2 y)
        {
            return !(x == y);
        }
    }

    public static class PointExt
    {
        public static bool IsEqualTo(this Point3 p1, Point3 p2)
        {
            return p1.X == p2.X && p1.Y == p2.Y && p1.Z == p2.Z;
        }

        public static bool IsEmpty(this Point3 p)
        {
            return p == Point3.Empty;
        }

        public static Point3 Add(this Point3 p1, Point3 p2)
        {
            return new Point3(p1.X + p2.X, p1.Y + p2.Y, p1.Z + p2.Z);
        }

        public static Point3 Add(this Point3 p, Vector3 v)
        {
            return new Point3(p.X + v.X, p.Y + v.Y, p.Z + v.Z);
        }

        public static Point3 Add(this Point3 p, Size3 s)
        {
            return new Point3(p.X + s.Width, p.Y + s.Height, p.Z + s.Depth);
        }

        public static Vector3 Subtract(this Point3 p1, Point3 p2)
        {
            return new Vector3(p1.X - p2.X, p1.Y - p2.Y, p1.Z - p2.Z);
        }

        public static Point3 Subtract(this Point3 p, Vector3 v)
        {
            return new Point3(p.X - v.X, p.Y - v.Y, p.Z - v.Z);
        }

        public static int BlockDistance(this Point3 p1, Point3 p2)
        {
            return Math.Abs(p1.X - p2.X) + Math.Abs(p1.Y - p2.Y) + Math.Abs(p1.Z - p2.Z);
        }

        public static bool IsAdjacent(this Point3 p1, Point3 p2)
        {
            return BlockDistance(p1, p2) == 1;
        }

        //public static Point3 Multiply(Point3 p, Matrix m)
        //{
        //    return new Point3(p.X * m.M11 + p.Y * m.M21 + m.OffsetX,
        //              p.X * m.M12 + p.Y * m.M22 + m.OffsetY);
        //}

        public static Point2 NextPoint2(this IRandom rng)
        {
            return new Point2(rng.Next(), rng.Next());
        }
        public static Point2 NextPoint2(this IRandom rng, int max)
        {
            return new Point2(rng.Next(max), rng.Next(max));
        }
        public static Point2 NextPoint2(this IRandom rng, int min, int max)
        {
            return new Point2(rng.Next(min, max), rng.Next(min, max));
        }
        public static Point2 NextPoint2(this IRandom rng, int min, int maxX, int maxY)
        {
            return new Point2(rng.Next(min, maxX), rng.Next(min, maxY));
        }
        public static Point2 NextPoint2(this IRandom rng, int minX, int maxX, int minY, int maxY)
        {
            return new Point2(rng.Next(minX, maxX), rng.Next(minY, maxY));
        }

        public static Point3 NextPoint3(this IRandom rng)
        {
            return new Point3(rng.Next(), rng.Next(), rng.Next());
        }
        public static Point3 NextPoint3(this IRandom rng, int max)
        {
            return new Point3(rng.Next(max), rng.Next(max), rng.Next(max));
        }
        public static Point3 NextPoint3(this IRandom rng, int min, int max)
        {
            return new Point3(rng.Next(min, max), rng.Next(min, max), rng.Next(min, max));
        }
        public static Point3 NextPoint3(this IRandom rng, int min, int maxX, int maxY, int maxZ)
        {
            return new Point3(rng.Next(min, maxX), rng.Next(min, maxY), rng.Next(min, maxZ));
        }
        public static Point3 NextPoint3(this IRandom rng, int minX, int maxX, int minY, int maxY, int minZ, int maxZ)
        {
            return new Point3(rng.Next(minX, maxX), rng.Next(minY, maxY), rng.Next(minZ, maxZ));
        }
    }
}
