using System;
using System.Collections.Generic;

namespace LaserTjack.Core
{
    public interface ICell : IEquatable<ICell>
    {
        IEnumerable<Direction> Refract(Direction direction);
    }
}