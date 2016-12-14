using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AoC2016.Utils;

namespace AoC2016.Solutions.Day01
{
    public class CityWalker
    {
        public enum Instruction
        {
            Left, Right, Forward
        }

        private enum Direction
        {
            North = 0, East = 1, South = 2, West = 3
        }

        private readonly IEnumerable<CityWalker.Instruction> _instructions;

        public CityWalker(string document)
        {
            _instructions = ParseDocument(document);
        }

        private static IEnumerable<Instruction> ParseDocument(string document)
        {
            return Regex.Split(document, @",\s*").SelectMany(inst =>
            {
                var forwardCount = int.Parse(inst.Substring(1));
                var instructions = new Instruction[forwardCount + 1];
                instructions[0] = inst[0] == 'L' ? Instruction.Left : Instruction.Right;
                for (var i = 1; i <= forwardCount; i++)
                {
                    instructions[i] = Instruction.Forward;
                }
                return instructions;
            });
        }

        public IEnumerable<Coordinate> GetLocations()
        {
            var direction = Direction.North;
            var x = 0;
            var y = 0;

            foreach (var instruction in _instructions)
            {

                direction = Turn(direction, instruction);
                switch (instruction)
                {
                    case Instruction.Left:
                    case Instruction.Right:
                        continue;

                    case Instruction.Forward:
                        switch (direction)
                        {
                            case Direction.North:
                                y--;
                                break;
                            case Direction.East:
                                x++;
                                break;
                            case Direction.South:
                                y++;
                                break;
                            case Direction.West:
                                x--;
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                yield return new Coordinate(x, y);
            }
        }

        private static Direction Turn(Direction currentDirection, Instruction instruction)
        {
            var ord = (int) currentDirection;
            switch (instruction)
            {
                case Instruction.Left:
                    ord--;
                    break;
                case Instruction.Right:
                    ord++;
                    break;
                case Instruction.Forward:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(instruction), instruction, null);
            }

            var numDirections = Enum.GetValues(typeof(Direction)).Length;
            if (ord < 0) ord += numDirections;
            ord %= numDirections;
            return (Direction) ord;
        }
    }
}