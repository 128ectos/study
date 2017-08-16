using System;
using System.Collections.Generic;
using System.Linq;

namespace Study
{
    class Program
    {
        static void Main(string[] args)
        {
            Action<string> log = Console.WriteLine;

            log("Problem 1.1");
            new[]
            {
                "foo",
                "bar",
                "baz",
                "rrr",
                "",
                null
            }
            .ToList()
            .ForEach(x => log($"isUnique(\"{x}\") -> {IsUnique(x)}"));

            log(string.Empty);
            log(string.Empty);




        }

        // Start off just inlining problems in this file to get bootstrapped
        // Once enough problems done will have to refactor organization

        #region Chapter 1

        // Problem 1.1
        static bool IsUnique(string str)
        {
            if(str == null) { return true; } // Depends on requirement
            var seen = new HashSet<char>();
            foreach(var c in str)
            {
                if(seen.Contains(c))
                {
                    return false;
                }
                seen.Add(c);
            }
            return true;
        }


        #endregion

    }
}
