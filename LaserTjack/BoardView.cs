namespace LaserTjack.Core
{
    public class BoardView
    {
        private readonly Board _board;

        public BoardView(Board board)
        {
            _board = board;
        }

        public int Rows => _board.Cells.GetLength(0);
        public int Columns => _board.Cells.GetLength(1);
        public ICell this[int row, int column] => _board.Cells[row, column];
    }
}