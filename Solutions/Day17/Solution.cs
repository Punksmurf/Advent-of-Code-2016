using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AoC2016.Utils;

namespace AoC2016.Solutions.Day17
{
    public class Solution : ISolution
    {
        public Task<string> SolveExampleAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                var inputs = new[] {"ihgpwlah", "kglvqrro", "ulqzkmiv"};
                var output = new StringBuilder();
                foreach (var input in inputs)
                {
                    output.AppendLine($"Solved {input} for {SolveShortest(input)}");
                    output.AppendLine($"{SolveLongest(input)}");
                }
                return output.ToString();
            });
        }

        public Task<string> SolveSilverAsync(string input)
        {
            return Task.Factory.StartNew(() => SolveShortest(input));
        }

        public Task<string> SolveGoldAsync(string input)
        {
            return Task.Factory.StartNew(() => SolveLongest(input));
        }

        public string SolveShortest(string input)
        {
            using (var md5 = new Md5(input))
            {
                var route = new List<Direction>();

                var queue = new Queue<RoomState>();
                queue.Enqueue(RoomState.Factory.Create(md5, route.ToArray()));

                while (queue.Any())
                {
                    var state = queue.Dequeue();

                    if (state.Cooordinate.X == 3 && state.Cooordinate.Y == 3)
                    {
                        return string.Concat(state.Route.Select(_ => _.GetChar()));
                    }
                    for (var d = 0; d < 4; d++)
                    {
                        if (!state.Doors[d]) continue;

                        var directions = state.Route.ToList();
                        directions.Add((Direction)d);
                        queue.Enqueue(RoomState.Factory.Create(md5, directions.ToArray()));
                    }
                }
                return "No solution";
            }
        }
        public string SolveLongest(string input)
        {
            using (var md5 = new Md5(input))
            {
                var route = new List<Direction>();

                var queue = new Queue<RoomState>();
                queue.Enqueue(RoomState.Factory.Create(md5, route.ToArray()));

                var longest = 0;

                while (queue.Any())
                {
                    var state = queue.Dequeue();

                    if (state.Cooordinate.X == 3 && state.Cooordinate.Y == 3)
                    {
                        longest = Math.Max(state.Route.Length, longest);
                    }
                    else
                    {
                        for (var d = 0; d < 4; d++)
                        {
                            if (!state.Doors[d]) continue;
//                        Console.WriteLine($"Adding: {(Direction)d}");

                            var directions = state.Route.ToList();
                            directions.Add((Direction) d);
                            queue.Enqueue(RoomState.Factory.Create(md5, directions.ToArray()));
                        }
                    }
                }
                return $"The most roundabout way of getting to the end takes {longest} steps";
            }
        }
    }
}