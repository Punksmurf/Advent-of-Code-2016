using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AoC2016.Solutions.Day5
{
    public class Solution : ISolution
    {
        public Task<string> SolveExampleAsync()
        {
            return Task.Run(() => FindDoorPasswordSilver("abc"));
        }

        public async Task<string> SolveSilverAsync(string input)
        {
            var password = await Task.Run(() => FindDoorPasswordSilver(input));
            return $"The password for the first door is {password}";
        }

        public async Task<string> SolveGoldAsync(string input)
        {
            var password = await Task.Run(() => FindDoorPasswordGold(input));
            return $"The password for the second door is {password}";
        }

        private static string FindDoorPasswordSilver(string doorId)
        {
            var index = 0;
            var password = new StringBuilder();
            using (var hasher = CreateHasher())
            {
                while (password.Length < 8)
                {
                    var next = FindNextDoorHash(hasher, doorId, index);
                    var byte3 = next.Item2[2].ToString("x2"); // hex representation of byte
                    password.Append(byte3[1]); // the second char of the 3rd byte is the 6th char of the hash string
                    index = next.Item1 + 1;
                }
                return password.ToString();
            }
        }

        private static string FindDoorPasswordGold(string doorId)
        {
            var index = 0;
            var password = "________";

            // Console.WriteLine($"Passord: {password}");

            using (var hasher = CreateHasher())
            {
                while (password.Contains('_'))
                {
                    var next = FindNextDoorHash(hasher, doorId, index);
                    index = next.Item1 + 1;

                    var byte3 = next.Item2[2].ToString("x2"); // hex representation of byte
                    var byte4 = next.Item2[3].ToString("x2"); // hex representation of byte
                    var pos = byte3[1] - 48;
                    if (pos < 0 || pos >= password.Length || password[pos] != '_') continue;

                    password = new StringBuilder(password) {[pos] = byte4[0]}.ToString();
                    //Console.WriteLine($"Passord: {password}");
                }
            }
            return password;
        }

        private static Tuple<int, byte[]> FindNextDoorHash(HashAlgorithm hasher, string doorId, int startIndex)
        {
            var index = startIndex;
            while (true)
            {
                var data = HashDoorAndIndex(hasher, doorId, index);

                // hex representation starts with 00000xxx…
                if (data[0] == 0 && data[1] == 0 && data[2] < 0x10)
                {
                    return Tuple.Create(index, data);
                }
                index++;
            }
        }

        private static byte[] HashDoorAndIndex(HashAlgorithm hasher, string doorId, int index)
        {
            return hasher.ComputeHash(Encoding.UTF8.GetBytes($"{doorId}{index}"));
        }

        private static HashAlgorithm CreateHasher()
        {
            return MD5.Create();
        }
    }
}