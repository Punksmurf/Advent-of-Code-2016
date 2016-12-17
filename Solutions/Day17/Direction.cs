using System;

namespace AoC2016.Solutions.Day17
{
    public static class DirectionExtensions
    {
        public static char GetChar(this Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return 'U';
                case Direction.Down:
                    return 'D';
                case Direction.Left:
                    return 'L';
                case Direction.Right:
                    return 'R';
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }
    }

    public enum Direction : byte
    {
        Up = 0, Down = 1, Left = 2, Right = 3
    }
}