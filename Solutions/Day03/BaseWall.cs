using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC2016.Solutions.Day03
{
    public abstract class BaseWall
    {
        private readonly IList<Triangle> _triangles;

        protected BaseWall(string inscriptions)
        {
            _triangles = Read(inscriptions);
        }

        public int CountValidTriangles()
        {
            return _triangles.AsParallel().Count(triangle => triangle.OrIsIt());
        }

        protected abstract IList<Triangle> Read(string inscriptions);
    }
}