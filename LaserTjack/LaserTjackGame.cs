using System;
using System.Linq;
using LaserTjack.Core.Pieces;

namespace LaserTjack.Core
{
    public class LaserTjackGame
    {
        private readonly Renderer _renderer;
        private readonly MoveProvider _moveProvider;
        private Board _currentBoard;

        public LaserTjackGame(Board board, MoveProvider moveProvider, Renderer renderer)
        {
            _renderer = renderer;
            _moveProvider = moveProvider;
            _currentBoard = board;
        }

        public void Play()
        {
            do
            {
                _renderer(CurrentBoard);
            } while (MakeMove());
            _renderer(CurrentBoard);
        }

        private BoardView CurrentBoard => new BoardView(_currentBoard);

        private bool MakeMove()
        {
            return MakeMove(_moveProvider(new Right<InvalidMove, BoardView>(new BoardView(_currentBoard))));
        }

        private bool MakeMove(MoveRequest moveRequest)
        {
            try
            {
                var from = moveRequest.From;
                var piece = _currentBoard.Cells[from.Row, from.Column] as IPiece;
                if (piece == null)
                {
                    MakeMove(_moveProvider(new Left<InvalidMove, BoardView>(InvalidMove.NoPiece)));
                }
                if (moveRequest.Rotation.HasValue)
                {
                    _currentBoard = _currentBoard.Rotate(piece, moveRequest.Rotation.Value);
                }
                else
                {
                    _currentBoard.Move(piece, moveRequest.To.Value).Match(
                        im => MakeMove(_moveProvider(new Left<InvalidMove, BoardView>(im))),
                        newBoard => _currentBoard = newBoard);
                }
            }
            catch (IndexOutOfRangeException)
            {
                MakeMove(_moveProvider(new Left<InvalidMove, BoardView>(InvalidMove.BadCoordinates)));
            }
            return !_currentBoard.WhiteLasers
                .Concat(_currentBoard.RedLasers)
                .SelectMany(_currentBoard.Fire)
                .OfType<Pharaoh>()
                .Any();
        }
    }
    
    public delegate void Renderer(BoardView board);

    public delegate MoveRequest MoveProvider(IEither<InvalidMove, BoardView> currentBoard);
}