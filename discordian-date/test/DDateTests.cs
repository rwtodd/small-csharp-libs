namespace RWTodd.DiscDate.Test;

public class DiscordianDateTests
{
    [Fact]
    public void OneDateTest()
    {
        Date dd = new(new DateTime(2020, 2, 19));
        Assert.Equal(3186, dd.Year);
        Assert.Equal("Chaoflux", dd.HolyDay);
        Assert.Equal("Chaos", dd.Season);
        Assert.Equal("Chs", dd.SeasonAbbrev);
        Assert.Equal(50, dd.DayOfSeason);
    }

    [Fact]
    public void DaysTilXTest()
    {
        Assert.Equal(154, new Date(new DateTime(8661, 2, 1)).DaysTilXDay);
        Assert.Equal(4, new Date(new DateTime(8661, 7, 1)).DaysTilXDay);
        Assert.Equal(0, new Date(new DateTime(8661, 7, 5)).DaysTilXDay);
        Assert.Equal(-1, new Date(new DateTime(8661, 7, 6)).DaysTilXDay);
        Assert.Equal(1953, new Date(new DateTime(8656, 2, 29)).DaysTilXDay);
    }
}