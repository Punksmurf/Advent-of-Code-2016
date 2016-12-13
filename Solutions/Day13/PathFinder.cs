using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AoC2016.Solutions.Day13
{
    public class PathFinder
    {
        private readonly Maze _maze;

        public PathFinder(Maze maze)
        {
            _maze = maze;
        }

        // Yay wiki: https://en.wikipedia.org/wiki/A*_search_algorithm
        public List<Coordinate> FindPath(Coordinate start, Coordinate goal)
        {
            if (!GetNeighbors(start).Any()) throw new Exception("Starting at a boxed in location!");

            // The set of nodes already evaluated.
            var closedSet = new HashSet<Coordinate>();

            // The set of currently discovered nodes still to be evaluated.
            // Initially, only the start node is known.
            var openSet = new HashSet<Coordinate> { start };

            // For each node, which node it can most efficiently be reached from.
            // If a node can be reached from many nodes, cameFrom will eventually contain the
            // most efficient previous step.
            var cameFrom = new Dictionary<Coordinate, Coordinate>();

            // For each node, the cost of getting from the start node to that node.
            // The cost of going from start to start is zero.
            var gScore = new Dictionary<Coordinate, int> {{start, 0}};

            // For each node, the total cost of getting from the start node to the goal
            // by passing by that node. That value is partly known, partly heuristic.
            // For the first node, that value is completely heuristic.
            var fScore = new Dictionary<Coordinate, int> {{start, HeuristicCostEstimate(start, goal)}};

            while (openSet.Any())
            {
                // the node in openSet having the lowest fScore[] value
                var current = openSet.OrderBy(c => fScore.ContainsKey(c) ? fScore[c] : int.MaxValue).First();
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

                openSet.Remove(current);
                closedSet.Add(current);

                foreach (var neighbor in GetNeighbors(current))
                {
                    // Ignore the neighbor which is already evaluated.
                    if (closedSet.Contains(neighbor)) continue;

                    // The distance from start to a neighbor
                    var tentativeScore = gScore[current] + 1; // 1 is distance between each map point
                    if (!openSet.Contains(neighbor))
                    {
                        openSet.Add(neighbor);
                    }

                    // This is not a better path.
                    if (tentativeScore >= (gScore.ContainsKey(neighbor) ? gScore[neighbor] : int.MaxValue)) continue;

                    // This path is the best until now. Record it!
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeScore;
                    fScore[neighbor] = tentativeScore + HeuristicCostEstimate(neighbor, goal);
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


        public int HeuristicCostEstimate(Coordinate start, Coordinate goal)
        {
            return Math.Abs(start.X - goal.X) + Math.Abs(start.Y - goal.Y);
        }
    }
}