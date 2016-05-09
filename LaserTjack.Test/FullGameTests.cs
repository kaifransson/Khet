using System;
using System.Collections.Generic;
using System.IO;
using LaserTjack.Console;
using LaserTjack.Core;
using Xunit;

namespace LaserTjack.Test
{
    public class FullGameTests
    {
        private readonly StringWriter _output = new StringWriter();
        private static readonly Renderer NullRenderer = board => { };

        public FullGameTests()
        {
            System.Console.SetOut(_output);
        }

        private static void BufferInput(string moves)
        {
            System.Console.SetIn(new StringReader(moves));
        }

        [Fact]
        public void QuickWinForRed()
        {
            BufferInput("24mr\n20rl");
            Program.Main();
            Assert.Contains(
                "red wins",
                _output.ToString(),
                StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void ToMove_NewGame_White()
        {
            var moveRequests = new Dictionary<int, MoveRequest>
            {
                {0, new MoveRequest(new Position(0, 4), null, Rotation.Clockwise)}
            };
            var move = -1;
            MoveProvider moveProvider = state =>
            {
                state.Match(
                    invalid =>
                    {
                        Assert.Equal(0, move);
                        Assert.Equal(InvalidMove.NotYourPiece, invalid);
                    },
                    board => move++);
                return moveRequests[move];
            };
            new LaserTjackGame(Boards.ClassicBoard, moveProvider, NullRenderer).Play();
        }
    }
}