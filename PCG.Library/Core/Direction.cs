using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using PCG.Library.Extensions;
using CommonExtensions.NumericExtensions;

namespace PCG.Library
{
    public struct Direction
    {
        public static readonly Direction Center = new Direction(0);
        public static readonly Direction North = new Direction(1);
        public static readonly Direction South = new Direction(2);
        public static readonly Direction West = new Direction(4);
        public static readonly Direction East = new Direction(8);
        public static readonly Direction Up = new Direction(16);
        public static readonly Direction Down = new Direction(32);
        public static readonly Direction Forward = new Direction(64);
        public static readonly Direction Backward = new Direction(128);
        //public static readonly Direction Ana = new Direction(64);
        //public static readonly Direction Kata = new Direction(128);

        public static readonly Direction NE = North | East;
        public static readonly Direction NW = North | West;
        public static readonly Direction SE = South | East;
        public static readonly Direction SW = South | West;

        public static readonly Direction X = West | East;
        public static readonly Direction Y = North | South;
        public static readonly Direction Z = Up | Down;
        public static readonly Direction T = Forward | Backward;

        public static readonly Direction XY = X | Y;
        public static readonly Direction XZ = X | Z;
        public static readonly Direction YZ = Y | Z;
        public static readonly Direction XT = X | T;
        public static readonly Direction YT = Y | T;
        public static readonly Direction ZT = Z | T;

        public static readonly Direction XYZ = X | Y | Z;
        public static readonly Direction XYT = X | Y | T;
        public static readonly Direction XZT = X | Z | T;
        public static readonly Direction YZT = Y | Z | T;

        public static readonly Direction All = X | Y | Z | T;

        public static readonly List<Direction> ListXYZ = new List<Direction>() 
        { North, West, Up, South, East, Down };
        public static readonly List<Direction> ListXY = new List<Direction>() 
        { North, West, South, East };
        public static readonly List<Direction> ListXZ = new List<Direction>() 
        { West, Up, East, Down };
        public static readonly List<Direction> ListYZ = new List<Direction>() 
        { North, Up, South, Down };
        public static readonly List<Direction> ListPlanes = new List<Direction>() 
        { XY, XZ, YZ };

        private readonly uint Value;
        private Direction(uint value) { Value = value; }

        public override bool Equals(object obj) { return obj is Direction && this.Value == ((Direction)obj).Value; }
        public override int GetHashCode() { return this.Value.GetHashCode(); }
        public override string ToString()
        {
            var result = string.Empty;
            if (this.Includes(Direction.North))
                result += "N";
            if (this.Includes(Direction.South))
                result += "S";
            if (this.Includes(Direction.West))
                result += "W";
            if (this.Includes(Direction.East))
                result += "E";
            if (this.Includes(Direction.Up))
                result += "U";
            if (this.Includes(Direction.Down))
                result += "D";
            if (this.Includes(Direction.Forward))
                result += "F";
            if (this.Includes(Direction.Backward))
                result += "B";
            return result;
        }
        public static Direction Parse(string source)
        {
            Direction parsed = Direction.Center;

            if (string.IsNullOrWhiteSpace(source))
                return parsed;

            source = source.ToLower();

            var dirRegex = new Regex(@"(north)|(south)|(east)|(west)|(left)|(right)|(upper)|(up)|(down)|(lower)|(forward)|(fore)|(backward)|(back)|(ana)|(kata)|[nsewudfbxyztak]", 
                RegexOptions.ExplicitCapture);

            var matches = dirRegex.Matches(source);

            //for (int i = 0; i < source.Length; i++)
            foreach (Match match in matches)
            {
                //switch(source[i])
                switch (match.Value)
                {
                    case "north":
                    case "n":
                        parsed |= Direction.North;
                        break;
                    case "south":
                    case "s":
                        parsed |= Direction.South;
                        break;
                    case "west":
                    case "left":
                    case "w":
                        parsed |= Direction.West;
                        break;
                    case "east":
                    case "right":
                    case "e":
                        parsed |= Direction.East;
                        break;
                    case "upper":
                    case "up":
                    case "u":
                        parsed |= Direction.Up;
                        break;
                    case "lower":
                    case "down":
                    case "d":
                        parsed |= Direction.Down;
                        break;
                    case "fore":
                    case "forward":
                    case "ana":
                    case "f":
                    case "a":
                        parsed |= Direction.Forward;
                        break;
                    case "back":
                    case "backward":
                    case "kata":
                    case "b":
                    case "k":
                        parsed |= Direction.Backward;
                        break;
                    case "x":
                        parsed |= Direction.X;
                        break;
                    case "y":
                        parsed |= Direction.Y;
                        break;
                    case "z":
                        parsed |= Direction.Z;
                        break;
                    case "t":
                        parsed |= Direction.T;
                        break;
                }
            }
            return parsed;
        }

        public static implicit operator Int32(Direction dir) { return (int)dir.Value; }
        public static implicit operator Direction(Int32 b) { return new Direction((uint)b); }
        public static implicit operator UInt32(Direction dir) { return dir.Value; }
        public static implicit operator Direction(UInt32 b) { return new Direction(b); }
        public static bool operator ==(Direction x, Direction y) { return x.Value == y.Value; }
        public static bool operator !=(Direction x, Direction y) { return x.Value != y.Value; }
        public static Direction operator |(Direction x, Direction y) { return new Direction(x.Value | y.Value); }
        public static Direction operator &(Direction x, Direction y) { return new Direction(x.Value & y.Value); }
        public static Direction operator ^(Direction x, Direction y) { return new Direction(x.Value ^ y.Value); }
        public static Direction operator <<(Direction x, int y) { return new Direction(x.Value << y); }
        public static Direction operator >>(Direction x, int y) { return new Direction(x.Value >> y); }
        public static Direction operator ~(Direction x) { return new Direction(~x.Value); }

        public bool Includes(Direction dir) { return (this.Value & dir.Value) == dir.Value; }
        public bool IncludesAny(Direction dir) { return (this.Value & dir.Value) != 0; }
        public bool Excludes(Direction dir) { return (this.Value & dir.Value) == 0; }
        public bool ExcludesAny(Direction dir) { return (this.Value & dir.Value) != dir.Value; }
        public int Count() { return Value.BitCount(); }

        public Direction Opposite
        { 
            get 
            {
                return ((this & 0xAAAAAAAA) >> 1) | ((this & 0x55555555) << 1);
            }
        }

        public Direction CycleXyCw
        {
            get
            {
                if (this == North) return East;
                if (this == East) return South;
                if (this == South) return West;
                if (this == West) return North;
                return this;
            }
        }

        public Direction CycleXyCcw
        {
            get
            {
                if (this == North) return West;
                if (this == West) return South;
                if (this == South) return East;
                if (this == East) return North;
                return this;
            }
        }
    }

    public static class DirectionExt
    {
        public static int MoveX(this int value, Direction dir)
        {
            if (dir.Includes(Direction.East)) return value + 1;
            if (dir.Includes(Direction.West)) return value - 1;
            return value;
        }

        public static int MoveY(this int value, Direction dir)
        {
            if (dir.Includes(Direction.South)) return value + 1;
            if (dir.Includes(Direction.North)) return value - 1;
            return value;
        }

        public static int MoveZ(this int value, Direction dir)
        {
            if (dir.Includes(Direction.Down)) return value + 1;
            if (dir.Includes(Direction.Up)) return value - 1;
            return value;
        }

        public static int MoveT(this int value, Direction dir)
        {
            if (dir.Includes(Direction.Backward)) return value + 1;
            if (dir.Includes(Direction.Forward)) return value - 1;
            return value;
        }

        public static Point2 Move(this Point2 value, Direction dir)
        {
            int x = value.X, y = value.Y;
            if (dir.Includes(Direction.North)) y -= 1;
            if (dir.Includes(Direction.South)) y += 1;
            if (dir.Includes(Direction.West)) x -= 1;
            if (dir.Includes(Direction.East)) x += 1;
            return new Point2(x,y);
        }

        public static Point3 Move(this Point3 value, Direction dir)
        {
            int x = value.X, y = value.Y, z = value.Z;
            if (dir.Includes(Direction.North)) y -= 1;
            if (dir.Includes(Direction.South)) y += 1;
            if (dir.Includes(Direction.West)) x -= 1;
            if (dir.Includes(Direction.East)) x += 1;
            if (dir.Includes(Direction.Up)) z -= 1;
            if (dir.Includes(Direction.Down)) z += 1;
            return new Point3(x, y, z);
        }

        public static Direction DirectionOf(this Point3 p1, Point3 p2)
        {
            Direction dir = Direction.Center;

            if (p1.X < p2.X) dir |= Direction.East;
            else if (p1.X > p2.X) dir |= Direction.West;

            if (p1.Y < p2.Y) dir |= Direction.South;
            else if (p1.Y > p2.Y) dir |= Direction.North;

            if (p1.Z < p2.Z) dir |= Direction.Down;
            else if (p1.Z > p2.Z) dir |= Direction.Up;

            return dir;
        }

        public static Direction NextDirection(this IRandom rng)
        {
            return Direction.ListXYZ[rng.Next(0, Direction.ListXYZ.Count)];
        }

        public static Direction NextDirectionXY(this IRandom rng)
        {
            return Direction.ListXY[rng.Next(0, Direction.ListXYZ.Count)];
        }

        public static Direction NextPlane(this IRandom rng)
        {
            return Direction.ListPlanes[rng.Next(0, Direction.ListPlanes.Count)];
        }

        public static IEnumerable<Direction> NextDirections(this IRandom rng)
        {
            return Direction.ListXYZ.Shuffle(rng).ToList();
        }

        public static IEnumerable<Direction> NextDirectionsXY(this IRandom rng)
        {
            return Direction.ListXY.Shuffle(rng).ToList();
        }

        public static IEnumerable<Direction> NextPlanes(this IRandom rng)
        {
            return Direction.ListPlanes.Shuffle(rng).ToList();
        }
    }
}
