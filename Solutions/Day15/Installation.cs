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

        public int Solve()
        {
            var stepSize = 1;
            var t = 0;
            foreach (var disc in Discs.OrderBy(_ => _.Number))
            {
                while (!disc.IsOpenAt(t))
                {
                    t += stepSize;
                }
                stepSize *= disc.Size;
            }
            return t;
        }
    }
}