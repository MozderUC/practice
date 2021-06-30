using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;

public class Kata
{
    // Not Kata
    // BinarySearch


    // 6 kyi

    // 5 kyi
    /// ValidParentheses https://www.codewars.com/kata/52774a314c2333f0a7000688
    /// MoveZeroes https://www.codewars.com/kata/52597aa56021e91c93000cb0
    /// GetReadableTime https://www.codewars.com/kata/52685f7382004e774f0001f7
    /// WeightSort https://www.codewars.com/kata/55c6126177c9441a570000cc
    /// Anagrams https://www.codewars.com/kata/523a86aa4230ebb5420001e1

    // 4 kyi

    // 3 kyi

    // 2 kyi

    // 1 kyi

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