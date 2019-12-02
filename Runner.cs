using System;
using System.Collections.Generic;
using ResultBase.StandardOperations;

namespace ResultBase
{
    public class Runner
    {
        public static void Main(string[] args)
        {

            var dict = new Dictionary<int, string>
            {
                [1] = "haha",
                [2] = "ah shit no",
                [3] = "3",
                [4] = "5"
            };

            var result = "5d"
                .Map(F.ParseAsInt)
                .Bind(dict.Get())
                .Bind(F.ParseAsInt)
                .Bind(dict.Get())
                .Catch(e => Console.WriteLine(e.Format()));
        }
    }
}