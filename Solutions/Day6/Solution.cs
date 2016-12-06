using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AoC2016.Solutions.Day6
{
    public class Solution : ISolution
    {
        public async Task<string> SolveExampleAsync()
        {
            const string input = @"eedadn
drvtee
eandsr
raavrd
atevrs
tsrnev
sdttsa
rasrtv
nssdts
ntnada
svetve
tesnvt
vntsnd
vrdear
dvrsen
enarar";

            return await Task.Run(() => UnJamSilver(input.Split('\n')));
        }

        public async Task<string> SolveSilverAsync(string input)
        {
            var message = await Task.Run(() => UnJamSilver(input.Split('\n')));
            return $"The unjammed message is {message}";
        }

        public async Task<string> SolveGoldAsync(string input)
        {
            var message = await Task.Run(() => UnJamGold(input.Split('\n')));
            return $"The real unjammed message is {message}";
        }

        public string UnJamSilver(string[] messages)
        {
            var numChars = messages.Select(_ => _.Length).Min();
            return string.Concat(
                Enumerable.Range(0, numChars).Select(columnIndex => messages.Select(message => message[columnIndex]))
                    .Select(column => column.GroupBy(_ => _)
                            .OrderByDescending(group => group.Count())
                            .Select(group => group.Key)
                            .First()
                    )
            );
        }

        public string UnJamGold(string[] messages)
        {
            var numChars = messages.Select(_ => _.Length).Min();
            return string.Concat(
                Enumerable.Range(0, numChars).Select(columnIndex => messages.Select(message => message[columnIndex]))
                    .Select(column => column.GroupBy(_ => _)
                            .OrderBy(group => group.Count())
                            .Select(group => group.Key)
                            .First()
                    )
            );
        }
    }
}