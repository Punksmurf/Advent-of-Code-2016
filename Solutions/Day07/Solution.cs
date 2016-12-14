using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoC2016.Solutions.Day07
{
    public class Solution : ISolution
    {
        private const string AbbaPattern = @"(?<a>\w)(?<b>(?!\k<a>)\w)(\k<b>)(\k<a>)";
        private const string BracketAbbaPattern = @"\[\w*" + AbbaPattern + @"\w*\]";
        private const string AbaPattern = @"(?<a>\w)(?<b>(?!\k<a>)\w)(\k<a>)";
        private const string BracketPattern = @"\[\w*\]";

        public Task<string> SolveExampleAsync()
        {

            return Task.Run(() =>
            {
                const string silverInput = @"abba[mnop]qrst
abcd[bddb]xyyx
aaaa[qwer]tyui
ioxxoj[asdfgh]zxcvbn";

                const string goldInput = @"aba[bab]xyz
xyx[xyx]xyx
aaa[kek]eke
zazbz[bzb]cd";

                var abbaCount = silverInput.Split('\n').Count(SupportsAutonomousBridgeBypassAnnotation);
                var sslCount = goldInput.Split('\n').Count(SupportsSuperSecretListening);
                return $"The number of IPs that suppport ABBA is {abbaCount} and the number of IPs that support SSL is {sslCount}";
            });
        }

        public Task<string> SolveSilverAsync(string input)
        {
            return Task.Run(() =>
            {
                var count = input.Split('\n').Count(SupportsAutonomousBridgeBypassAnnotation);
                return $"The number of IPs that suppport Autonomous Bridge Bypass Annotation is {count}";
            });
        }

        public Task<string> SolveGoldAsync(string input)
        {
            return Task.Run(() =>
            {
                var count = input.Split('\n').Count(SupportsSuperSecretListening);
                return $"The number of IPs that suppport Super-Secret Listening is {count}";
            });
        }

        private static bool SupportsAutonomousBridgeBypassAnnotation(string line)
        {
            return Regex.IsMatch(line, AbbaPattern) && !Regex.IsMatch(line, BracketAbbaPattern);
        }

        private static IEnumerable<string> GetAreaBroadcastAccessors(string line)
        {
            // Remove bracketed characters
            // Replace with a . to prevent matching over the pattern -> a[...]ba would match otherwise
            line = Regex.Replace(line, BracketPattern, ".");

            var abaStrings = new List<string>();

            // patterns can overlap so we can't just do a Regex.Matches(…)
            var regex = new Regex(AbaPattern);
            var match = regex.Match(line);
            while (match.Success)
            {
                abaStrings.Add(match.Value);
                match = regex.Match(line, match.Index + 1);
            }
            return abaStrings;
        }

        private static string AbaToBab(string aba)
        {
            if (aba.Length != 3 || aba[0] != aba[2] || aba[0] == aba[1]) throw new ArgumentException(nameof(aba));
            return string.Concat(new[] {aba[1], aba[0], aba[1]});
        }

        private static bool SupportsSuperSecretListening(string line)
        {
            var abas = GetAreaBroadcastAccessors(line);

            foreach (var aba in abas)
            {
                var bab = AbaToBab(aba);
                var pattern = @"\[\w*" + bab + @"\w*\]";

                if (Regex.IsMatch(line, pattern)) return true;
            }
            return false;
        }
    }
}