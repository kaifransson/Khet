using LaserTjack.Core;

namespace LaserTjack.Console
{
    public static class Program
    {
        public static void Main()
        {
            new LaserTjackGame(Boards.ClassicBoard,
                new MoveParser(System.Console.In, System.Console.Out).GetNextMove,
                new BoardRenderer(System.Console.Out).Render)
                .Play();
            System.Console.Out.WriteLine("Red wins!");
            System.Console.In.Read();
        }
    }
}