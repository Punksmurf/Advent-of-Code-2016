using System.Collections.Generic;
using System.Linq;

namespace AoC2016.Solutions.Day11.State
{
    public class Command : ICommand
    {
        public Direction Direction { get; }
        public IEnumerable<InventoryItem> Inventory { get; }

        public int[] IndexedInventory
        {
            get { return Inventory.Select(_ => _inventoryFactory.PositionOf(_)).ToArray(); }
        }

        private readonly InventoryItem.Factory _inventoryFactory;

        public Command(InventoryItem.Factory inventoryFactory, Direction direction, IEnumerable<InventoryItem> inventory)
        {
            _inventoryFactory = inventoryFactory;
            Direction = direction;
            Inventory = inventory;
        }

        public override string ToString()
        {
            return $"Direction: {Direction}, Inventory: " + string.Concat(Inventory.Select(_ => _.Representation + " "));
        }
    }
}