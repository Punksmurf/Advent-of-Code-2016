using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2016.Solutions.Day2
{
    public class Finger // aka the KeyPresser
    {
        private enum Instruction
        {
            MoveUp, MoveDown, MoveLeft, MoveRight,
            Press
        }

        private readonly KeyPad _keyPad;
        private readonly IEnumerable<Instruction> _instructions;

        public Finger(KeyPad keyPad, string procedure)
        {
            _keyPad = keyPad;
            _instructions = Memorize(procedure);
        }

        public IEnumerable<char> GetKeyPresses(char startKey)
        {
            var startingPosition = _keyPad.GetPosition(startKey);
            var x = startingPosition.Item1;
            var y = startingPosition.Item2;

            foreach (var instruction in _instructions)
            {
                var oldX = x;
                var oldY = y;

                switch (instruction)
                {
                    case Instruction.MoveUp: y--; break;
                    case Instruction.MoveDown: y++; break;
                    case Instruction.MoveLeft: x--; break;
                    case Instruction.MoveRight: x++; break;
                    case Instruction.Press:
                        yield return _keyPad.GetKey(x, y);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                if (_keyPad.IsValid(x, y)) continue;
                x = oldX;
                y = oldY;
            }
        }

        private static IEnumerable<Instruction> Memorize(string procedure)
        {
            return procedure.Split('\n').SelectMany(line =>
            {
                var instructions = new Instruction[line.Length + 1];
                for (var i = 0; i < line.Length; i++)
                {
                    switch (line[i])
                    {
                        case 'U':
                            instructions[i] = Instruction.MoveUp;
                            break;
                        case 'D':
                            instructions[i] = Instruction.MoveDown;
                            break;
                        case 'L':
                            instructions[i] = Instruction.MoveLeft;
                            break;
                        case 'R':
                            instructions[i] = Instruction.MoveRight;
                            break;
                    }
                }
                instructions[instructions.Length - 1] = Instruction.Press;
                return instructions;
            }).ToList();
        }
    }
}