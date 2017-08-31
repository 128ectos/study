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

            log(string.Empty);
            log(string.Empty);

            log("Problem 1.4");
            new List<string>
            {
                "Tact Coa",
                "no",
                "tttt",
                "",
                "foof",
                "fooof",
                "foobar" 
            }
            .ForEach(x => log($"{x} -> {IsPalindromePermutation(x)}"));

            log(string.Empty);
            log(string.Empty);

            log("Problem 1.5");
            new List<Tuple<string, string>>
            {
                new Tuple<string, string>("pale", "ple"),
                new Tuple<string, string>("pales", "pale"),
                new Tuple<string, string>("pale", "bale"),
                new Tuple<string, string>("pale", "bake"),
            }
            .ForEach(x => log($"'{x.Item1}', '{x.Item2}' -> '{IsMaxOneEditAway(x.Item1, x.Item2)}'"));
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

        // Problem 1.4
        static bool IsPalindromePermutation(string str)
        {
            var charCounts = new Dictionary<char, int>();
            foreach (var c in str.ToLowerInvariant())
            {
                if(c == ' ') { continue; }
                if (charCounts.ContainsKey(c))
                {
                    charCounts[c]++;
                    continue;
                }
                charCounts[c] = 1;
            }

            var numUnevenChars = charCounts.Count(x => x.Value % 2 == 1);
            return numUnevenChars == 0 || numUnevenChars == 1;
        }

        // Problem 1.5
        // todo rework this to handle string "edit" rules instead of just char checks
        static bool IsOneEditAway(string a, string b)
        {
            var diff = a.Length - b.Length;
            if(-2 >= diff || diff >= 2)
            {
                return false;
            }

            var aCharCountMap = a.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
            var noMatch = new List<char>();
            foreach (var c in b)
            {
                if(aCharCountMap.ContainsKey(c))
                {
                    aCharCountMap[c]--;
                }
                else
                {
                    noMatch.Add(c);
                }
            }

            if(noMatch.Count() >= 2) { return false; }
            var totDiffCount = aCharCountMap.Values.Sum();
            return totDiffCount == 0 || totDiffCount == 1;
        }

        static bool IsMaxOneEditAway(string a, string b)
        {
            var lenDiff = a.Length - b.Length;
            if(lenDiff >= 2 || lenDiff <= -2) { return false; }

            if (lenDiff == 1) // Removed case
            {
                return IsOneCharRemoved(a, b);
            }
            else if (lenDiff == -1) // Added case
            {
                return IsOneCharRemoved(b, a);
            }
            else // Equal len case
            {
                var charDiff = false;
                for (var i = 0; i < a.Length; i++)
                {
                    if (a[i] == b[i])
                    {
                        continue;
                    }
                    else
                    {
                        if (charDiff == true)
                        {
                            return false;
                        }
                        else
                        {
                            charDiff = true;
                        }
                    }
                }
                return true;
            }
        }

        static bool IsOneCharRemoved(string a, string b)
        {
            var charRm = false;
            for(var i = 0; i < a.Length; i++)
            {
                if(i == a.Length - 1 && !charRm) { return true; }
                if(!charRm)
                {
                    if(a[i] == b[i])
                    {
                        continue;
                    }
                    else
                    {
                        charRm = true;
                        continue;
                    }
                }
                else // One char has already been removed
                {
                    if(a[i] == b[i-1])
                    {
                        continue;
                    }
                    else
                    {
                        return false; // Char is diff
                    }
                }
            }
            return true;
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
