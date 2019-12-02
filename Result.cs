using System;
using System.Diagnostics.CodeAnalysis;

namespace ResultBase
{
    public class Result<T, TErr> where TErr : Error
    {
        [MaybeNull]
        protected readonly T Item;
        protected readonly TErr? Error;
        protected readonly bool InSuccessState;

        protected Result(T item)
        {
            Item = item;
            InSuccessState = true;
        }

        protected Result(TErr error)
        {
            Error = error;
            Item = default!;
            InSuccessState = false;
        }

        public Result<TNew, TErrNew> Bind<TNew, TErrNew>(IFunction<T, TNew, TErrNew> func) where TErrNew : Error =>
            InSuccessState ? func.Invoke(Item) : new Result<TNew, TErrNew>(func.Error(Error!));

        public Result<TNew, TErr> Bind<TNew>(Func<T, Result<TNew, TErr>> func) =>
            InSuccessState ? func(Item) : new Result<TNew, TErr>(Error!);

        public Result<T, TErr> Catch(Action<TErr> action)
        {
            if (!InSuccessState)
            {
                action(Error!);
            }

            return this;
        }
    }
}