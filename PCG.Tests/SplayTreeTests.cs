using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PCG.Library.Collections;

namespace PCG.Tests
{
    [TestClass]
    public class SplayTreeTests
    {
        [TestMethod]
        public void SplayTreeTest()
        {
            var tree = new SplayTree<int>();
            for (int i = 0; i < 100; i++)
            {
                tree.Add(i);
            }
            Assert.AreEqual(100, tree.Count);
            var x = tree.Find(10);
            Assert.AreEqual(10, x);
            tree.Remove(x);
            Assert.AreEqual(99, tree.Count);
            tree.Clear();
            Assert.AreEqual(0, tree.Count);
            
        }
    }
}
