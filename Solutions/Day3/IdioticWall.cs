using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AoC2016.Solutions.Day3
{
    public class IdioticWall : BaseWall
    {
        public IdioticWall(string inscriptions) : base(inscriptions)
        {
        }

        protected override IList<Triangle> Read(string inscriptions)
        {
            var triangles = new List<Triangle>();
            const string pattern = @"^\s*(\d+)\s+(\d+)\s+(\d+)\n\s*(\d+)\s+(\d+)\s+(\d+)\n\s*(\d+)\s+(\d+)\s+(\d+)$";
            foreach (Match match in Regex.Matches(inscriptions, pattern, RegexOptions.Multiline))
            {
                if (match.Groups.Count != 10) throw new InvalidDataException("Each block must contain 9 sides");
                try
                {
                    triangles.Add(new Triangle(
                        int.Parse(match.Groups[1].Value),
                        int.Parse(match.Groups[4].Value),
                        int.Parse(match.Groups[7].Value)
                    ));
                    triangles.Add(new Triangle(
                        int.Parse(match.Groups[2].Value),
                        int.Parse(match.Groups[5].Value),
                        int.Parse(match.Groups[8].Value)
                    ));
                    triangles.Add(new Triangle(
                        int.Parse(match.Groups[3].Value),
                        int.Parse(match.Groups[6].Value),
                        int.Parse(match.Groups[9].Value)
                    ));
                }
                catch (Exception)
                {
                    Console.WriteLine($"Invalid match: {match.Groups[0]}");
                }
            }
            return triangles;
        }
    }
}