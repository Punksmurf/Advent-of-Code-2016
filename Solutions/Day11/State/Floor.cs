using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace AoC2016.Solutions.Day11.State
{

    public class Floor : IEquatable<Floor>
    {
        public int Number { get; }
        public ImmutableHashSet<InventoryItem> Inventory { get; }

        public Floor(int number)
        {
            Number = number;
            Inventory = ImmutableHashSet<InventoryItem>.Empty;
        }

        public Floor(int number, IEnumerable<InventoryItem> inventory)
        {
            Number = number;
            Inventory = ImmutableHashSet.CreateRange(inventory);
        }

        public IEnumerable<InventoryItem[]> GetValidRemovableCombinations()
        {
            var combinations = new List<InventoryItem[]>();
            var inventoryArray = Inventory.ToImmutableArray();

            for (var i = 0; i < inventoryArray.Length; i++)
            {
                combinations.Add(new [] { inventoryArray[i] });

                for (var j = i + 1; j < inventoryArray.Length; j++)
                {
                    combinations.Add(new [] { inventoryArray[i], inventoryArray[j] });
                }
            }

            return combinations
                .Where(_ => _.IsValidInventory())
                .Where(CanRemove);
        }

        public bool CanAdd(IEnumerable<InventoryItem> newItems)
        {

            return Inventory.Union(newItems).IsValidInventory();
        }

        public bool CanRemove(IEnumerable<InventoryItem> oldItems)
        {
            return Inventory.Except(oldItems).IsValidInventory();
        }

        public Floor Add(ICollection<InventoryItem> newItems)
        {
            if (CanAdd(newItems)) return new Floor(Number, Inventory.Union(newItems));

            var sb = new StringBuilder();
            sb
                .Append("Not allowed to keep new items on floor ").Append(Number).AppendLine()

                .Append("Current items: ")
                .AppendLine(string.Concat(Inventory.Select(_ => _.Representation + " ")))
                .AppendLine()

                .Append("New items: ")
                .Append(string.Concat(newItems.Select(_ => _.Representation + " ")));

            throw new Exception(sb.ToString());
        }

        public Floor Remove(ICollection<InventoryItem> oldItems)
        {
            if (CanAdd(oldItems)) return new Floor(Number, Inventory.Except(oldItems));

            var sb = new StringBuilder();
            sb
                .Append("Not allowed to remove items from floor ").Append(Number).AppendLine()

                .Append("Current items: ")
                .AppendLine(string.Concat(Inventory.Select(_ => _.Representation + " ")))
                .AppendLine()

                .Append("Items to remove: ")
                .Append(string.Concat(oldItems.Select(_ => _.Representation + " ")));

            throw new Exception(sb.ToString());
        }

        public bool Equals(Floor other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            // Use SetEquals instead of Equals on Inventory
            return Number == other.Number && Inventory.SetEquals(other.Inventory);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Floor) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Number * 397) ^ GetInventoryHashCode();
            }
        }

        private int GetInventoryHashCode()
        {
            var result = 1;

            var inv = Inventory.OrderBy(_ => _.Representation).ToArray();

            for (var i = 0; i < inv.Length; i++)
            {
                result ^= 31 * inv[i].GetHashCode();
            }

            return result;
        }
    }
}