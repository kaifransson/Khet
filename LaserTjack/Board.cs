using System;
using System.Collections.Generic;
using System.Linq;
using LaserTjack.Core.Pieces;

namespace LaserTjack.Core
{
    public class Board : IEquatable<Board>
    {
        public Board(ICell[,] cells)
        {
            Cells = cells;
        }

        public IEnumerable<Laser> WhiteLasers => Lasers
            .Where(laser => laser.Color == PlayerColor.White);

        public IEnumerable<Laser> RedLasers => Lasers
            .Where(laser => laser.Color == PlayerColor.Red);

        private IEnumerable<Laser> Lasers => Cells
            .Cast<ICell>()
            .OfType<Laser>();

        public ICell[,] Cells { get; }

        public bool Equals(Board other)
        {
            return Cells.Cast<ICell>()
                .SequenceEqual(other.Cells.Cast<ICell>());
        }

        public IEnumerable<IPiece> Fire(Laser laser)
        {
            var seed = new[] {new PropagationStep(laser, new[] {laser.AimingDirection})};
            IList<PropagationStep> currentSteps = seed;
            while ((currentSteps = currentSteps.SelectMany(Propagate).ToList()).Any())
            {
                foreach (var piece in TerminatedPieces(currentSteps))
                {
                    yield return piece;
                }
            }
        }

        public Board Rotate(IPiece piece, Rotation rotation)
        {
            var newCells = (ICell[,])Cells.Clone();
            var oriented = piece as IOrientedPiece;
            if (oriented != null)
            {
                var coordinates = PositionOf(oriented);
                newCells[coordinates.Row, coordinates.Column] = oriented.Rotate(rotation);
            }
            return new Board(newCells);
        }

        public IEither<InvalidMove, Board> Move(IPiece piece, Direction direction)
        {
            var newCells = (ICell[,])Cells.Clone();
            var mDestination = TryNavigate(direction, PositionOf(piece));

            return mDestination.Match(
                destination => MakeMove(newCells, piece, destination),
                () => new Left<InvalidMove, Board>(InvalidMove.OutOfBounds));
        }

        private static IEnumerable<IPiece> TerminatedPieces(IEnumerable<PropagationStep> steps)
        {
            return steps
                .Where(step => !step.Directions.Any())
                .Select(step => step.Cell)
                .OfType<IPiece>();
        }

        private IEnumerable<PropagationStep> Propagate(PropagationStep propagationStep)
        {
            var position = PositionOf(propagationStep.Cell);
            var nextSteps =
                from direction in propagationStep.Directions
                from nextStep in TryNavigate(direction, position).Match(
                    cell => new[] {new PropagationStep(cell, cell.Refract(direction))},
                    Enumerable.Empty<PropagationStep>)
                select nextStep;
            foreach (var step in nextSteps)
            {
                yield return step;
            }
        }

        private Maybe<ICell> TryNavigate(Direction direction, Position position)
        {
            int row, column;
            switch (direction)
            {
                case Direction.Up:
                    row = position.Row - 1;
                    column = position.Column;
                    break;
                case Direction.Right:
                    row = position.Row;
                    column = position.Column + 1;
                    break;
                case Direction.Down:
                    row = position.Row + 1;
                    column = position.Column;
                    break;
                case Direction.Left:
                    row = position.Row;
                    column = position.Column - 1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction));
            }

            return AreCoordinatesInBounds(row, column)
                ? Maybe<ICell>.Just(Cells[row, column])
                : Maybe<ICell>.Nothing;
        }

        private bool AreCoordinatesInBounds(int row, int column)
        {
            var rowInBounds = row >= Cells.GetLowerBound(0) && row <= Cells.GetUpperBound(0);
            var columnInBounds = column >= Cells.GetLowerBound(1) && column <= Cells.GetUpperBound(1);

            return rowInBounds && columnInBounds;
        }

        private Position PositionOf(ICell needle)
        {
            foreach (var row in Enumerable.Range(Cells.GetLowerBound(0), Cells.GetLength(0)))
                foreach (var column in Enumerable.Range(Cells.GetLowerBound(1), Cells.GetLength(1)))
                {
                    if (Cells[row, column] == needle) return new Position(row, column);
                }

            throw new ArgumentException(nameof(needle));
        }

        private IEither<InvalidMove, Board> MakeMove(ICell[,] cells, IPiece piece, ICell destination)
        {
            if (destination is IPiece) return new Left<InvalidMove, Board>(InvalidMove.Occupied);

            SwapCells(cells, piece, destination);
            return new Right<InvalidMove, Board>(new Board(cells));
        }

        private void SwapCells(ICell[,] newCells, ICell a, ICell b)
        {
            var aCoordinates = PositionOf(a);
            var bCoordinates = PositionOf(b);
            newCells[bCoordinates.Row, bCoordinates.Column] = a;
            newCells[aCoordinates.Row, aCoordinates.Column] = b;
        }

        private class PropagationStep
        {
            public PropagationStep(ICell cell, IEnumerable<Direction> directions)
            {
                Cell = cell;
                Directions = directions;
            }

            public ICell Cell { get; }
            public IEnumerable<Direction> Directions { get; }
        }
    }

    public enum InvalidMove
    {
        OutOfBounds,
        Occupied,
        NoPiece,
        BadCoordinates,
        NotYourPiece
    }
}