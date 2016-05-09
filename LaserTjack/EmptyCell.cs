using System.Collections.Generic;

namespace LaserTjack.Core
{
    public class EmptyCell : ICell
    {
        public IEnumerable<Direction> Refract(Direction direction)
        {
            yield return direction;
        }

        public bool Equals(ICell other)
        {
            return other is EmptyCell;
        }
    }
}