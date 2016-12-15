using System;
using System.Text.RegularExpressions;

namespace AoC2016.Solutions.Day15
{
    public class Disc
    {
        public int Number { get; }
        public int Size { get; }
        public int StartPosition { get; }

        public static class Factory
        {
            public static Disc Create(string description)
            {
                const string pattern = @".*?(?<number>\d+).*?(?<size>\d+).*?\d+.*?(?<startposition>\d+)\.";
                var match = Regex.Match(description, pattern);
                if (!match.Success) throw new Exception($"Unable to parse disc description {description}");
                return new Disc(
                        int.Parse(match.Groups["number"].Value),
                        int.Parse(match.Groups["size"].Value),
                        int.Parse(match.Groups["number"].Value)
                );
            }
        }

        public Disc(int number, int size, int startPosition)
        {
            Number = number;
            Size = size;
            StartPosition = startPosition;
        }

        public bool IsOpenAt(int t)
        {
            return (StartPosition + Number + t) % Size == 0;
        }
    }
}