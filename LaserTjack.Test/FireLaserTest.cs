using LaserTjack.Core;
using LaserTjack.Core.Pieces;
using Xunit;

namespace LaserTjack.Test
{
    public class FireLaserTest
    {
        private static Laser CreateLaserFacing(Direction laserOrientation)
        {
            return new Laser(new EmptyCell(), PlayerColor.White, laserOrientation);
        }

        [Fact]
        public void Fire_EmptyBoard_NoPiecesHit()
        {
            var laser = CreateLaserFacing(Direction.Right);
            var board = new Board(new ICell[,]
            {
                {laser}
            });

            var piecesHit = board.Fire(laser);

            Assert.Empty(piecesHit);
        }

        [Fact]
        public void Fire_OnPharaoh_ItGetsHit()
        {
            var laser = CreateLaserFacing(Direction.Right);
            var pharaoh = new Pharaoh();
            var board = new Board(new ICell[,]
            {
                {laser, pharaoh}
            });

            var piecesHit = board.Fire(laser);

            Assert.Equal(new[] {pharaoh}, piecesHit);
        }

        [Fact]
        public void Fire_OnPharaohThroughDjed_OnlyPharaohIsHit()
        {
            var laser = CreateLaserFacing(Direction.Right);
            var pharaoh = new Pharaoh();
            var board = new Board(new ICell[,]
            {
                {laser, new EyeOfHorus(Direction.Up), pharaoh}
            });

            var piecesHit = board.Fire(laser);

            Assert.Equal(new[] {pharaoh}, piecesHit);
        }

        [Fact]
        public void Fire_OnFarAwayPharaoh_ItGetsHit()
        {
            var laser = CreateLaserFacing(Direction.Right);
            var pharaoh = new Pharaoh();
            var board = new Board(new ICell[,]
            {
                {laser, new EmptyCell(), new EmptyCell(), new EmptyCell(), pharaoh}
            });

            var piecesHit = board.Fire(laser);

            Assert.Equal(new[] {pharaoh}, piecesHit);
        }

        [Fact]
        public void Fire_OnObeliskViaPyramidSpiral_ItGetsHit()
        {
            var laser = CreateLaserFacing(Direction.Right);
            var obelisk = new Obelisk();
            var board = new Board(new ICell[,]
            {
                {laser          , new EmptyCell()             , new EmptyCell(), new Pyramid(Direction.Left)},
                {new EmptyCell(), new Pyramid(Direction.Down) , obelisk        , new EmptyCell()            },
                {new EmptyCell(), new Pyramid(Direction.Right), new EmptyCell(), new Pyramid(Direction.Up)  }
            });

            var piecesHit = board.Fire(laser);

            Assert.Equal(new[] {obelisk}, piecesHit);
        }
    }
}