using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCG.Library.Collections
{
    public class SgTree<T> where T : IComparable
    {
        protected class SgNode
        {
            public SgNode Parent;
            public SgNode Left;
            public SgNode Right;
            public T Key;
        }

        private SgNode Root;
        private int Count;
        private int MaxCount;

        private SgNode Flatten(SgNode node, SgNode subtree)
        {
            if (node == null)
                return subtree;
            node.Right = Flatten(node.Right, subtree);
            return Flatten(node.Left, node);
        }

        private SgNode BuildTree(int n, SgNode node)
        {
            if (n == 0)
            {
                node.Left = null;
                return node;
            }
            var r = BuildTree((n - 1) / 2, node);
            var s = BuildTree((n - 1 / 2), r.Right);
            r.Right = s.Left;
            s.Left = r;
            return s;
        }

        private SgNode RebuildTree(int n, SgNode sg)
        {
            var dummy = new SgNode();
            var z = Flatten(sg, dummy);
            BuildTree(n, z);
            return dummy.Left;
        }

        //private SgNode Find(T key)
        //{
        //    var node = Root;
        //    while (node != null)
        //    {
        //        switch (key.CompareTo(node.Key))
        //        {
        //            case -1:
        //                node = node.Left; break;
        //            case 1:
        //                node = node.Right; break;
        //            case 0:
        //                return node;
        //        }
        //    }
        //    return null;
        //}

        //private SgNode Insert(T key)
        //{
        //    var inNode = new SgNode() { Key = key };
        //    var depth = 0;
        //    if (Root == null)
        //    {
        //        Root = inNode;
        //        return Root;
        //    }

        //    var node = Root;
        //    while (node != null)
        //    {
        //        ++depth;
        //        switch (key.CompareTo(node.Key))
        //        {
        //            case -1:
        //                if (node.Left == null)
        //                {
        //                    inNode.Parent = node;
        //                    node.Left = inNode;
        //                }
        //                else
        //                {
        //                    node = node.Left;
        //                }
        //                break;
        //            case 1:
        //                if (node.Right == null)
        //                {
        //                    inNode.Parent = node;
        //                    node.Right = inNode;
        //                }
        //                else
        //                {
        //                    node = node.Right;
        //                }
        //                break;
        //            case 0:
        //                return node;
        //        }
        //    }



        //    return null;
        //}

        //private SgNode Delete(T key)
        //{
        //    var node = Root;
        //    SgNode parent = null;
        //    var isLeft = true;
        //    while (node != null)
        //    {
        //        switch (key.CompareTo(node.Key))
        //        {
        //            case -1:
        //                parent = node;
        //                node = node.Left;
        //                isLeft = true;
        //                break;
        //            case 1:
        //                parent = node;
        //                node = node.Right;
        //                isLeft = false;
        //                break;
        //            case 0:
        //                node = null;
        //                break;
        //        }
        //    }
        //    if (parent == null)
        //    {
        //        Root = null;
        //        return null;
        //    }

        //    if (isLeft)
        //        node = parent.Left;
        //    else
        //        node = parent.Right;

        //    if (node == null)
        //        return null;

        //    if (node.Left != null && node.Right != null)
        //    {
        //        var lkey = node.Left.Key;
        //        Delete(node.Left.Key);
        //        node.Key = lkey;
        //    }
        //    else if (node.Left != null)
        //    {
        //        if (isLeft)
        //            parent.Left = node.Left;
        //        else
        //            parent.Right = node.Left;
        //    }
        //    else if (node.Right != null)
        //    {
        //        if (isLeft)
        //            parent.Left = node.Right;
        //        else
        //            parent.Right = node.Right;
        //    }
        //    else
        //    {
        //        if (isLeft)
        //            parent.Left = null;
        //        else
        //            parent.Right = null;
        //    }

        //    return node;
        //}

        //private int Size(SgNode node)
        //{
        //    int szLeft = 0;
        //    int szRight = 0;
        //    var unexplored = new Queue<SgNode>();
        //    unexplored.Enqueue(node);
        //    do
        //    {
        //        node = unexplored.Dequeue();

        //        if (node.Left != null)
        //        {
        //            unexplored.Enqueue(node.Left);
        //            szLeft++;
        //        }

        //        if (node.Right != null)
        //        {
        //            szRight++;
        //            unexplored.Enqueue(node.Right);
        //        }
        //    } while (unexplored.Count != 0);

        //    return szLeft + szRight + 1;
        //}
    }
}
