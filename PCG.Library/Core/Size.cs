using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonExtensions.NumericExtensions;

namespace PCG.Library
{
    public struct Size3
    {
        private int height;
        private int width;
        private int depth;

        public Size3(int width, int height, int depth)
            : this()
        {
            this.width = width;
            this.height = height;
            this.depth = depth;
        }

        public int Width { get { return width; } }
        public int Height { get { return height; } }
        public int Depth { get { return depth; } }
        public int Volume { get { return height * width * depth; } }


        public override bool Equals(object obj)
        {
            if (obj is Size3)
            {
                var that = (Size3)obj;
                return this.IsEqualTo(that);
            }
            else { return false; }
        }

        public override int GetHashCode()
        {
            return (this.Height ^ this.Width.RotateLeft(10) ^ this.Depth.RotateLeft(20)).Scramble();
        }

        public override string ToString()
        {
            return this.Width.ToString() + "," + this.Height.ToString() + "," + this.Depth.ToString();
        }

        public static Size3 Parse(string value)
        {
            string[] components = value.Split(',');

            if (components.Length != 3)
                throw new ArgumentException("Argument contains wrong number of elements to make a Size3.", "value");

            return new Size3(
                Convert.ToInt32(components[0]),
                Convert.ToInt32(components[1]),
                Convert.ToInt32(components[2]));
        }

        public static readonly Size3 Empty = new Size3();

        public static explicit operator Point3(Size3 s)
        {
            return new Point3(s.Width, s.Height, s.Depth);
        }

        public static explicit operator Vector3(Size3 s)
        {
            return new Vector3(s.Width, s.Height, s.Depth);
        }

        public static bool operator ==(Size3 a, Size3 b)
        {
            return a.IsEqualTo(b);
        }

        public static bool operator !=(Size3 a, Size3 b)
        {
            return !a.IsEqualTo(b);
        }

        public static Size3 operator +(Size3 s1, Size3 s2)
        {
            return s1.Add(s2);
        }

        public static Size3 operator +(Size3 s, Vector3 v)
        {
            return s.Add(v);
        }

        public static Size3 operator -(Size3 s1, Size3 s2)
        {
            return s1.Subtract(s2); ;
        }

        public static Size3 operator -(Size3 s, Vector3 v)
        {
            return s.Subtract(v);
        }

        public static Size3 operator *(Size3 s, int scalar)
        {
            return s.Scale(scalar);
        }
    }

    public static class SizeExtensions
    {
        public static bool IsEqualTo(this Size3 s1, Size3 s2)
        {
            return s1.Width == s2.Width && s1.Height == s2.Height && s1.Depth == s2.Depth;
        }

        public static bool IsEmpty(this Size3 s)
        {
            return s == Size3.Empty;
        }

        public static Size3 Add(this Size3 s1, Size3 s2)
        {
            return new Size3(s1.Width + s2.Width, s1.Height + s2.Height, s1.Depth + s2.Depth);
        }

        public static Size3 Add(this Size3 s, Vector3 v)
        {
            return new Size3(s.Width + v.X, s.Height + v.Y, s.Depth + v.Z);
        }

        public static Size3 Subtract(this Size3 s1, Size3 s2)
        {
            return new Size3(s1.Width - s2.Width, s1.Height - s2.Height, s1.Depth - s2.Depth);
        }

        public static Size3 Subtract(this Size3 s, Vector3 v)
        {
            return new Size3(s.Width - v.X, s.Height - v.Y, s.Depth - v.Z);
        }

        public static Size3 Scale(this Size3 size, int scalar)
        {
            return new Size3(size.Width * scalar, size.Height * scalar, size.Depth * scalar);
        }
    }
}
