using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AoC2016.Service;

namespace AoC2016
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Expected 2 arguments");
                Console.WriteLine("Usage: AoC2016 <session token> - attempt to solve all days (0..25)");
                Console.WriteLine("Usage: AoC2016 <session token> <day> - attempt to solve <day>");
                Console.WriteLine("Usage: AoC2016 <session token> <start day> <end day> - attempt to solve days <start day> through <end day>");
                return;
            }

            var sessionToken = args[0];
            var startDay = 1;
            if (args.Length > 1)
            {
                startDay = int.Parse(args[1]);
            }

            var endDay = startDay;
            if (args.Length == 1)
            {
                endDay = 25;
            }
            else if (args.Length > 2)
            {
                endDay = int.Parse(args[2]);
            }

            var challengeDataService = new ChallengeDataService(sessionToken);
            var solver = new AoCSolver(challengeDataService);

            var tasks = new List<Task>(endDay - startDay + 1);
            for (var day = startDay; day <= endDay; day++)
            {
                tasks.Add(solver.Solve(day));
            }

            Task.WaitAll(tasks.ToArray());
        }
    }
}