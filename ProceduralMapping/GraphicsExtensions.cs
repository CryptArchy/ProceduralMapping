using PCG.Library;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCG.Library.Collections;

namespace ProceduralMapping
{
    public enum CellMapDisplayMode
    {
        FlatColor,        
        EdgeValue,
        CellValue,
        CellValueByte
    }

    public static class GraphicsExtensions
    {
        public static List<Bitmap> Draw(this CellMap<int> map, bool treatEdgesAsWalls, int cellWidth, int cellHeight, CellMapDisplayMode displayMode, int flatColor = 0)
        {            
            var layers = new List<Bitmap>();
            var vert = new Size(0, cellHeight - 1);
            var horz = new Size(cellWidth - 1, 0);

            var cellOffsetX = cellWidth;
            var cellOffsetY = cellHeight;
            var imageWidth = map.Width * cellWidth + cellOffsetX*2;
            var imageHeight = map.Height * cellHeight + cellOffsetY*2;

            Bitmap image;
            Graphics graphics = null;
            
            foreach (var cell in map)
            {
                if (cell.Position.Z >= layers.Count)
                {
                    image = new Bitmap(imageWidth, imageHeight);
                    layers.Add(image);
                    if (graphics != null)
                        graphics.Dispose();
                    graphics = Graphics.FromImage(image);
                    graphics.Clear(Color.White);

                    var fontSize = 6f;
                    for (int x = 0; x < map.Width; x++)
                        graphics.DrawString(x.ToString(), new Font(FontFamily.GenericSansSerif, fontSize), Brushes.Black, x * cellWidth + cellOffsetX, 0);
                    for (int y = 0; y < map.Height; y++)
                        graphics.DrawString(y.ToString(), new Font(FontFamily.GenericSansSerif, fontSize), Brushes.Black, 0, y * cellHeight + cellOffsetY);
                    graphics.DrawString(cell.Position.Z.ToString(), new Font(FontFamily.GenericSansSerif, fontSize), Brushes.Black, 0, 0);
                }

                var nw = new Point(cell.Position.X * cellWidth + cellOffsetX, cell.Position.Y * cellHeight + cellOffsetY);
                var ne = nw + horz;
                var sw = nw + vert;
                var se = nw + horz + vert;

                Color colFloor;
                int rgb;
                switch (displayMode)
                {
                    case CellMapDisplayMode.CellValue:
                        colFloor = Color.FromArgb(cell.Value);
                        break;
                    case CellMapDisplayMode.CellValueByte:
                        rgb = (byte)(cell.Value);
                        colFloor = Color.FromArgb(rgb, rgb, rgb);
                        break;
                    case CellMapDisplayMode.EdgeValue:
                        rgb = (byte)((cell.Edges & Direction.XY).Count() * 63);
                        colFloor = Color.FromArgb(rgb, rgb, rgb);
                        break;
                    case CellMapDisplayMode.FlatColor:
                    default:
                        colFloor = Color.FromArgb(flatColor);
                        break;
                }

                Pen wallPen = Pens.Black;
                Pen hallPen = new Pen(colFloor);
                Brush floorBrush = new SolidBrush(colFloor);

                bool drawUp = cell.Edges.Includes(Direction.Up) ^ treatEdgesAsWalls;
                bool drawDown = cell.Edges.Includes(Direction.Down) ^ treatEdgesAsWalls;
                var nwFloor = new Point3(nw.X + 1, nw.Y + 1, 0);
                var szFloor = new Size3(cellWidth - 2, cellHeight - 2, 0);

                //if (drawUp && drawDown)
                //    graphics.DrawMarker(nwFloor, szFloor, Color.LightSlateGray, Color.DarkSlateBlue);
                //else if (drawUp)
                //    graphics.DrawMarker(nwFloor, szFloor, colFloor, Color.Black);
                //else if (drawDown)
                //    graphics.DrawMarker(nwFloor, szFloor, Color.Black, colFloor);

                if (drawUp && drawDown)
                    floorBrush = CreateRadialBrush(nw.X, nw.Y, szFloor.Width, szFloor.Height, Color.Black, colFloor, Color.Black);
                else if (drawUp)
                    floorBrush = new LinearGradientBrush(new Point(nw.X, nw.Y), new Point(nw.X + szFloor.Width, nw.Y + szFloor.Height), colFloor, Color.DarkSlateBlue);
                    //floorBrush = CreateRadialBrush(nw.X, nw.Y, szFloor.Width, szFloor.Height, colFloor, Color.Black);
                else if (drawDown)
                    floorBrush = new LinearGradientBrush(new Point(nw.X, nw.Y), new Point(nw.X + szFloor.Width, nw.Y + szFloor.Height), Color.DarkSlateBlue, colFloor);
                    //floorBrush = CreateRadialBrush(nw.X, nw.Y, szFloor.Width, szFloor.Height, Color.Black, colFloor);

                graphics.FillRectangle(floorBrush, nwFloor.X, nwFloor.Y, szFloor.Width, szFloor.Height);


                if (cell.Edges.Includes(Direction.North) != treatEdgesAsWalls)
                    graphics.DrawLine(hallPen, nw, ne);
                if (cell.Edges.Includes(Direction.South) != treatEdgesAsWalls)
                    graphics.DrawLine(hallPen, sw, se);
                if (cell.Edges.Includes(Direction.West) != treatEdgesAsWalls)
                    graphics.DrawLine(hallPen, nw, sw);
                if (cell.Edges.Includes(Direction.East) != treatEdgesAsWalls)
                    graphics.DrawLine(hallPen, ne, se);


                if (cell.Edges.Includes(Direction.North) == treatEdgesAsWalls)
                    graphics.DrawLine(wallPen, nw, ne);
                if (cell.Edges.Includes(Direction.South) == treatEdgesAsWalls)
                    graphics.DrawLine(wallPen, sw, se);
                if (cell.Edges.Includes(Direction.West) == treatEdgesAsWalls)
                    graphics.DrawLine(wallPen, nw, sw);
                if (cell.Edges.Includes(Direction.East) == treatEdgesAsWalls)
                    graphics.DrawLine(wallPen, ne, se);
            }

            return layers;
        }

        public static void DrawPath(this List<Bitmap> layers, List<Direction> path, Point3 start, Point3 end, int cellWidth, int cellHeight)
        {
            Graphics graphics;
            var graphicsSet = new List<Graphics>();
            foreach (var image in layers)
                graphicsSet.Add(Graphics.FromImage(image));

            var cellOffsetX = cellWidth;
            var cellOffsetY = cellHeight;
            Point3 cp = start;
            Point3 np = start;

            foreach (var step in path)
            {
                graphics = graphicsSet[cp.Z];

                np = cp.Move(step);
                var cur = new Point(cp.X * cellWidth + (cellWidth / 2) + cellOffsetX, cp.Y * cellHeight + (cellHeight / 2) + cellOffsetY);
                var nxt = new Point(np.X * cellWidth + (cellWidth / 2) + cellOffsetX, np.Y * cellHeight + (cellHeight / 2) + cellOffsetY);
                cp = np;

                graphics.DrawLine(Pens.Red, cur, nxt);
            }

            var szMarker = new Size3(cellWidth, cellHeight, 1);
            var pntStart = new Point3(start.X, start.Y, start.Z);
            var pntEnd = new Point3(end.X, end.Y, end.Z);

            graphics = graphicsSet[pntStart.Z];
            graphics.DrawMarker(pntStart, szMarker, Color.Red, Color.Black);
            graphics = graphicsSet[pntEnd.Z];
            graphics.DrawMarker(pntEnd, szMarker, Color.Black, Color.Red);

            foreach (var g in graphicsSet)
                g.Dispose();           
        }

        public static void DrawCellMap(this Graphics graphics, CellMap<int> map, bool treatEdgesAsWalls, int cellWidth, int cellHeight, int layer)
        {
            var vert = new Size(0, cellHeight-1);
            var horz = new Size(cellWidth-1, 0);

            foreach (var cell in map)
            {
                var nw = new Point(cell.Position.X * cellWidth, cell.Position.Y * cellHeight);
                var ne = nw + horz;
                var sw = nw + vert;
                var se = nw + horz + vert;
                
                var rgb = (byte)((cell.Edges & Direction.XY).Count() * 50 + 50);//(byte)(255 - cell.Value);
                Color colFloor = Color.FromArgb(rgb, rgb, rgb);
                Pen wallPen = Pens.Black;
                Pen hallPen = new Pen(colFloor);
                Brush floorBrush = new SolidBrush(colFloor);

                bool drawUp = cell.Edges.Includes(Direction.Up) ^ treatEdgesAsWalls;
                bool drawDown = cell.Edges.Includes(Direction.Down) ^ treatEdgesAsWalls;
                var nwFloor = new Point3(nw.X + 1, nw.Y + 1, 0);
                var szFloor = new Size3(cellWidth - 2, cellHeight - 2, 0);


                graphics.FillRectangle(floorBrush, nwFloor.X, nwFloor.Y, szFloor.Width, szFloor.Height);

                if (drawUp && drawDown)
                    graphics.DrawMarker(nwFloor, szFloor, Color.LightSlateGray, Color.DarkSlateBlue);
                else if (drawUp)
                    graphics.DrawMarker(nwFloor, szFloor, colFloor, Color.Black);
                else if (drawDown)
                    graphics.DrawMarker(nwFloor, szFloor, Color.Black, colFloor);


                if (cell.Edges.Includes(Direction.North) != treatEdgesAsWalls)
                    graphics.DrawLine(hallPen, nw, ne);
                if (cell.Edges.Includes(Direction.South) != treatEdgesAsWalls)
                    graphics.DrawLine(hallPen, sw, se);
                if (cell.Edges.Includes(Direction.West) != treatEdgesAsWalls)
                    graphics.DrawLine(hallPen, nw, sw);
                if (cell.Edges.Includes(Direction.East) != treatEdgesAsWalls)
                    graphics.DrawLine(hallPen, ne, se);


                if (cell.Edges.Includes(Direction.North) == treatEdgesAsWalls)
                    graphics.DrawLine(wallPen, nw, ne);                
                if (cell.Edges.Includes(Direction.South) == treatEdgesAsWalls)
                    graphics.DrawLine(wallPen, sw, se);
                if (cell.Edges.Includes(Direction.West) == treatEdgesAsWalls)
                    graphics.DrawLine(wallPen, nw, sw);
                if (cell.Edges.Includes(Direction.East) == treatEdgesAsWalls)
                    graphics.DrawLine(wallPen, ne, se);
            }
        }

        public static void DrawCellEdges(this Graphics graphics, bool treatEdgesAsWalls, Direction edges, Point nw, Point ne, Point sw, Point se)
        {
            if (edges.Includes(Direction.North) == treatEdgesAsWalls)
                graphics.DrawLine(Pens.Black, nw, ne);
            if (edges.Includes(Direction.South) == treatEdgesAsWalls)
                graphics.DrawLine(Pens.Black, sw, se);
            if (edges.Includes(Direction.West) == treatEdgesAsWalls)
                graphics.DrawLine(Pens.Black, nw, sw);
            if (edges.Includes(Direction.East) == treatEdgesAsWalls)
                graphics.DrawLine(Pens.Black, ne, se);

            //if ((edges & Direction.North) == 0)
            //    graphics.DrawLine(Pens.Black, ne, nw);
            //if ((edges & Direction.South) == 0)
            //    graphics.DrawLine(Pens.Black, se, sw);
            //if ((edges & Direction.East) == 0)
            //    graphics.DrawLine(Pens.Black, nw, sw);
            //if ((edges & Direction.West) == 0)
            //    graphics.DrawLine(Pens.Black, ne, se);
        }

        public static void DrawCellContents(this Graphics graphics, bool treatEdgesAsWalls, Cell<int> cell, Point nw, Point se)
        {
            var fillW = Math.Abs(se.X - nw.X);
            var fillH = Math.Abs(se.Y - nw.Y);
            var rgb = (byte)((cell.Edges & Direction.XY).Count()*50 + 50);//(byte)(255 - cell.Value);
            Color colFloor = Color.FromArgb(rgb, rgb, rgb);
            Brush brush;
            bool drawUp = cell.Edges.Includes(Direction.Up) ^ treatEdgesAsWalls;
            bool drawDown = cell.Edges.Includes(Direction.Down) ^ treatEdgesAsWalls;
            
            if (drawUp && drawDown)
                brush = CreateRadialBrush(nw.X, nw.Y, fillW, fillH, Color.Black, colFloor, Color.Black);
            else if (drawUp)
                brush = CreateRadialBrush(nw.X, nw.Y, fillW, fillH, colFloor, Color.Black);
            //brush = new LinearGradientBrush(new Point(0, 0), new Point(10, 10), Color.LightBlue, Color.White);
            else if (drawDown)
                brush = CreateRadialBrush(nw.X, nw.Y, fillW, fillH, Color.Black, colFloor);
            //brush = new LinearGradientBrush(new Point(0, 0), new Point(10, 10), Color.White, Color.LightBlue);
            else
                brush = new SolidBrush(colFloor);

            graphics.FillRectangle(brush, nw.X, nw.Y, fillW, fillH);
        }

        public static void DrawPath(this Graphics graphics, Point3 start, Point3 end, List<Direction> path, int cellWidth, int cellHeight)
        {
            Point3 cp = start;
            Point3 np = start;
            var cellOffsetX = cellWidth;
            var cellOffsetY = cellHeight;

            foreach (var step in path)
            {
                np = cp.Move(step);
                var cur = new Point(cp.X * cellWidth + (cellWidth / 2) + cellOffsetX, cp.Y * cellHeight + (cellHeight / 2) + cellOffsetY);
                var nxt = new Point(np.X * cellWidth + (cellWidth / 2) + cellOffsetX, np.Y * cellHeight + (cellHeight / 2) + cellOffsetY);
                cp = np;

                graphics.DrawLine(Pens.Red, cur, nxt);
            }

            var szMarker = new Size3(cellWidth - 2, cellHeight - 2, 1);
            var pntStart = new Point3(start.X, start.Y, start.Z);
            var pntEnd = new Point3(end.X, end.Y, end.Z);

            graphics.DrawMarker(pntStart, szMarker, Color.Red, Color.Black);
            graphics.DrawMarker(pntEnd, szMarker, Color.Black, Color.Red);
        }

        public static void DrawMarker(this Graphics graphics, Point3 pos, Size3 cellSize, Color inner, params Color[] outer)
        {
            var cellOffsetX = cellSize.Width;
            var cellOffsetY = cellSize.Height;

            var brush = CreateRadialBrush(
                pos.X * cellSize.Width + cellOffsetX,
                pos.Y * cellSize.Height + cellOffsetY,
                cellSize.Width, cellSize.Height,
                inner, outer);

            graphics.FillEllipse(
                brush,
                pos.X * cellSize.Width + cellOffsetX,
                pos.Y * cellSize.Height + cellOffsetY,
                cellSize.Width, cellSize.Height);
        }

        public static Brush CreateRadialBrush(int x, int y, int w, int h, Color center, params Color[] surround)
        {
            var path = new GraphicsPath();
            path.AddEllipse(x, y, w, h);
            var pgb = new PathGradientBrush(path);
            pgb.CenterColor = center;
            pgb.SurroundColors = surround;
            return pgb;
        }
    }
}
