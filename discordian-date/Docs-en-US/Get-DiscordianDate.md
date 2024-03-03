---
external help file: RWTodd.DiscordianDate-help.xml
Module Name: RWTodd.DiscordianDate
online version:
schema: 2.0.0
---

# Get-DiscordianDate

## SYNOPSIS
Converts dates to the Discordian calendar.

## SYNTAX

### FormattedOutput (Default)
```
Get-DiscordianDate [[-Date] <DateTime>] [-Relative <Int32>] [-Format <String>]
 [-ProgressAction <ActionPreference>] [<CommonParameters>]
```

### RawOutput
```
Get-DiscordianDate [[-Date] <DateTime>] [-Relative <Int32>] [-Raw] [-ProgressAction <ActionPreference>]
 [<CommonParameters>]
```

## DESCRIPTION
The command supports all options present in the UNIX `ddate` utility.  With no arguments, it formats the current date in a default, friendly format.  Custom format strings can be provided with the `-Format` parameter. Though the default display is the formatted string, this utility actually returns a Powershell object with properties for various aspects of the Discordian date. I'd like to see the venerable `ddate` utility do that!

## EXAMPLES

### Example 1
```powershell
PS C:\> Get-DiscordianDate # assuming it is 2020-2-21

Today is Boomtime, the 52nd day of Chaos in the YOLD 3186
```

The current date is converted to a discordian date and described.

### Example 2
```powershell
PS C:\> Get-DiscordianDate 2020-2-19 -Relative 2

Boomtime, Chaos 52, 3186 YOLD
```

The given date is adjusted by 2 days and converted to a discordian date and described.

### Example 3
```powershell
PS C:\> 11:52 AM$ Get-DiscordianDate 2020-2-19 -Format "It is %H and %X til the big day!" 

It is Chaoflux and 2,425,712 til the big day!
```

A custom format is given.

### Example 4
```powershell
PS C:\> 11:52 AM$ Get-DiscordianDate 2020-2-19 -Raw

3186-Chs-50
```

No formatting is done at all.  A default display is provided.

### Example 5
```powershell
PS C:\> 11:52 AM$ Get-DiscordianDate 2020-2-19 | Format-List

Formatted     : Setting Orange, Chaos 50, 3186 YOLD
Year          : 3186
DaysTilXDay   : 2425712
IsTibs        : False
Season        : Chaos
SeasonAbbrev  : Chs
Weekday       : Setting Orange
WeekdayAbbrev : SO
DayOfSeason   : 50
HolyDay       : Chaoflux
IsHolyDay     : True
```

The returned object has many useful properties.

## PARAMETERS

### -Date
The Gregorian-calendar date to convert to Discordian.

```yaml
Type: DateTime
Parameter Sets: (All)
Aliases:

Required: False
Position: 0
Default value: Today's Date
Accept pipeline input: False
Accept wildcard characters: False
```

### -Format
The format string. When a format is not given, a default one is provided by the tool.  The flags allowed in the format string are the following:

%% Outputs a literal percent sign.

%n Outputs a newline.

%t Outputs a tab.

%A Outputs the Discordian weekday.

%a Outputs an abbreviation for the Discordian weekday.

%B Outputs the Discordian Season.

%b Outputs an abbreviation for the Discordian Season.

%d Outputs the day of the season.

%e Outputs the ordinal-number version of the day (e.g., `3rd` `24th`)

%H Outputs the name of the current Holy Day, if any.

%X Outputs the number of days left until X-Day

%Y Outputs the Discordian year.

%. Outputs a random exclamation.

%N Removes the rest of the format string, unless it is a Holy Day.

%{ ... %}  Outputs "St. Tib's Day" on St. Tib's Day, and removes everything between the brackets. Otherwise, formats everything between the brackets.

```yaml
Type: String
Parameter Sets: FormattedOutput
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Raw
This switch indicates that a formatted string should not be added as a NoteProperty to the returned object.

```yaml
Type: SwitchParameter
Parameter Sets: RawOutput
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Relative
Adjust the date by the given number of days (can be positive or negative).

This is a useful shorthand when you want to see 'tomorrow' or 'two days ago'.

```yaml
Type: Int32
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### RWTodd.Discordian.Date

An object with properties for various facets of the discordian date.

## NOTES

## RELATED LINKS

[Wikipedia Entry for Discordian Calendar](https://en.wikipedia.org/wiki/Discordian_calendar)