namespace RWTodd.Romanized.Test;

public class HebrewTests
{
    [Fact]
    public void TestABG()
    {
        Assert.Equal("\u05d0", Hebrew.Convert("A"));
        Assert.Equal("\u05d1",Hebrew.Convert("B"));
        Assert.Equal("\u05d2",Hebrew.Convert("G"));
        Assert.Equal("אבג",Hebrew.Convert("ABG"));
    }

    [Fact]
    public void TestAutoFinals()
    {
        Assert.Equal("אן בנם", Hebrew.Convert("AN BNM"));
        Assert.Equal("אן בנם", Hebrew.Convert("ANf BNiMf"));
    }

    [Fact]
    public void TestNiqqud()
    {
        Assert.Equal("\u05d1\u05b6\u05d0\u05dd", Hebrew.Convert("B3AM"));
        Assert.Equal("\u05d1\u05b6\u05bc\u05d0\u05de\u05bc\u05b1", Hebrew.Convert("B3*AMi*;3"));
    }

    [Fact]
    public void TestSefirot()
    {        
        Assert.Equal("כתר",Hebrew.Convert("KThR"));
        Assert.Equal("חכמה",Hebrew.Convert("ChKMH"));
        Assert.Equal("בינה",Hebrew.Convert("BINH"));
        Assert.Equal("דעת",Hebrew.Convert("DOTh"));
        Assert.Equal("חסד",Hebrew.Convert("ChSD"));
        Assert.Equal("גבורה",Hebrew.Convert("GBVRH"));
        Assert.Equal("תפארת",Hebrew.Convert("ThPARTh"));
        Assert.Equal("נצח",Hebrew.Convert("NTzCh"));
        Assert.Equal("הוד",Hebrew.Convert("HVD"));
        Assert.Equal("יסוד",Hebrew.Convert("ISVD"));
        Assert.Equal("מלכות",Hebrew.Convert("MLKVTh"));
    }
}