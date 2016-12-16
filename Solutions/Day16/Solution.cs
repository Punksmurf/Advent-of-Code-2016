using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AoC2016.Solutions.Day16
{
    public class Solution : ISolution
    {
        public Task<string> SolveExampleAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                const string input = "10000";
                var inputList = input.ToCharArray().Select(c => c == '1').ToList();
                inputList = DragonDouble(inputList, 20);
                var checksum = CreateChecksum(inputList);
                return string.Concat(checksum.Select(_ => _ ? '1' : '0'));
            });
        }

        public Task<string> SolveSilverAsync(string input)
        {
            return Task.Factory.StartNew(() =>
            {
                var inputList = input.ToCharArray().Select(c => c == '1').ToList();
                inputList = DragonDouble(inputList, 272);
                var checksum = CreateChecksum(inputList);
                return string.Concat(checksum.Select(_ => _ ? '1' : '0'));
            });
        }

        public Task<string> SolveGoldAsync(string input)
        {
            return Task.Factory.StartNew(() =>
            {
                var inputList = input.ToCharArray().Select(c => c == '1').ToList();
                inputList = DragonDouble(inputList, 35651584);
                var checksum = CreateChecksum(inputList);
                return string.Concat(checksum.Select(_ => _ ? '1' : '0'));
            });
        }

        private static List<bool> DragonDouble(IReadOnlyList<bool> input, int diskLength)
        {
            while (input.Count < diskLength)
            {
                input = DragonDouble(input);
            }
            return input.Take(diskLength).ToList();
        }

        private static List<bool> DragonDouble(IReadOnlyList<bool> input)
        {
            var result = new List<bool>(input) {false};
            result.AddRange(input.Select(_ => !_).Reverse());
            return result;
        }

        private static IEnumerable<bool> CreateChecksum(IReadOnlyList<bool> input)
        {
            while (input.Count % 2 == 0)
            {
                input = CreatePotentialChecksum(input).ToList();
            }
            return input;
        }

        private static IEnumerable<bool> CreatePotentialChecksum(IReadOnlyList<bool> input)
        {
            if (input.Count % 2 != 0)
            {
                throw new Exception("Input must have even length");
            }
            for (var i = 0; i < input.Count; i += 2)
            {
                var a = input[i];
                var b = input[i + 1];
                yield return a == b;
            }
        }



    }
}