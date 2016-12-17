using System;
using System.Linq;
using AoC2016.Utils;

namespace AoC2016.Solutions.Day17
{
    public class RoomState
    {
        public static class Factory
        {
            public static RoomState Create(Md5 md5, Direction[] route)
            {
                var x = 0;
                var y = 0;
                foreach (var direction in route)
                {
                    switch (direction)
                    {
                        case Direction.Up:
                            y--;
                            break;
                        case Direction.Down:
                            y++;
                            break;
                        case Direction.Left:
                            x--;
                            break;
                        case Direction.Right:
                            x++;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

                // We'll only need to do a few so let's just not optimize only getting the first 2 bytes of the hash in a string
                var doors = md5.HashWithSalt(string.Concat(route.Select(_ => _.GetChar())))
                        .ToCharArray()
                        .Take(4)
                        .Select(c => c >= 'b' && c <= 'f')
                        .ToArray();

                // We cannot walk through the walls
                if (x == 0) doors[(int)Direction.Left] = false;
                if (x == 3) doors[(int)Direction.Right] = false;
                if (y == 0) doors[(int)Direction.Up] = false;
                if (y == 3) doors[(int)Direction.Down] = false;

                return new RoomState(route, new Coordinate(x, y), doors);
            }
        }

        public Direction[] Route { get; }
        public Coordinate Cooordinate { get; }
        public bool[] Doors { get; } // Up, Down, Left, Right

        public RoomState(Direction[] route, Coordinate cooordinate, bool[] doors)
        {
            Route = route;
            Cooordinate = cooordinate;
            Doors = doors;
        }
    }
}