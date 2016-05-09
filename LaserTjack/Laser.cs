using System;
using System.Collections.Generic;

namespace LaserTjack.Core
{
    public class Laser : ICell
    {
        private readonly EmptyCell _baseCell;
        public Direction AimingDirection { get; }
        public PlayerColor Color { get; }

        public Laser(EmptyCell baseCell, PlayerColor laserColor, Direction laserOrientation)
        {
            _baseCell = baseCell;
            Color = laserColor;
            AimingDirection = laserOrientation;
        }

        public IEnumerable<Direction> Refract(Direction direction) => _baseCell.Refract(direction);
        public bool Equals(ICell other)
        {
            throw new NotImplementedException();
        }
    }
}