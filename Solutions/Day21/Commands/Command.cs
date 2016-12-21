using System;
using System.Text.RegularExpressions;

namespace AoC2016.Solutions.Day21.Commands
{
    public abstract class Command
    {
        public static class Factory
        {
            public static Command Create(string input)
            {
                const string pattern = @"(?<command>\w+ \w+).*?(?<a>\w+).*?(?<b>\w+)$";
                var match = Regex.Match(input, pattern);
                if (!match.Success) throw new Exception($"Cannot parse input: {input}");
                switch (match.Groups["command"].Value.ToLower())
                {
                    case "swap position":
                        return new SwapByPosition(int.Parse(match.Groups["a"].Value), int.Parse(match.Groups["b"].Value));
                    case "swap letter":
                        return new SwapLetters(match.Groups["a"].Value[0], match.Groups["b"].Value[0]);
                    case "rotate left":
                        return new RotateLeft(int.Parse(match.Groups["a"].Value));
                    case "rotate right":
                        return new RotateRight(int.Parse(match.Groups["a"].Value));
                    case "rotate based": //on position of letter [b]
                        return new RotateByPosition(match.Groups["b"].Value[0]);
                    case "reverse positions":
                        return new Reverse(int.Parse(match.Groups["a"].Value), int.Parse(match.Groups["b"].Value));
                    case "move position":
                        return new Move(int.Parse(match.Groups["a"].Value), int.Parse(match.Groups["b"].Value));
                    default: throw new Exception($"Cannot parse input: {input}; command {match.Groups["command"].Value} not recognized");
                }
            }
        }

        public abstract void Execute(ref char[] password);
        public abstract void ExecuteReverse(ref char[] password);

        protected static void SwapLetters(ref char[] password, char a, char b)
        {
            Swap(ref password, Array.IndexOf(password, a), Array.IndexOf(password, b));
        }

        protected static void Swap(ref char[] password, int a, int b)
        {
            var t = password[a];
            password[a] = password[b];
            password[b] = t;
        }

        protected static void RotateByPosition(ref char[] password, char letter)
        {
            /*
                `rotate based on position of letter X` means that the whole string should be rotated to the right based
                on the index of letter X (counting from 0) as determined before this instruction does any rotations.
                Once the index is determined, rotate the string to the right one time, plus a number of times equal to
                that index, plus one additional time if the index was at least 4
            */

            var index = Array.IndexOf(password, letter);
            var amount = 1 + index;
            if (index >= 4) amount += 1;
            RotateRight(ref password, amount);

        }

        protected static void RotateRight(ref char[] password, int amount)
        {
            RotateLeft(ref password, -amount);
        }

        protected static void RotateLeft(ref char[] password, int amount)
        {
            amount = (amount % password.Length + password.Length) % password.Length;

            for (var i = 0; i < amount; i++)
            {
                var t = password[0];
                for (var j = 0; j < password.Length - 1; j++)
                {
                    password[j] = password[j + 1];
                }
                password[password.Length - 1] = t;
            }
        }

        protected static void Reverse(ref char[] password, int a, int b)
        {
            if (a > b)
            {
                var t = a;
                a = b;
                b = t;
            }
            for (var i = 0; i < (b - a + 1) / 2; i++)
            {
                var t = password[a + i];
                password[a + i] = password[b - i];
                password[b - i] = t;
            }
        }

        protected static void Move(ref char[] password, int a, int b)
        {
            if (a < b)
            {
                var t = password[a];
                for (var i = a; i < b; i++)
                {
                    password[i] = password[i + 1];
                }
                password[b] = t;
            }
            else if (a > b)
            {
                var t = password[a];
                for (var i = a; i > b; i--)
                {
                    password[i] = password[i - 1];
                }
                password[b] = t;
            }
        }
    }
}