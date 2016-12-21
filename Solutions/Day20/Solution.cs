using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AoC2016.Solutions.Day20
{
    public class Solution : ISolution
    {
        public Task<string> SolveExampleAsync()
        {
            const string input = @"5-8
0-2
4-7";
            return Task.Run(() => $"We got {SolveSilver(input)}");
        }

        public Task<string> SolveSilverAsync(string input)
        {
            return Task.Run(() => $"The lowest allowable IP is: {SolveSilver(input)}");
        }

        public Task<string> SolveGoldAsync(string input)
        {
            return Task.Run(() => $"The total number of allowable IPs is: {SolveGold(input)}");
        }

        public uint SolveSilver(string input)
        {
            var ranges = CreateRanges(input);
            return ranges[0].Low > 0 ? 0 : ranges.OrderBy(_ => _.High).First().High + 1;
        }

        public uint SolveGold(string input)
        {
            return CreateRanges(input).Aggregate(4294967295, (current, range) => current - range.Length()) + 1; // 0 is inclusive
        }

        public List<Range> CreateRanges(string input)
        {
            var ranges = new List<Range>();

            foreach (var range in Range.Factory.CreateAll(input).OrderBy(_ => _.Low))
            {
                if (!ranges.Any())
                {
                    ranges.Add(range);
                    continue;
                }

                var last = ranges.Last();
                if (last.Intersects(range))
                {
                    last.Merge(range);
                }
                else
                {
                    ranges.Add(range);
                }
            }

            return ranges;
        }
    }
}