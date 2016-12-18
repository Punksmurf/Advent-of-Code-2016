using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using AoC2016.Utils;

namespace AoC2016.Solutions.Day13
{
    public class PathFinder
    {
        private readonly Maze _maze;

        public PathFinder(Maze maze)
        {
            _maze = maze;
        }

        public List<Coordinate> FindPath(Coordinate start, Coordinate goal)
        {
            var queue = new Queue<Coordinate>();
            queue.Enqueue(start);

            // They'll also be in cameFrom.Values but the hashSet is faster
            var found = new HashSet<Coordinate> { start };
            var cameFrom = new Dictionary<Coordinate, Coordinate>();

            while (queue.Any())
            {
                var current = queue.Dequeue();

                if (current.Equals(goal))
                {
                    var path = new List<Coordinate> {current};
                    while (cameFrom.ContainsKey(current))
                    {
                        current = cameFrom[current];
                        path.Add(current);
                    }
                    return path;
                }

                foreach (var neighbor in GetNeighbors(current))
                {
                    if (found.Contains(neighbor)) continue;
                    found.Add(neighbor);
                    cameFrom.Add(neighbor, current);
                    queue.Enqueue(neighbor);
                }
            }
            throw new Exception("No valid path found");

        }

        public HashSet<Coordinate> FindReachableCoordinates(Coordinate start, int maxDepth)
        {
            var visited = new HashSet<Coordinate>();
            var queue = new Queue<Coordinate>();
            queue.Enqueue(start);

            var depth = 0;
            while (depth <= maxDepth)
            {
                var nextQueue = new Queue<Coordinate>();
                while (queue.Any())
                {
                    var current = queue.Dequeue();
                    visited.Add(current);

                    foreach (var neighbor in GetNeighbors(current).Where(_ => !visited.Contains(_)))
                    {
                        nextQueue.Enqueue(neighbor);
                    }
                }
                queue = nextQueue;
                depth++;
            }
            return visited;
        }

        public ImmutableHashSet<Coordinate> GetNeighbors(Coordinate coordinate)
        {
            return new List<Coordinate>
            {
                new Coordinate(coordinate.X - 1, coordinate.Y),
                new Coordinate(coordinate.X + 1, coordinate.Y),
                new Coordinate(coordinate.X, coordinate.Y - 1),
                new Coordinate(coordinate.X, coordinate.Y + 1),
                new Coordinate(coordinate.X, coordinate.Y + 1)
            }
                .Where(_ => _.X >= 0 && _.Y >= 0)
                .Where(_ => _maze.IsOpen(_.X, _.Y))
                .ToImmutableHashSet();
        }
    }
}