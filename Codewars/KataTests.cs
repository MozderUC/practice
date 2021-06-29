﻿using NUnit.Framework;

[TestFixture]
public class KataTest
{
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