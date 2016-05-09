using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LaserTjack.Core;
using LaserTjack.Core.Pieces;

namespace LaserTjack.Console
{
    internal class BoardRenderer
    {
        private readonly TextWriter _output;

        public BoardRenderer(TextWriter output)
        {
            _output = output;
        }

        public void Render(BoardView board)
        {
            var sb = new StringBuilder();
            foreach (var row in Enumerable.Range(0, board.Rows))
            {
                foreach (var column in Enumerable.Range(0, board.Columns))
                {
                    var cell = board[row,column];
                    sb.Append($" {Render(cell)} ");
                }
                sb.AppendLine();
            }
            _output.WriteLine(sb.ToString());
        }

        private static string Render(ICell cell)
        {
            var cellSymbol = CellSymbols[cell.GetType()];
            var orientedPiece = cell as IOrientedPiece;
            return (orientedPiece != null)
                ? $"{cellSymbol}_{DirectionSymbols[orientedPiece.Orientation]}"
                : $" {cellSymbol} ";
        }

        private static readonly IReadOnlyDictionary<Direction, char> DirectionSymbols = new Dictionary<Direction, char>
        {
            {Direction.Up, 'u'},
            {Direction.Right, 'r'},
            {Direction.Down, 'd'},
            {Direction.Left, 'l'}
        };

        private static readonly IReadOnlyDictionary<Type, char> CellSymbols = new Dictionary<Type, char>
        {
            {typeof(EmptyCell), '_'},
            {typeof(Laser), 'l'},
            {typeof(Pharaoh), 'P'},
            {typeof(Djed), 'd'},
            {typeof(Pyramid), 'p'},
            {typeof(EyeOfHorus), 'h'},
            {typeof(Obelisk), 'o'}
        };
    }
}