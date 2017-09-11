using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PCG.Library.Extensions;

namespace PCG.Tests
{
    [TestClass]
    public class ArrayTests
    {
        [TestMethod]
        public void TransposeTest()
        {
            var input = new int[] { 
                0x0, 0x1, 0x2, 0x3, 
                0x4, 0x5, 0x6, 0x7, 
                0x8, 0x9, 0xA, 0xB, 
                0xC, 0xD, 0xE, 0xF };
            var expected = new int[] { 
                0x0, 0x4, 0x8, 0xC, 
                0x1, 0x5, 0x9, 0xD, 
                0x2, 0x6, 0xA, 0xE, 
                0x3, 0x7, 0xB, 0xF };
            var output = input.Transpose(4);

            Assert.AreSame(input, output);
            for (int i = 0; i < expected.Length; i++)
                Assert.AreEqual(expected[i], output[i]);
        }

        [TestMethod]
        public void CoTranspose_Test()
        {
            var input = new int[] { 
                0x0, 0x1, 0x2, 0x3, 
                0x4, 0x5, 0x6, 0x7, 
                0x8, 0x9, 0xA, 0xB, 
                0xC, 0xD, 0xE, 0xF };
            var expected = new int[] { 
                0xF, 0xB, 0x7, 0x3, 
                0xE, 0xA, 0x6, 0x2, 
                0xD, 0x9, 0x5, 0x1, 
                0xC, 0x8, 0x4, 0x0 };
            var output = input.CoTranspose(4);

            Assert.AreSame(input, output);
            for (int i = 0; i < expected.Length; i++)
                Assert.AreEqual(expected[i], output[i]);
        }

        [TestMethod]
        public void Rotate90CWTest()
        {
            var input = new int[] { 
                0x0, 0x1, 0x2, 0x3, 
                0x4, 0x5, 0x6, 0x7, 
                0x8, 0x9, 0xA, 0xB, 
                0xC, 0xD, 0xE, 0xF };
            var expected = new int[] { 
                0xC, 0x8, 0x4, 0x0, 
                0xD, 0x9, 0x5, 0x1, 
                0xE, 0xA, 0x6, 0x2, 
                0xF, 0xB, 0x7, 0x3 };
            var output = input.Rotate90CW(4);

            Assert.AreSame(input, output);
            for (int i = 0; i < expected.Length; i++)
                Assert.AreEqual(expected[i], output[i]);
        }

        [TestMethod]
        public void Rotate90CCWTest()
        {
            var input = new int[] { 
                0x0, 0x1, 0x2, 0x3, 
                0x4, 0x5, 0x6, 0x7, 
                0x8, 0x9, 0xA, 0xB, 
                0xC, 0xD, 0xE, 0xF };
            var expected = new int[] { 
                0x3, 0x7, 0xB, 0xF, 
                0x2, 0x6, 0xA, 0xE, 
                0x1, 0x5, 0x9, 0xD, 
                0x0, 0x4, 0x8, 0xC };
            var output = input.Rotate90CCW(4);

            Assert.AreSame(input, output);
            for (int i = 0; i < expected.Length; i++)
                Assert.AreEqual(expected[i], output[i]);
        }

        [TestMethod]
        public void Rotate180Test()
        {
            var input = new int[] { 
                0x0, 0x1, 0x2, 0x3, 
                0x4, 0x5, 0x6, 0x7, 
                0x8, 0x9, 0xA, 0xB, 
                0xC, 0xD, 0xE, 0xF };
            var expected = new int[] { 
                0xF, 0xE, 0xD, 0xC, 
                0xB, 0xA, 0x9, 0x8, 
                0x7, 0x6, 0x5, 0x4, 
                0x3, 0x2, 0x1, 0x0 };
            var output = input.Rotate180();

            Assert.AreSame(input, output);
            for (int i = 0; i < expected.Length; i++)
                Assert.AreEqual(expected[i], output[i]);
        }

        [TestMethod]
        public void FlipH_Test()
        {
            var input = new int[] { 
                0x0, 0x1, 0x2, 0x3, 
                0x4, 0x5, 0x6, 0x7, 
                0x8, 0x9, 0xA, 0xB, 
                0xC, 0xD, 0xE, 0xF };
            var expected = new int[] { 
                0x3, 0x2, 0x1, 0x0, 
                0x7, 0x6, 0x5, 0x4, 
                0xB, 0xA, 0x9, 0x8, 
                0xF, 0xE, 0xD, 0xC };
            var output = input.FlipH(4);

            Assert.AreSame(input, output);
            for (int i = 0; i < expected.Length; i++)
                Assert.AreEqual(expected[i], output[i]);
        }

        [TestMethod]
        public void FlipV_Test()
        {
            var input = new int[] { 
                0x0, 0x1, 0x2, 0x3, 
                0x4, 0x5, 0x6, 0x7, 
                0x8, 0x9, 0xA, 0xB, 
                0xC, 0xD, 0xE, 0xF };
            var expected = new int[] { 
                0xC, 0xD, 0xE, 0xF, 
                0x8, 0x9, 0xA, 0xB, 
                0x4, 0x5, 0x6, 0x7, 
                0x0, 0x1, 0x2, 0x3};
            var output = input.FlipV(4);

            Assert.AreSame(input, output);
            for (int i = 0; i < expected.Length; i++)
                Assert.AreEqual(expected[i], output[i]);
        }

        [TestMethod]
        public void Rotate45CWTest()
        {
            var input = new int[] { 
                0x0, 0x1, 0x2, 0x3, 
                0x4, 0x5, 0x6, 0x7, 
                0x8, 0x9, 0xA, 0xB, 
                0xC, 0xD, 0xE, 0xF };
            var expected = new int[] { 
                0x0, 0x4, 0x1, 0x8, 
                0x5, 0x2, 0xC, 0x9, 
                0x6, 0x3, 0xD, 0xA, 
                0x7, 0xE, 0xB, 0xF };
            var output = input.Rotate45CW(4);

            //Assert.AreSame(input, output);
            for (int i = 0; i < expected.Length; i++)
                Assert.AreEqual(expected[i], output[i]);
        }
    }
}
