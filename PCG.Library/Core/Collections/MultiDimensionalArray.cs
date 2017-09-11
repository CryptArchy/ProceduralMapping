using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonExtensions.NumericExtensions;

namespace PCG.Library.Core.Collections
{
//    public interface IMultiDimensionalArray<T>
//    {
//        Size3 Size { get; }
//        T[] Cells { get; }
//        int PosToIdx(int x, int y, int z);
//        int PosToIdx(Point3 pos);
//        Point3 IdxToPos(int index);
//    }

//    public interface IPositionedValue<TContainer, T> where TContainer: IMultiDimensionalArray<T>
//    { 
//        int Index { get; }
//        Point3 Position { get; }
//        TContainer Container { get; }
//        T Value { get; set; }
//    }

//    public class MultiDimensionalArray<TOut, T> 
//        : IMultiDimensionalArray<T>, IEnumerable<TOut>
//        where TOut : IPositionedValue<IMultiDimensionalArray<T>, T>
//    {
//        public Size3 Size { get; private set; }
//        public T[] Cells { get; private set; }

//        public int Width { get { return Size.Width; } }
//        public int Height { get { return Size.Height; } }
//        public int Depth { get { return Size.Depth; } }
//        public int Count { get { return Size.Volume; } }

//        #region Constructors
//        public MultiDimensionalArray(Size3 size, Action<T> initializer)
//        {
//            Size = size;
//            Cells = new T[Size.Volume];
//            int index = 0;
//            for (int z = 0; z < Size.Depth; z++)
//                for (int y = 0; y < Size.Height; y++)
//                    for (int x = 0; x < Size.Width; x++)
//                    {
//                        var cell = default(T);
//                        Cells[index] = cell;
//                        if (initializer != null)
//                            initializer(cell);
//                        index++;
//                    }
//        }

//        public MultiDimensionalArray(Size3 size)
//            : this(size, (Action<T>)null) { }

//        public MultiDimensionalArray(Size3 size, IEnumerable<T> cells)
//        {
//            Size = size;
//            Cells = cells.ToArray();
//        }
//        #endregion

//        #region Public Methods
//        public int PosToIdx(int x, int y, int z)
//        {
//            return x + y * Size.Width + z * Size.Width * Size.Height;
//        }

//        public int PosToIdx(Point3 pos)
//        {
//            return PosToIdx(pos.X, pos.Y, pos.Z);
//        }

//        public Point3 IdxToPos(int index)
//        {
//            return new Point3(index % Size.Width,
//                index / Size.Width % Size.Height,
//                index / (Size.Width * Size.Height));
//        }

//        public IPositionedValue<IMultiDimensionalArray<T>, T> this[int index]
//        {
//            get 
//            {
//                if (IsInBounds(index))
//                    return new PositionedValue(index, this);
//                else
//                    throw new ArgumentOutOfRangeException();
//            }
//        }

//        public IPositionedValue<IMultiDimensionalArray<T>, T> this[int x, int y, int z]
//        {
//            get { return this[PosToIdx(x, y, z)]; }
//        }

//        public IPositionedValue<IMultiDimensionalArray<T>, T> this[Point3 p]
//        {
//            get { return this[PosToIdx(p)]; }
//        }

//        public bool IsInBounds(int index)
//        {
//            return index.BetweenMin(0, Cells.Length);
//        }

//        public bool IsInBounds(int x, int y, int z)
//        {
//            return IsInBounds(PosToIdx(x, y, z));
//        }

//        public bool IsInBounds(Point3 p)
//        {
//            return IsInBounds(PosToIdx(p));
//        }

//        public IPositionedValue<IMultiDimensionalArray<T>, T> TryGet(int index)
//        {
//            if (IsInBounds(index)) 
//                return new PositionedValue(index, this);
//            else 
//                return null;
//        }

//        public IPositionedValue<IMultiDimensionalArray<T>, T> TryGet(Point3 p)
//        {
//            if (IsInBounds(p))
//                return new PositionedValue(p, this);
//            else
//                return null;
//        }

//        public IPositionedValue<IMultiDimensionalArray<T>, T> TryGet(int x, int y, int z)
//        {
//            if (IsInBounds(x,y,z))
//                return new PositionedValue(new Point3(x,y,z), this);
//            else
//                return null;
//        } 
//        #endregion

//        #region Enumerators
//        public IEnumerator<IPositionedValue<IMultiDimensionalArray<T>, T>> GetEnumerator()
//        {
//            for (int z = 0; z < Size.Depth; z++)
//                for (int y = 0; y < Size.Height; y++)
//                    for (int x = 0; x < Size.Width; x++)
//                        yield return new PositionedValue(new Point3(x, y, z), this);
//        }

//        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
//        {
//            return GetEnumerator();
//        }


//        public IEnumerable<IPositionedValue<IMultiDimensionalArray<T>, T>> Enumerate(Point3? start = null, Size3? size = null)
//        {
//            if (!start.HasValue) start = new Point3(0, 0, 0);
//            if (!size.HasValue) size = this.Size;

//            for (int z = start.Value.Z; z < size.Value.Depth; z++)
//                for (int y = start.Value.Y; y < size.Value.Height; y++)
//                    for (int x = start.Value.X; x < size.Value.Width; x++)
//                        yield return new PositionedValue(new Point3(x, y, z), this);
//        }

//        public IEnumerable<IEnumerable<IPositionedValue<IMultiDimensionalArray<T>, T>>> Layers()
//        {
//            for (int z = 0; z < Size.Depth; z++)
//                yield return Layer(z);
//        }

//        public IEnumerable<IPositionedValue<IMultiDimensionalArray<T>, T>> Layer(int z)
//        {
//            for (int y = 0; y < Size.Height; y++)
//                for (int x = 0; x < Size.Width; x++)
//                    yield return new PositionedValue(new Point3(x, y, z), this);
//        }

//        public IEnumerable<IPositionedValue<IMultiDimensionalArray<T>, T>> Shaft(int x, int y)
//        {
//            for (int z = 0; z < Size.Depth; z++)
//                yield return new PositionedValue(new Point3(x, y, z), this);
//        }

//        public IEnumerable<IEnumerable<IPositionedValue<IMultiDimensionalArray<T>, T>>> Columns(int z = 0)
//        {
//            for (int x = 0; x < Size.Width; x++)
//                yield return Column(x, z);
//        }

//        public IEnumerable<IPositionedValue<IMultiDimensionalArray<T>, T>> Column(int x, int z = 0)
//        {
//            for (int y = 0; y < Size.Height; y++)
//                yield return new PositionedValue(new Point3(x, y, z), this);
//        }

//        public IEnumerable<IEnumerable<IPositionedValue<IMultiDimensionalArray<T>, T>>> Rows(int z = 0)
//        {
//            for (int y = 0; y < Size.Height; y++)
//                yield return Row(y, z);
//        }

//        public IEnumerable<IPositionedValue<IMultiDimensionalArray<T>, T>> Row(int y, int z = 0)
//        {
//            for (int x = 0; x < Size.Width; x++)
//                yield return new PositionedValue(new Point3(x, y, z), this);
//        }

//        public IEnumerable<IEnumerable<IPositionedValue<IMultiDimensionalArray<T>, T>>> Regions(Size3 size)
//        {
//            var origin = new Point3(0, 0, 0);
//            while (true)
//            {
//                yield return Region(size, origin);

//                if (origin.X < this.Width - 1)
//                    origin = new Point3(origin.X + size.Width, origin.Y, origin.Z);
//                else
//                {
//                    if (origin.Y < this.Height - 1)
//                        origin = new Point3(0, origin.Y + size.Height, origin.Z);
//                    else
//                    {
//                        if (origin.Z < this.Depth - 1)
//                            origin = new Point3(0, 0, origin.Z + size.Depth);
//                        else
//                            break;
//                    }
//                }
//            }
//        }

//        public IEnumerable<IPositionedValue<IMultiDimensionalArray<T>, T>> Region(Size3 size, Point3 origin)
//        {
//            for (int z = origin.Z; z < size.Depth; z++)
//                for (int y = origin.Y; y < size.Height; y++)
//                    for (int x = origin.Z; x < size.Width; x++)
//                    {
//                        var p = new Point3(x, y, z);
//                        if (IsInBounds(p))
//                            yield return new PositionedValue(p, this);
//                        else
//                            yield break;
//                    }
//        }
//        #endregion

//        #region Protected Members
//        protected T TryGet(int index, T defaultValue = default(T))
//        {
//            if (IsInBounds(index))
//                return Cells[index];
//            else
//                return defaultValue;
//        }

//        protected T TryGet(Point3 p, T defaultValue = default(T))
//        {
//            return TryGet(PosToIdx(p), defaultValue);
//        }

//        protected T TryGet(int x, int y, int z, T defaultValue = default(T))
//        {
//            return TryGet(PosToIdx(x, y, z), defaultValue);
//        }

//        protected class PositionedValue : IPositionedValue<IMultiDimensionalArray<T>, T>
//        {
//            public int Index { get; private set; }
//            public Point3 Position { get; private set; }
//            public IMultiDimensionalArray<T> Container { get; private set; }
//            public T Value
//            {
//                get { return Container.Cells[Index]; }
//                set { Container.Cells[Index] = Value; }
//            }

//            public PositionedValue(Point3 pos, IMultiDimensionalArray<T> container)
//                : this(container.PosToIdx(pos), pos, container) { }

//            public PositionedValue(int index, IMultiDimensionalArray<T> container)
//                : this(index, container.IdxToPos(index), container) { }

//            public PositionedValue(int index, Point3 pos, IMultiDimensionalArray<T> container)
//            {
//                Index = index;
//                Position = pos;
//                Container = container;
//            }            
//        }
//        #endregion

//        IEnumerator<TOut> IEnumerable<TOut>.GetEnumerator()
//        {
//            for (int z = 0; z < Size.Depth; z++)
//                for (int y = 0; y < Size.Height; y++)
//                    for (int x = 0; x < Size.Width; x++)
//                        yield return new PositionedValue(new Point3(x, y, z), this);
//        }
//    }

//    public interface IEdgeMap<T> : IMultiDimensionalArray<T>
//    {
//        Direction[] Edges { get; }
//    }

//    public interface IPositionedEdge<T> : IPositionedValue<IEdgeMap<T>, T>
//    {
//        Direction Edges { get; set; }
//    }

//    public class EdgeMap<TOut, T> : MultiDimensionalArray<TOut, T>, IEdgeMap<T>
//        where TOut : IPositionedEdge<T>
//    {
//        protected Direction[] Edges { get; set; }

//        protected class PositionedEdge : PositionedValue, IPositionedEdge<T>
//        {
//            public Direction Edges
//            {
//                get { return Container.Edges[Index]; }
//                set { Container.Edges[Index] = value; }
//            }
            
//            public new IEdgeMap<T> Container { get; private set; }
//        }
//    }

//    public static class Test
//    {
//        public static void DoStuff()
//        {
//            var map = new EdgeMap<IPositionedEdge<int>,int>();
            
//            foreach (var cell in map)
//            {
//                cell.Edges |= Direction.North;
//            }
//        }
//    }
}