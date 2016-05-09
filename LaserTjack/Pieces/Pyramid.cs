using System.Collections.Generic;

namespace LaserTjack.Core.Pieces
{
    public class Pyramid : IOrientedPiece
    {
        public Pyramid(Direction orientation)
        {
            Orientation = orientation;
        }

        public IEnumerable<Direction> Refract(Direction direction)
        {
            if (direction == Orientation.Reverse())
                yield return direction.NextClockwise();
            else if (direction == Orientation.NextClockwise()) yield return Orientation;
        }

        public bool Equals(ICell other)
        {
            return (other as Pyramid)?.Orientation == Orientation;
        }

        public Direction Orientation { get; }

        public IOrientedPiece Rotate(Rotation rotation)
        {
            return new Pyramid(Orientation.Transform(rotation));
        }
    }
}