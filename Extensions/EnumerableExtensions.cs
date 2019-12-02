using System.Collections.Generic;

namespace ResultBase.Extensions
{
    public static class EnumerableExtensions
    {
        public static string Join(this IEnumerable<string> input, string joiner) => string.Join(joiner, input);
    }
}