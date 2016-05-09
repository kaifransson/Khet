using System.Collections.Generic;
using System.Linq;

namespace LaserTjack.Core.Pieces
{
    public class EyeOfHorus : IOrientedPiece
    {
        public EyeOfHorus(Direction orientation)
        {
            Orientation = orientation;
        }

        public IEnumerable<Direction> Refract(Direction direction)
        {
            return new Djed(Orientation)
                .Refract(direction)
                .Concat(new EmptyCell()
                    .Refract(direction));
        }

        public bool Equals(ICell other)
        {
            return (other as EyeOfHorus)?.Orientation == Orientation;
        }

        public Direction Orientation { get; }

        public IOrientedPiece Rotate(Rotation rotation)
        {
            return new EyeOfHorus(Orientation.Transform(rotation));
        }
    }
}