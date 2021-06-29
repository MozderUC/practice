using NUnit.Framework;

[TestFixture]
public class KataTest
{
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