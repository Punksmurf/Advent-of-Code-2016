using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoC2016.Solutions.Day14
{
    public class Solution : ISolution
    {
        public Task<string> SolveExampleAsync()
        {
            return Task.Factory.StartNew(() => Solve("abc", 0));
        }

        public Task<string> SolveSilverAsync(string input)
        {
            return Task.Factory.StartNew(() => Solve(input, 0));
        }

        public Task<string> SolveGoldAsync(string input)
        {
            return Task.Factory.StartNew(() => Solve(input, 2016));
        }


        private static string Solve(string salt, int stretch)
        {
            using (var hasher = MD5.Create())
            {
                const string pattern3 = @"(\w)\1{2}";
                const string pattern5 = @"(\w)\1{4}";

                var tripletKeys = new List<Tuple<string, char, int>>(); // hash, triplet, index
                var validKeys = new List<string>();

                var index = 0;
                while (true)
                {
                    tripletKeys = tripletKeys.Where(t => t.Item3 >= index - 1000).ToList();

                    var hash = HashSaltIndex(hasher, salt, index, stretch);

                    foreach (Match match5 in Regex.Matches(hash, pattern5))
                    {
                        var c = match5.Groups[1].Value[0];

                        // Console.WriteLine($"{index} {hash} 5 {c}");

                        foreach (var matchingTriplet in tripletKeys.Where(t => t.Item2 == c).OrderBy(t => t.Item3))
                        {
                            // Console.WriteLine(
                            //     $"On index {matchingTriplet.Item3} hash {matchingTriplet.Item1} has 3 times {matchingTriplet.Item2} for which we found matching hash {hash} at {index}.");
                            tripletKeys.Remove(matchingTriplet);
                            validKeys.Add(matchingTriplet.Item1);

                            if (validKeys.Count == 64)
                            {
                                return $"The index of our 64th key was {matchingTriplet.Item3}";
                            }
                        }
                    }

                    // we only consider the first match
                    var match3 = Regex.Match(hash, pattern3);
                    if (match3.Success)
                    {
                        // Console.WriteLine($"{index} {hash} 3 {match3.Groups[1].Value[0]}");
                        tripletKeys.Add(Tuple.Create(hash, match3.Groups[1].Value[0], index));
                    }

                    index++;
                }
            }
        }

        private static string HashSaltIndex(HashAlgorithm hasher, string salt, int index, int stretch = 0)
        {
            return Hash(hasher, $"{salt}{index}", stretch);
        }

        private static string Hash(HashAlgorithm hasher, string input, int stretch)
        {
            for (var i = 0; i < stretch + 1; i++)
            {
                input = Hash(hasher, input);
            }
            return input;
        }

        private static string Hash(HashAlgorithm hasher, string input)
        {
            var hash = hasher.ComputeHash(Encoding.UTF8.GetBytes(input));
            var sb = new StringBuilder(32);
            foreach (var b in hash)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}