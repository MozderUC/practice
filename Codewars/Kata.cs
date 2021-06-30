using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;

public static class Kata
{
    // Not Kata
    // BinarySearch


    // 6 kyi

    // 5 kyi
    // ValidParentheses https://www.codewars.com/kata/52774a314c2333f0a7000688
    // MoveZeroes https://www.codewars.com/kata/52597aa56021e91c93000cb0
    // GetReadableTime https://www.codewars.com/kata/52685f7382004e774f0001f7
    // WeightSort https://www.codewars.com/kata/55c6126177c9441a570000cc
    // Anagrams https://www.codewars.com/kata/523a86aa4230ebb5420001e1

    // 4 kyi
    // Snail https://www.codewars.com/kata/521c2db8ddc89b9b7a0000c1
    // formatDuration https://www.codewars.com/kata/52742f58faf5485cae000b9a
    // DblLinear https://www.codewars.com/kata/5672682212c8ecf83e000050/train/csharp

    // 3 kyi

    // 2 kyi

    // 1 kyi

    public static int DblLinear (int n)
    {
        var s = new SortedSet<int> {1};

        var t1 = new List<int> {1};
        var t2 = new List<int>();

        while (s.Count <= 6*n)
        {
            t1.ForEach(i =>
            {
                s.Add(i * 2 + 1);
                s.Add(i * 3 + 1);

                t2.Add(i * 2 + 1);
                t2.Add(i * 3 + 1);
            });

            t1.Clear();
            t1.AddRange(t2);
            t2.Clear();
        }

        return s.Distinct().ElementAt(n);
    }

    public static string formatDuration(int seconds)
    {
        if (seconds == 0)
            return "now";

        var years = seconds / 31536000;
        var timeSpan = new TimeSpan(0,0,seconds % 31536000);

        var arr = new []
        {
            ("year", years),
            ("day", timeSpan.Days),
            ("hour", timeSpan.Hours),
            ("minute", timeSpan.Minutes),
            ("second", timeSpan.Seconds)
        }
            .Where(x => x.Item2 > 0)
            .Select(x => x.Item2 > 1 ? (x.Item1 + "s", x.Item2) : x)
            .ToArray();

        var result = new StringBuilder();
        for (var i = 0; i < arr.Length; i++)
        {
            if (i == 0)
            {
                result.Append($"{arr[i].Item2} {arr[i].Item1}");
                continue;
            }

            if (i == arr.Length - 1)
            {
                result.Append($" and {arr[i].Item2} {arr[i].Item1}");
                continue;
            }

            result.Append($", {arr[i].Item2} {arr[i].Item1}");
        }

        return result.ToString();
    }

    public static int[] Snail(int[][] array)
    {
        var lapCount = 0;
        var result = new List<int>();
        var n = array[0].Length;

        for (var i = 0; i < n/2; i++)
        {
            //move1
            result.AddRange(array[lapCount][lapCount..(n - lapCount)]);

            //move2
            result.AddRange(array[(lapCount+1)..(n - lapCount)].Select(x => x[n - lapCount - 1]));

            //move3
            result.AddRange(array[n-1 - lapCount].Reverse().ToArray()[(lapCount + 1)..(n - lapCount)]);

            //move4
            result.AddRange(array[(lapCount+1)..(n - lapCount - 1)].Select(x => x[lapCount]).Reverse());

            lapCount++;
        }

        if (n % 2 != 0)
        {
            result.Add(array[n/2][n/2]);
        }

        return result.ToArray();
    }

    public static List<string> Anagrams(string word, List<string> words)
    {
        var alphabeticalOrderedWord = string.Join("", word.ToCharArray().OrderBy(x => x));
        return words.Where(x => string.Join("", x.ToCharArray().OrderBy(c => c)) == alphabeticalOrderedWord).ToList();
    }

    public static string orderWeight(string s)
    {
        return string.Join(
            " ",
            s
                .Split(" ")
                .Select(x => new KeyValuePair<int, string>(x.Aggregate(0, (a, b) => a + b - '0'), x))
                .OrderBy(x => x.Key)
                .ThenBy(x => x.Value)
                .Select(x => x.Value));
    }

    public static string GetReadableTime(int seconds)
    {
        return $"{seconds / 3600:d2}:" +
               $"{seconds % 3600 / 60:d2}:" +
               $"{seconds % 3600 % 60:d2}";
    }

    // O(n + zeroCount)
    public static int[] MoveZeroes(int[] arr)
    {
        var zerosCount = 0;

        for (var i = 0; i < arr.Length; i++)
        {
            if (arr[i] == 0)
            {
                zerosCount++;
                continue;
            }
            arr[i - zerosCount] = arr[i];
        }

        for (var i = arr.Length - zerosCount; i < arr.Length; i++)
        {
            arr[i] = 0;
        }

        return arr;
    }

    public static bool BinarySearch(IEnumerable<int> sortedSet, int valueToSearch)
    {
        var sortedArray = sortedSet as int[] ?? sortedSet.ToArray();
        return BinarySearchImpl(sortedArray, valueToSearch, 0, sortedArray.Length - 1);
    }

    private static bool BinarySearchImpl(int[] sortedArray, int valueToSearch, int startIndex, int endIndex)
    {
        if (endIndex - startIndex == 1)
        {
            return sortedArray[startIndex] == valueToSearch || sortedArray[endIndex] == valueToSearch;
        }

        var midIndex = startIndex + (endIndex - startIndex) / 2;

        // take left part of array
        if (sortedArray[midIndex] > valueToSearch)
        {
            return BinarySearchImpl(sortedArray, valueToSearch, startIndex, midIndex);
        }

        // take right part of array
        if (sortedArray[midIndex] < valueToSearch)
        {
            return BinarySearchImpl(sortedArray, valueToSearch, midIndex, endIndex);
        }

        return true;
    }

    public static bool ValidParentheses(string input)
    {
        var stack = new Stack<char>();

        foreach (var i in input)
        {
            if (i == '(')
            {
                stack.Push(i);
            }

            if (i == ')')
            {
                if (stack.Count == 0)
                    return false;

                stack.Pop();
            }
        }

        return stack.Count == 0;
    }
}