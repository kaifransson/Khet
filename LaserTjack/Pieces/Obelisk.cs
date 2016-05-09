using System.Collections.Generic;

namespace LaserTjack.Core.Pieces
{
    public class Obelisk : IPiece
    {
        public IEnumerable<Direction> Refract(Direction direction)
        {
            yield break;
        }

        public bool Equals(ICell other)
        {
            return other is Obelisk;
        }
    }
}