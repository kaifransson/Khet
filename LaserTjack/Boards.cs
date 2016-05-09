using LaserTjack.Core.Pieces;

namespace LaserTjack.Core
{
    public static class Boards
    {
        public static Board ClassicBoard
        {
            get
            {
                var whiteLaser = new Laser(new EmptyCell(), PlayerColor.White, Direction.Right);
                var redLaser = new Laser(new EmptyCell(), PlayerColor.Red, Direction.Left);

                return new Board(new ICell[,]
                {
                    {new EmptyCell(), new EmptyCell(), new EmptyCell(), new Pyramid(Direction.Left), new Pyramid(Direction.Down), new EmptyCell(), new EmptyCell(), redLaser},
                    {new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell()},
                    {new Pyramid(Direction.Right), new EmptyCell(), new EmptyCell(), new Pyramid(Direction.Right), new Pyramid(Direction.Up), new EmptyCell(), new Pyramid(Direction.Up), new EmptyCell()},
                    {new Obelisk(), new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell(), new Pyramid(Direction.Right), new EmptyCell(), new EmptyCell()},
                    {new Pharaoh(), new EmptyCell(), new EmptyCell(), new EyeOfHorus(Direction.Left), new Djed(Direction.Up), new EmptyCell(), new EmptyCell(), new Obelisk()},
                    {new Obelisk(), new EmptyCell(), new EmptyCell(), new Djed(Direction.Down), new EyeOfHorus(Direction.Right), new EmptyCell(), new EmptyCell(), new Pharaoh()},
                    {new EmptyCell(), new EmptyCell(), new Pyramid(Direction.Left), new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell(), new Obelisk()},
                    {new EmptyCell(), new Pyramid(Direction.Down), new EmptyCell(), new Pyramid(Direction.Down), new Pyramid(Direction.Left), new EmptyCell(), new EmptyCell(), new Pyramid(Direction.Left)},
                    {new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell()},
                    {whiteLaser, new EmptyCell(), new EmptyCell(), new Pyramid(Direction.Up), new Pyramid(Direction.Right), new EmptyCell(), new EmptyCell(), new EmptyCell()}
                });
            }
        }
    }
}