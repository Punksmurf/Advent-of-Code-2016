using System;
using System.Linq;
using System.Threading.Tasks;

namespace AoC2016.Solutions.Day15
{
    public class Solution : ISolution
    {
        public Task<string> SolveExampleAsync()
        {

            return Task.Factory.StartNew(() =>
            {
                var installation = new Installation();
                installation.AddDisc(new Disc(1, 5, 4));
                installation.AddDisc(new Disc(2, 2, 1));

                var i = 0;
                while (!installation.IsOpenAt(i)) i++;

                return $"We need to start the installation at t={i}";
            });
        }

        public Task<string> SolveSilverAsync(string input)
        {
            // Using brute force, instead of solving it nicely
            return Task.Factory.StartNew(() =>
            {
                var installation = new Installation();

                foreach (var disc in input.Split('\n').Select(Disc.Factory.Create))
                {
                    installation.AddDisc(disc);
                }

                var i = 0;
                while (!installation.IsOpenAt(i)) i++;

                return $"We need to start the installation at t={i}";
            });

        }

        public Task<string> SolveGoldAsync(string input)
        {
            // Going from 414960 to 4564560 combinations will take a little longer though...
            return Task.Factory.StartNew(() =>
            {
                var installation = new Installation();

                foreach (var disc in input.Split('\n').Select(Disc.Factory.Create))
                {
                    installation.AddDisc(disc);
                }
                installation.AddDisc(new Disc(7, 11, 0));

                var i = 0;
                while (!installation.IsOpenAt(i)) i++;

                return $"We need to start the installation at t={i}";
            });
        }
    }
}