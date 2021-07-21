using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    // MaxSequence https://www.codewars.com/kata/54521e9ec8e60bc4de000d6c
    // chooseBestSum (generate combinations) https://www.codewars.com/kata/55e7280b40e1c4a06d0000aa

    // 4 kyi
    // Snail https://www.codewars.com/kata/521c2db8ddc89b9b7a0000c1
    // formatDuration https://www.codewars.com/kata/52742f58faf5485cae000b9a
    // DblLinear https://www.codewars.com/kata/5672682212c8ecf83e000050/train/csharp
    // ValidateSolution (Sudoku board validator) https://www.codewars.com/kata/529bf0e9bdf7657179000008
    // PathFinderOne https://www.codewars.com/kata/5765870e190b1472ec0022a2
    // SinglePermutations (generate permutations) https://www.codewars.com/kata/5254ca2719453dcc0b00027d
    // productFib https://www.codewars.com/kata/5541f58a944b85ce6d00006a
    // Rot13 https://www.codewars.com/kata/530e15517bc88ac656000716
    // Extract https://www.codewars.com/kata/51ba717bb08c1cd60f00002f
    // NextBiggerNumber https://www.codewars.com/kata/55983863da40caa2c900004e
    // NextSmaller https://www.codewars.com/kata/5659c6d896bc135c4c00021e
    // PyramidSlideDown https://www.codewars.com/kata/551f23362ff852e2ab000037

    // 3 kyi
    // ValidateBattlefield https://www.codewars.com/kata/52bb6539a4cf1b12d90005b7
    // PathFinderThree https://www.codewars.com/kata/576986639772456f6f00030c

    // 2 kyi

    // 1 kyi


    public static int LongestSlideDown(int[][] pyramid)
    {


        return 0;
    }

    public static int PathFinderThree(string maze)
    {
        if (maze.Length == 1) return 0;

        var N = maze.IndexOf("\n");

        var m = maze.Replace("\n", "");

        // tuple is cost/vertex
        var adjacencyList = new List<(int, int)>[N*N];

        // fill adjacencyList
        for (var i = 0; i < m.Length; i++)
        {
            adjacencyList[i] = new List<(int, int)>();
            adjacencyList[i].AddRange(GetAdjacentVertexes(i, m, N));
        }

        // tuple is cost/vertex
        var q = new SortedSet<(int,int)>();
        var dst = Enumerable.Repeat(int.MaxValue,N*N).ToArray();

        q.Add((0,0));
        dst[0] = 0;

        // Dijkstra algo
        while (q.Any())
        {
            var c = q.Min;
            q.Remove(c);

            if(dst[c.Item2] == int.MaxValue)
                continue;

            foreach (var t in adjacencyList[c.Item2])
            {
                var d = c.Item1 + t.Item1;
                if (d < dst[t.Item2])
                {
                    dst[t.Item2] = d;
                    q.Add((d, t.Item2));
                }
            }
        }

        return dst[N * N - 1] == -1 ? -1 : dst[N * N - 1];
    }

    private static IEnumerable<(int,int)> GetAdjacentVertexes(int i, string maze, int N)
    {
        var result = new List<(int,int)>();
        var g = i % N;

        if (g == 0 || i == 0)
        {
            result.Add((Math.Abs(maze[i + 1] - maze[i]),i + 1));
        }
        else if (g == N - 1 || i == N - 1)
        {
            result.Add((Math.Abs(maze[i - 1] - maze[i]), i - 1));
        }
        else
        {
            result.Add((Math.Abs(maze[i + 1] - maze[i]),i + 1));
            result.Add((Math.Abs(maze[i - 1] - maze[i]), i - 1));
        }

        if (i < N)
        {
            result.Add((Math.Abs(maze[i + N] - maze[i]),i + N));
        }
        else if (i > N * N - 1 - N)
        {
            result.Add((Math.Abs(maze[i - N] - maze[i]),i - N));
        }
        else
        {
            result.Add((Math.Abs(maze[i + N] - maze[i]),i + N));
            result.Add((Math.Abs(maze[i - N] - maze[i]),i - N));
        }

        return result;
    }

    public static long NextSmaller(long n)
    {
        if (n < 10)
            return -1;

        var arr = n.ToString().Select(x => (int?)(x - '0')).ToArray();

        for (var i = arr.Length - 2; i >= 0; i--)
        {
            var r = arr.Skip(i+1).ToList();
            var index = r.IndexOf(r.Where(s => s < arr[i]).DefaultIfEmpty().Max());

            if (index == -1) continue;

            var t = arr[i];
            arr[i] = r[index];

            r.RemoveAt(index);
            r.Add(t);

            if (arr[0] == 0)
                return -1;

            return long.Parse(string.Join("", arr.Take(i+1).Concat(r.OrderByDescending(x => x))));
        }

        return -1;
    }

    public static long NextBiggerNumber(long n)
    {
        if (n < 10)
            return -1;

        var arr = n.ToString().Select(x => (int?)(x - '0')).ToArray();

        for (var i = arr.Length - 2; i >= 0; i--)
        {
            var r = arr.Skip(i+1).ToList();
            var index = r.IndexOf(r.Where(s => s > arr[i]).DefaultIfEmpty().Min());

            if (index == -1) continue;

            var t = arr[i];
            arr[i] = r[index];

            r.RemoveAt(index);
            r.Add(t);

            return long.Parse(string.Join("",arr.Take(i+1).Concat(r.OrderBy(x => x))));
        }

        return -1;
    }

    public static string Extract(int[] args)
    {
        var result = new StringBuilder();
        var increasingSequence = new List<int>();

        for (var i = 0; i < args.Length; i++)
        {
            if (i + 1 < args.Length && args[i + 1] - args[i] == 1)
            {
                increasingSequence.Add(args[i]);
                continue;
            }

            if (increasingSequence.Any())
            {
                if (increasingSequence.Count == 1)
                {
                    result.Append(increasingSequence[0]+",");
                    result.Append(args[i]+",");
                }
                else
                {
                    result.Append($"{increasingSequence.First()}-{args[i]},");
                }

                increasingSequence.Clear();
                continue;
            }

            result.Append(args[i] + ",");
        }

        // remove comma at the end (move the pointer (i.e. last index) back one character)
        result.Length--;

        return result.ToString();
    }

    public static class Combinations
    {
        private static List<int> list;
        private static Stack<int> combination;
        private static List<int[]> combinations;

        public static int? chooseBestSum(int t, int k, List<int> ls)
        {
            list = ls;
            combination = new Stack<int>();
            combinations = new List<int[]>();

            Comb(0,k);

            return combinations.Select(x => (int?)x.Sum()).OrderBy(x => x).LastOrDefault(x => x <= t);
        }

        // Explaining how to generate combination in c++: https://stackoverflow.com/questions/12991758/creating-all-possible-k-combinations-of-n-items-in-c
        private static void Comb(int offset, int k)
        {
            if (k == 0) {
                combinations.Add(combination.ToArray());
                return;
            }
            for (var i = offset; i <= list.Count - k; ++i) {
                combination.Push(list[i]);
                Comb(i+1, k-1);
                combination.Pop();
            }
        }
    }

    public static int MaxSequence(int[] arr)
    {
        var result = 0;
        var temp = 0;

        for (var i = 0; i < arr.Length; i++)
        {
            for (var j = i; j < arr.Length; j++)
            {
                temp += arr[j];
                if (temp > result)
                {
                    result = temp;
                }
            }

            temp = 0;
        }

        return result;
    }

    public static string Rot13(string message)
    {
        var result = new StringBuilder();

        foreach (var c in message)
        {
            if (char.IsUpper(c))
            {
                // upper letters range 65 - 90
                result.Append(
                    c + 13 > 90
                        ? (char) (64 + (c + 13) % 90)
                        : (char) (c + 13));
                continue;
            }

            if (char.IsLower(c))
            {
                // upper letters range 97 - 122
                result.Append(
                    c + 13 > 122
                        ? (char)(96 + (c + 13) % 122)
                        : (char)(c + 13));
                continue;
            }

            result.Append(c);
        }

        return result.ToString();
    }

    public static ulong[] productFib(ulong prod)
    {
        var n = (ulong)1;
        var n1 = (ulong)1;
        var t = (ulong)0;

        while (true)
        {
            if (n * n1 == prod)
            {
                return new[] {n, n1, (ulong) 1};
            }
            if (n * n1 > prod)
            {
                return new[] {n, n1, (ulong) 0};
            }

            t = n1;
            n1 = n + n1;
            n = t;
        }
    }

    public static class StringPermutations
    {
        private static bool[] used;
        private static char[] source;
        private static Stack<char> v;
        private static SortedSet<string> result;

        public static IEnumerable<string> SinglePermutations(string s)
        {
            source = s.ToCharArray();
            used = new bool[source.Length];
            result = new SortedSet<string>();
            v = new Stack<char>();

            CalcPermutations(0);

            return result.ToList();
        }

        private static void CalcPermutations(int k)
        {
            if (k == source.Length)
            {
                var permutation = new char[source.Length];
                v.CopyTo(permutation, 0);

                // cause result is SortedSet -> Contains use BinarySearch O(log(n))
                if (!result.Contains(new string(permutation)))
                    result.Add(new string(permutation));

                return;
            }

            for (var i = 0; i < source.Length; i++)
            {
                if (used[i]) continue;

                used[i] = true;
                v.Push(source[i]);
                CalcPermutations(k+1);
                v.Pop();
                used[i] = false;
            }
        }
    }

    public static class Finder
    {
        private static List<int>[] adjacencyList;
        private static bool[] used;
        private static bool exitFound;
        private static int N;

        public static bool PathFinderOne(string maze)
        {
            if (maze.Length == 1) return true;

            N = maze.IndexOf("\n");

            var m = maze.Replace("\n", "");

            adjacencyList = new List<int>[N * N];
            used = new bool[N * N];
            exitFound = false;

            // fill adjacencyList
            for (var i = 0; i < m.Length; i++)
            {
                adjacencyList[i] = new List<int>();

                if (m[i] == 'W') continue;

                adjacencyList[i].AddRange(GetAdjacentVertexes(i, m, N));
            }

            DFS(0);

            return used[N*N-1];
        }

        private static void DFS(int v)
        {
            used[v] = true;

            // DFS optimization: where find exit stop executing DFS
            if (v == N * N - 1)
            {
                exitFound = true;
            }

            foreach (var u in adjacencyList[v].Where(u => !used[u]))
            {
                DFS(u);

                if (exitFound)
                {
                    return;
                }
            }
        }

        private static IEnumerable<int> GetAdjacentVertexes(int i, string maze, int N)
        {
            var result = new List<int>();
            var g = i % N;
            if (g == 0 || i == 0)
            {
                if (maze[i + 1] != 'W')
                {
                    result.Add(i + 1);
                }
            }
            else if (g == N - 1 || i == N - 1)
            {
                if (maze[i - 1] != 'W')
                {
                    result.Add(i - 1);
                }
            }
            else
            {
                if (maze[i + 1] != 'W')
                {
                    result.Add(i + 1);
                }

                if (maze[i - 1] != 'W')
                {
                    result.Add(i - 1);
                }
            }


            if (i < N)
            {
                if (maze[i + N] != 'W')
                {
                    result.Add(i + N);
                }
            }
            else if (i > N * N - 1 - N)
            {
                if (maze[i - N] != 'W')
                {
                    result.Add(i - N);
                }
            }
            else
            {
                if (maze[i + N] != 'W')
                {
                    result.Add(i + N);
                }

                if (maze[i - N] != 'W')
                {
                    result.Add(i - N);
                }
            }

            return result;
        }
    }

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