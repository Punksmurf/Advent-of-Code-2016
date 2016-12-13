using System.Collections.Generic;

namespace AoC2016.Solutions.Day11.State
{
    public interface ICommand
    {
        Direction Direction { get; }
        IEnumerable<InventoryItem> Inventory { get; }
        int[] IndexedInventory { get; }
    }
}