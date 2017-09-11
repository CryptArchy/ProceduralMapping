using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCG.Library.Collections
{
    public class ConsList<T> : IEnumerable<T>
    {
        private class ConsCell
        {
            public T Value { get; set; }
            public ConsCell Next { get; set; }
            public ConsCell Prev { get; set; }
            public ConsCell(T value, ConsCell prev = null, ConsCell next = null) 
            { Value = value; Prev = prev; Next = next; }
        }

        private ConsCell _last;
        private ConsCell _first;

        public T First { get { return _first.Value; } }
        public T Last { get { return _last.Value; } }
        public int Count { get; private set; }

        public T this[int i]
        {
            get { return GetCellAt(i).Value; }
            set { SetCellAt(i, value); }
        }

        private ConsCell GetCellAt(int index)
        {
            if (index >= Count) index %= Count;
            if (index < 0) index = (index % Count) + Count;

            var cur = _first;
            while (index > 0)
            {
                cur = cur.Next;
                index--;
            }
            return cur;
        }

        private ConsCell SetCellAt(int index, T value)
        {
            if (index >= Count) index %= Count;
            if (index < 0) index = (index % Count) + Count;

            var cur = _first;
            while (index > 0)
            {
                cur = cur.Next;
                index--;
            }

            cur.Value = value;
            return cur;
        }

        public bool Contains(T value)
        {
            var cur = _first;
            while (cur != null)
            {
                if (cur.Value.Equals(value))
                    return true;
                cur = cur.Next;
            }
            return false;
        }

        public ConsList<T> Add(T value)
        {
            var added = new ConsCell(value, _last);

            if (_last != null)
                _last.Next = added;

            _last = added;

            if (_first == null)
                _first = _last;

            Count++;
            return this;
        }

        public ConsList<T> Append(ConsList<T> that)
        {            
            that._first.Prev = this._last;
            this._last.Next = that._first;
            
            that._first = this._first;
            this._last = that._last;

            this._first.Prev = null;
            this._last.Next = null;

            this.Count += that.Count;
            that.Count = this.Count;

            return this;
        }

        public ConsList<T> Prepend(ConsList<T> that)
        {
            that.Append(this);
            return this;
        }

        public ConsList<T> Insert(int index, T value)
        {
            if (index >= Count)
                return Add(value);

            var cur = _first;
            while (index > 0)
            {
                cur = cur.Next;
                index--;
            }

            var inserted = new ConsCell(value, cur.Prev, cur);

            if (cur.Prev == null)
                _first = inserted;
            else
                cur.Prev.Next = inserted;

            cur.Prev = inserted;
            return this;
        }

        public ConsList<T> Remove(T value)
        {
            var cur = _first;
            while (cur != null && !cur.Value.Equals(value))
                cur = cur.Next;

            if (cur != null)
            {
                cur.Prev.Next = cur.Next;
                cur.Next.Prev = cur.Prev;
                Count--;
            }
            return this;
        }

        public ConsList<T> RemoveAll(T value)
        {
            var cur = _first;
            while (cur != null)
            {
                if (cur.Value.Equals(value))
                {
                    cur.Prev.Next = cur.Next;
                    cur.Next.Prev = cur.Prev;
                    Count--;
                }
                cur = cur.Next;
            }

            return this;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var cur = _first;
            while (cur != null)
            {
                yield return cur.Value;
                cur = cur.Next;
            }
            yield break;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
