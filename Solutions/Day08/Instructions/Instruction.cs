using System;
using System.Text.RegularExpressions;

namespace AoC2016.Solutions.Day08.Instructions
{
    public abstract class Instruction
    {
        public static class Factory
        {
            public static Instruction Create(string instruction)
            {
                const string pattern = @"(?<command>rect|rotate (?<rotatetype>\w+)) (?<arguments>.*)";
                var match = Regex.Match(instruction, pattern);
                if (!match.Success) throw new Exception($"Invalid instruction: {instruction}");

                switch (match.Groups["command"].Value.ToLower())
                {
                    case "rect":
                        return CreatRect(match.Groups["arguments"].Value);
                    case "rotate row":
                    case "rotate column":
                        return CreateRotate(match.Groups["rotatetype"].Value, match.Groups["arguments"].Value);
                    default:
                        throw new Exception($"Invalid instruction: {instruction}");
                }
            }

            private static Rect CreatRect(string arguments)
            {
                const string pattern = @"(?<width>[-\d]+)x(?<height>[-\d]+)";
                var match = Regex.Match(arguments, pattern);
                if (!match.Success) throw new Exception($"Rect does not understand these arguments: {arguments}");

                var width = int.Parse(match.Groups["width"].Value);
                var height = int.Parse(match.Groups["height"].Value);

                return new Rect(width, height);

            }

            private static Rotate CreateRotate(string type, string arguments)
            {
                const string pattern = @"(?:x|y)=(?<row_or_column>[-\d]+) by (?<amount>[-\d]+)";
                var match = Regex.Match(arguments, pattern);
                if (!match.Success) throw new Exception($"Rotate does not understand these arguments: {arguments}");

                var rowOrColumn = int.Parse(match.Groups["row_or_column"].Value);
                var amount = int.Parse(match.Groups["amount"].Value);


                switch (type)
                {
                    case "row":
                        return new RotateRow(rowOrColumn, amount);
                    case "column":
                        return new RotateColumn(rowOrColumn, amount);
                    default:
                        throw new Exception($"Rotate does not understand this type: {type}");

                }
            }


        }

        public abstract void Visit(Display display);
    }
}