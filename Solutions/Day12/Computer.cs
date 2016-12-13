using System;
using System.Collections.Generic;

namespace AoC2016.Solutions.Day12
{
    public class Computer
    {
        public int A { get; private set; }
        public int B { get; private set; }
        public int C { get; private set; }
        public int D { get; private set; }

        public int Ip { get; private set; }

        private readonly List<Instruction> _program;

        public Computer(List<Instruction> program)
        {
            _program = program;
            Reset();
        }

        public void Reset()
        {
            Ip = 0;
            A = 0;
            B = 0;
            C = 0;
            D = 0;
        }

        public void Run()
        {
            while (Ip < _program.Count)
            {
                Accept(_program[Ip]);
            }
        }

        private void Accept(Instruction instruction)
        {
            instruction.Visit(this);
        }

        public int GetRegisterValue(char registerName)
        {
            switch (registerName)
            {
                case 'A':
                case 'a':
                    return A;
                case 'B':
                case 'b':
                    return B;
                case 'C':
                case 'c':
                    return C;
                case 'D':
                case 'd':
                    return D;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void SetRegisterValue(char registerName, int value)
        {
            switch (registerName)
            {
                case 'A':
                case 'a':
                    A = value;
                    break;
                case 'B':
                case 'b':
                    B = value;
                    break;
                case 'C':
                case 'c':
                    C = value;
                    break;
                case 'D':
                case 'd':
                    D = value;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void IncreaseRegisterValue(char registerName, int value)
        {
            switch (registerName)
            {
                case 'A':
                case 'a':
                    A += value;
                    break;
                case 'B':
                case 'b':
                    B += value;
                    break;
                case 'C':
                case 'c':
                    C += value;
                    break;
                case 'D':
                case 'd':
                    D += value;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void IncreaseIp() => IncreaseIp(1);

        public void IncreaseIp(int value)
        {
            Ip += value;
        }
    }
}