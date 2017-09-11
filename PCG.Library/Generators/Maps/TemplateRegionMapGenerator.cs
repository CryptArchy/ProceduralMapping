using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonExtensions.NumericExtensions;
using PCG.Library.Collections;

namespace PCG.Library.Generators
{
    public class TemplateRegionMapGenerator : IMapGenerator<int>
    {
        private IMapGenerator<int> genRectangle = new EmptyMapGenerator(Direction.XY, false);
        private IMapGenerator<int> genCircle = new EmptyCircleMapGenerator();
        private IMapGenerator<int> genEllipse = new EmptyEllipseMapGenerator();
        private IMapGenerator<int> genPath = GrowingTreeMazeGenerator.TakeNewestCellStrategy;
        private IPathFinder pathFinder = new Intelligence.AStarPathFinder();
        private CellMap<int> empty;

        public CellMap<int> Generate(Size3 size, IRandom rng, Configuration config)
        {
            return Generate(size, rng);
        }

        public CellMap<int> Generate(Size3 size, IRandom rng)
        {
            var map = new CellMap<int>(size);
            empty = genRectangle.Generate(size, rng);

            var regionFactor = 4;
            var regions = genPath.Generate(new Size3(regionFactor, regionFactor, 1), rng);

            var regionSize = new Size3(
                (size.Width / regionFactor).GreaterOf(1),
                (size.Height / regionFactor).GreaterOf(1),
                (size.Depth / regionFactor).GreaterOf(1));

            var halfSize = new Size3(regionSize.Width / 2, regionSize.Height / 2, 1);            

            for (int y = 0; y < regionFactor; y++)
            {
                for (int x = 0; x < regionFactor; x++)
                {
                    var regionCenter = new Point3(x*regionSize.Width + halfSize.Width, y*regionSize.Height + halfSize.Height, 0);
                    var featureSize = new Size3(rng.Next(3, regionSize.Width + 1), rng.Next(3, regionSize.Height + 1), 1);
                    var featureStart = new Point3(
                        x * regionSize.Width + rng.Next(0, regionSize.Width - featureSize.Width),
                        y * regionSize.Height + rng.Next(0, regionSize.Height - featureSize.Height),
                        0);
                    Point3 featureCenter = new Point3(featureStart.X + featureSize.Width / 2, featureStart.Y + featureSize.Height / 2, 0);

                    var next = rng.Next(1, 10);
                    if (next.Between(1, 7))
                    {
                        MakeRectangularRoom(featureSize, rng, map, featureStart.X, featureStart.Y);
                    }
                    else if (next == 8)
                    {
                        MakeCircularRoom(featureSize, rng, map, featureStart.X, featureStart.Y);
                        var smallestDimension = featureSize.Width.LesserOf(featureSize.Height);
                        featureCenter = new Point3(featureStart.X + smallestDimension / 2, featureStart.Y + smallestDimension / 2, 0);
                    }
                    else if (next == 9)
                    {
                        MakeEllipticalRoom(featureSize, rng, map, featureStart.X, featureStart.Y);
                    }

                    CarvePath(map, regionCenter, featureCenter);

                    var connections = regions[x, y, 0].Edges;
                    if (y != 0 && connections.Includes(Direction.North))
                        CarvePath(map, regionCenter, new Point3(regionCenter.X, regionCenter.Y - regionSize.Height, 0));
                    if (y != regionFactor-1 && connections.Includes(Direction.South))
                        CarvePath(map, regionCenter, new Point3(regionCenter.X, regionCenter.Y + regionSize.Height, 0));
                    if (x != 0 && connections.Includes(Direction.West))
                        CarvePath(map, regionCenter, new Point3(regionCenter.X - regionSize.Width, regionCenter.Y, 0));
                    if (x != regionFactor - 1 && connections.Includes(Direction.East))
                        CarvePath(map, regionCenter, new Point3(regionCenter.X + regionSize.Width, regionCenter.Y, 0));
                }
            }

            for (int x = 0; x < size.Width; x++)
            {
                map[x, 0, 0].Edges &= ~Direction.North;
                map[x, size.Height - 1, 0].Edges &= ~Direction.South;
            }

            for (int y = 0; y < size.Height; y++)
            {
                map[0, y, 0].Edges &= ~Direction.West;
                map[size.Width - 1, y, 0].Edges &= ~Direction.East;
            }

            return map;
        }

        public Configuration GetDefaultConfig()
        {
            var config = new Configuration();
            return config;
        }

        private void MakeRectangularRoom(Size3 size, IRandom rng, CellMap<int> map, int x, int y)
        {
            map.CopyFrom(genRectangle.Generate(size, rng), new Point3(x, y, 0));
        }

        private void MakeCircularRoom(Size3 size, IRandom rng, CellMap<int> map, int x, int y)
        {
            var squareSize = new Size3(size.Width.LesserOf(size.Height), size.Height.LesserOf(size.Width), size.Depth);
            map.CopyFrom(genCircle.Generate(squareSize, rng), new Point3(x, y, 0));
        }

        private void MakeEllipticalRoom(Size3 size, IRandom rng, CellMap<int> map, int x, int y)
        {
            map.CopyFrom(genEllipse.Generate(size, rng), new Point3(x, y, 0));
        }

        private void CarvePath(CellMap<int> map, Point3 start, Point3 goal)
        {
            var path = pathFinder.FindRoute(empty, start, goal);
            var pos = start;
            foreach (var step in path)
            {
                map[pos].Value = 200;
                pos = map.AddBorder(pos, step);
            }
            map[pos].Value = 200;
        }
    }
}
