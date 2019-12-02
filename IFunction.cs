using System;

namespace ResultBase
{
    public interface IFunction<in T, TNew, TErr> where TErr : Error
    {
        Result<TNew, TErr> Invoke(T t);
        TErr Error(Error error);
    }

    public class TraceFunction<T, TNew, TErr> : IFunction<T, TNew, TErr> where TErr : Error
    {
        private readonly Func<T, Result<TNew, TErr>> _func;
        private readonly Func<Error, TErr> _errorFunc;

        public TraceFunction(Func<T, Result<TNew, TErr>> func, Func<Error, TErr> errorFunc)
        {
            _func = func;
            _errorFunc = errorFunc;
        }

        public Result<TNew, TErr> Invoke(T t) => _func(t);
        public TErr Error(Error error) => _errorFunc(error);
    }

    public static class TraceFunction
    {
        public static IFunction<T, TNew, TErr> New<T, TNew, TErr>(Func<T, Result<TNew, TErr>> func,
            Func<Error, TErr> errorFunc) where TErr : Error =>
            new TraceFunction<T, TNew, TErr>(func, errorFunc);

        public static IFunction<T, TNew, TErr> WithError<T, TNew, TErr>(this Func<T, Result<TNew, TErr>> func,
            Func<Error, TErr> errorFunc) where TErr : Error =>
            new TraceFunction<T, TNew, TErr>(func, errorFunc);
    }
}