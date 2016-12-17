using System;
using System.Collections.Generic;
using AoC2016.Utils;

namespace AoC2016.Solutions.Day13
{
    public class Maze
    {
        private readonly int _seed;

        public Maze(int seed)
        {
            _seed = seed;
        }

        public bool IsOpen(int x, int y)
        {
            if (x < 0 || y < 0) throw new IndexOutOfRangeException();
            var value = x * x + 3 * x + 2 * x * y + y + y * y + _seed;
            return SparseBitCount(value) % 2 == 0;

        }

        public void Print(int w, int h, ICollection<Coordinate> path = null)
        {
            Console.Write("  ");
            for (var x = 0; x < w; x++)
            {
                var xs = x.ToString();
                Console.Write(xs[xs.Length - 1]);
            }
            Console.WriteLine();

            for (var y = 0; y < h; y++)
            {
                var ys = y.ToString();
                Console.Write(ys[ys.Length - 1]);
                Console.Write(' ');
                for (var x = 0; x < w; x++)
                {
                    if (path?.Contains(new Coordinate(x, y)) ?? false)
                    {
                        Console.Write('O');

                    }
                    else
                    {
                        Console.Write(IsOpen(x, y) ? '.' : '#');
                    }
                }
                Console.WriteLine();
            }
        }

        private static int SparseBitCount(int n)
        {
            var count = 0;
            while (n != 0)
            {
                count++;
                n &= n - 1;
            }
            return count;
        }
    }
}