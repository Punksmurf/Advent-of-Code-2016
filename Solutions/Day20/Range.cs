using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace AoC2016.Solutions.Day20
{
    public class Range
    {
        public static class Factory
        {
            public static Range Create(string input)
            {
                const string pattern = @"(\d+)-(\d+)";
                var match = Regex.Match(input, pattern);
                if (!match.Success) throw new Exception($"Cannot parse input: {input}");
                return new Range(uint.Parse(match.Groups[1].Value), uint.Parse(match.Groups[2].Value));
            }

            public static IEnumerable<Range> CreateAll(string input)
            {
                return input.Split('\n').Select(Create);
            }
        }

        public uint Low { get; private set; }
        public uint High { get; private set; }

        public Range(uint a, uint b)
        {
            Low = Math.Min(a, b);
            High = Math.Max(a, b);
        }

        public uint Length()
        {
            return High - Low + 1; // Both values are inclusive
        }

        public bool Intersects(Range other)
        {
            return Low <= (other.High == uint.MaxValue ? other.High : other.High + 1) &&
                   High >= (other.Low == uint.MinValue ? other.Low : other.Low - 1);
        }

        public void Merge(Range other)
        {
            if (!Intersects(other)) throw new Exception("Ranges do not intersect");
            Low = Math.Min(Low, other.Low);
            High = Math.Max(High, other.High);
        }

        public override string ToString()
        {
            return $"Range [{Low}, {High}]";
        }
    }
}