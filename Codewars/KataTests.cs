using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

[TestFixture]
public class KataTest
{
    [TestCase(new[] { 1, 2 }, "1,2")]
    [TestCase(new[] { -6, -3, -2, -1, 0, 1, 3, 4, 5, 7, 8, 9, 10, 11, 14, 15, 17, 18, 19, 20 }, "-6,-3-1,3-5,7-11,14,15,17-20")]
    [TestCase(new[] { -3, -2, -1, 2, 10, 15, 16, 18, 19, 20 }, "-3--1,2,10,15,16,18-20")]
    public void Extract (int[] input, string result)
    {
        Assert.That(Kata.Extract(input), Is.EqualTo(result));
    }

    [TestCase(163, 3, new[] { 50, 55, 56, 57, 58 }, 163)]
    [TestCase(163, 3, new[] { 50}, null)]
    [TestCase(230, 3, new[] { 91, 74, 73, 85, 73, 81, 87}, 228)]
    public void chooseBestSum_FindCombinationFromListByKAndSelectOneWithTheMaxSum_MaxSum (int t, int k, IEnumerable<int> list, int? result)
    {
        Assert.That(Kata.Combinations.chooseBestSum(t,k,list.ToList()), Is.EqualTo(result));
    }

    [TestCase(new[]{-2, 1, -3, 4, -1, 2, 1, -5, 4}, 6)]
    [TestCase(new[]{0}, 0)]
    public void MaxSequence_FindMaximumSumOfContiguousSubsequenceInArray_SubsequenceOfArray (int[] input, int result)
    {
        Assert.That(Kata.MaxSequence(input), Is.EqualTo(result));
    }

    [TestCase("test", "grfg")]
    [TestCase("Test", "Grfg")]
    public void Rot13_SubstituteLetterWithTheLetter13LettersAfterIt_CipherString (string input, string result)
    {
        Assert.That(Kata.Rot13(input), Is.EqualTo(result));
    }

    [Test]
    public void productFib_FindProductOfTwoFidNumberThatEqualToInput_FirstMultiplierSecondMultiplierResult() {
        var r = new ulong[] { 55, 89, 1 };
        Assert.That(Kata.productFib(4895), Is.EquivalentTo(r));
    }

    [TestCase("a", new [] { "a" })]
    [TestCase("ab", new [] { "ab", "ba" })]
    [TestCase("aabb", new [] { "aabb", "abab", "abba", "baab", "baba", "bbaa" })]
    [TestCase("123", new [] { "123","132","213","231","312","321"})]
    public void SinglePermutations_GenerateListOfPermutationsOfString_ListOfPermutationsWithoutDuplicates(string input, IEnumerable<string> result)
    {
        Assert.AreEqual(result, Kata.StringPermutations.SinglePermutations(input).OrderBy(x => x).ToList());
    }

    [Test]
    public void PathFinder_FindIfThereAreExit_IsExitExist()
    {

        string a = ".W.\n" +
                   ".W.\n" +
                   "...",

            b = ".W.\n" +
                ".W.\n" +
                "W..",

            c = "......\n" +
                "......\n" +
                "......\n" +
                "......\n" +
                "......\n" +
                "......",

            d = "......\n" +
                "......\n" +
                "......\n" +
                "......\n" +
                ".....W\n" +
                "....W.";

        Assert.AreEqual(true, Kata.Finder.PathFinder(a));
        Assert.AreEqual(false, Kata.Finder.PathFinder(b));
        Assert.AreEqual(true, Kata.Finder.PathFinder(c));
        Assert.AreEqual(false, Kata.Finder.PathFinder(d));
    }

    private static object[] validateBattleshipGameFieldTestCases = {
        new object[]
        {
            true,
            new[,]
            {{1, 0, 0, 0, 0, 1, 1, 0, 0, 0},
            {1, 0, 1, 0, 0, 0, 0, 0, 1, 0},
            {1, 0, 1, 0, 1, 1, 1, 0, 1, 0},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
            {0, 0, 0, 0, 1, 1, 1, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
            {0, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 1, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}}
        },
        new object[]
        {
            false,
            new[,]
            {{1, 0, 0, 0, 0, 1, 1, 0, 0, 0},
            {1, 0, 1, 0, 0, 0, 0, 0, 1, 0},
            {1, 0, 1, 0, 1, 1, 1, 0, 1, 0},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
            {0, 1, 0, 0, 1, 1, 1, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
            {0, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 1, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}}
        },
    };

    [TestCaseSource(nameof(validateBattleshipGameFieldTestCases))]
    public void ValidateBattlefield_ValidateBattleshipGameField_IsValid(bool expected, int[,] board) => Assert.That(Kata.ValidateBattlefield(board), Is.EqualTo(expected));

    private static object[] validateSudokuBoardTestCases = {
        new object[]
        {
            true,
            new[]
            {
                new[] {5, 3, 4, 6, 7, 8, 9, 1, 2},
                new[] {6, 7, 2, 1, 9, 5, 3, 4, 8},
                new[] {1, 9, 8, 3, 4, 2, 5, 6, 7},
                new[] {8, 5, 9, 7, 6, 1, 4, 2, 3},
                new[] {4, 2, 6, 8, 5, 3, 7, 9, 1},
                new[] {7, 1, 3, 9, 2, 4, 8, 5, 6},
                new[] {9, 6, 1, 5, 3, 7, 2, 8, 4},
                new[] {2, 8, 7, 4, 1, 9, 6, 3, 5},
                new[] {3, 4, 5, 2, 8, 6, 1, 7, 9},
            }
        },
        new object[]
        {
            false,
            new[]
            {
                new[] {5, 3, 4, 6, 7, 8, 9, 1, 2},
                new[] {6, 7, 2, 1, 9, 5, 3, 4, 8},
                new[] {1, 9, 8, 3, 0, 2, 5, 6, 7},
                new[] {8, 5, 0, 7, 6, 1, 4, 2, 3},
                new[] {4, 2, 6, 8, 5, 3, 7, 9, 1},
                new[] {7, 0, 3, 9, 2, 4, 8, 5, 6},
                new[] {9, 6, 1, 5, 3, 7, 2, 8, 4},
                new[] {2, 8, 7, 4, 1, 9, 6, 3, 5},
                new[] {3, 0, 0, 2, 8, 6, 1, 7, 9},
            },
        },
    };

    [TestCaseSource(nameof(validateSudokuBoardTestCases))]
    public void ValidateSolution_ValidateSudokuBoard_IsValid(bool expected, int[][] board) => Assert.That(Kata.ValidateSolution(board), Is.EqualTo(expected));

    [TestCase(10, 22)]
    [TestCase(20, 57)]
    [TestCase(30, 91)]
    [TestCase(50, 175)]
    public static void DblLinear_ApplyTwiceLinearFunction_ResultElement(int input, int result)
    {
        Assert.That(Kata.DblLinear(input), Is.EqualTo(result));
    }

    [Test]
    public void FormatDuration_FormatSecondsToReadableString_FormattedString() {
        Assert.AreEqual("now",Kata.formatDuration(0));
        Assert.AreEqual("1 second",Kata.formatDuration(1));
        Assert.AreEqual("1 minute and 2 seconds",Kata.formatDuration(62));
        Assert.AreEqual("2 minutes",Kata.formatDuration(120));
        Assert.AreEqual("1 hour, 1 minute and 2 seconds",Kata.formatDuration(3662));
        Assert.AreEqual("182 days, 1 hour, 44 minutes and 40 seconds",Kata.formatDuration(15731080));
        Assert.AreEqual("4 years, 68 days, 3 hours and 4 minutes",Kata.formatDuration(132030240));
        Assert.AreEqual("6 years, 192 days, 13 hours, 3 minutes and 54 seconds",Kata.formatDuration(205851834));
        Assert.AreEqual("8 years, 12 days, 13 hours, 41 minutes and 1 second",Kata.formatDuration(253374061));
        Assert.AreEqual("7 years, 246 days, 15 hours, 32 minutes and 54 seconds",Kata.formatDuration(242062374));
        Assert.AreEqual("3 years, 85 days, 1 hour, 9 minutes and 26 seconds",Kata.formatDuration(101956166));
        Assert.AreEqual("1 year, 19 days, 18 hours, 19 minutes and 46 seconds",Kata.formatDuration(33243586));
    }

    [Test]
    public void Snail_SortArrayAsSnail_SortedArray()
    {
        int[][] inputArray =
        {
            new []{1,  2,  3,  4,  5,  6},
            new []{20, 21, 22, 23, 24, 7},
            new []{19, 32, 33, 34, 25, 8},
            new []{18, 31, 36, 35, 26, 9},
            new []{17, 30, 29, 28, 27, 10},
            new []{16, 15, 14, 13, 12, 11},
        };

        int[] result =
        {
            1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29,
            30, 31, 32, 33, 34, 35, 36
        };

        Assert.That(Kata.Snail(inputArray), Is.EqualTo(result));
    }

    [TestCase("a", new [] {"a", "b", "c"}, new []{"a"})]
    [TestCase("racer", new [] {"carer", "arcre", "carre", "racrs", "racers", "arceer", "raccer", "carrer", "cerarr"}, new []{"carer", "arcre", "carre"})]
    public void Anagrams_FindAnagramsFromList_ListOfAnagrams(string word, IEnumerable<string> listOfAnagramsCandidate, IEnumerable<string> listOfAnagrams)
    {
        Assert.That(Kata.Anagrams(word, listOfAnagramsCandidate.ToList()), Is.EquivalentTo(listOfAnagrams));
    }

    [TestCase("103 123 4444 99 2000", "2000 103 123 4444 99")]
    [TestCase("2000 10003 1234000 44444444 9999 11 11 22 123", "11 11 2000 10003 22 123 1234000 44444444 9999")]
    public void WeightSort_SortArrayElementsByTheSumOfTheirDigits_SortedArray(string input, string result) {
        Assert.That(Kata.orderWeight(input), Is.EqualTo(result));
    }

    [TestCase(0, "00:00:00")]
    [TestCase(5, "00:00:05")]
    [TestCase(60, "00:01:00")]
    [TestCase(86399, "23:59:59")]
    [TestCase(359999, "99:59:59")]
    public void GetReadableTime_ParseIntToTimeString_TimeString(int input, string result)
    {
        Assert.That(Kata.GetReadableTime(input), Is.EqualTo(result));
    }

    [TestCase(new []{1, 2, 0, 1, 0, 1, 0, 3, 0, 1}, new []{1, 2, 1, 1, 3, 1, 0, 0, 0, 0})]
    [TestCase(new []{0, 0, 0, 0}, new []{0, 0, 0, 0})]
    [TestCase(new []{1, 0, 2, 0, 3, 0}, new []{1, 2, 3, 0, 0, 0})]
    public void MoveZeroes_MoveZerosToTheEndOfArray_ArrayWithTheZerosInTheEnd(int[] input, int[] result)
    {
        Assert.That(Kata.MoveZeroes(input), Is.EqualTo(result));
    }

    [TestCase(new []{1, 2, 5, 6, 8, 17, 18, 21, 22, 107, 289, 1089}, 290)]
    [TestCase(new []{1, 2, 5, 6, 8, 17, 18, 21, 22, 107, 289, 1089}, 1090)]
    [TestCase(new []{1, 2, 5, 6, 8, 17, 18, 21, 22, 107, 289, 1089}, 0)]
    public void BinarySearch_SearchValueThatDontExistInInput_False(int[] input, int valueToSearch)
    {
        Assert.That(Kata.BinarySearch(input , valueToSearch), Is.False);
    }

    [TestCase(new []{1, 2, 5, 6, 8, 17, 18, 21, 22, 107, 289, 1089}, 1)]
    [TestCase(new []{1, 2, 5, 6, 8, 17, 18, 21, 22, 107, 289, 1089}, 1089)]
    [TestCase(new []{1, 2, 5, 6, 8, 17, 18, 21, 22, 107, 289, 1089}, 107)]
    public void BinarySearch_SearchValueThatExistInInput_True(int[] input, int valueToSearch)
    {
        Assert.That(Kata.BinarySearch(input , valueToSearch), Is.True);
    }

    [TestCase(")((((")]
    [TestCase("()()()()()))(((()()()")]
    [TestCase("((()))))")]
    public void ValidParentheses_InvalidInput_False(string input)
    {
        Assert.That(Kata.ValidParentheses(input), Is.False);
    }

    [TestCase("()")]
    [TestCase("(()()())")]
    [TestCase("((()))")]
    public void ValidParentheses_ValidInput_True(string input)
    {
        Assert.That(Kata.ValidParentheses(input), Is.True);
    }
}