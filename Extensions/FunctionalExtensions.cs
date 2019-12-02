using System;

namespace ResultBase.Extensions
{
    public static class FunctionalExtensions
    {
        public static U UseIn<T, U>(this T input, Func<T, U> func) => func(input);
    }
}