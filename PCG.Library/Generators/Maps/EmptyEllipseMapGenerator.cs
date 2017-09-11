using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCG.Library.Collections;

namespace PCG.Library.Generators
{
    public class EmptyEllipseMapGenerator : IMapGenerator<int>
    {
        public CellMap<int> Generate(Size3 size, IRandom rng, Configuration config)
        {
            return Generate(size, rng);
        }

        public CellMap<int> Generate(Size3 size, IRandom rng)
        {
            var map = new CellMap<int>(size);

            var rx = size.Width / 2;
            var ry = size.Height / 2;
            var ex = 0;
            var ey = 0;

            if ((size.Width & 1) == 0) { rx--; ex++; }
            if ((size.Height & 1) == 0) { ry--; ey++; }

            PlotEllipse(rx, ry, rx, ry, ex, ey, map);

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

        private void FillCenter(int x, int y, int cx, int cy, int ex, int ey, CellMap<int> map)
        {
            // Iterate tiles up-to-down from ( x + center_x, -y + center_y) to (x + center_x, y + center_y) 
            for (int j = -y + cy; j < y + cy + ey; ++j)
                MarkInternal(x + cx + ex, j, map);

            // Iterate tiles up-to-down from (-x + center_x, -y + center_y) to (-x + center_x,  y + center_y)
            for (int j = -y + cy; j < y + cy + ey; ++j)
                MarkInternal(-x + cx, j, map);
        }

        private void PlotEllipse(int cx, int cy, int rx, int ry, int ex, int ey, CellMap<int> map)
        {
            var x = rx;
            var y = 0;
            var xchange = ry * ry * (1 - 2 * rx);
            var ychange = rx * rx;
            var ellipseError = 0;
            var twoSquareA = 2 * rx * rx;
            var twoSquareB = 2 * ry * ry;
            var stoppingX = twoSquareB * rx;
            var stoppingY = 0;

            //build first set of points
            while (stoppingX >= stoppingY)
            {
                MarkExternal(cx + x + ex, cy + y + ey, map); //point in quadrant 1
                MarkExternal(cx - x, cy + y + ey, map); //point in quadrant 2
                MarkExternal(cx - x, cy - y, map); //point in quadrant 3
                MarkExternal(cx + x + ex, cy - y, map); //point in quadrant 4
                FillCenter(x, y, cx, cy, ex, ey, map);

                y++;
                stoppingY += twoSquareA;
                ellipseError += ychange;
                ychange += twoSquareA;
                if (2 * ellipseError + xchange > 0)
                {
                    x--;
                    stoppingX -= twoSquareB;
                    ellipseError += xchange;
                    xchange += twoSquareB;
                }
            }

            //build second set of points
            x = 0;
            y = ry;
            xchange = ry * ry;
            ychange = rx * rx * (1 - 2 * ry);
            ellipseError = 0;
            stoppingX = 0;
            stoppingY = twoSquareA * ry;

            while (stoppingX <= stoppingY)
            {
                MarkExternal(cx + x + ex, cy + y + ey, map); //point in quadrant 1
                MarkExternal(cx - x, cy + y + ey, map); //point in quadrant 2
                MarkExternal(cx - x, cy - y, map); //point in quadrant 3
                MarkExternal(cx + x + ex, cy - y, map); //point in quadrant 4
                FillCenter(x, y, cx, cy, ex, ey, map);

                x++;
                stoppingX += twoSquareB;
                ellipseError += xchange;
                xchange += twoSquareB;
                if (2 * ellipseError + ychange > 0)
                {
                    y--;
                    stoppingY -= twoSquareA;
                    ellipseError += ychange;
                    ychange += twoSquareA;
                }
            }
        }

        private void MarkExternal(int x, int y, CellMap<int> map)
        {
            map[x, y, 0].Value = 1;
        }

        private void MarkInternal(int x, int y, CellMap<int> map)
        {
            map[x, y, 0].Value = 2;
            //map[x, y, 0].Edges = Direction.XY;
        }
    }
}
