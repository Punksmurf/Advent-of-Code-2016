using System.Collections.Generic;
using System.Linq;

namespace AoC2016.Solutions.Day10
{
    public class Output : IValueReceiver
    {
        public int LastValue => Values.Count == 0 ? 0 : Values.Last();
        public List<int> Values { get; } = new List<int>();


        public bool CanAddValue() => true;

        public void AddValue(int value) => Values.Add(value);
    }
}