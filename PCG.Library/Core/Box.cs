using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonExtensions.NumericExtensions;

namespace PCG.Library.Core
{
    public struct Box
    {
        private int x;
        private int y;
        private int z;
        private int w;
        private int h;
        private int d;

        public Box(int x, int y, int z, int w, int h, int d) : this()
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
            this.h = h;
            this.d = d;
        }

        public int X { get { return x; } }
        public int Y { get { return y; } }
        public int Z { get { return z; } }
        public int W { get { return w; } }
        public int H { get { return h; } }
        public int D { get { return d; } }

        public Size3 Size { get { return new Size3(w, h, d); } }
        public Point3 Origin { get { return NWU; } }

        public Point3 NWU { get { return new Point3(x, y, z); } }
        public Point3 NEU { get { return new Point3(x + w, y, z); } }
        public Point3 SWU { get { return new Point3(x, y + h, z); } }
        public Point3 SEU { get { return new Point3(x + w, y + h, z); } }
        public Point3 NWD { get { return new Point3(x, y, z + d); } }
        public Point3 NED { get { return new Point3(x + w, y, z + d); } }
        public Point3 SWD { get { return new Point3(x, y + h, z + d); } }
        public Point3 SED { get { return new Point3(x + w, y + h, z + d); } }        

        public override bool Equals(object obj)
        {
            if (obj is Box)
            {
                var that = (Box)obj;
                return this.IsEqualTo(that);
            }
            else { return false; }
        }

        public override int GetHashCode()
        {
            return (this.X ^ this.Y.RotateLeft(5) ^ this.Z.RotateLeft(12)
                  ^ this.W.RotateLeft(17) ^ this.H.RotateLeft(22) ^ this.D.RotateLeft(27)).Scramble();
        }

        public override string ToString()
        {
            return string.Format("{0}-{1}", NWU, SED);
        }

        public static Box Parse(string source)
        {
            throw new NotImplementedException();
        }

        public static readonly Box Empty = new Box();
        public static readonly Box Unit = new Box(0, 0, 0, 1, 1, 1);

        public static bool operator ==(Box b1, Box b2)
        {
            return b1.IsEqualTo(b2);
        }

        public static bool operator !=(Box b1, Box b2)
        {
            return !b1.IsEqualTo(b2);
        }
    }

    public static class BoxExt
    {
        public static bool IsEqualTo(this Box b1, Box b2)
        {
            return b1.X == b2.X
                && b1.Y == b2.Y
                && b1.Z == b2.Z
                && b1.W == b2.W
                && b1.H == b2.H
                && b1.D == b2.D;
        }

        public static bool IsEmpty(this Box b)
        {
            return b == Box.Empty;
        }

        public static Box Offset(this Box b, Vector3 v)
        {
            return new Box(b.X + v.X, b.Y + v.Y, b.Z + v.Z, b.W, b.H, b.D);
        }

        public static Box Scale(this Box b, int scalar)
        {
            return new Box(b.X, b.Y, b.Z, b.X * scalar, b.Y * scalar, b.Z * scalar);
        }

        public static bool Contains(this Box box, Box innerBox)
        {
            return box.X <= innerBox.X
                && box.Y <= innerBox.Y
                && box.Z <= innerBox.Z
                && (box.X + box.W) >= (innerBox.X + innerBox.W)
                && (box.Y + box.H) >= (innerBox.Y + innerBox.H)
                && (box.Z + box.D) >= (innerBox.Z + innerBox.D);
        }

        public static bool Contains(this Box b, Point3 p)
        {
            return b.X <= p.X
                && b.Y <= p.Y
                && b.Z <= p.Z
                && (b.X + b.W) >= (p.X)
                && (b.Y + b.H) >= (p.Y)
                && (b.Z + b.D) >= (p.Z);
        }

        public static bool Intersects(this Box b1, Box b2)
        {
            throw new NotImplementedException();
        }

        public static Box IntersectWith(this Box b1, Box b2)
        {
            throw new NotImplementedException();
        }

        public static Box UnionWith(this Box b1, Box b2)
        {
            throw new NotImplementedException();
        }

        public static Box Transform(this Box b, Matrix matrix)
        {
            throw new NotImplementedException();
        }
    }
}
