using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonExtensions.NumericExtensions;
using PCG.Library.Collections;

namespace PCG.Library.Generators
{
    public class TemplateBinaryDivisionMapGenerator : IMapGenerator<int>
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

            var divisions = 4;

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
