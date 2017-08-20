﻿using System;
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

            log("Problem 1.2");
            new List<Tuple<string, string>>
            {
                new Tuple<string, string>("foo", "foo"),
                new Tuple<string, string>("foo", "ofo"),
                new Tuple<string, string>("foo", "oof"),
                new Tuple<string, string>("bar", "arb"),
                new Tuple<string, string>("foo", "bar"),
                new Tuple<string, string>("foo", null),
                new Tuple<string, string>(null, null),
                new Tuple<string, string>(null, "baz"),
                new Tuple<string, string>("foo", "baz"),
                new Tuple<string, string>("fo", "baz")
            }
            .ToList()
            .ForEach(x => log($"isPermutation(\"{x.Item1}\",\"{x.Item2}\") -> {IsPermutation(x.Item1, x.Item2)}"));

            log(string.Empty);
            log(string.Empty);

            log("Problem 1.3");
            new Dictionary<string, int>
            {
                // 0123456789123456
                { "Mr John Smith    ", 13 },
                { "Foo bar baz    ", 11 },
                { "Foo bar  ", 7 },
                { "a b cc    ", 6 },
                { "a b  c      ", 6 }
            }
            .ToList()
            .ForEach(x => log($"'{x.Key}' -> '{Urlify(x.Key, x.Value)}'"));

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

        // Problem 1.2
        static bool IsPermutation(string a, string b)
        {
            // Null checking
            if(a == null)
            {
                if(b == null)
                {
                    return true;
                }
                return false;
            }
            if(b == null)
            {
                return false;
            }

            var charCounts = new Dictionary<char, int>();
            foreach(var c in a)
            {
                if (!charCounts.ContainsKey(c))
                {
                    charCounts[c] = 0;
                }
                charCounts[c]++;
            }

            foreach(var c in b)
            {
                if(!charCounts.ContainsKey(c))
                {
                    return false;
                }
                charCounts[c]--;
            }

            return charCounts.Values.All(x => x == 0);
        }

        // Problem 1.3
        static string Urlify(string str, int trueLen)
        {
            var charAry = str.ToCharArray();
            var shift = str.Length - trueLen;
            
            for(var i = trueLen - 1; i >= 0; i--)
            {
                var next = str[i];
                if (next != ' ')
                {
                    Swap(charAry, i, i + shift);
                }
                else
                {
                    charAry[i + shift - 2] = '%';
                    charAry[i + shift - 1] = '2';
                    charAry[i + shift] = '0';
                    shift -= 2;
                }
            }

            return new string(charAry);
        }

        #endregion

        #region Util

        static void Swap<T>(T[] ary, int iA, int iB)
        {
            var tmp = ary[iA];
            ary[iA] = ary[iB];
            ary[iB] = tmp;
        }

        #endregion
    }
}
