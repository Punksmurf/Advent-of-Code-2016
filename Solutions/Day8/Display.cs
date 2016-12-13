﻿using System;
using System.Text;
using System.Threading;
using AoC2016.Solutions.Day8.Instructions;

namespace AoC2016.Solutions.Day8
{
    public class Display
    {
        private readonly int _width;
        private readonly int _height;
        private readonly bool[,] pixels;

        public Display(int width, int height)
        {
            _width = width;
            _height = height;
            pixels = new bool[width,height];
        }

        public void Accept(Instruction visitor)
        {
            visitor.Visit(this);
            Show();
        }

        public void Rect(int width, int height)
        {
            width = Math.Max(0, Math.Min(width, _width));
            height = Math.Max(0, Math.Min(height, _height));

            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    pixels[x, y] = true;
                }
            }
        }

        public void RotateRow(int row, int amount)
        {
            while (amount < 0) amount += _width;
            amount %= _width;

            row = Math.Max(0, Math.Min(_height - 1, row));

            for (var i = 0; i < amount; i++)
            {
                var endValue = pixels[_width - 1, row];
                for (var x = _width - 1; x > 0; x--)
                {
                    pixels[x, row] = pixels[x - 1, row];
                }
                pixels[0, row] = endValue;
            }
        }

        public void RotateColumn(int column, int amount)
        {
            while (amount < 0) amount += _height;
            amount %= _height;

            column = Math.Max(0, Math.Min(_width - 1, column));

            for (var i = 0; i < amount; i++)
            {
                var endValue = pixels[column, _height - 1];
                for (var y = _height - 1; y > 0; y--)
                {
                    pixels[column, y] = pixels[column, y - 1];
                }
                pixels[column, 0] = endValue;
            }
        }

        public int CountLitPixels()
        {
            var count = 0;
            for (var x = 0; x < _width; x++)
            {
                for (var y = 0; y < _height; y++)
                {
                    if (pixels[x, y]) count++;
                }
            }
            return count;
        }

        private void Show()
        {
            var cW = Console.BufferWidth;
            var cH = Console.BufferHeight;

            if (cW < _width + 2 || cH <= _height) return;

            var display = BuildDisplay();

            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.Write(new StringBuilder(_width + 2).Append('•', _width + 2));
            for (var y = 0; y < _height; y++)
            {
                Console.SetCursorPosition(0, y + 1);
                Console.Write('•');
                Console.Write(display[y]);
                Console.Write('•');
            }
            Console.SetCursorPosition(0, _height + 1);
            Console.Write(new StringBuilder(_width + 2).Append('•', _width + 2));

            Thread.Sleep(500);
        }

        public string[] BuildDisplay()
        {
            var lines = new string[_height];

            for (var y = 0; y < _height; y++)
            {
                var line = new StringBuilder();
                for (var x = 0; x < _width; x++)
                {
                    line.Append(pixels[x, y] ? '#' : ' ');
                }
                lines[y] = line.ToString();
            }

            return lines;
        }
    }
}