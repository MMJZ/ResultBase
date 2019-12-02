using System.Collections.Generic;
using System.Linq;
using ResultBase.Extensions;

namespace ResultBase
{
    public class Error
    {
        public Error? UnderlyingError { get; }
        public string Message { get; }

        public Error(string message) : this(message, default)
        {
        }

        private Error(string message, Error? underlyingError)
        {
            Message = message;
            UnderlyingError = underlyingError;
        }

        public Error Enrich(string message) => new Error(message, this);

        private IEnumerable<Error> Flatten() =>
            (UnderlyingError?.Flatten() ?? Enumerable.Empty<Error>())
            .Append(this);

        public string Format() => Flatten()
            .Select(e => e.Message)
            .Join("\n- which caused:\n");
    }
}