using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCG.Library.Collections
{
    public class BinaryTree<T> where T : IComparable
    {
        protected class BTNode
        {
            public BTNode Left;
            public BTNode Right;
            public T Key;
        }

        private BTNode Root;
        private int Count;
        private int MaxCount;

        private BTNode Find(T key)
        {
            var node = Root;
            while (node != null)
            {
                switch(key.CompareTo(node.Key))
                {
                    case -1:
                        node = node.Left; break;
                    case 1:
                        node = node.Right; break;
                    case 0:
                        return node;
                }
            }
            return null;
        }

        private BTNode Insert(T key)
        {
            var inNode = new BTNode() { Key = key };
            if (Root == null)
            {
                Root = inNode;
                return Root;
            }

            var node = Root;
            while (node != null)
            {
                switch (key.CompareTo(node.Key))
                {
                    case -1:
                        if (node.Left == null)
                        {
                            node.Left = inNode;
                            return inNode;
                        }
                        else
                        {
                            node = node.Left;
                        }
                        break;
                    case 1:
                        if (node.Right == null)
                        {
                            node.Right = inNode;
                            return inNode;
                        }
                        else
                        {
                            node = node.Right;
                        }
                        break;
                    case 0:
                        return node;
                }
            }

            return null;
        }

        private BTNode Delete(T key)
        {
            var node = Root;
            BTNode parent = null;
            var isLeft = true;
            while (node != null)
            {
                switch (key.CompareTo(node.Key))
                {
                    case -1:
                        parent = node;
                        node = node.Left; 
                        isLeft = true;
                        break;
                    case 1:
                        parent = node;
                        node = node.Right; 
                        isLeft = false;
                        break;
                    case 0:
                        node = null;
                        break;
                }
            }
            if (parent == null)
            {
                Root = null;
                return null;
            }

            if (isLeft)
                node = parent.Left;
            else
                node = parent.Right;

            if (node == null) 
                return null;

            if (node.Left != null && node.Right != null)
            {
                var lkey = node.Left.Key;
                Delete(node.Left.Key);
                node.Key = lkey;
            }
            else if (node.Left != null)
            {
                if (isLeft)
                    parent.Left = node.Left;
                else
                    parent.Right = node.Left;
            }
            else if (node.Right != null)
            {
                if (isLeft)
                    parent.Left = node.Right;
                else
                    parent.Right = node.Right;
            }
            else
            {
                if (isLeft)
                    parent.Left = null;
                else
                    parent.Right = null;
            }

            return node;
        }

        private int Size(BTNode node)
        {
            int szLeft = 0;
            int szRight = 0;
            var unexplored = new Queue<BTNode>();
            unexplored.Enqueue(node);
            do
            {
                node = unexplored.Dequeue();

                if (node.Left != null)
                {
                    unexplored.Enqueue(node.Left);
                    szLeft++;
                }

                if (node.Right != null)
                {
                    szRight++;
                    unexplored.Enqueue(node.Right);
                }
            } while (unexplored.Count != 0);

            return szLeft + szRight + 1;
        }
    }
}
