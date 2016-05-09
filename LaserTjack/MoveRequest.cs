namespace LaserTjack.Core
{
    public class MoveRequest
    {
        public MoveRequest(Position from, Direction? to, Rotation? rotation)
        {
            From = from;
            To = to;
            Rotation = rotation;
        }

        public Position From { get; }
        public Direction? To { get; }
        public Rotation? Rotation { get; }
    }
}