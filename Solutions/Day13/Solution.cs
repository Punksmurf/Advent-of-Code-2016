using System;
using System.Threading.Tasks;
using AoC2016.Utils;

namespace AoC2016.Solutions.Day13
{
    public class Solution : ISolution
    {
        public Task<string> SolveExampleAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                var maze = new Maze(10);
                var pathFinder = new PathFinder(maze);
                var path = pathFinder.FindPath(new Coordinate(1, 1), new Coordinate(7, 4));
                // maze.Print(10, 7, path);

                // Number of steps is one less than the length of the path
                return $"Example path size: {path.Count - 1}.";
            });
        }

        public Task<string> SolveSilverAsync(string input)
        {
            return Task.Factory.StartNew(() =>
            {
                var maze = new Maze(int.Parse(input));
                try
                {
                    var pathFinder = new PathFinder(maze);
                    var path = pathFinder.FindPath(new Coordinate(1, 1), new Coordinate(31, 39));
                    // maze.Print(50, 50, path);

                    // Number of steps is one less than the length of the path
                    return $"Example path size: {path.Count - 1}.";
                }
                catch (Exception)
                {
                    maze.Print(50, 50);
                    return "No valid path found.";
                }

            });
        }

        public Task<string> SolveGoldAsync(string input)
        {
            return Task.Factory.StartNew(() =>
            {
                var maze = new Maze(int.Parse(input));
                var pathFinder = new PathFinder(maze);
                var reachable = pathFinder.FindReachableCoordinates(new Coordinate(1, 1), 50);
                // maze.Print(50, 50, reachable);
                return $"Reachable coordinates: {reachable.Count}.";
            });
        }

    }
}