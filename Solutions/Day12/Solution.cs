using System.Threading.Tasks;

namespace AoC2016.Solutions.Day12
{
    public class Solution : ISolution
    {
        public Task<string> SolveExampleAsync()
        {
            const string input = @"cpy 41 a
inc a
inc a
dec a
jnz a 2
dec a";
            return Task.Factory.StartNew(() => $"Value of register a: {SolveSilver(input)}");
        }

        public Task<string> SolveSilverAsync(string input)
        {
            return Task.Factory.StartNew(() => $"Value of register a: {SolveSilver(input)}");
        }

        public Task<string> SolveGoldAsync(string input)
        {
            return Task.Factory.StartNew(() => $"Value of register a: {SolveGold(input)}");
        }

        private static int SolveSilver(string input)
        {
            var program = Instruction.ParseProgram(input);
            var computer = new Computer(program);
            computer.Run();
            return computer.A;
        }

        private static int SolveGold(string input)
        {
            var program = Instruction.ParseProgram(input);
            var computer = new Computer(program);
            computer.SetRegisterValue('C', 1);
            computer.Run();
            return computer.A;
        }
    }
}