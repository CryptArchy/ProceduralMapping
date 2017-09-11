using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonExtensions.NumericExtensions;

namespace PCG.Library.Collections
{
    public class CellMap<T> : IEnumerable<CellWrapper<T>>
    {
        public readonly Size3 Size;
        protected Cell<T>[] Cells { get; set; }

        public int Width { get { return Size.Width; } }
        public int Height { get { return Size.Height; } }
        public int Depth { get { return Size.Depth; } }
        public int Count { get { return Size.Volume; } }

        public CellMap(Size3 size, Action<CellWrapper<T>> initializer)
        {
            Size = size;
            Cells = new Cell<T>[Size.Volume];
            int index = 0;
            for (int z = 0; z < Size.Depth; z++)
                for (int y = 0; y < Size.Height; y++)
                    for (int x = 0; x < Size.Width; x++)
                    {
                        var cell = new Cell<T>();
                        Cells[index] = cell;
                        if (initializer != null)
                            initializer(new CellWrapper<T>(index, new Point3(x,y,z), this, cell));
                        index++;
                    }
        }

        public CellMap(Size3 size)
            : this(size, (Action<CellWrapper<T>>)null) { }

        public CellMap(int width, int height, int depth)
            : this(new Size3(width, height, depth)) { }

        public CellMap(Size3 size, IEnumerable<Cell<T>> cells)
        {
            Size = size;
            Cells = cells.ToArray();
        }

        public Cell<T> this[int i]
        {
            get { return Cells[i]; }
            set { Cells[i] = value; }
        }

        public Cell<T> this[int x, int y, int z]
        {
            get { return Cells[PosToIdx(x, y, z)]; }
            set { Cells[PosToIdx(x, y, z)] = value; }
        }

        public Cell<T> this[Point3 p]
        {
            get { return Cells[PosToIdx(p)]; }
            set { Cells[PosToIdx(p)] = value; }
        }

        public bool IsInBounds(int x, int y, int z)
        {
            return x.BetweenMin(0, Size.Width) && y.BetweenMin(0, Size.Height) && z.BetweenMin(0, Size.Depth);
        }

        public bool IsInBounds(Point3 p)
        {
            return IsInBounds(p.X, p.Y, p.Z);
        }

        public bool TryGet(Point3 p, out Cell<T> cell)
        {
            if (IsInBounds(p))
            {
                cell = this[p];
                return true;
            }
            else
            {
                cell = null;
                return false;
            }
        }

        public Cell<T> TryGet(int x, int y, int z)
        {
            if (IsInBounds(x,y,z))
                return this[x,y,z];
            else
                return null;
        }

        public Cell<T> TryGet(Point3 p)
        {
            return TryGet(p.X, p.Y, p.Z);
        }

        public int PosToIdx(int x, int y, int z)
        {
            return x + y * Size.Width + z * Size.Width * Size.Height;
        }

        public int PosToIdx(Point3 pos)
        {
            return PosToIdx(pos.X, pos.Y, pos.Z);
        }

        public Point3 IdxToPos(int index)
        {
            return new Point3(index % Size.Width,
                index / Size.Width % Size.Height,
                index / (Size.Width * Size.Height));
        }

        #region Enumerators
        public IEnumerator<CellWrapper<T>> GetEnumerator()
        {
            for (int z = 0; z < Size.Depth; z++)
                for (int y = 0; y < Size.Height; y++)
                    for (int x = 0; x < Size.Width; x++)
                        yield return new CellWrapper<T>(new Point3(x, y, z), this);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerable<CellWrapper<T>> Enumerate(Point3? start = null, Size3? size = null)
        {
            if (!start.HasValue) start = new Point3(0, 0, 0);
            if (!size.HasValue) size = this.Size;

            for (int z = start.Value.Z; z < size.Value.Depth; z++)
                for (int y = start.Value.Y; y < size.Value.Height; y++)
                    for (int x = start.Value.X; x < size.Value.Width; x++)
                        yield return new CellWrapper<T>(new Point3(x, y, z), this);
        }

        public IEnumerable<IEnumerable<CellWrapper<T>>> Layers()
        {
            for (int z = 0; z < Size.Depth; z++)
                yield return Layer(z);
        }

        public IEnumerable<CellWrapper<T>> Layer(int z)
        {
            for (int y = 0; y < Size.Height; y++)
                for (int x = 0; x < Size.Width; x++)
                    yield return new CellWrapper<T>(new Point3(x, y, z), this);
        }
                
        public IEnumerable<CellWrapper<T>> Shaft(int x, int y)
        {
            for (int z = 0; z < Size.Depth; z++)
                yield return new CellWrapper<T>(new Point3(x, y, z), this);
        }

        public IEnumerable<IEnumerable<CellWrapper<T>>> Columns(int z = 0)
        {
            for (int x = 0; x < Size.Width; x++)
                yield return Column(x, z);
        }

        public IEnumerable<CellWrapper<T>> Column(int x, int z = 0)
        {
            for (int y = 0; y < Size.Height; y++)
                yield return new CellWrapper<T>(new Point3(x, y, z), this);
        }

        public IEnumerable<IEnumerable<CellWrapper<T>>> Rows(int z = 0)
        {
            for (int y = 0; y < Size.Height; y++)
                yield return Row(y, z);
        }

        public IEnumerable<CellWrapper<T>> Row(int y, int z = 0)
        {
            for (int x = 0; x < Size.Width; x++)
                yield return new CellWrapper<T>(new Point3(x, y, z), this);
        }

        public IEnumerable<IEnumerable<CellWrapper<T>>> Regions(Size3 size)
        {
            var origin = new Point3(0, 0, 0);
            while (true)
            {
                yield return Region(size, origin);
                if (origin.X < this.Width-1)
                    origin = new Point3(origin.X + size.Width, origin.Y, origin.Z);
                else
                {
                    if (origin.Y < this.Height-1)
                        origin = new Point3(0, origin.Y + size.Height, origin.Z);
                    else
                    {
                        if (origin.Z < this.Depth-1)
                            origin = new Point3(0, 0, origin.Z + size.Depth);
                        else
                            break;
                    }
                }
            }
        }

        public IEnumerable<CellWrapper<T>> Region(Size3 size, Point3 origin)
        {
            for (int z = origin.Z; z < size.Depth; z++)
                for (int y = origin.Y; y < size.Height; y++)
                    for (int x = origin.Z; x < size.Width; x++)
                    {
                        var p = new Point3(x, y, z);
                        if (IsInBounds(p))
                            yield return new CellWrapper<T>(p, this);
                        else
                            yield break;
                    }
        }

        public IEnumerable<Point3> TraversableNeighbors<T>(Point3 point)
        {
            var cc = this[point];
            foreach (var dir in Direction.ListXY)
                if (cc.Edges.Includes(dir))
                    yield return point.Move(dir);
        }
        #endregion

        #region Edges and Borders
        public CellMap<T> AddEdge(Point3 cellPos, Direction edge)
        {
            var cell = TryGet(cellPos);
            if (cell != null)
                AddEdge(cell, edge);
            return this;
        }

        public CellMap<T> AddEdge(Cell<T> cell, Direction edge)
        {
            cell.Edges |= edge;
            return this;
        }

        public CellMap<T> RemoveEdge(Point3 cellPos, Direction edge)
        {
            var cell = TryGet(cellPos);
            if (cell != null)
                RemoveEdge(cell, edge);
            return this;
        }

        public CellMap<T> RemoveEdge(Cell<T> cell, Direction edge)
        {
            cell.Edges &= ~edge;
            return this;
        }

        public void AddBorders(Point3 start, IEnumerable<Direction> dirs)
        {
            foreach (var dir in dirs) AddBorder(start, dir);
        }

        public void AddBorders(Point3 start, params Direction[] dirs)
        {
            foreach (var dir in dirs) AddBorder(start, dir);
        }

        public Point3 AddBorder(Point3 start, Direction dir)
        {
            return AddBorder(start, start.Move(dir), dir);
        }

        public Point3 AddBorder(Point3 start, Point3 dest)
        {
            return AddBorder(start, dest, start.DirectionOf(dest));
        }

        public Point3 AddBorder(Point3 start, Point3 dest, Direction dir)
        {
            if (this.IsInBounds(dest) && this.IsInBounds(start))
            {
                this[start].Edges |= dir;
                this[dest].Edges |= dir.Opposite;
                return dest;
            }
            return start;
        }

        public void RemoveBorders(Point3 start, IEnumerable<Direction> dirs)
        {
            foreach (var dir in dirs) RemoveBorder(start, dir);
        }

        public void RemoveBorders(Point3 start, params Direction[] dirs)
        {
            foreach (var dir in dirs) RemoveBorder(start, dir);
        }

        public Point3 RemoveBorder(Point3 start, Direction dir)
        {
            return RemoveBorder(start, start.Move(dir), dir);
        }

        public Point3 RemoveBorder(Point3 start, Point3 dest)
        {
            return RemoveBorder(start, dest, start.DirectionOf(dest));
        }

        public Point3 RemoveBorder(Point3 start, Point3 dest, Direction dir)
        {
            if (this.IsInBounds(dest) && this.IsInBounds(start))
            {
                this[start].Edges &= ~dir;
                this[dest].Edges &= ~dir.Opposite;
                return dest;
            }
            return start;
        }
        #endregion

        public CellMap<T> Submap(Point3 origin, Size3 size)
        {
            return new CellMap<T>(size, this.Enumerate(origin, size).Cast<Cell<T>>());
        }

        public void CopyFrom(CellMap<T> source, Point3 start)
        {
            var edgesToTest = new List<Direction>();

            foreach (var cell in source)
            {
                var pos = start + cell.Position;
                this[pos] = (Cell<T>)cell;

                if (cell.Position.Z == 0)
                    edgesToTest.Add(Direction.Up);
                if (cell.Position.Z == source.Depth - 1)
                    edgesToTest.Add(Direction.Down);
                if (cell.Position.Y == 0)
                    edgesToTest.Add(Direction.North);
                if (cell.Position.Y == source.Height - 1)
                    edgesToTest.Add(Direction.South);
                if (cell.Position.X == 0)
                    edgesToTest.Add(Direction.West);
                if (cell.Position.X == source.Width - 1)
                    edgesToTest.Add(Direction.East);

                if (edgesToTest.Count > 0)
                {
                    ResolveBorders(pos, edgesToTest, KeepTargetBorders, ExcludeNullBorders);
                    edgesToTest.Clear();
                }
            }
        }

        #region Resolve Borders
        private Action<CellMap<T>, Cell<T>, Cell<T>, Direction> CellBorderResolutionStrategy;
        private Action<CellMap<T>, Cell<T>, Direction> NullBorderResolutionStrategy;

        public void ResolveBorders(Point3 srcPoint, IEnumerable<Direction> edgesToTest,
            Action<CellMap<T>, Cell<T>, Cell<T>, Direction> resolveCellBorder,
            Action<CellMap<T>, Cell<T>, Direction> resolveNullBorder)
        {
            var srcCell = this[srcPoint];

            foreach (var edge in edgesToTest)
            {
                var trgCell = this.TryGet(srcPoint.Move(edge));
                if (trgCell != null)
                    resolveCellBorder(this, srcCell, trgCell, edge);
                else
                    resolveNullBorder(this, srcCell, edge);
            }
        }

        private static void MatchCellBorders(CellMap<T> map, Cell<T> srcCell, Cell<T> trgCell, Direction edge)
        {
            if (srcCell.Edges.Includes(edge))
                trgCell.Edges |= edge.Opposite;
            else if (trgCell.Edges.Includes(edge.Opposite))
                srcCell.Edges |= edge;
        }

        private static void KeepTargetBorders(CellMap<T> map, Cell<T> srcCell, Cell<T> trgCell, Direction edge)
        {
            if (trgCell.Edges.Includes(edge.Opposite))
                map.AddEdge(srcCell, edge);
            else
                map.RemoveEdge(srcCell, edge);
        }

        private static void KeepSourceBorders(CellMap<T> map, Cell<T> srcCell, Cell<T> trgCell, Direction edge)
        {
            if (srcCell.Edges.Includes(edge))
                map.AddEdge(trgCell, edge.Opposite);
            else
                map.RemoveEdge(trgCell, edge.Opposite);
        }

        private static void IncludeNullBorders(CellMap<T> map, Cell<T> srcCell, Direction edge)
        {
            if (!srcCell.Edges.Includes(edge))
                srcCell.Edges |= edge;
        }

        private static void ExcludeNullBorders(CellMap<T> map, Cell<T> srcCell, Direction edge)
        {
            if (srcCell.Edges.Includes(edge))
                srcCell.Edges &= ~edge;
        }
        #endregion
    }
}
