﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC2016.Solutions.Day1
{
    public class CityWalker
    {
        public enum Instruction
        {
            Left, Right, Forward
        }

        private enum Direction
        {
            North, East, South, West
        }

        private readonly IList<Instruction> _instructions;

        private Location _location;
        private Direction _direction;
        private int _instructionIndex;

        public CityWalker(string document)
        {
            _instructions = ParseDocument(document);
            Reset();
        }

        public void Reset()
        {
            _location = new Location(0, 0);
            _direction = Direction.North;
            _instructionIndex = -1;
        }

        public bool HasNext()
        {
            return _instructionIndex + 1 < _instructions.Count;
        }

        public Location Next()
        {
            if (!HasNext()) throw new IndexOutOfRangeException();
            _instructionIndex++;
            ExecuteInstruction(_instructions[_instructionIndex]);
            return _location.Clone();
        }

        public Location GetLocation()
        {
            return _location.Clone();
        }

        private static IList<Instruction> ParseDocument(string document)
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
            }).ToList();
        }

        private void ExecuteInstruction(Instruction instruction)
        {
            switch (instruction)
            {
                case Instruction.Left:
                case Instruction.Right:
                    _direction = Turn(_direction, instruction);
                    break;
                case Instruction.Forward:
                    switch (_direction)
                    {
                        case Direction.North: _location.Y++; break;
                        case Direction.East: _location.X++; break;
                        case Direction.South: _location.Y--; break;
                        case Direction.West: _location.X--; break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static Direction Turn(Direction currentDirection, Instruction instruction)
        {
            switch (instruction)
            {
                case Instruction.Left:
                    switch (currentDirection)
                    {
                        case Direction.North: return Direction.East;
                        case Direction.East: return Direction.South;
                        case Direction.South: return Direction.West;
                        case Direction.West: return Direction.North;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(currentDirection), currentDirection, null);
                    }
                case Instruction.Right:

                    switch (currentDirection)
                    {
                        case Direction.North: return Direction.West;
                        case Direction.East: return Direction.North;
                        case Direction.South: return Direction.East;
                        case Direction.West: return Direction.South;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(currentDirection), currentDirection, null);
                    }
                case Instruction.Forward:
                    return currentDirection;
                default:
                    throw new ArgumentOutOfRangeException(nameof(instruction), instruction, null);
            }
        }
    }
}