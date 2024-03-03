using System;

namespace RWTodd.Discordian;

public sealed class Date
{
    public static readonly string DEFAULT_FMT = "%{%A, %B %d%}, %Y YOLD";
    public static readonly string TODAY_FMT = "Today is %{%A, the %e day of %B%} in the YOLD %Y%N%nCelebrate %H";

    /// <summary>
    /// The Year of Our Lady Discord.
    /// </summary>
    public int Year { get; }
    /// <summary>
    /// How many days are left until the X-Day.
    /// </summary>
    public int DaysTilXDay { get; }
    /// <summary>
    /// Is this St. Tib's Day?
    /// </summary>
    public bool IsTibs { get; }
    /// <summary>
    /// The full name of the current Season
    /// </summary>
    public string Season { get; }
    /// <summary>
    /// Give the abbreviated name of the current Season.
    /// </summary>
    public string SeasonAbbrev { get; }
    /// <summary>
    /// The full name fo the current day of the week.
    /// </summary>
    public string Weekday { get; }
    /// <summary>
    /// The abbreviated name of the current day of the week.
    /// </summary>
    public string WeekdayAbbrev { get; }
    /// <summary>
    /// Gives the day (1-73) of the current Season.
    /// </summary>
    public int DayOfSeason { get; }
    /// <summary>t
    /// The name of the Holy Day, if it is one (Days 5 and 50).
    /// </summary>
    public string HolyDay { get; }
    /// <summary>
    /// Is this a Holy Day?
    /// </summary>
    public bool IsHolyDay { get; }

    /// <summary>
    /// Generates the Discordian Date for the given <c>DateTime</c>.
    /// </summary>
    /// <param name="dt">The Date and Time to convert to Discordian</param>
    public Date(DateTime dt)
    {
        Year = dt.Year + 1166;
        DaysTilXDay = (int)Math.Ceiling(X_DAY.Subtract(dt).TotalDays);
        IsTibs = (dt.Month == 2) && (dt.Day == 29);

        var adjustedYDay = dt.DayOfYear - 1;
        if (DateTime.IsLeapYear(dt.Year) && (dt.Month > 2))
        {
            --adjustedYDay;
        }
        var seasonNbr = adjustedYDay / 73;
        Season = IsTibs ? TIBS : SEASONS[2 * seasonNbr];
        SeasonAbbrev = IsTibs ? TIBS : SEASONS[2 * seasonNbr + 1];
        var wkdayNbrIdx = 2 * (adjustedYDay % 5);
        Weekday = IsTibs ? TIBS : WEEKDAYS[wkdayNbrIdx];
        WeekdayAbbrev = IsTibs ? TIBS : WEEKDAYS[wkdayNbrIdx + 1];

        DayOfSeason = (adjustedYDay % 73) + 1;
        HolyDay = DayOfSeason switch
        {
            5 => HOLIDAY_5[seasonNbr],
            50 => HOLIDAY_50[seasonNbr],
            _ => ""
        };
        IsHolyDay = HolyDay.Length > 0;
    }

    /// <summary>
    /// Generates the Discordian Date for the current date.
    /// </summary>
    public Date() : this(DateTime.Now) { }

    /// <summary>
    /// Generates a string for the date according to the given format string. The accepted format
    /// strings correspond very closely with those accepted by the Linux 'ddate' command.
    /// </summary>
    /// <remarks>
    /// <list type="bullet">
    /// <item><description><c>%A/%a</c> = the weekday/the abbreviated weekday</description></item>
    /// <item><description><c>%B/%b</c> = the season/the abbreviated season</description></item>
    /// <item><description><c>%d/%e</c> = the day of season/the ordinal day of season</description></item>
    /// <item><description><c>%H</c> = the name of the Holy Day</description></item>
    /// <item><description><c>%X</c> = days until X-Day</description></item>
    /// <item><description><c>%Y</c> = the year</description></item>
    /// <item><description><c>%N</c> = skip the rest unless it's a holy day</description></item>
    /// <item><description><c>%{ ... %}</c> = if it's Tibs, announce that and skip the "...". Otherwise, format whatever is in the "..." part.</description></item>
    /// <item><description><c>%%, %n, %t</c> = a literal '%', newline, or tab</description></item>
    /// <item><description><c>%.</c> = give a random exclamation</description></item>
    /// </list>
    /// </remarks>
    /// <param name="fstr">The format string.</param>
    /// <returns>The generated string.</returns>
    public string Format(string fstr)
    {
        int last = fstr.Length;
        System.Text.StringBuilder buff = new(last * 2);
        int idx = 0;

        while (idx < last)
        {
            if (fstr[idx] == '%')
            {
                idx++;
                switch (fstr[idx])
                {
                    case '%': buff.Append('%'); break;
                    case 'A': buff.Append(Weekday); break;
                    case 'a': buff.Append(WeekdayAbbrev); break;
                    case 'B': buff.Append(Season); break;
                    case 'b': buff.Append(SeasonAbbrev); break;
                    case 'd': buff.Append(DayOfSeason); break;
                    case 'e':
                        {
                            string ordinal;
                            if ((DayOfSeason < 10) || (DayOfSeason > 20))
                            {
                                ordinal = (DayOfSeason % 10) switch
                                {
                                    1 => "st",
                                    2 => "nd",
                                    3 => "rd",
                                    _ => "th"
                                };
                            }
                            else { ordinal = "th"; }
                            buff.Append(DayOfSeason)
                                    .Append(ordinal); break;
                        }
                    case 'H': buff.Append(HolyDay); break;
                    case 'n': buff.Append('\n'); break;
                    case 't': buff.Append('\t'); break;
                    case 'X':
                        buff.Append(DaysTilXDay.ToString("N0", System.Globalization.CultureInfo.CurrentCulture));
                        break;
                    case 'Y': buff.Append(Year); break;
                    case '.': buff.Append(EXCLAIM[new Random().Next(EXCLAIM.Length)]); break;
                    case '}': /* nothing */ break;
                    case 'N':
                        if (!IsHolyDay)
                        {
                            idx = last;
                        }
                        break;
                    case '{':
                        if (IsTibs)
                        {
                            buff.Append(TIBS);
                            idx = fstr.IndexOf("%}", idx) + 1;
                            if (idx == 0)
                            {
                                idx = last;
                            }
                        }
                        break;
                    default: buff.Append(fstr[idx]); break;
                }
            }
            else
            {
                buff.Append(fstr[idx]);
            }
            idx++;
        }

        return buff.ToString();
    }

    private readonly static DateTime X_DAY = new(8661, 7, 5);
    private readonly static string TIBS = "St. Tib's Day";
    private readonly static string[] SEASONS = {
        "Chaos", "Chs", "Discord", "Dsc", "Confusion", "Cfn",
        "Bureaucracy", "Bcy", "The Aftermath", "Afm" };
    private readonly static string[] WEEKDAYS = {
        "Sweetmorn", "SM", "Boomtime", "BT",
        "Pungenday", "PD", "Prickle-Prickle", "PP",
        "Setting Orange", "SO" };
    private readonly static string[] HOLIDAY_5 = {
        "Mungday", "Mojoday", "Syaday", "Zaraday", "Maladay" };
    private readonly static string[] HOLIDAY_50 = {
        "Chaoflux", "Discoflux", "Confuflux", "Bureflux", "Afflux" };
    private readonly static string[] EXCLAIM = {
        "Hail Eris!", "All Hail Discordia!", "Kallisti!", "Fnord.", "Or not.",
        "Wibble.", "Pzat!", "P'tang!", "Frink!", "Slack!", "Praise \"Bob\"!", "Or kill me.",
        "Grudnuk demand sustenance!", "Keep the Lasagna flying!",
        "You are what you see.", "Or is it?", "This statement is false.",
        "Lies and slander, sire!", "Hee hee hee!", "Hail Eris, Hack Powershell!"
    };
}
