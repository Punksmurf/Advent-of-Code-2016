using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AoC2016.Solutions.Day11.State
{
    public class LightBuilding : IBuilding, IEquatable<LightBuilding>
    {
        private const int NumFloors = 4;

        private readonly int[] _state;
        private readonly InventoryItem.Factory _inventoryFactory;

        public LightBuilding(InventoryItem.Factory inventoryFactory, int currentFloor, List<Floor> floors)
        {
            _inventoryFactory = inventoryFactory;
            _state = new int[inventoryFactory.Count() + 1];
            SetFloor(currentFloor);

            foreach (var floor in floors)
            {
                foreach (var item in floor.Inventory)
                {
                    _state[inventoryFactory.PositionOf(item)] = floor.Number - 1;
                }
            }
        }

        public LightBuilding(InventoryItem.Factory inventoryFactory, int[] state)
        {
            _inventoryFactory = inventoryFactory;
            _state = state;
        }

        private int GetFloor()
        {
            return _state[_state.Length - 1];
        }

        private void SetFloor(int floor)
        {
            _state[_state.Length - 1] = floor;
        }

        public bool IsFinished()
        {
            for (var i = 0; i < _state.Length - 1; i++)
            {
                if (_state[i] != NumFloors - 1) return false;
            }
            return true;
        }

        public void Print()
        {
            Console.WriteLine($"Building {GetHashCode()}");
            for (var i = NumFloors - 1; i >= 0; i--)
            {
                PrintFloor(i);
            }
        }

        private void PrintFloor(int floor)
        {
            var sb = new StringBuilder();
            sb.Append(floor).Append(' ');
            sb.Append(floor == GetFloor() ? "E  " : ".  ");
            for (var i = 0; i < _state.Length - 1; i++)
            {
                sb.Append(_state[i] == floor ? _inventoryFactory.GetInstance(i).Representation + " " : ".  ");
            }
            Console.WriteLine(sb.ToString());
        }

        public IEnumerable<ICommand> CreatePossibleCommands()
        {
            var combinations = GetValidRemovableCombinationsFromFloor(GetFloor());
            var commands = new List<ICommand>();

            // We can go down
            if (GetFloor() -1 >= 0)
            {
                var floor = GetFloor() - 1;
                commands.AddRange(combinations.Where(_ => CanAddToFloor(floor, _)).Select(_ => new LightCommand(_inventoryFactory, Direction.Down,  _)));
            }

            // We can go up
            if (GetFloor() + 1 < NumFloors)
            {
                var floor = GetFloor() + 1;
                commands.AddRange(combinations.Where(_ => CanAddToFloor(floor, _)).Select(_ => new LightCommand(_inventoryFactory, Direction.Up,  _)));
            }

            return commands;
        }

        public IBuilding ExecuteCommand(ICommand command)
        {
            var newState = new int[_state.Length];
            _state.CopyTo(newState, 0);

            var d = 0;
            switch (command.Direction)
            {
                case Direction.Up:
                    d = 1;
                    break;
                case Direction.Down:
                    d = -1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            // Set Floor
            newState[newState.Length - 1] += d;

            foreach (var itemId in command.IndexedInventory)
            {
                newState[itemId] += d;
            }

            return new LightBuilding(_inventoryFactory, newState);
        }

        private int[] GetElementOnFloor(int floor)
        {
            return Enumerable.Range(0, _state.Length - 1).Where(i => _state[i] == floor).ToArray();
        }

        private int[][] GetValidRemovableCombinationsFromFloor(int floor)
        {
            var combinations = new List<int[]>();
            var inventory = GetElementOnFloor(floor);

            for (var i = 0; i < inventory.Length; i++)
            {
                combinations.Add(new [] { inventory[i] });

                for (var j = i + 1; j < inventory.Length; j++)
                {
                    combinations.Add(new [] { inventory[i], inventory[j] });
                }
            }

            return combinations.Where(_ => CanRemoveFromFloor(floor, _)).ToArray();
        }

        private bool CanAddToFloor(int floor, int[] items)
        {
            return IsValidInventory(GetElementOnFloor(floor).Concat(items).ToArray());
        }

        private bool CanRemoveFromFloor(int floor, int[] items)
        {
            return IsValidInventory(GetElementOnFloor(floor).Where(_ => !items.Contains(_)).ToArray());
        }

        private bool IsValidInventory(int[] inventory)
        {
            //if all microchips have a generator or no generators

            // no generators
            if (!inventory.Any(_ => _inventoryFactory.GeneratorPositions.Contains(_))) return true;

            // there are generators, so let's hope every chip has their own
            return inventory
                .Where(_ => _inventoryFactory.MicrochipPositions.Contains(_))
                .All(chipId =>
                {
                    var match = _inventoryFactory.MicrochipToGeneratorMatch.First(m => m[0] == chipId);
                    return inventory.Contains(match[1]);
                });
        }

        public bool Equals(LightBuilding other)
        {
            if (_state.Length != other._state.Length) return false;
            return !_state.Where((t, i) => t != other._state[i]).Any();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((LightBuilding) obj);
        }

        public override int GetHashCode()
        {
            var result = 1;
            for (var i = 0; i < _state.Length; i++)
            {
                result += (int) Math.Pow(NumFloors, i) * _state[i];
            }
            return result;
        }
    }
}