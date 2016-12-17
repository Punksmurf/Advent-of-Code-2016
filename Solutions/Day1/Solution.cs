using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AoC2016.Utils;

namespace AoC2016.Solutions.Day1
{
    public class Solution : ISolution
    {
        public async Task<string> SolveExampleAsync()
        {
            return await SolveSilverAsync("R2, L3");
        }

        public async Task<string> SolveSilverAsync(string input)
        {
            return await Task.Run(() => SolveSilver(input));
        }

        public async Task<string> SolveGoldAsync(string input)
        {
            return await Task.Run(() => SolveGold(input));
        }

        private static string SolveSilver(string input)
        {
            var walker = new CityWalker(input);

            var location = walker.GetLocations().Last();
            var distance = Math.Abs(location.X) + Math.Abs(location.Y);

            return $"We think Easter Bunny HQ is at {location.X},{location.Y} for a total distance of {distance}";
        }

        private static string SolveGold(string input)
        {
            var walker = new CityWalker(input);

            var history = new HashSet<Coordinate>();

            Coordinate found = null;
            foreach (var location in walker.GetLocations())
            {
                if (history.Contains(location))
                {
                    found = location;
                    break;
                }

                history.Add(location);
            }

            var distance = found != null ? Math.Abs(found.X) + Math.Abs(found.Y) : 0;

            return $"Easter Bunny HQ is actually at {found?.X},{found?.Y} for a total distance of {distance}";
        }
    }
}