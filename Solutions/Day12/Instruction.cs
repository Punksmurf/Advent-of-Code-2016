using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC2016.Solutions.Day12
{
    public class Instruction
    {
        public InstructionName InstructionName { get; }

        public string ArgumentA { get; }
        public string ArgumentB { get; }

        public static List<Instruction> ParseProgram(string program)
        {
            return program.Split('\n').Select(Parse).ToList();
        }

        public static Instruction Parse(string instruction)
        {
            const string pattern = @"(?<name>[a-z]{3})\s(?<a>[a-z]|-?\d+)\s?(?<b>[a-z]|-?\d+)?";
            var match = Regex.Match(instruction, pattern);
            if (!match.Success) throw new Exception($"Unable to parse instruction {instruction}");

            var argA = match.Groups["a"].Success ? match.Groups["a"].Value : null;
            var argB = match.Groups["b"].Success ? match.Groups["b"].Value : null;

            switch (match.Groups["name"].Value.ToLower())
            {
                case "cpy":
                    if (argA == null || argB == null) throw new Exception($"Unable to parse instruction {instruction} (missing arguments)");
                    return new Instruction(InstructionName.Copy, argA, argB);
                case "inc":
                    if (argA == null) throw new Exception($"Unable to parse instruction {instruction} (missing arguments)");
                    return new Instruction(InstructionName.Increment, argA, argB);
                case "dec":
                    if (argA == null) throw new Exception($"Unable to parse instruction {instruction} (missing arguments)");
                    return new Instruction(InstructionName.Decrement, argA, argB);
                case "jnz":
                    if (argA == null || argB == null) throw new Exception($"Unable to parse instruction {instruction} (missing arguments)");
                    return new Instruction(InstructionName.JumpNonZero, argA, argB);
                default:
                    throw new Exception($"Unable to parse instruction {instruction} (invalid name)");
            }
        }

        private Instruction(InstructionName instructionName, string argumentA, string argumentB)
        {
            InstructionName = instructionName;
            ArgumentA = argumentA;
            ArgumentB = argumentB;
        }

        public void Visit(Computer computer)
        {
            switch (InstructionName)
            {
                case InstructionName.Copy:
                    computer.SetRegisterValue(ArgumentB[0], GetValueA(computer));
                    computer.IncreaseIp();
                    break;
                case InstructionName.Increment:
                    computer.IncreaseRegisterValue(ArgumentA[0], 1);
                    computer.IncreaseIp();
                    break;
                case InstructionName.Decrement:
                    computer.IncreaseRegisterValue(ArgumentA[0], -1);
                    computer.IncreaseIp();
                    break;
                case InstructionName.JumpNonZero:
                    if (GetValueA(computer) != 0)
                    {
                        computer.IncreaseIp(GetValueB(computer));
                    }
                    else
                    {
                        computer.IncreaseIp();
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private int GetValueA(Computer computer)
        {
            int value;
            return int.TryParse(ArgumentA, out value) ? value : computer.GetRegisterValue(ArgumentA[0]);
        }

        private int GetValueB(Computer computer)
        {
            int value;
            return int.TryParse(ArgumentB, out value) ? value : computer.GetRegisterValue(ArgumentB[0]);
        }

    }
}