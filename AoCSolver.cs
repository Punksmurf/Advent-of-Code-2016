using System;
using System.Diagnostics;
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

            Task<string> example;
            Task<string> silver;
            Task<string> gold;

            var exampleSw = new Stopwatch();
            var silverSw = new Stopwatch();
            var goldSw = new Stopwatch();


            try
            {
                exampleSw.Start();
                example = solution.SolveExampleAsync();
            }
            catch (NotImplementedException)
            {
                example = Task.FromResult("Not implemented");
            }

            try
            {
                silverSw.Start();
                silver = solution.SolveSilverAsync(content);
            }
            catch (NotImplementedException)
            {
                silver = Task.FromResult("Not implemented");
            }

            try
            {
                goldSw.Start();
                gold = solution.SolveGoldAsync(content);
            }
            catch (NotImplementedException)
            {
                gold = Task.FromResult("Not implemented");
            }

            Console.WriteLine($"Example solution for day {day}: {await example} ({exampleSw.ElapsedMilliseconds}ms)");
            Console.WriteLine($"Silver solution for day {day}: {await silver} ({silverSw.ElapsedMilliseconds}ms)");
            Console.WriteLine($"Gold solution for day {day}: {await gold} ({goldSw.ElapsedMilliseconds}ms)");
        }
    }
}