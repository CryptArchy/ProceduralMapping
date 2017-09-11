using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCG.Library.Collections
{
    public class Cell<T>
    {
        public Direction Edges { get; set; }
        public T Value { get; set; }

        public Cell(T value, Direction edges)
        {
            Edges = edges;
            Value = value;
        }

        public Cell(T value) : this(value, Direction.Center) { }

        public Cell() : this(default(T), Direction.Center) { }

        public override bool Equals(object obj)
        {
            var that = obj as Cell<T>;
            return that != null && that.Value.Equals(this.Value);
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        public override string ToString()
        {
            var result = Edges.ToString() + ":" + Value.ToString();
            return result;
        }
    }

    public class CellWrapper<T>
    {
        private CellMap<T> Map { get; set; }
        private Cell<T> Cell { get; set; }

        public int Index { get; set; }
        public Point3 Position { get; set; }

        public Direction Edges { get { return Cell.Edges; } set { Cell.Edges = value; } }
        public T Value { get { return Cell.Value; } set { Cell.Value = value; } }
        public int X { get { return Position.X; } }
        public int Y { get { return Position.Y; } }
        public int Z { get { return Position.Z; } }

        public CellWrapper(Point3 pos, CellMap<T> map)
            : this(map.PosToIdx(pos), pos, map)
        { }

        public CellWrapper(int index, CellMap<T> map)
            : this(index, map.IdxToPos(index), map)
        { }

        public CellWrapper(int index, Point3 pos, CellMap<T> map)
            :this(index, pos, map, map[index])
        { }

        public CellWrapper(int index, Point3 pos, CellMap<T> map, Cell<T> cell)
        {
            Index = index;
            Position = pos;

            Map = map;
            Cell = cell;
        }

        public static explicit operator Cell<T>(CellWrapper<T> cell)
        {
            return new Cell<T>(cell.Value, cell.Edges);
        }
    }
}
