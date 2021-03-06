﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AoC2016.Solutions.Day03
{
    public class NormalWall : BaseWall
    {
        public NormalWall(string inscriptions) : base(inscriptions)
        {
        }

        protected override IList<Triangle> Read(string inscriptions)
        {
            var triangles = new List<Triangle>();
            const string pattern = @"^\s*(\d+)\s+(\d+)\s+(\d+)$";
            foreach (Match match in Regex.Matches(inscriptions, pattern, RegexOptions.Multiline))
            {
                if (match.Groups.Count != 4) throw new InvalidDataException("Each line must contain 3 sides");
                try
                {
                    triangles.Add(new Triangle(
                        int.Parse(match.Groups[1].Value),
                        int.Parse(match.Groups[2].Value),
                        int.Parse(match.Groups[3].Value)
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