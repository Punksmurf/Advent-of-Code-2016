using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AoC2016.Solutions.Day08.Instructions;

namespace AoC2016.Solutions.Day08
{
    public class Solution : ISolution
    {
        public Task<string> SolveExampleAsync()
        {
            var display = new Display(7,3);

            const string input = @"rect 3x2
rotate column x=1 by 1
rotate row y=0 by 4
rotate column x=1 by 1";

            var instructions = input.Split('\n').Select(Instruction.Factory.Create);

            foreach (var instruction in instructions)
            {
                display.Accept(instruction);
            }

            var result = BuildDisplayOutput(display);
            var count = display.CountLitPixels();

            return Task.Run(() => $"This is what the display looks like:\n{result}\nThe number of lit pixels is {count}");
        }

        public Task<string> SolveSilverAsync(string input)
        {
            var display = new Display(50, 6, true);

            var instructions = input.Split('\n').Select(Instruction.Factory.Create);

            foreach (var instruction in instructions)
            {
                display.Accept(instruction);
            }
            display.Show();
            var count = display.CountLitPixels();

            return Task.Run(() => $"The number of lit pixels is {count}");
        }

        public Task<string> SolveGoldAsync(string input)
        {
            return Task.Run(() =>
            {
                var display = new Display(50, 6);

                var instructions = input.Split('\n').Select(Instruction.Factory.Create);

                foreach (var instruction in instructions)
                {
                    display.Accept(instruction);
                }

                return '\n' + BuildDisplayOutput(display);

            });
        }

        private static string BuildDisplayOutput(Display display)
        {
            var result = new StringBuilder();
            foreach (var line in display.BuildDisplay())
            {
                result.Append(line).Append('\n');
            }
            return result.ToString();
        }
    }
}