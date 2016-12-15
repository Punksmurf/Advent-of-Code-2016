using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AoC2016.Solutions.Day15
{
    public class Installation
    {
        public Collection<Disc> Discs { get; } = new Collection<Disc>();

        public void AddDisc(Disc disc)
        {
            Discs.Add(disc);
        }

        public bool IsOpenAt(int t)
        {
            return Discs.All(d => d.IsOpenAt(t));
        }
    }
}