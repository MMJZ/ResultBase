using System.Collections.Generic;
using ResultBase.StandardResults;

namespace ResultBase.StandardOperations
{
    public static class FExtensions
    {
        public static IFunction<TKey, TValue, Error> Get<TKey, TValue>(this Dictionary<TKey, TValue> dict) =>
            TraceFunction.New<TKey, TValue, Error>(
                key => dict.TryGetValue(key, out var result)
                    ? Option.FromValue(result)
                    : Option<TValue>.FromNone(new Error($"Failed to find key '{key}' in the dictionary")),
                error => error.Enrich("No value provided for dictionary lookup"));
    }
}