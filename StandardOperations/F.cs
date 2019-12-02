using System;
using ResultBase.StandardResults;

namespace ResultBase.StandardOperations
{
    public static class F
    {
        public static T Id<T>(T t) => t;

        public static Func<T, Func<U, V>> Curry<T, U, V>(Func<T, U, V> func) => t => u => func(t, u);

        public static Result<TNew, TErr> Map<T, TNew, TErr>(this T item, IFunction<T, TNew, TErr> func)
            where TErr : Error => func.Invoke(item);

        public static IFunction<string, Guid, Error> ParseAsGuid =>
            TraceFunction.New<string, Guid, Error>(
                input => Guid.TryParse(input, out var result)
                    ? Option.FromValue(result)
                    : Option<Guid>.FromNone(new Error($"Failed to parse '{input}' as a guid")),
                error => error.Enrich("No value provided to parse as a guid"));

        public static IFunction<string, int, Error> ParseAsInt =>
            TraceFunction.New<string, int, Error>(
                input => int.TryParse(input, out var result)
                    ? Option.FromValue(result)
                    : Option<int>.FromNone(new Error($"Failed to parse '{input}' as an integer")),
                err => err.Enrich("No value provided to parse as an integer"));
    }
}