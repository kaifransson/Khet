using System;

namespace LaserTjack.Core
{
    public interface IEither<out TLeft, out TRight>
    {
        void Match(Action<TLeft> left, Action<TRight> right);
    }

    public sealed class Left<TLeft, TRight> : IEither<TLeft, TRight>
    {
        private readonly TLeft _value;

        public Left(TLeft value)
        {
            _value = value;
        }

        public void Match(Action<TLeft> left, Action<TRight> right)
        {
            left(_value);
        }
    }

    public sealed class Right<TLeft, TRight> : IEither<TLeft, TRight>
    {
        private readonly TRight _value;

        public Right(TRight value)
        {
            _value = value;
        }

        public void Match(Action<TLeft> left, Action<TRight> right)
        {
            right(_value);
        }
    }
}