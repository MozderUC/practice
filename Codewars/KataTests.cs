using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

[TestFixture]
public class KataTest
{
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