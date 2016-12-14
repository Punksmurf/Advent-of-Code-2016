using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoC2016.Solutions.Day09
{
    public class Solution : ISolution
    {
        public Task<string> SolveExampleAsync()
        {
            var inputs = new[]
            {
                "ADVENT",
                "A(1x5)BC",
                "(3x3)XYZ",
                "A(2x2)BCD(2x2)EFG",
                "(6x1)(1x3)A",
                "X(8x2)(3x3)ABCY",

                "(27x12)(20x12)(13x14)(7x10)(1x12)A",
                "(25x3)(3x3)ABC(2x3)XY(5x2)PQRSTX(18x9)(3x2)TWO(5x7)SEVEN"
            };

            foreach (var input in inputs)
            {
                // Console.WriteLine($"{input} - {CalculateDecompressedLengthRecursively(input)}");
            }

            return Task.Run(() => "Done.");
        }

        public async Task<string> SolveSilverAsync(string input)
        {
            return await Task.Run(() => $"The length of the decompressed string is: {CalculateDecompressedLength(input)}");
        }

        public async Task<string> SolveGoldAsync(string input)
        {
            return await Task.Run(() => $"The length of the recursively decompressed string is: {CalculateDecompressedLengthRecursively(input)}");
        }

        private static long CalculateDecompressedLength(string input)
        {
            input = Regex.Replace(input, @"\s", "");

            const string pattern = @"\((?<length>\d+)x(?<times>\d+)\)";
            var matcher = new Regex(pattern);
            long decompressedLength = 0;

            var match = matcher.Match(input);
            var startAt = 0;
            while (match.Success)
            {
                var length = int.Parse(match.Groups["length"].Value);
                var times = int.Parse(match.Groups["times"].Value);

                var inBetween = match.Index - startAt;

                decompressedLength += inBetween + length * times;

                startAt = match.Index + match.Length + length;
                match = matcher.Match(input, startAt);
            }

            decompressedLength += input.Length - startAt;

            return decompressedLength;
        }

        private static long CalculateDecompressedLengthRecursively(string input)
        {
            input = Regex.Replace(input, @"\s", "");
            const string pattern = @"\((?<length>\d+)x(?<times>\d+)\)";
            var matcher = new Regex(pattern);
            long decompressedLength = 0;

            var match = matcher.Match(input);
            var startAt = 0;
            while (match.Success)
            {
                var length = int.Parse(match.Groups["length"].Value);
                var times = int.Parse(match.Groups["times"].Value);

                var inBetween = match.Index - startAt;

                var part = input.Substring(match.Index + match.Length, length);
                var partLength = CalculateDecompressedLengthRecursively(part);

                decompressedLength += inBetween + partLength * times;

                startAt = match.Index + match.Length + length;
                match = matcher.Match(input, startAt);
            }

            decompressedLength += input.Length - startAt;

            return decompressedLength;
        }
    }
}