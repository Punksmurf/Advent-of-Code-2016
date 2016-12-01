using System;
using AoC2016.Service;

namespace AoC2016
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Expected 2 arguments");
                Console.WriteLine("Usage: AoC2016 <session token> <day>");
                return;
            }

            var sessionToken = args[0];
            var day = int.Parse(args[1]);

            var challengeDataService = new ChallengeDataService(sessionToken);
            var solver = new AoCSolver(challengeDataService);
            solver.Solve(day).Wait();
        }
    }
}