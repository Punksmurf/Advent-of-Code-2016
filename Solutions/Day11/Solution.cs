using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AoC2016.Solutions.Day11.State;
using Type = AoC2016.Solutions.Day11.State.Type;

namespace AoC2016.Solutions.Day11
{
    public class Solution : ISolution
    {
        private const bool ShowDebug = false;

        public async Task<string> SolveExampleAsync()
        {
            return await Task.Run(() => SolveExample());

        }

        public async Task<string> SolveSilverAsync(string input)
        {
            return await Task.Run(() => SolveSilver());

        }

        public async Task<string> SolveGoldAsync(string input)
        {
            return await Task.Run(() => SolveGold());
        }

        private static string SolveExample()
        {
            var inventoryFactory = new InventoryItem.Factory();
            var floors = new List<Floor>
            {
                new Floor(1, new List<InventoryItem>
                {
                    inventoryFactory.GetInstance(Type.Microchip, "Hydrogen", "HM"),
                    inventoryFactory.GetInstance(Type.Microchip, "Lithium", "LM")
                }),
                new Floor(2, new List<InventoryItem>
                {
                    inventoryFactory.GetInstance(Type.Generator, "Hydrogen", "HG")
                }),
                new Floor(3, new List<InventoryItem>
                {
                    inventoryFactory.GetInstance(Type.Generator, "Lithium", "LG")
                }),
                new Floor(4)
            };

            return Solve(inventoryFactory, floors);
        }

        private static string SolveSilver()
        {
            var inventoryFactory = new InventoryItem.Factory();
            var floors = new List<Floor>
            {
                new Floor(1, new List<InventoryItem>
                {
                    inventoryFactory.GetInstance(Type.Generator, "Thulium", "AG"),
                    inventoryFactory.GetInstance(Type.Microchip, "Thulium", "AM"),
                    inventoryFactory.GetInstance(Type.Generator, "Plutonium", "BG"),
                    inventoryFactory.GetInstance(Type.Generator, "Strontium", "CG")
                }),
                new Floor(2, new List<InventoryItem>
                {
                    inventoryFactory.GetInstance(Type.Microchip, "Plutonium", "BM"),
                    inventoryFactory.GetInstance(Type.Microchip, "Strontium", "CM")
                }),
                new Floor(3, new List<InventoryItem>
                {
                    inventoryFactory.GetInstance(Type.Generator, "Promethium", "DG"),
                    inventoryFactory.GetInstance(Type.Microchip, "Promethium", "DM"),
                    inventoryFactory.GetInstance(Type.Generator, "Ruthenium", "EG"),
                    inventoryFactory.GetInstance(Type.Microchip, "Ruthenium", "EM")
                }),
                new Floor(4)
            };

            return Solve(inventoryFactory, floors);
        }

        private static string SolveGold()
        {
            var inventoryFactory = new InventoryItem.Factory();
            var floors = new List<Floor>
            {
                new Floor(1, new List<InventoryItem>
                {
                    inventoryFactory.GetInstance(Type.Generator, "Elerium", "FG"),
                    inventoryFactory.GetInstance(Type.Microchip, "Elerium", "FM"),
                    inventoryFactory.GetInstance(Type.Generator, "Dilithium", "GG"),
                    inventoryFactory.GetInstance(Type.Microchip, "Dilithium", "GM"),

                    inventoryFactory.GetInstance(Type.Generator, "Thulium", "AG"),
                    inventoryFactory.GetInstance(Type.Microchip, "Thulium", "AM"),
                    inventoryFactory.GetInstance(Type.Generator, "Plutonium", "BG"),
                    inventoryFactory.GetInstance(Type.Generator, "Strontium", "CG")
                }),
                new Floor(2, new List<InventoryItem>
                {
                    inventoryFactory.GetInstance(Type.Microchip, "Plutonium", "BM"),
                    inventoryFactory.GetInstance(Type.Microchip, "Strontium", "CM")
                }),
                new Floor(3, new List<InventoryItem>
                {
                    inventoryFactory.GetInstance(Type.Generator, "Promethium", "DG"),
                    inventoryFactory.GetInstance(Type.Microchip, "Promethium", "DM"),
                    inventoryFactory.GetInstance(Type.Generator, "Ruthenium", "EG"),
                    inventoryFactory.GetInstance(Type.Microchip, "Ruthenium", "EM")
                }),
                new Floor(4)
            };

            return Solve(inventoryFactory, floors);
        }

        private static string Solve(InventoryItem.Factory inventoryFactory, List<Floor> floors)
        {
//            IBuilding building = new Building(inventoryFactory, 0, floors);
            IBuilding building = new LightBuilding(inventoryFactory, 0, floors);

            var queue = new Queue<IBuilding>();
            var states = new Dictionary<IBuilding, int>();

            queue.Enqueue(building);
            states.Add(building, 0);

            var iterations = 0;

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var getCommandStopwatch = new Stopwatch();
            var executeCommandStopwatch = new Stopwatch();
            var lookupStopwatch = new Stopwatch();

            var depth = 0;
            while (queue.Any()) // new queue is empty, too
            {
                var newQueue = new Queue<IBuilding>();

                while (queue.Any())
                {
                    iterations++;
                    if (iterations % 50000 == 0 && ShowDebug)
                    {
                        Console.WriteLine($"Solving for day 11; {iterations} iterations done after {stopwatch.ElapsedMilliseconds}ms");
                        Console.WriteLine($"Iterations/s: {iterations/(stopwatch.ElapsedMilliseconds/1000f)}");
                        Console.WriteLine($"Depth: {depth}");
                        Console.WriteLine($"Queue size: {queue.Count}");
                        Console.WriteLine($"States size: {states.Count}");
                        Console.WriteLine($"Time spend getting commands: {getCommandStopwatch.ElapsedMilliseconds} ({100f * getCommandStopwatch.ElapsedMilliseconds / stopwatch.ElapsedMilliseconds}%)");
                        Console.WriteLine($"Time spend executing commands: {executeCommandStopwatch.ElapsedMilliseconds} ({100f * executeCommandStopwatch.ElapsedMilliseconds / stopwatch.ElapsedMilliseconds}%)");
                        Console.WriteLine($"Time spend executing lookups: {lookupStopwatch.ElapsedMilliseconds} ({100f * lookupStopwatch.ElapsedMilliseconds / stopwatch.ElapsedMilliseconds}%)");
                        Console.WriteLine();
                    }

                    building = queue.Dequeue();

                    getCommandStopwatch.Start();
                    var commands = building.CreatePossibleCommands();
                    getCommandStopwatch.Stop();

                    foreach (var command in commands)
                    {
                        executeCommandStopwatch.Start();
                        var newBuilding = building.ExecuteCommand(command);
                        executeCommandStopwatch.Stop();

                        lookupStopwatch.Start();
                        var contains = states.ContainsKey(newBuilding);
                        lookupStopwatch.Stop();

                        if (!contains)
                        {
                            states.Add(newBuilding, depth + 1);
                            newQueue.Enqueue(newBuilding);
                        }
                        if (newBuilding.IsFinished())
                        {
                            stopwatch.Stop();
                            return $"Finished at {depth + 1} after {iterations} iterations and it took {stopwatch.ElapsedMilliseconds}ms";
                        }
                    }
                }

                queue = newQueue;
                depth++;
            }

            return "no result";
        }

    }
}