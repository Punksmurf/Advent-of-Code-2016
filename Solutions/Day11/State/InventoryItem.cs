using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AoC2016.Solutions.Day11.State
{
    public static class InventoryItemExtensions
    {
        public static IEnumerable<InventoryItem> MatchingInventoryItems(this IEnumerable<InventoryItem> items, InventoryItem item)
        {
            return items.Where(_ => _.IsMatch(item));
        }

        public static IEnumerable<InventoryItem> OpposingInventoryItems(this IEnumerable<InventoryItem> items, InventoryItem item)
        {
            return items.Where(_ => _.Type != item.Type);
        }

        public static IEnumerable<InventoryItem> DamagingIntenvoryItems(this IEnumerable<InventoryItem> items, InventoryItem item)
        {
            return item.Type == Type.Generator
                ? Enumerable.Empty<InventoryItem>()
                : items.Where(_ => _.Type == Type.Generator && !_.IsMatch(item));
        }

        public static bool IsValidInventory(this IEnumerable<InventoryItem> items)
        {
            var itemsArray = items.ToArray();
            return itemsArray.All(item => itemsArray.MatchingInventoryItems(item).Any() || !itemsArray.DamagingIntenvoryItems(item).Any());
        }
    }

    public class InventoryItem : IEquatable<InventoryItem>
    {

        public Type Type { get; }
        public string Name { get; }
        public string Representation { get; }


        public class Factory
        {
            private readonly ConcurrentDictionary<string, InventoryItem> _instances = new ConcurrentDictionary<string, InventoryItem>();
            public int[] GeneratorPositions { get; private set; }
            public int[] MicrochipPositions { get; private set; }
            public int[][] MicrochipToGeneratorMatch { get; private set; }

            public InventoryItem GetInstance(Type type, string name, string representation)
            {
                var key = GetKey(type, name, representation);
                if (_instances.ContainsKey(key)) return _instances[key];

                _instances[key] = new InventoryItem(type, name, representation);

                // Okay this is probably the most inefficient way of going about this, but it's not like generating
                // these lookups is where we can gain performance.
                GeneratorPositions = _instances.Values
                    .Where(_ => _.Type == Type.Generator)
                    .Select(PositionOf)
                    .ToArray();

                MicrochipPositions = _instances.Values
                    .Where(_ => _.Type == Type.Microchip)
                    .Select(PositionOf)
                    .ToArray();

                MicrochipToGeneratorMatch = MicrochipPositions
                    .Select(GetInstance)
                    .Select(chip => new[] {PositionOf(chip), PositionOf(Type.Generator, chip.Name)})
                    .ToArray();

                return _instances[key];
            }

            public InventoryItem GetInstance(int position)
            {
                var key = _instances.Keys.OrderBy(_ => _).ToList()[position];
                return _instances[key];
            }

            public int Count()
            {
                return _instances.Count;
            }

            public int PositionOf(Type type, string name)
            {
                try
                {
                    var item = _instances.Values.First(_ => _.Type == type && _.Name.Equals(name));
                    return PositionOf(item);
                }
                catch (Exception)
                {
                    return -1; //probably just hasn't been created yet
                }
            }

            public int PositionOf(InventoryItem item)
            {
                var key = GetKey(item);
                return _instances.Keys.OrderBy(_ => _).ToList().IndexOf(key);
            }

            private static string GetKey(InventoryItem item)
            {
                return GetKey(item.Type, item.Name, item.Representation);
            }

            private static string GetKey(Type type, string name, string representation)
            {
                return new StringBuilder().Append(name).Append(type).Append(representation).ToString();
            }

        }



        private InventoryItem(Type type, string name, string representation)
        {
            Type = type;
            Name = name;
            Representation = representation;
        }

        public bool IsMatch(InventoryItem other)
        {
            return Type != other.Type && Name == other.Name;
        }

        public bool Equals(InventoryItem other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Type == other.Type && string.Equals(Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((InventoryItem) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int) Type * 397) ^ Name.GetHashCode();
            }
        }
    }
}