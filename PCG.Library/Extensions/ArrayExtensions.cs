using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CommonExtensions.NumericExtensions;

namespace PCG.Library.Extensions
{
    public static class ArrayExtensions
    {
        public static T[] Rotate90CW<T>(this T[] array, int width)
        {
            array.Transpose(width);
            for (int y = 0; y < width; y++)
                Array.Reverse(array, y * width, width);
            return array;
        }

        public static T[] Rotate90CCW<T>(this T[] array, int width)
        {
            for (int y = 0; y < width; y++)
                Array.Reverse(array, y * width, width);
            array.Transpose(width);
            return array;
        }

        public static T[] Rotate180<T>(this T[] array)
        {
            Array.Reverse(array);
            return array;
        }

        public static T[] Rotate45CW<T>(this T[] array, int width)
        {
            var rotated = new T[width * width];
            var max = width * 2 - 2;
            var count = 0;
            var y = 0; var x = 0;

            for (int i = 0; i <= max; i++)
            {
                y = i.LesserOf(width - 1);
                x = 0;
                while (y >= 0 && x < width)
                {
                    if (y + x == i)
                        rotated[count++] = array[PosToIdx(x++, y--, width)];
                    else
                        x++;
                }
            }

            return rotated;
        }

        public static T[] Transpose<T>(this T[] array, int width)
        {
            for (int y = 0; y < width; y++)
                for (int x = y+1; x < width; x++)
                    array.Swap(PosToIdx(x,y,width), PosToIdx(y,x,width));

            return array;
        }

        public static T[] CoTranspose<T>(this T[] array, int width)
        {
            Array.Reverse(array);
            array.Transpose(width);
            return array;
        }

        public static T[] FlipV<T>(this T[] array, int width)
        {
            Array.Reverse(array);
            return FlipH(array, width);
        }

        public static T[] FlipH<T>(this T[] array, int width)
        {
            for (int y = 0; y < width; y++)
                Array.Reverse(array, y * width, width);
            return array;
        }

        public static T[] Swap<T>(this T[] array, int index1, int index2)
        {
            var buffer = array[index2];
            array[index2] = array[index1];
            array[index1] = buffer;
            return array;
        }

        private static int PosToIdx(int x, int y, int width)
        {
            return x + y * width;
        }
    }
}
