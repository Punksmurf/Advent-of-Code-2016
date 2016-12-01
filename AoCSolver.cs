using System;
using System.Threading.Tasks;
using AoC2016.Service;
using AoC2016.Solutions;

namespace AoC2016
{
    public class AoCSolver
    {
        private readonly IChallengeDataService _dataService;

        public AoCSolver(IChallengeDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task Solve(int day)
        {
            ISolution solution;

            try
            {
                solution = SolutionProvider.GetSolution(day);
            }
            catch (NotImplementedException)
            {
                Console.WriteLine($"Solution for day {day} not implemented");
                return;
            }

            var content = await _dataService.GetAsync(day);

            try
            {
                var silver = await solution.SolveSilverAsync(content);
                Console.WriteLine($"Silver solution for day {day}: {silver}");
            }
            catch (NotImplementedException)
            {
                Console.WriteLine($"Silver solution for day {day} not implemented");
            }

            try
            {
                var gold = await solution.SolveGoldAsync(content);
                Console.WriteLine($"Gold solution for day {day}: {gold}");
            }
            catch (NotImplementedException)
            {
                Console.WriteLine($"Gold solution for day {day} not implemented");
            }
        }
    }
}