using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AoC2016.Solutions.Day19
{
    public class Solution : ISolution
    {
        public Task<string> SolveExampleAsync()
        {
            return Task.Factory.StartNew(() => $"When there are 5 elfs, elf {SolveGold2(7) + 1} ends up with all the presents");
        }

        public Task<string> SolveSilverAsync(string input)
        {
            var n = int.Parse(input);
            return Task.Factory.StartNew(() => $"When there are {n} elfs, elf {SolveSilver(n) + 1} ends up with all the presents");

        }

        public Task<string> SolveGoldAsync(string input)
        {
            var n = int.Parse(input);
            return Task.Factory.StartNew(() => $"When there are {n} elfs, under the new rules elf {SolveGold2(n) + 1} ends up with all the presents");

        }

        public static int SolveSilver(int n)
        {
            var elfs = new BitArray(n, true);

            while (true)
            {
                for (var i = 0; i < n; i++)
                {
                    if (!elfs[i]) continue;

                    var nextElf = GetNextElfWithPresentsSilver(elfs, i+1);
                    if (nextElf == i) return i;
                    elfs[nextElf] = false;
                }
            }
        }

        public static int GetNextElfWithPresentsSilver(BitArray elfs, int start = 0)
        {
            while (true)
            {
                for (var i = start; i < elfs.Length; i++)
                {
                    if (elfs[i]) return i;
                }
                if (start == 0) throw new Exception("No elves with presents left - maybe Santa stole them to shut them up?");
                start = 0;
            }
        }

        public static int SolveGold2(int n)
        {
            var elfs = new List<int>(n);
            for (var i = 0; i < n; i++) elfs.Add(i);

            var lastElf = n;
            var r = 0;
            while (true)
            {
                r++;
                if (r % 1000 == 0)
                {
                    Console.Write('.');
                    if (r % 100000 == 0)
                    {
                        Console.WriteLine();
                    }
                }
                var elfIndex = elfs.IndexOf(lastElf);
                elfIndex++;
                elfIndex %= elfs.Count;
                lastElf = elfs[elfIndex];

                var d = elfs.Count / 2;
                var oppositeIndex = elfIndex + d;
                oppositeIndex %= elfs.Count;
//                Console.WriteLine($"Elf {elfs[elfIndex]} ({elfIndex}) eliminates {elfs[oppositeIndex]} ({oppositeIndex})");

                elfs.RemoveAt(oppositeIndex);
                if (elfs.Count == 1)
                {
                    return elfs[0];
                }
            }
        }

    }
}