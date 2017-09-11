using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCG.Library.Core.Collections
{
    public class ObservableList<T> : IList<T>, ICollection<T>, IEnumerable<T>, INotifyCollectionChanged
    {
        protected List<T> InnerList { get; set; }

        public ObservableList() { InnerList = new List<T>(); }
        public ObservableList(int capacity) { InnerList = new List<T>(capacity); }
        public ObservableList(IEnumerable<T> collection) { InnerList = new List<T>(collection); }

        #region INotifyCollectionChanged
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        protected void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
        {
            if (CollectionChanged != null)
                CollectionChanged(this, args);
        }
        protected void OnItemAdded(T item)
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
        }
        protected void OnItemAdded(IList<T> items)
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, items));
        }
        protected void OnItemMoved(T item, int newIndex, int oldIndex)
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Move, item, newIndex, oldIndex));
        }
        protected void OnItemRemoved(T item) 
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
        }
        protected void OnItemReplaced(T newItem, T oldItem, int index) 
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, newItem, oldItem, index));
        }
        protected void OnCollectionReset(IList<T> oldItems, int newStartingIndex) 
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset, oldItems, newStartingIndex));
        }
        #endregion

        #region IEnumerable
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return InnerList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return InnerList.GetEnumerator();
        }
        #endregion

        #region ICollection
        public int Count
        {
            get { return InnerList.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public void Add(T item)
        {
            InnerList.Add(item);
            OnItemAdded(item);
        }

        public void Clear()
        {
            if (CollectionChanged == null)
                InnerList.Clear();
            else
            {
                var oldItems = new T[InnerList.Count];
                InnerList.CopyTo(oldItems);
                InnerList.Clear();
                OnCollectionReset(oldItems, 0);
            }
        }

        public bool Contains(T item)
        {
            return InnerList.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            InnerList.CopyTo(array, arrayIndex);
        }        

        public bool Remove(T item)
        {
            var success = InnerList.Remove(item);
            OnItemRemoved(item);
            return success;
        }
        #endregion

        #region IList
        public int IndexOf(T item)
        {
            return InnerList.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            InnerList.Insert(index, item);
            OnItemAdded(item);
            for (int i = index + 1; i < InnerList.Count; i++)
                OnItemMoved(InnerList[i], i, i - 1);
        }

        public void RemoveAt(int index)
        {
            var item = InnerList[index];
            InnerList.RemoveAt(index);
            OnItemRemoved(item);
            for (int i = index; i < InnerList.Count; i++)
                OnItemMoved(InnerList[i], i, i + 1);
        }

        public T this[int index]
        {
            get
            {
                return InnerList[index];
            }
            set
            {
                var oldItem = InnerList[index];
                InnerList[index] = value;
                OnItemReplaced(value, oldItem, index);
            }
        } 
        #endregion
    }

    public class ObservableDictionary<TKey,TValue>
    {

    }

    public class ObservableSet<T> { }
}
