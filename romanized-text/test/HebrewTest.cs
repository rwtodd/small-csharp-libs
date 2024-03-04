namespace RWTodd.Romanized.Test;

public class HebrewTests
{
    [Theory]
    [InlineData("A", "\u05d0")]
    [InlineData("B", "\u05d1")]
    [InlineData("G", "\u05d2")]
    [InlineData("ABG", "\u05d0\u05d1\u05d2")]
    public void TestABG(string input, string expected)
    {
        string result = Hebrew.Convert(input);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("AN BNM", "אן בנם")]
    [InlineData("ANf BNiMf", "אן בנם")]
    public void TestAutoFinals(string input, string expected)
    {
        string result = Hebrew.Convert(input);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("B3AM", "\u05d1\u05b6\u05d0\u05dd")]
    [InlineData("B3*AMi*;3", "\u05d1\u05b6\u05bc\u05d0\u05de\u05bc\u05b1")]
    public void TestNiqqud(string input, string expected)
    {
        string result = Hebrew.Convert(input);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("KThR", "כתר")]
    [InlineData("ChKMH", "חכמה")]
    [InlineData("BINH", "בינה")]
    [InlineData("DOTh", "דעת")]
    [InlineData("ChSD", "חסד")]
    [InlineData("GBVRH", "גבורה")]
    [InlineData("ThPARTh", "תפארת")]
    [InlineData("NTzCh", "נצח")]
    [InlineData("HVD", "הוד")]
    [InlineData("ISVD", "יסוד")]
    [InlineData("MLKVTh", "מלכות")]
    public void TestSefirot(string input, string expected)
    {
        string result = Hebrew.Convert(input);
        Assert.Equal(expected, result);
    }
}