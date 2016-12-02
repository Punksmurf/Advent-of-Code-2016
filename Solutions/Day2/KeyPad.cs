using System;

namespace AoC2016.Solutions.Day2
{
    public class KeyPad
    {
        public readonly int Width;
        public readonly int Height;
        private readonly char[] _layout;

        public KeyPad()
        {
            Width = 3;
            Height = 3;
            _layout = new []
            {
                '1', '2', '3',
                '4', '5', '6',
                '7', '8', '9'
            };
        }

        public KeyPad(int width, int height, char[] layout)
        {
            Width = width;
            Height = height;
            _layout = layout;
        }

        public char GetKey(int x, int y)
        {
            if (x < 0 || x >= Width) throw new IndexOutOfRangeException();
            if (y < 0 || y >= Height) throw new IndexOutOfRangeException();
            return _layout[y * Width + x];
        }

        public Tuple<int, int> GetPosition(char keyValue)
        {
            var idx = Array.IndexOf(_layout, keyValue);
            if (idx < 0) throw new ArgumentOutOfRangeException(nameof(idx));

            var x = idx % Width;
            var y = (idx - x) / Width;

            return Tuple.Create(x, y);
        }

        public bool IsValid(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height && GetKey(x, y) != ' ';
        }
    }
}