namespace LaserTjack.Core.Pieces
{
    public interface IOrientedPiece : IPiece
    {
        Direction Orientation { get; }
        IOrientedPiece Rotate(Rotation rotation);
    }
}