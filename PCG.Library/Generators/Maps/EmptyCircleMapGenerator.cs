using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonExtensions.NumericExtensions;
using PCG.Library.Collections;

namespace PCG.Library.Generators
{
    public class EmptyCircleMapGenerator : IMapGenerator<int>
    {
        public CellMap<int> Generate(Size3 size, IRandom rng, Configuration config)
        {
            return Generate(size, rng);
        }

        public CellMap<int> Generate(Size3 size, IRandom rng)
        {
            var map = new CellMap<int>(size);
            var smallAxis = size.Width.LesserOf(size.Height);
            var r = smallAxis / 2;
            var xc = r;
            var yc = r;
            var isEvenSize = (smallAxis & 1) == 0;

            if (isEvenSize) 
                PlotCircleEven(--xc, --yc, --r, 0, map); 
            else
                PlotCircleOdd(xc, yc, r, 0, map);

            //var southwest = new List<Direction>(){Direction.South, Direction.West};
            //foreach (var cell in map)
            //{
            //    if (cell.Cell.Value != 0)
            //    {
            //        foreach (var edge in southwest)
            //        {
            //            var neighbor = map.TryGet(cell.Position.Move(edge));
            //            if (neighbor != null && neighbor.Value != 0)
            //            {
            //                if (cell.Cell.Value == 1)
            //                    map.AddEdge(cell.Cell, edge);
            //                if (neighbor.Value == 1)
            //                    map.AddEdge(neighbor, edge.Opposite);
            //            }
            //        }
            //    }
            //}

            foreach (var cell in map)
            {
                if (cell.Value != 0)
                {
                    foreach (var edge in Direction.ListXY)
                    {
                        var neighbor = map.TryGet(cell.Position.Move(edge));
                        if (neighbor != null && neighbor.Value != 0)
                            map.AddEdge(cell.Position, edge);
                    }
                }
            }

            return map;
        }

        public Configuration GetDefaultConfig()
        {
            var config = new Configuration();
            return config;
        }

        private void TestPosition()
        {

        }

        private void PlotCircleOdd(int xc, int yc, int r, int color, CellMap<int> map)
        {
            if (r <= 0) return;

            int x = r, y = 0;//local coords     
            int cd2 = 0;    //current distance squared - radius squared

            MarkExternal(xc - r, yc, map);
            MarkExternal(xc + r, yc, map);
            MarkExternal(xc, yc - r, map);
            MarkExternal(xc, yc + r, map);

            for (int j = xc - r; j < xc + r; ++j)
                MarkInternal(xc, j, map);

            while (x > y)    //only formulate 1/8 of circle
            {
                cd2 -= (--x) - (++y);
                if (cd2 < 0) cd2 += x++;

                MarkExternal(xc - x, yc - y, map);//upper left left
                MarkExternal(xc - y, yc - x, map);//upper upper left
                MarkExternal(xc + y, yc - x, map);//upper upper right
                MarkExternal(xc + x, yc - y, map);//upper right right
                MarkExternal(xc - x, yc + y, map);//lower left left
                MarkExternal(xc - y, yc + x, map);//lower lower left
                MarkExternal(xc + y, yc + x, map);//lower lower right
                MarkExternal(xc + x, yc + y, map);//lower right right              

                // Iterate tiles up-to-down from ( x + center_x, -y + center_y) to (x + center_x, y + center_y) 
                for (int j = -y + yc; j < y + yc; ++j)
                    MarkInternal(x + xc, j, map);

                // Iterate tiles up-to-down from ( y + center_x, -x + center_y) to ( y + center_x,  x + center_y)
                for (int j = -x + yc; j < x + yc; ++j)
                    MarkInternal(y + xc, j, map);

                // Iterate tiles up-to-down from (-x + center_x, -y + center_y) to (-x + center_x,  y + center_y)
                for (int j = -y + yc; j < y + yc; ++j)
                    MarkInternal(-x + xc, j, map);

                // Iterate tiles up-to-down from (-y + center_x, -x + center_y) to (-y + center_x, x + center_y)
                for (int j = -x + yc; j < x + yc; ++j)
                    MarkInternal(-y + xc, j, map);
            }
        }

        private void PlotCircleEven(int xc, int yc, int r, int color, CellMap<int> map)
        {
            if (r <= 0) return;

            int x = r, y = 0;//local coords     
            int cd2 = 0;    //current distance squared - radius squared

            MarkExternal(xc - r, yc, map);
            MarkExternal(xc + 1 + r, yc, map);
            MarkExternal(xc, yc - r, map);
            MarkExternal(xc, yc + 1 + r, map);

            MarkExternal(xc - r, yc + 1, map);
            MarkExternal(xc + 1 + r, yc + 1, map);
            MarkExternal(xc + 1, yc - r, map);
            MarkExternal(xc + 1, yc + 1 + r, map);

            for (int j = xc - r; j < xc + 1 + r; ++j)
            {
                MarkInternal(xc, j, map);
                MarkInternal(xc + 1, j, map);
            }

            while (x > y)    //only formulate 1/8 of circle
            {
                cd2 -= (--x) - (++y);
                if (cd2 < 0) cd2 += x++;

                MarkExternal(xc - x, yc - y, map);//upper left left
                MarkExternal(xc - y, yc - x, map);//upper upper left
                MarkExternal(xc + 1 + y, yc - x, map);//upper upper right
                MarkExternal(xc + 1 + x, yc - y, map);//upper right right
                MarkExternal(xc - x, yc + 1 + y, map);//lower left left
                MarkExternal(xc - y, yc + 1 + x, map);//lower lower left
                MarkExternal(xc + 1 + y, yc + 1 + x, map);//lower lower right
                MarkExternal(xc + 1 + x, yc + 1 + y, map);//lower right right  

                // Iterate tiles up-to-down from ( x + center_x, -y + center_y) to (x + center_x, y + center_y) 
                for (int j = -y + yc; j < y + yc + 1; ++j)
                    MarkInternal(x + xc + 1, j, map);

                // Iterate tiles up-to-down from ( y + center_x, -x + center_y) to ( y + center_x,  x + center_y)
                for (int j = -x + yc; j < x + yc + 1; ++j)
                    MarkInternal(y + xc + 1, j, map);

                // Iterate tiles up-to-down from (-x + center_x, -y + center_y) to (-x + center_x,  y + center_y)
                for (int j = -y + yc; j < y + yc + 1; ++j)
                    MarkInternal(-x + xc, j, map);

                // Iterate tiles up-to-down from (-y + center_x, -x + center_y) to (-y + center_x, x + center_y)
                for (int j = -x + yc; j < x + yc + 1; ++j)
                    MarkInternal(-y + xc, j, map);
            }
        }

        private void MarkExternal(int x, int y, CellMap<int> map)
        {
            map[x, y, 0].Value = 1;
            //map[x, y, 0].Edges |= openEdges;
        }

        private void MarkInternal(int x, int y, CellMap<int> map)
        {
            map[x, y, 0].Value = 2;
            //map[x, y, 0].Edges = Direction.XY;
        }
    }
}
