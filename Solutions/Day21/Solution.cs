using System;
using System.Linq;
using System.Threading.Tasks;
using AoC2016.Solutions.Day21.Commands;

namespace AoC2016.Solutions.Day21
{
    public class Solution : ISolution
    {
        public Task<string> SolveExampleAsync()
        {
            return Task.Run(() =>
            {
                const string input = @"swap position 4 with position 0
swap letter d with letter b
reverse positions 0 through 4
rotate left 1 step
move position 1 to position 4
move position 3 to position 0
rotate based on position of letter b
rotate based on position of letter d";

                var password = "abcde".ToCharArray();
                return $"The scrambled password is {string.Concat(SolveSilver(input, password))}";
            });
        }

        public Task<string> SolveSilverAsync(string input)
        {
            return Task.Run(() =>
            {
                var password = "abcdefgh".ToCharArray();
                return $"The scrambled password is {string.Concat(SolveSilver(input, password))}";
            });
        }

        public Task<string> SolveGoldAsync(string input)
        {
            return Task.Run(() =>
            {
                var scrambledPassword = "fbgdceah".ToCharArray();
                return $"The unscrambled password is {string.Concat(SolveGold(input, scrambledPassword))}";
            });
        }

        public char[] SolveSilver(string input, char[] password)
        {
            foreach (var command in input.Split('\n').Select(Command.Factory.Create))
            {
                command.Execute(ref password);
            }
            return password;
        }

        public char[] SolveGold(string input, char[] scrambledPassword)
        {
            foreach (var command in input.Split('\n').Select(Command.Factory.Create).Reverse())
            {
                command.ExecuteReverse(ref scrambledPassword);
            }
            return scrambledPassword;
        }

    }
}