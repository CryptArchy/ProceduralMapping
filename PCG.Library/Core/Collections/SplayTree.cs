using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCG.Library.Collections
{
    public class SplayNode<T> where T : IComparable
    {
        public SplayNode<T> Parent { get; set; }
        public SplayNode<T> Left { get; set; }
        public SplayNode<T> Right { get; set; }
        public T Value { get; set; }
    }

    public class SplayTree<T> where T : IComparable
    {
        protected SplayNode<T> Root { get; set; }
        public int Count { get; protected set; }              

        public int DepthOf(T item)
        {
            int depth = 0;
            SplayNode<T> find = FindNode(item, out depth);
            if (find != null)
            {
                item = find.Value;
                Splay(find);
            }
            return depth;
        }

        public T Find(T item)
        {
            int depth = 0;
            SplayNode<T> find = FindNode(item, out depth);
            if (find != null)
            {
                Splay(find);
                return find.Value;
            }
            return default(T);
        }

        public void Add(T item)
        {
            if (item == null)
                return;
            var newNode = new SplayNode<T>() { Value = item };
            Add(newNode);
        }

        public void Remove(T item)
        {
            int depth = 0;
            SplayNode<T> find = FindNode(item, out depth);
            Remove(find);
        }

        public void Clear()
        {
            while (Root != null)
            {
                Remove(Root);
            }
        }        

        protected SplayNode<T> FindNode(T item, out int depth)
        {
            SplayNode<T> z = Root;
            depth = 0;
            while (z != null)
            {
                if (item.CompareTo(z.Value) < 0)
                {
                    z = z.Right;
                    depth++;
                }
                else if (item.CompareTo(z.Value) > 0)
                {
                    z = z.Left;
                    depth++;
                }
                else
                {
                    return z;
                }
            }
            return null;
        }        

        protected void Add(SplayNode<T> node)
        {            
            SplayNode<T> traveler = Root;
            SplayNode<T> parent = null;
            while (traveler != null)
            {
                parent = traveler;
                if (node.Value.CompareTo(parent.Value) < 0)
                    traveler = traveler.Right;
                else
                    traveler = traveler.Left;
            }
            node.Parent = parent;

            if (parent == null)
                Root = node;
            else if (node.Value.CompareTo(parent.Value) < 0)
                parent.Right = node;
            else
                parent.Left = node;

            Splay(node);
            Count++;
        }

        protected void Remove(SplayNode<T> item)
        {
            if (item == null)
            {
                return;
            }
            Splay(item);
            if ((item.Left != null) && (item.Right != null))
            {
                SplayNode<T> min = item.Left;
                while (min.Right != null)
                {
                    min = min.Right;
                }
                min.Right = item.Right;
                item.Right.Parent = min;
                item.Left.Parent = null;
                Root = item.Left;
            }
            else if (item.Right != null)
            {
                item.Right.Parent = null;
                Root = item.Right;
            }
            else if (item.Left != null)
            {
                item.Left.Parent = null;
                Root = item.Left;
            }
            else
            {
                Root = null;
            }
            item.Parent = null;
            item.Left = null;
            item.Right = null;
            item = null;
            Count--;
        }

        protected void Splay(SplayNode<T> x)
        {
            while (x.Parent != null)
            {
                SplayNode<T> Parent = x.Parent;
                SplayNode<T> GrandParent = Parent.Parent;
                if (GrandParent == null)
                {
                    if (x == Parent.Left)
                        MakeLeftChildParent(x, Parent);
                    else
                        MakeRightChildParent(x, Parent);
                }
                else
                {
                    if (x == Parent.Left)
                    {
                        if (Parent == GrandParent.Left)
                        {
                            MakeLeftChildParent(Parent, GrandParent);
                            MakeLeftChildParent(x, Parent);
                        }
                        else
                        {
                            MakeLeftChildParent(x, x.Parent);
                            MakeRightChildParent(x, x.Parent);
                        }
                    }
                    else
                    {
                        if (Parent == GrandParent.Left)
                        {
                            MakeRightChildParent(x, x.Parent);
                            MakeLeftChildParent(x, x.Parent);
                        }
                        else
                        {
                            MakeRightChildParent(Parent, GrandParent);
                            MakeRightChildParent(x, Parent);
                        }
                    }
                }
            }
            Root = x;
        }

        protected void MakeLeftChildParent(SplayNode<T> c, SplayNode<T> p)
        {
            if ((c == null) || (p == null) || (p.Left != c) || (c.Parent != p))
            {
                throw new Exception("WRONG");
            }
            if (p.Parent != null)
            {
                if (p == p.Parent.Left)
                {
                    p.Parent.Left = c;
                }
                else
                {
                    p.Parent.Right = c;
                }
            }
            if (c.Right != null)
            {
                c.Right.Parent = p;
            }
            c.Parent = p.Parent;
            p.Parent = c;
            p.Left = c.Right;
            c.Right = p;
        }

        protected void MakeRightChildParent(SplayNode<T> c, SplayNode<T> p)
        {
            if ((c == null) || (p == null) || (p.Right != c) || (c.Parent != p))
            {
                throw new Exception("WRONG");
            }
            if (p.Parent != null)
            {
                if (p == p.Parent.Left)
                {
                    p.Parent.Left = c;
                }
                else
                {
                    p.Parent.Right = c;
                }
            }
            if (c.Left != null)
            {
                c.Left.Parent = p;
            }
            c.Parent = p.Parent;
            p.Parent = c;
            p.Right = c.Left;
            c.Left = p;
        }
    }
}
