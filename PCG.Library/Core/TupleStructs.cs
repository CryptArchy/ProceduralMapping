using System;
using CommonExtensions.NumericExtensions;

namespace PCG.Models
{
    public struct Point3
    {
        private int x;
		public int X { get { return x; } }

        private int y;
		public int Y { get { return y; } }

        private int z;
		public int Z { get { return z; } }

        public Point3(int x, int y, int z) : this()
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public override bool Equals(object obj)
        {
            if (obj is Point3)
            {
                var that = (Point3)obj;
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

        public static bool operator ==(Point3 a, Point3 b)
        {
            return a.X == b.X && a.Y == b.Y && a.Z == b.Z;
        }

        public static bool operator !=(Point3 a, Point3 b)
        {
            return !(a == b);
        }
    }
    public struct Vector3
    {
        private int x;
		public int X { get { return x; } }

        private int y;
		public int Y { get { return y; } }

        private int z;
		public int Z { get { return z; } }

        public Vector3(int x, int y, int z) : this()
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

        public static bool operator ==(Vector3 a, Vector3 b)
        {
            return a.X == b.X && a.Y == b.Y && a.Z == b.Z;
        }

        public static bool operator !=(Vector3 a, Vector3 b)
        {
            return !(a == b);
        }
    }
    public struct Size3
    {
        private int height;
		public int Height { get { return height; } }

        private int width;
		public int Width { get { return width; } }

        private int depth;
		public int Depth { get { return depth; } }

        public Size3(int height, int width, int depth) : this()
        {
            this.height = height;
            this.width = width;
            this.depth = depth;
        }

        public override bool Equals(object obj)
        {
            if (obj is Size3)
            {
                var that = (Size3)obj;
                return this == that;
            }
            else { return false; }
        }

        public override int GetHashCode()
        {
            return (this.Height ^ this.Width.RotateLeft(10) ^ this.Depth.RotateLeft(20)).Scramble();
        }

        public override string ToString()
        {
            return this.Height.ToString() + "," + this.Width.ToString() + "," + this.Depth.ToString();
        }

        public static bool operator ==(Size3 a, Size3 b)
        {
            return a.Height == b.Height && a.Width == b.Width && a.Depth == b.Depth;
        }

        public static bool operator !=(Size3 a, Size3 b)
        {
            return !(a == b);
        }
    }
}

