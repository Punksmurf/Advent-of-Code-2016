using System.Collections.Generic;

namespace AoC2016.Solutions.Day16
{
    public class Dragon
    {
        /*
            The fun thing is that the content we need to write is a modified dragon curve.
            For a normal dragon curve:
                - start with 0
                - add 0, plus the reverse and inverse of itself
                - repeat

            So you get:
                0
                0 0 1
                001 0 011
                0010011 0 0011011
                001001100011011 0 001...

            The interesting bit is that because you continuously reverse and inverse itself you get a repeating pattern
                a = start
                b = reverse + inverse of a
                a0b0a1b0a0b1a1b
            Where the extra added 0's and 1's actually follow the rules of the dragon curve itself.

            This also works with our pattern, it's just the a and b parts that get longer:
                aaa0bbb0aaa1bbb0aaa0bbb1aaa1bbb

            This means we don't need to keep our entire "dragon" in memory and can just calculate the values on the fly
            for each index.

            The odd thing is, it takes a little longer, even for our gold star input (when getting a whole dragon).
        */

        public bool[] Pattern { get; }
        private readonly int _partLength;

        public Dragon(bool[] pattern)
        {
            Pattern = pattern;
            _partLength = Pattern.Length + 1;
        }

        public IEnumerable<bool> GetDragon(int length)
        {
            for (var i = 0; i < length; i++)
            {
                yield return GetModifiedDragonValue(i);
            }
        }

        public bool GetModifiedDragonValue(int i)
        {
            // We're in-between patterns, so we need to find the dragon value
            if (i % _partLength >= Pattern.Length) return GetNormalDragonValue(i / _partLength);

            // If i is in the pattern, get it from there
            if (i / _partLength % 2 == 0)
            {
                // even pattern = the original value
                return Pattern[i % _partLength];
            }
            else
            {
                // odd pattern = reverse inveerse value
                return !Pattern[Pattern.Length - 1 - (i % _partLength)];
            }
        }

        public static bool GetNormalDragonValue(int n)
        {
            while (true)
            {
                if (n % 2 == 0)
                {
                    return n / 2 % 2 != 0;
                }
                n = n / 2;
            }
        }
    }
}