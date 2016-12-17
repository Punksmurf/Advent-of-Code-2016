using System;
using System.Security.Cryptography;
using System.Text;

namespace AoC2016.Utils
{
    /// <summary>
    /// Helper class for AoC hashing. Can be initialized with a salt (from the puzzle input), so you don't have to keep
    /// track of it elsewhere.
    /// </summary>
    public class Md5 : IDisposable
    {
        private readonly HashAlgorithm _hasher = MD5.Create();

        public string Salt { get; }

        public Md5() : this("")
        {
        }

        public Md5(string salt)
        {
            Salt = salt;
        }

        public string HashWithSalt(string input, int stretch = 0)
        {
            return Hash($"{Salt}{input}", stretch);
        }

        public string Hash(string input, int stretch)
        {
            for (var i = 0; i < stretch + 1; i++)
            {
                input = Hash(input);
            }
            return input;
        }

        public string Hash(string input)
        {
            var hash = _hasher.ComputeHash(Encoding.UTF8.GetBytes(input));
            var sb = new StringBuilder(32);
            foreach (var b in hash)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }

        public void Dispose()
        {
            _hasher.Dispose();
        }

    }
}