using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2016.Solutions.Day10
{
    public class Bot : IValueReceiver
    {
        public int Id { get; }
        private int[] _values = new int[0];

        public bool CanTakeValues() => _values.Length == 2;

        private readonly List<int[]> _hasCompared = new List<int[]>();

        public Bot(int id)
        {
            Id = id;
        }

        public int[] TakeValues()
        {
            if (!CanTakeValues()) throw new IndexOutOfRangeException();
            var result = _values;
            _hasCompared.Add(result);
            _values = new int[0];
            return result;
        }

        public bool CanAddValue() => _values.Length < 2;

        public void AddValue(int value)
        {
            if (!CanAddValue()) throw new IndexOutOfRangeException();
            if (_values.Length == 1)
            {
                _values = new[] {_values[0], value};
                Array.Sort(_values);
            }
            else
            {
                _values = new[] {value};
            }
        }

        public bool DidCompare(int low, int high)
        {
            return _hasCompared.Any(a => a[0] == low && a[1] == high);
        }
    }
}