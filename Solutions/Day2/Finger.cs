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
        private readonly IList<Instruction> _instructions;
        private int _currentInstruction;
        private char _currentKey;

        public Finger(KeyPad keyPad, string procedure, char startKey)
        {
            _keyPad = keyPad;
            _instructions = Memorize(procedure);
            _currentInstruction = -1;
            _currentKey = startKey;
        }

        public char PressNextKey()
        {
            var startingPosition = _keyPad.GetPosition(_currentKey);
            var x = startingPosition.Item1;
            var y = startingPosition.Item2;

            while (HasNext())
            {
                var instruction = Next();

                var oldX = x;
                var oldY = y;

                switch (instruction)
                {
                    case Instruction.MoveUp: y--; break;
                    case Instruction.MoveDown: y++; break;
                    case Instruction.MoveLeft: x--; break;
                    case Instruction.MoveRight: x++; break;
                    case Instruction.Press:
                        _currentKey = _keyPad.GetKey(x, y);
                        return _currentKey;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                if (!_keyPad.IsValid(x, y))
                {
                    x = oldX;
                    y = oldY;
                }
            }

            throw new Exception("No Press instruction found at the end of the instructions");
        }

        public bool Finished()
        {
            return _currentInstruction == _instructions.Count - 1;
        }

        private bool HasNext()
        {
            return _currentInstruction + 1 < _instructions.Count;
        }

        private Instruction Next()
        {
            if (!HasNext())
            {
                throw new IndexOutOfRangeException();
            }
            _currentInstruction++;
            return _instructions[_currentInstruction];
        }

        private static IList<Instruction> Memorize(string procedure)
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