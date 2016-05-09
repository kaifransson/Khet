using System.Collections.Generic;
using System.Linq;

namespace LaserTjack.Core.Pieces
{
    public class Djed : IOrientedPiece
    {
        public Djed(Direction orientation)
        {
            Orientation = orientation;
        }

        public IEnumerable<Direction> Refract(Direction direction)
        {
            return new Pyramid(Orientation)
                .Refract(direction)
                .Concat(new Pyramid(Orientation.Reverse())
                    .Refract(direction));
        }

        public bool Equals(ICell other)
        {
            return (other as Djed)?.Orientation == Orientation;
        }

        public Direction Orientation { get; }

        public IOrientedPiece Rotate(Rotation rotation)
        {
            return new Djed(Orientation.Transform(rotation));
        }
    }
}