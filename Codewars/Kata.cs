﻿using System;
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
    // ValidateSolution (Sudoku board validator) https://www.codewars.com/kata/529bf0e9bdf7657179000008/solutions/csharp

    // 3 kyi
    // ValidateBattlefield https://www.codewars.com/kata/52bb6539a4cf1b12d90005b7

    // 2 kyi

    // 1 kyi

    public static bool ValidateBattlefield(int[,] field)
    {
        var reservedArea = new List<(int, int)>();
        var reservedAreaTemp = new List<(int, int)>();

        var listOfShip = new List<int>();

        var indexesOfVerticalShip = new List<(int, int)>();

        var h = 1;
        var v = 1;

        for (var i = 0; i < 10; i++)
        {
            for (var j = 0; j < 10; j++)
            {
                if (field[i, j] != 1)
                    continue;

                reservedAreaTemp.AddRange(GetElementsAroundElement((i,j), field));

                // horizontal ship
                while (true)
                {
                    if (j + h >= 9 || field[i, j + h] != 1)
                        break;

                    if (reservedArea.Contains((i, j + h)))
                    {
                        return false;
                    }
                    reservedAreaTemp.AddRange(GetElementsAroundElement((i,j+h), field));

                    h++;
                }

                if (h > 1)
                {
                    listOfShip.Add(h);
                    reservedArea.AddRange(reservedAreaTemp);
                    reservedAreaTemp.Clear();
                    j += h-1;
                    h = 1;
                    continue;
                }

                // vertical ship
                if (indexesOfVerticalShip.Contains((i + 1, j)) || indexesOfVerticalShip.Contains((i - 1, j)))
                {
                    reservedAreaTemp.Clear();
                    continue;
                }
                while (true)
                {
                    if (i+v >= 9 || field[i+v, j] != 1)
                        break;

                    if (reservedArea.Contains((i+v, j)))
                    {
                        return false;
                    }
                    reservedAreaTemp.AddRange(GetElementsAroundElement((i+v,j), field));
                    indexesOfVerticalShip.Add((i+v, j));

                    v++;
                }

                if (v > 1)
                {
                    indexesOfVerticalShip.Add((i,j));
                    listOfShip.Add(v);
                    reservedArea.AddRange(reservedAreaTemp);
                    reservedAreaTemp.Clear();
                    v = 1;
                    continue;
                }


                // 1 size ship
                if (h == 1 && v == 1)
                {
                    if (reservedArea.Contains((i, j)))
                    {
                        return false;
                    }

                    reservedArea.AddRange(reservedAreaTemp);
                    listOfShip.Add(1);
                }
            }
        }

        return listOfShip.OrderBy(x => x).SequenceEqual(new[] {1, 1, 1, 1, 2, 2, 2, 3, 3, 4});
    }

    private static IEnumerable<(int, int)> GetElementsAroundElement((int, int) element, int[,] field)
    {
        var result = new List<(int, int)>();

        // top (3)
        try
        {
            field.GetValue(element.Item1 - 1, element.Item2 - 1);
            result.Add((element.Item1 - 1, element.Item2 - 1));
        }
        catch (Exception e)
        {}
        try
        {
            field.GetValue(element.Item1 - 1, element.Item2);
            result.Add((element.Item1 - 1, element.Item2));
        }
        catch (Exception e)
        {}
        try
        {
            field.GetValue(element.Item1 - 1, element.Item2 + 1);
            result.Add((element.Item1 - 1, element.Item2 + 1));
        }
        catch (Exception e)
        {}


        // bottom (3)
        try
        {
            field.GetValue(element.Item1 + 1, element.Item2 - 1);
            result.Add((element.Item1 + 1, element.Item2 - 1));
        }
        catch (Exception e)
        {}
        try
        {
            field.GetValue(element.Item1 + 1, element.Item2);
            result.Add((element.Item1 + 1, element.Item2));
        }
        catch (Exception e)
        {}
        try
        {
            field.GetValue(element.Item1 + 1, element.Item2 + 1);
            result.Add((element.Item1 + 1, element.Item2 + 1));
        }
        catch (Exception e)
        {}

        // left (1)
        try
        {
            field.GetValue(element.Item1, element.Item2 - 1);
            result.Add((element.Item1, element.Item2 - 1));
        }
        catch (Exception e)
        {}

        // right (1)
        try
        {
            field.GetValue(element.Item1, element.Item2 + 1);
            result.Add((element.Item1, element.Item2 + 1));
        }
        catch (Exception e)
        {}


        return result;
    }

    public static bool ValidateSolution(int[][] board)
    {
        try
        {
            // if add not unique key to the dictionary -> occur exception -> so that we can check validness of board
            var horizontal = new Dictionary<int, int>();
            var subGrids = new Dictionary<int, int>[3];
            var vertical = new Dictionary<int, int>[9];

            for (var i = 0; i < 9; i++)
            {
                horizontal.Clear();
                if (i % 3 == 0 && i != 0)
                {
                    subGrids[0].Clear();
                    subGrids[1].Clear();
                    subGrids[2].Clear();
                }

                for (var j = 0; j < 9; j++)
                {
                    horizontal.Add(board[i][j], 0);

                    subGrids[j/3] ??= new Dictionary<int, int>();
                    subGrids[j/3].Add(board[i][j], 0);

                    vertical[j] ??= new Dictionary<int, int>();
                    vertical[j].Add(board[i][j], 0);
                }
            }
        }
        catch (ArgumentException e)
        {
            return false;
        }

        return true;
    }

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