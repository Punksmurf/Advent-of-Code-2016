using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AoC2016.Solutions.Day18
{
    public class Solution : ISolution
    {
        public Task<string> SolveExampleAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                const string input = ".^^.^.^^^^";
                var traps = 0;
                var safe = 0;

                foreach (var state in GetTraps(input.ToCharArray().Select(c => c == '^').ToArray()).Take(10))
                {
                    traps += state.Count(_ => _);
                    safe += state.Count(_ => !_);
//                    Console.WriteLine(CreateStateString(state));
                }
                return $"There are {traps} traps in the room and {safe} safe tiles.";
            });
        }

        public Task<string> SolveSilverAsync(string input)
        {
            return Task.Factory.StartNew(() =>
            {
                var traps = 0;
                var safe = 0;

                foreach (var state in GetTraps(input.ToCharArray().Select(c => c == '^').ToArray()).Take(40))
                {
                    traps += state.Count(_ => _);
                    safe += state.Count(_ => !_);
//                    Console.WriteLine(CreateStateString(state));
                }
                return $"There are {traps} traps in the room and {safe} safe tiles.";
            });
        }

        public Task<string> SolveGoldAsync(string input)
        {
            return Task.Factory.StartNew(() =>
            {
                var traps = 0;
                var safe = 0;

                foreach (var state in GetTraps(input.ToCharArray().Select(c => c == '^').ToArray()).Take(400000))
                {
                    traps += state.Count(_ => _);
                    safe += state.Count(_ => !_);
//                    Console.WriteLine(CreateStateString(state));
                }
                return $"There are {traps} traps in the room and {safe} safe tiles.";
            });
        }

        private static string CreateStateString(bool[] state)
        {
            return string.Concat(state.Select(_ => _ ? '^' : '.'));
        }

        private static IEnumerable<bool[]> GetTraps(bool[] state)
        {
            /*
                Its left and center tiles are traps, but its right tile is not.
                Its center and right tiles are traps, but its left tile is not.
                Only its left tile is a trap.
                Only its right tile is a trap.

                111 110 101 100 011 010 001 000
                 0   1   0   1   1   0   1   0  = Rule 90
            */

            // begin with the current state
            yield return state;

            while (true)
            {
                var newState = new bool[state.Length];
                for (var i = 0; i < state.Length; i++)
                {
                    var sl = i > 0 && state[i - 1];
                    var sm = state[i];
                    var sr = i < state.Length - 1 && state[i + 1];

                    var ns = false;
                    if (sl && sm && !sr)
                    {
                        ns = true;
                    }
                    else if (!sl && sm && sr)
                    {
                        ns = true;
                    }
                    else if (sl && !sm && !sr)
                    {
                        ns = true;
                    }
                    else if (!sl && !sm && sr)
                    {
                        ns = true;
                    }
                    newState[i] = ns;
                }
                state = newState;
                yield return state;
            }
        }
    }
}