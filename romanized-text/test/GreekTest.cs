namespace RWTodd.Romanized.Test;

public class TestGreek
{
    [Theory]
    [InlineData("A*A", "\u03b1\u0391")]
    [InlineData("B*B", "\u03b2\u0392")]
    [InlineData("G*G", "\u03b3\u0393")]
    [InlineData("ABG", "αβγ")]
    public void TestAbg(string input, string expectedOutput)
    {
        string result = Greek.Convert(input); // Call your C# greek function
        Assert.Equal(expectedOutput, result);
    }

    [Theory]
    [InlineData("BAS SAS", "βας σας")]
    [InlineData("BAS *S*A*S", "βας ΣΑΣ")]
    public void TestAutoFinals(string input, string expectedOutput)
    {
        string result = Greek.Convert(input);
        Assert.Equal(expectedOutput, result);
    }

    [Theory]
    [InlineData("*)B?A", "\u0392\u0313\u0323\u03b1")]
    public void TestAccents(string input, string expectedOutput)
    {
        string result = Greek.Convert(input);
        Assert.Equal(expectedOutput, result);
    }
}