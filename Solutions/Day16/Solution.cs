using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
                var dragon = new Dragon(input.ToCharArray().Select(c => c == '1').ToArray());
                var checksum = Checksum(dragon.GetModifiedDragonValue, 20);
                return string.Concat(checksum.Select(_ => _ ? '1' : '0'));
            });
        }

        public Task<string> SolveSilverAsync(string input)
        {
            return Task.Factory.StartNew(() =>
            {
                var dragon = new Dragon(input.ToCharArray().Select(c => c == '1').ToArray());
                var checksum = Checksum(dragon.GetModifiedDragonValue, 272);
                return string.Concat(checksum.Select(_ => _ ? '1' : '0'));
            });
        }

        public Task<string> SolveGoldAsync(string input)
        {
            return Task.Factory.StartNew(() =>
            {
                var dragon = new Dragon(input.ToCharArray().Select(c => c == '1').ToArray());
                var checksum = Checksum(dragon.GetModifiedDragonValue, 35651584);
                return string.Concat(checksum.Select(_ => _ ? '1' : '0'));
            });
        }

        private static IEnumerable<bool> Checksum(Func<int, bool> input, int length)
        {
            var outLength = length / 2;
            if (outLength % 2 == 1)
            {
                for (var i = 0; i < outLength; i++)
                {
                    yield return input(i * 2) == input(i * 2 + 1);
                }
            }
            else
            {
                foreach (var b in Checksum(i => input(i * 2) == input(i * 2 + 1), outLength))
                {
                    yield return b;
                }
            }
        }

    }
}