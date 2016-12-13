using System;
using System.Linq;
using System.Threading.Tasks;

namespace AoC2016.Solutions.Day10
{
    public class Solution : ISolution
    {
        public Task<string> SolveExampleAsync()
        {
            const string input = @"value 5 goes to bot 2
bot 2 gives low to bot 1 and high to bot 0
value 3 goes to bot 1
bot 1 gives low to output 1 and high to bot 0
bot 0 gives low to output 2 and high to output 0
value 2 goes to bot 2";
            return Task.Factory.StartNew(() => SolveExample(input));
        }

        public Task<string> SolveSilverAsync(string input)
        {
            return Task.Factory.StartNew(() => SolveSilver(input));
        }

        public Task<string> SolveGoldAsync(string input)
        {
            return Task.Factory.StartNew(() => SolveGold(input));
        }

        private static string SolveExample(string input)
        {
            var factory = SolveFactory(input);

            var output0 = factory.GetOutput(0).LastValue;
            var output1 = factory.GetOutput(1).LastValue;
            var output2 = factory.GetOutput(2).LastValue;
            var botToCompare5And2Is2 = factory.FindBotsWhichCompared(5, 2).Any(_ => _.Id == 2);

            return $"Outputs 0-2: {output0}, {output1}, {output2}; correct bot compared: {botToCompare5And2Is2}.";
        }

        public static string SolveSilver(string input)
        {
            var factory = SolveFactory(input);
            var botToCompare61And17 = factory.FindBotsWhichCompared(61, 17).First().Id;
            return $"The bot to compare 61 and 18 chips is {botToCompare61And17}.";
        }

        public static string SolveGold(string input)
        {
            var factory = SolveFactory(input);

            var output0 = factory.GetOutput(0).LastValue;
            var output1 = factory.GetOutput(1).LastValue;
            var output2 = factory.GetOutput(2).LastValue;

            return $"The result is {output0 * output1 * output2}.";
        }

        private static BunnyFactory SolveFactory(string input)
        {
            var instructions = Instruction.Factory.CreateInstructions(input);
            var factory = new BunnyFactory();

            while (instructions.Any(_ => !_.Done))
            {
                foreach (var instruction in instructions.Where(_ => !_.Done))
                {
                    factory.Accept(instruction);
                }
            }

            return factory;
        }
    }
}