using System;
using System.Collections.Generic;
using System.IO;
using LaserTjack.Core;

namespace LaserTjack.Console
{
    internal class MoveParser
    {
        private static readonly IReadOnlyDictionary<InvalidMove, string> ErrorMessages =
            new Dictionary<InvalidMove, string>
            {
                {InvalidMove.OutOfBounds, "Tried to move out of bounds"},
                {InvalidMove.Occupied, "Cell is occupied"},
                {InvalidMove.NoPiece, "There's no piece there"},
                {InvalidMove.BadCoordinates, "Bad coordinates"}
            };

        private static readonly IReadOnlyDictionary<char, Rotation> ParseRotation = new Dictionary<char, Rotation>
        {
            {'r', Rotation.Clockwise},
            {'l', Rotation.CounterClockwise}
        };

        private static readonly IReadOnlyDictionary<char, Direction> ParseDirection = new Dictionary<char, Direction>
        {
            {'u', Direction.Up},
            {'r', Direction.Right},
            {'d', Direction.Down},
            {'l', Direction.Left}
        };

        private readonly TextReader _userInput;
        private readonly TextWriter _userOutput;

        public MoveParser(TextReader userInput, TextWriter userOutput)
        {
            _userInput = userInput;
            _userOutput = userOutput;
        }

        public MoveRequest GetNextMove(IEither<InvalidMove, BoardView> currentboard)
        {
            currentboard.Match(
                HandleInvalidMove,
                board => { });
            return NextMove();
        }

        private void HandleInvalidMove(InvalidMove invalidMove)
        {
            _userOutput.WriteLine(ErrorMessages[invalidMove]);
        }

        private MoveRequest NextMove()
        {
            var input = _userInput.ReadLine();
            var position = new Position(int.Parse(input[0].ToString()), int.Parse(input[1].ToString()));
            Direction? direction = null;
            Rotation? rotation = null;
            switch (input[2])
            {
                case 'm':
                    direction = ParseDirection[input[3]];
                    break;
                case 'r':
                    rotation = ParseRotation[input[3]];
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return new MoveRequest(position, direction, rotation);
        }
    }
}