using System;
using System.Diagnostics;
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

                var t = installation.Solve();

                return $"We need to start the installation at t={t}";
            });
        }

        public Task<string> SolveSilverAsync(string input)
        {
            return Task.Factory.StartNew(() =>
            {
                var installation = new Installation();

                foreach (var disc in input.Split('\n').Select(Disc.Factory.Create))
                {
                    installation.AddDisc(disc);
                }

                var t = installation.Solve();

                return $"We need to start the installation at t={t}";
            });

        }

        public Task<string> SolveGoldAsync(string input)
        {
            return Task.Factory.StartNew(() =>
            {
                var installation = new Installation();
                foreach (var disc in input.Split('\n').Select(Disc.Factory.Create))
                {
                    installation.AddDisc(disc);
                }
                installation.AddDisc(new Disc(7, 11, 0));

                var t = installation.Solve();

                return $"We need to start the installation at t={t}";
            });
        }
    }
}