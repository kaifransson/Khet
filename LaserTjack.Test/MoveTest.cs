using LaserTjack.Core;
using LaserTjack.Core.Pieces;
using Xunit;

namespace LaserTjack.Test
{
    public class MoveTest
    {
        private static void Fail(string message)
        {
            Assert.True(false, message);
        }

        [Fact]
        public void Rotate_PieceWithNoOrientationClockwise_NothingHappens()
        {
            var obelisk = new Obelisk();
            var board = new Board(new ICell[,]
            {
                {obelisk}
            });

            var newBoard = board.Rotate(obelisk, Rotation.Clockwise);

            Assert.Equal(board, newBoard);
        }

        [Fact]
        public void Rotate_PieceWithOrientation_ResultIsDifferentFromOldBoard()
        {
            var pyramid = new Pyramid(Direction.Up);
            var board = new Board(new ICell[,]
            {
                {pyramid}
            });

            var newBoard = board.Rotate(pyramid, Rotation.CounterClockwise);

            Assert.NotEqual(board, newBoard);

        }

        [Fact]
        public void Rotate_PieceWithOrientationClockwise_GetsRotated()
        {
            var pyramid = new Pyramid(Direction.Up);
            var board = new Board(new ICell[,]
            {
                {pyramid}
            });

            var newBoard = board.Rotate(pyramid, Rotation.Clockwise);

            var expected = new Board(new ICell[,]
            {
                {new Pyramid(Direction.Right)}
            });
            Assert.Equal(expected, newBoard);
        }

        [Fact]
        public void Rotate_SeveralPieces_AllAreRotated()
        {
            var pyramid = new Pyramid(Direction.Up);
            var djed = new Djed(Direction.Down);
            var eyeOfHorus = new EyeOfHorus(Direction.Left);
            var board = new Board(new ICell[,]
            {
                {pyramid},
                {djed},
                {eyeOfHorus}
            });

            var newBoard = board
                .Rotate(pyramid, Rotation.Clockwise)
                .Rotate(djed, Rotation.CounterClockwise)
                .Rotate(eyeOfHorus, Rotation.Clockwise);

            var expected = new Board(new ICell[,]
            {
                {new Pyramid(Direction.Right)},
                {new Djed(Direction.Right)},
                {new EyeOfHorus(Direction.Up)}
            });
            Assert.Equal(expected, newBoard);
        }

        [Fact]
        public void Move_PieceInDirectionLegally_PieceIsMoved()
        {
            var pharaoh = new Pharaoh();
            var board = new Board(new ICell[,]
            {
                {pharaoh, new EmptyCell()}
            });

            var expected = new Board(new ICell[,]
            {
                {new EmptyCell(), pharaoh}
            });
            board.Move(pharaoh, Direction.Right).Match(
                invalidMove => Fail("Move was invalid"),
                newBoard => Assert.Equal(expected, newBoard));
        }

        [Fact]
        public void Move_PieceOffTheBoard_IsInvalidMove()
        {
            var pharaoh = new Pharaoh();
            var board = new Board(new ICell[,]
            {
                {pharaoh}
            });

            board.Move(pharaoh, Direction.Up).Match(
                invalidMove => Assert.Equal(InvalidMove.OutOfBounds, invalidMove),
                newBoard => Fail("Move was valid"));
        }

        [Fact]
        public void Move_PyramidIntoOtherPyramid_IsIllegal()
        {
            var pyramid = new Pyramid(Direction.Up);
            var board = new Board(new ICell[,]
            {
                {pyramid, new Pyramid(Direction.Down)}
            });

            board.Move(pyramid, Direction.Right).Match(
                invalidMove => Assert.Equal(InvalidMove.Occupied, invalidMove),
                _ => Fail("Move was valid"));
        }
    }
}