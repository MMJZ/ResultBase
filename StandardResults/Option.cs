namespace ResultBase.StandardResults
{
    public class Option<T> : Result<T, Error>
    {
        internal Option(T item) : base(item)
        {
        }

        private Option(Error error) : base(error)
        {
        }

        public static Option<T> FromNone() => new Option<T>(new Error("Option result has no value"));
        public static Option<T> FromNone(Error error) => new Option<T>(error);
    }

    public class Option
    {
        public static Option<T> FromValue<T>(T item) => new Option<T>(item);
    }
}