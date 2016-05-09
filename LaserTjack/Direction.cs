using System;

namespace LaserTjack.Core
{
    public static class DirectionExtensions
    {
        public static Direction Reverse(this Direction direction)
        {
            return NextClockwise(NextClockwise(direction));
        }

        public static Direction NextClockwise(this Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return Direction.Right;
                case Direction.Right:
                    return Direction.Down;
                case Direction.Down:
                    return Direction.Left;
                case Direction.Left:
                    return Direction.Up;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        public static Direction NextCounterClockwise(this Direction direction)
        {
            return NextClockwise(Reverse(direction));
        }

        public static Direction Transform(this Direction direction, Rotation rotation)
        {
            switch (rotation)
            {
                case Rotation.Clockwise:
                    return direction.NextClockwise();
                case Rotation.CounterClockwise:
                    return direction.NextCounterClockwise();
                default:
                    throw new ArgumentOutOfRangeException(nameof(rotation), rotation, null);
            }
        }
    }

    [Flags]
    public enum Direction
    {
        Up = 0,
        Right = 1 << 0,
        Down = 1 << 1,
        Left = 1 << 2
    }
}