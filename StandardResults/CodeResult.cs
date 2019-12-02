using System;
using System.Net;

namespace ResultBase.StandardResults
{
    public class CodeResult<T, TErr> : Result<T, TErr> where TErr : Error
    {
        private readonly HttpStatusCode _statusCode;

        internal CodeResult(T item, HttpStatusCode statusCode) : base(item)
        {
            _statusCode = statusCode;
        }

        internal CodeResult(HttpStatusCode statusCode, TErr error) : base(error)
        {
            _statusCode = statusCode;
        }

        public new Result<TNew, TErrNew> Bind<TNew, TErrNew>(IFunction<T, TNew, TErrNew> func) where TErrNew : Error =>
            InSuccessState ? func.Invoke(Item) : new CodeResult<TNew, TErrNew>(_statusCode, func.Error(Error!));

        public new Result<TNew, TErr> Bind<TNew>(Func<T, Result<TNew, TErr>> func) =>
            InSuccessState ? func.Invoke(Item) : new CodeResult<TNew, TErr>(_statusCode, Error!);
    }

    public static class CodeResult
    {
        public static CodeResult<T, TErr> FromSuccess<T, TErr>(T item) where TErr : Error =>
            new CodeResult<T, TErr>(item, HttpStatusCode.OK);

        public static CodeResult<T, TErr> FromSuccess<T, TErr>(T item, HttpStatusCode statusCode) where TErr : Error =>
            new CodeResult<T, TErr>(item, statusCode);

        public static CodeResult<T, TErr> FromError<T, TErr>(TErr err) where TErr : Error =>
            new CodeResult<T, TErr>(HttpStatusCode.InternalServerError, err);

        public static CodeResult<T, TErr> FromError<T, TErr>(HttpStatusCode statusCode, TErr err) where TErr : Error =>
            new CodeResult<T, TErr>(statusCode, err);
    }
}