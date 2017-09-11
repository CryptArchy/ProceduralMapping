using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Totally stolen from http://www.superstarcoders.com/blogs/posts/efficient-avl-tree-in-c-sharp.aspx

namespace PCG.Library.Collections
{
    public class AvlTree<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>, IDictionary<TKey, TValue>
    {
        private IComparer<TKey> _comparer;
        private AvlNode _root;
        private int _count;

        private sealed class AvlNode
        {
            public AvlNode Parent;
            public AvlNode Left;
            public AvlNode Right;
            public TKey Key;
            public TValue Value;
            public int Balance;
        }

        public AvlTree(IComparer<TKey> comparer)
        {
            _comparer = comparer;
        }

        public AvlTree()
            : this(Comparer<TKey>.Default)
        {
        }

        public void Insert(TKey key, TValue value)
        {
            if (_root == null)
            {
                _root = new AvlNode { Key = key, Value = value };
            }
            else
            {
                AvlNode node = _root;
                while (node != null)
                {
                    int compare = _comparer.Compare(key, node.Key);
                    if (compare < 0)
                    {
                        AvlNode left = node.Left;
                        if (left == null)
                        {
                            node.Left = new AvlNode { Key = key, Value = value, Parent = node };
                            InsertBalance(node, 1);
                            _count++;
                            return;
                        }
                        else
                        {
                            node = left;
                        }
                    }
                    else if (compare > 0)
                    {
                        AvlNode right = node.Right;
                        if (right == null)
                        {
                            node.Right = new AvlNode { Key = key, Value = value, Parent = node };
                            InsertBalance(node, -1);
                            _count++;
                            return;
                        }
                        else
                        {
                            node = right;
                        }
                    }
                    else
                    {
                        node.Value = value;
                        return;
                    }
                }
            }
        }

        public bool Delete(TKey key)
        {
            AvlNode node = _root;
            while (node != null)
            {
                if (_comparer.Compare(key, node.Key) < 0)
                {
                    node = node.Left;
                }
                else if (_comparer.Compare(key, node.Key) > 0)
                {
                    node = node.Right;
                }
                else
                {
                    AvlNode left = node.Left;
                    AvlNode right = node.Right;
                    if (left == null)
                    {
                        if (right == null)
                        {
                            if (node == _root)
                            {
                                _root = null;
                            }
                            else
                            {
                                AvlNode parent = node.Parent;
                                if (parent.Left == node)
                                {
                                    parent.Left = null;
                                    DeleteBalance(parent, -1);
                                }
                                else
                                {
                                    parent.Right = null;
                                    DeleteBalance(parent, 1);
                                }
                            }
                        }
                        else
                        {
                            Replace(node, right);
                            DeleteBalance(node, 0);
                        }
                    }
                    else if (right == null)
                    {
                        Replace(node, left);
                        DeleteBalance(node, 0);
                    }
                    else
                    {
                        AvlNode successor = right;
                        if (successor.Left == null)
                        {
                            AvlNode parent = node.Parent;
                            successor.Parent = parent;
                            successor.Left = left;
                            successor.Balance = node.Balance;
                            if (left != null)
                            {
                                left.Parent = successor;
                            }
                            if (node == _root)
                            {
                                _root = successor;
                            }
                            else
                            {
                                if (parent.Left == node)
                                {
                                    parent.Left = successor;
                                }
                                else
                                {
                                    parent.Right = successor;
                                }
                            }
                            DeleteBalance(successor, 1);
                        }
                        else
                        {
                            while (successor.Left != null)
                            {
                                successor = successor.Left;
                            }
                            AvlNode parent = node.Parent;
                            AvlNode successorParent = successor.Parent;
                            AvlNode successorRight = successor.Right;
                            if (successorParent.Left == successor)
                            {
                                successorParent.Left = successorRight;
                            }
                            else
                            {
                                successorParent.Right = successorRight;
                            }
                            if (successorRight != null)
                            {
                                successorRight.Parent = successorParent;
                            }
                            successor.Parent = parent;
                            successor.Left = left;
                            successor.Balance = node.Balance;
                            successor.Right = right;
                            right.Parent = successor;
                            if (left != null)
                            {
                                left.Parent = successor;
                            }
                            if (node == _root)
                            {
                                _root = successor;
                            }
                            else
                            {
                                if (parent.Left == node)
                                {
                                    parent.Left = successor;
                                }
                                else
                                {
                                    parent.Right = successor;
                                }
                            }
                            DeleteBalance(successorParent, -1);
                        }
                    }
                    _count--;
                    return true;
                }
            }
            return false;
        }

        public bool Search(TKey key, out TValue value)
        {
            AvlNode node = _root;
            while (node != null)
            {
                if (_comparer.Compare(key, node.Key) < 0)
                {
                    node = node.Left;
                }
                else if (_comparer.Compare(key, node.Key) > 0)
                {
                    node = node.Right;
                }
                else
                {
                    value = node.Value;
                    return true;
                }
            }
            value = default(TValue);
            return false;
        }

        public void Clear()
        {
            _root = null;
            _count = 0;
        }

        private void InsertBalance(AvlNode node, int balance)
        {
            while (node != null)
            {
                balance = (node.Balance += balance);
                if (balance == 0)
                {
                    return;
                }
                else if (balance == 2)
                {
                    if (node.Left.Balance == 1)
                    {
                        RotateRight(node);
                    }
                    else
                    {
                        RotateLeftRight(node);
                    }
                    return;
                }
                else if (balance == -2)
                {
                    if (node.Right.Balance == -1)
                    {
                        RotateLeft(node);
                    }
                    else
                    {
                        RotateRightLeft(node);
                    }
                    return;
                }
                AvlNode parent = node.Parent;
                if (parent != null)
                {
                    balance = parent.Left == node ? 1 : -1;
                }
                node = parent;
            }
        }

        private void DeleteBalance(AvlNode node, int balance)
        {
            while (node != null)
            {
                balance = (node.Balance += balance);
                if (balance == 2)
                {
                    if (node.Left.Balance >= 0)
                    {
                        node = RotateRight(node);
                        if (node.Balance == -1)
                        {
                            return;
                        }
                    }
                    else
                    {
                        node = RotateLeftRight(node);
                    }
                }
                else if (balance == -2)
                {
                    if (node.Right.Balance <= 0)
                    {
                        node = RotateLeft(node);
                        if (node.Balance == 1)
                        {
                            return;
                        }
                    }
                    else
                    {
                        node = RotateRightLeft(node);
                    }
                }
                else if (balance != 0)
                {
                    return;
                }
                AvlNode parent = node.Parent;
                if (parent != null)
                {
                    balance = parent.Left == node ? -1 : 1;
                }
                node = parent;
            }
        }

        private static void Replace(AvlNode target, AvlNode source)
        {
            AvlNode left = source.Left;
            AvlNode right = source.Right;
            target.Balance = source.Balance;
            target.Key = source.Key;
            target.Value = source.Value;
            target.Left = left;
            target.Right = right;
            if (left != null)
            {
                left.Parent = target;
            }
            if (right != null)
            {
                right.Parent = target;
            }
        }

        private AvlNode RotateLeft(AvlNode node)
        {
            AvlNode right = node.Right;
            AvlNode rightLeft = right.Left;
            AvlNode parent = node.Parent;
            right.Parent = parent;
            right.Left = node;
            node.Right = rightLeft;
            node.Parent = right;
            if (rightLeft != null)
            {
                rightLeft.Parent = node;
            }
            if (node == _root)
            {
                _root = right;
            }
            else if (parent.Right == node)
            {
                parent.Right = right;
            }
            else
            {
                parent.Left = right;
            }
            right.Balance++;
            node.Balance = -right.Balance;
            return right;
        }

        private AvlNode RotateRight(AvlNode node)
        {
            AvlNode left = node.Left;
            AvlNode leftRight = left.Right;
            AvlNode parent = node.Parent;
            left.Parent = parent;
            left.Right = node;
            node.Left = leftRight;
            node.Parent = left;
            if (leftRight != null)
            {
                leftRight.Parent = node;
            }
            if (node == _root)
            {
                _root = left;
            }
            else if (parent.Left == node)
            {
                parent.Left = left;
            }
            else
            {
                parent.Right = left;
            }
            left.Balance--;
            node.Balance = -left.Balance;
            return left;
        }

        private AvlNode RotateLeftRight(AvlNode node)
        {
            AvlNode left = node.Left;
            AvlNode leftRight = left.Right;
            AvlNode parent = node.Parent;
            AvlNode leftRightRight = leftRight.Right;
            AvlNode leftRightLeft = leftRight.Left;
            leftRight.Parent = parent;
            node.Left = leftRightRight;
            left.Right = leftRightLeft;
            leftRight.Left = left;
            leftRight.Right = node;
            left.Parent = leftRight;
            node.Parent = leftRight;
            if (leftRightRight != null)
            {
                leftRightRight.Parent = node;
            }
            if (leftRightLeft != null)
            {
                leftRightLeft.Parent = left;
            }
            if (node == _root)
            {
                _root = leftRight;
            }
            else if (parent.Left == node)
            {
                parent.Left = leftRight;
            }
            else
            {
                parent.Right = leftRight;
            }
            if (leftRight.Balance == -1)
            {
                node.Balance = 0;
                left.Balance = 1;
            }
            else if (leftRight.Balance == 0)
            {
                node.Balance = 0;
                left.Balance = 0;
            }
            else
            {
                node.Balance = -1;
                left.Balance = 0;
            }
            leftRight.Balance = 0;
            return leftRight;
        }

        private AvlNode RotateRightLeft(AvlNode node)
        {
            AvlNode right = node.Right;
            AvlNode rightLeft = right.Left;
            AvlNode parent = node.Parent;
            AvlNode rightLeftLeft = rightLeft.Left;
            AvlNode rightLeftRight = rightLeft.Right;
            rightLeft.Parent = parent;
            node.Right = rightLeftLeft;
            right.Left = rightLeftRight;
            rightLeft.Right = right;
            rightLeft.Left = node;
            right.Parent = rightLeft;
            node.Parent = rightLeft;
            if (rightLeftLeft != null)
            {
                rightLeftLeft.Parent = node;
            }
            if (rightLeftRight != null)
            {
                rightLeftRight.Parent = right;
            }
            if (node == _root)
            {
                _root = rightLeft;
            }
            else if (parent.Right == node)
            {
                parent.Right = rightLeft;
            }
            else
            {
                parent.Left = rightLeft;
            }
            if (rightLeft.Balance == 1)
            {
                node.Balance = 0;
                right.Balance = -1;
            }
            else if (rightLeft.Balance == 0)
            {
                node.Balance = 0;
                right.Balance = 0;
            }
            else
            {
                node.Balance = 1;
                right.Balance = 0;
            }
            rightLeft.Balance = 0;
            return rightLeft;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return new AvlNodeEnumerator(_root);
        }

        sealed class AvlNodeEnumerator : IEnumerator<KeyValuePair<TKey, TValue>>
        {
            private AvlNode _root;
            private Action _action;
            private AvlNode _current;
            private AvlNode _right;

            public AvlNodeEnumerator(AvlNode root)
            {
                _right = _root = root;
                _action = _root == null ? Action.End : Action.Right;
            }

            public bool MoveNext()
            {
                switch (_action)
                {
                    case Action.Right:
                        _current = _right;
                        while (_current.Left != null)
                        {
                            _current = _current.Left;
                        }
                        _right = _current.Right;
                        _action = _right != null ? Action.Right : Action.Parent;
                        return true;
                    case Action.Parent:
                        while (_current.Parent != null)
                        {
                            AvlNode previous = _current;
                            _current = _current.Parent;
                            if (_current.Left == previous)
                            {
                                _right = _current.Right;
                                _action = _right != null ? Action.Right : Action.Parent;
                                return true;
                            }
                        }
                        _action = Action.End;
                        return false;
                    default:
                        return false;
                }
            }

            public void Reset()
            {
                _right = _root;
                _action = _root == null ? Action.End : Action.Right;
            }

            public KeyValuePair<TKey,TValue> Current
            {
                get
                {
                    return new KeyValuePair<TKey,TValue>(_current.Key, _current.Value);
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            public void Dispose()
            {
            }

            enum Action
            {
                Parent,
                Right,
                End
            }
        }

        #region IDictionary Implementation
        public void Add(TKey key, TValue value)
        {
            this.Insert(key, value);
        }

        public bool ContainsKey(TKey key)
        {
            TValue value;
            return this.Search(key, out value);
        }

        public ICollection<TKey> Keys
        {
            get { return this.Select(kvp => kvp.Key).ToArray(); }
        }

        public bool Remove(TKey key)
        {
            return this.Delete(key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return this.Search(key, out value);
        }

        public ICollection<TValue> Values
        {
            get { return this.Select(kvp => kvp.Value).ToArray(); }
        }

        public TValue this[TKey key]
        {
            get
            {
                TValue value;
                this.Search(key, out value);
                return value;
            }
            set
            {
                this.Insert(key, value);
            }
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            this.Insert(item.Key, item.Value);
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            TValue value;
            return this.Search(item.Key, out value);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            foreach (var kvp in this)
                array[arrayIndex++] = kvp;
        }

        public int Count
        {
            get { return _count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return this.Delete(item.Key);
        }
        #endregion
    }
}
