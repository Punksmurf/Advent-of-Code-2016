using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AoC2016.Solutions.Day4
{
    public class Room
    {

        public static class Factory
        {
            private const string Pattern = @"([a-z-]*)-(\d*)\[([a-z]*)\]";

            public static Room Create(string description)
            {
                var match = Regex.Match(description, Pattern);
                if (!match.Success) throw new ArgumentException(nameof(description));
                return new Room(match.Groups[1].Value, int.Parse(match.Groups[2].Value), match.Groups[3].Value);
            }

            public static IEnumerable<Room> CreateIEnumerable(string list)
            {
                return list
                    .Split('\n')
                    .Where(_ => Regex.IsMatch(_, Pattern))
                    .Select(Create);
            }
        }

        public readonly string Name;
        public readonly int Sector;
        public readonly string Checksum;

        public Room(string name, int sector, string checksum)
        {
            Name = name;
            Sector = sector;
            Checksum = checksum;
        }

        public string CalculateChecksum()
        {
            return string.Concat(
                Name.Where(_ => _ != '-')
                    .GroupBy(_ => _)
                    .OrderByDescending(group => group.Count())
                    .ThenBy(group => group.Key)
                    .Take(5)
                    .Select(group => group.Key)
            );
        }

        public bool IsValid()
        {
            return CalculateChecksum().Equals(Checksum);
        }

        public string GetDecryptedName()
        {
            return string.Concat(
                Name.Select(_ => Rotate(_, Sector))
            );
        }

        private static char Rotate(char chr, int amount)
        {
            if (chr == ' ' || chr == '-') return ' ';
            return (char) ((chr - 97 + amount) % 26 + 97);
        }

        public override string ToString()
        {
            const int nameColumnWidth = 55;

            var sb = new StringBuilder()
                .Append(Name)
                .Append(' ', nameColumnWidth - Name.Length)
                .Append(GetDecryptedName())
                .Append(' ', nameColumnWidth - Name.Length)
                .Append(Sector)
                .Append(' ');

            if (IsValid())
            {
                sb.Append("Valid:   ").Append(Checksum);
            }
            else
            {
                sb.Append("Invalid: ").Append(CalculateChecksum()).Append(" =/= ").Append(Checksum);
            }

            return sb.ToString();
        }
    }
}