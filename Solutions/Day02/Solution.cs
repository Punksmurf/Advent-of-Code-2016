using System.Threading.Tasks;

namespace AoC2016.Solutions.Day02
{
    public class Solution : ISolution
    {
        public async Task<string> SolveExampleAsync()
        {
            return await Task.Run(() => SolveSilver("ULL\nRRDDD\nLURDL\nUUUUD"));
        }

        public async Task<string> SolveSilverAsync(string input)
        {
            return await Task.Run(() => SolveSilver(input));
        }

        public async Task<string> SolveGoldAsync(string input)
        {
            return await Task.Run(() => SolveGold(input));
        }

        private static string SolveSilver(string input)
        {
            var code = Solve(new KeyPad(), input);

            return $"Based on the given keypad and instructions, the code to the toilet must be {code}";
        }

        private static string SolveGold(string input)
        {
            var keyPad = new KeyPad(5, 5, new []
            {
                ' ', ' ', '1', ' ', ' ',
                ' ', '2', '3', '4', ' ',
                '5', '6', '7', '8', '9',
                ' ', 'A', 'B', 'C', ' ',
                ' ', ' ', 'D', ' ', ' '
            });

            var code = Solve(keyPad, input);

            return $"Stupid keypad, but the code is: {code}";
        }

        private static string Solve(KeyPad keyPad, string secretDocument)
        {
            var finger = new Finger(keyPad, secretDocument);
            return string.Concat(finger.GetKeyPresses('5'));
        }
    }
}