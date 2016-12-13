using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace AoC2016.Solutions.Day11.State
{
    public class Building : IBuilding, IEquatable<Building>
    {
        public int CurrentFloor { get; }
        public ImmutableList<Floor> Floors { get; }
        public string Guid { get; } = System.Guid.NewGuid().ToString();
        private readonly InventoryItem.Factory _inventoryFactory;

        public Building(InventoryItem.Factory inventoryFactory)
        {
            _inventoryFactory = inventoryFactory;
            CurrentFloor = 0;
            Floors = ImmutableList.CreateRange(new List<Floor>(4)
            {
                new Floor(1),
                new Floor(2),
                new Floor(3),
                new Floor(4)
            });
        }

        public Building(InventoryItem.Factory inventoryFactory, int currentFloor, List<Floor> floors)
        {
            _inventoryFactory = inventoryFactory;
            CurrentFloor = currentFloor;
            Floors = ImmutableList.CreateRange(floors);
        }

        public bool IsFinished()
        {
            for (var i = 0; i < Floors.Count - 1; i++)
            {
                if (Floors[i].Inventory.Count > 0) return false;
            }
            return true;
        }

        public void Print()
        {
            Print(0);
        }

        public void Print(int level)
        {
            Console.WriteLine($"Building {Guid}");
            for (var i = Floors.Count - 1; i >= 0; i--)
            {
                for (var j = 0; j < level; j++)
                {
                    Console.Write("  ");
                }
                Console.Write($"{Floors[i].Number} ");
                Console.Write(i == CurrentFloor ? "E " : ". ");
                Console.WriteLine(string.Concat(Floors[i].Inventory.Select(_ => _.Representation + " ")));
            }
        }

        public IEnumerable<ICommand> CreatePossibleCommands()
        {
            var combinations = Floors[CurrentFloor].GetValidRemovableCombinations().ToList();
            var commands = new List<Command>();
            
            // We can go down
            if (CurrentFloor -1 >= 0)
            {
                var floor = Floors[CurrentFloor - 1];
                commands.AddRange(combinations.Where(_ => floor.CanAdd(_)).Select(_ => new Command(_inventoryFactory, Direction.Down,  _)));
            }

            // We can go up
            if (CurrentFloor + 1 < Floors.Count)
            {
                var floor = Floors[CurrentFloor + 1];
                commands.AddRange(combinations.Where(_ => floor.CanAdd(_)).Select(_ => new Command(_inventoryFactory, Direction.Up,  _)));
            }

            return commands;
        }

        public IBuilding ExecuteCommand(ICommand command)
        {
            var newFloors = new List<Floor>();

            var nextFloor = CurrentFloor;
            switch (command.Direction)
            {
                case Direction.Up:
                    nextFloor++;
                    break;
                case Direction.Down:
                    nextFloor--;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            for (var i = 0; i < Floors.Count; i++)
            {
                if (i == CurrentFloor)
                {
                    newFloors.Add(Floors[i].Remove(command.Inventory as ICollection<InventoryItem>));
                }
                else if (i == nextFloor)
                {
                    newFloors.Add(Floors[i].Add(command.Inventory as ICollection<InventoryItem>));
                }
                else
                {
                    newFloors.Add(Floors[i]);
                }
            }

            return new Building(_inventoryFactory, nextFloor, newFloors);
        }

        public bool Equals(Building other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            // Use SequenceEqual instead of Equals on Floors
            return CurrentFloor == other.CurrentFloor && Floors.SequenceEqual(other.Floors);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Building) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (CurrentFloor * 397) ^ GetFloorsHashCode();
            }
        }

        private int GetFloorsHashCode()
        {
            var result = 1;

            for (var i = 0; i < Floors.Count; i++)
            {
                result ^= 31 * Floors[i].GetHashCode();
            }

            return result;
        }

    }
}