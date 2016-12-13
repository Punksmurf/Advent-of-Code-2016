using System.Collections.Generic;
using System.Linq;

namespace AoC2016.Solutions.Day11.State
{
    public class LightCommand : ICommand
    {
        public Direction Direction { get; }

        public IEnumerable<InventoryItem> Inventory
        {
            get { return IndexedInventory.Select(_ => _inventoryFactory.GetInstance(_)); }
        }

        public int[] IndexedInventory { get; }

        private readonly InventoryItem.Factory _inventoryFactory;

        public LightCommand(InventoryItem.Factory inventoryFactory, Direction direction, int[] indexedInventory)
        {
            _inventoryFactory = inventoryFactory;
            Direction = direction;
            IndexedInventory = indexedInventory;
        }
    }
}