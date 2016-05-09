using System;

namespace LaserTjack.Core
{
    public abstract class Maybe<T>
    {
        public static Maybe<T> Just(T value)
        {
            return new MJust(value);
        }

        public static readonly Maybe<T> Nothing = new MNothing();

        public abstract TResult Match<TResult>(Func<T, TResult> just, Func<TResult> nothing);

        private class MNothing : Maybe<T>
        {
            public override TResult Match<TResult>(Func<T, TResult> just, Func<TResult> nothing)
            {
                return nothing();
            }
        }

        private class MJust : Maybe<T>
        {
            private readonly T _value;

            public MJust(T value)
            {
                _value = value;
            }

            public override TResult Match<TResult>(Func<T, TResult> just, Func<TResult> nothing)
            {
                return just(_value);
            }
        }
    }
}