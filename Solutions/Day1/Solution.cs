using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AoC2016.Solutions.Day1
{
    public class Solution : ISolution
    {
        public int GetDay()
        {
            return 1;
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
            while (walker.HasNext()) walker.Next();

            var location = walker.GetLocation();
            var distance = Math.Abs(location.X) + Math.Abs(location.Y);

            return $"We think Easter Bunny HQ is at {location.X},{location.Y} for a total distance of {distance}";
        }

        private static string SolveGold(string input)
        {
            var walker = new CityWalker(input);

            var locationHistory = new List<Location>();

            while (walker.HasNext())
            {
                // When we turn, we stay at the same location but that doesn't count as a double visit to the same spot
                var currentLocation = walker.GetLocation();
                while (walker.HasNext() && currentLocation.Equals(walker.Next())) ;

                var location = walker.GetLocation();
                if (locationHistory.Contains(location))
                {
                    break;
                }
                locationHistory.Add(location);
            }
            var finalLocation = walker.GetLocation();
            var distance = Math.Abs(finalLocation.X) + Math.Abs(finalLocation.Y);

            return $"Easter Bunny HQ is actually at {finalLocation.X},{finalLocation.Y} for a total distance of {distance}";
        }
    }
}