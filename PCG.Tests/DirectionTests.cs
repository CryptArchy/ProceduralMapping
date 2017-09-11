using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PCG.Library;

namespace PCG.Tests
{
    [TestClass]
    public class DirectionTests
    {
        [TestMethod]
        public void OppositeDirectionTest()
        {
            var directions = new Direction[] { Direction.North, Direction.South, Direction.West, Direction.East, Direction.Up, Direction.Down, Direction.Forward, Direction.Backward };
            var expected = new Direction[] { Direction.South, Direction.North, Direction.East, Direction.West, Direction.Down, Direction.Up, Direction.Backward, Direction.Forward };

            for (int i = 0; i < directions.Length; i++)
                Assert.AreEqual(expected[i], directions[i].Opposite);
        }

        [TestMethod]
        public void ParseTest()
        {
            var scenarios = new string[] { 
                "n", "s", "e", "w", "u", "d", "f", "b", 
                "N", "S", "E", "W", "U", "D", "F", "B",
                "north", "south", "east", "west", "up", "down", "forward", "backward",
                "x", "y", "z", "t",
                "xy", "xyz", "xyzt",
                "northeast", "Northwest", 
                "southEast", "SouthWest", 
                "UpEastBackwardSouth"};
            var expecteds = new Direction[] { 
                Direction.North, Direction.South, Direction.East, Direction.West, Direction.Up, Direction.Down, Direction.Forward, Direction.Backward,
                Direction.North, Direction.South, Direction.East, Direction.West, Direction.Up, Direction.Down, Direction.Forward, Direction.Backward,
                Direction.North, Direction.South, Direction.East, Direction.West, Direction.Up, Direction.Down, Direction.Forward, Direction.Backward,
                Direction.X, Direction.Y, Direction.Z, Direction.T,
                Direction.XY, Direction.XYZ, Direction.All,
                Direction.North | Direction.East, Direction.North | Direction.West, 
                Direction.South | Direction.East, Direction.South | Direction.West,
                Direction.Backward | Direction.Up | Direction.South | Direction.East
                };

            for (int i = 0; i < scenarios.Length; i++)
            {
                var actual = Direction.Parse(scenarios[i]);
                Assert.AreEqual(expecteds[i], actual);
            }
        }
    }
}
