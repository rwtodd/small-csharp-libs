function Get-DiscordianDate {
    [CmdletBinding(DefaultParameterSetName = 'FormattedOutput')]
    param (
        [Parameter(Position = 0)]
        [DateTime] $Date = [DateTime]::Now,

        [Parameter()]
        [int] $Relative = 0,

        [Parameter(ParameterSetName = 'FormattedOutput')]
        [string] $Format = "",

        [Parameter(ParameterSetName = 'RawOutput', Mandatory)]
        [switch] $Raw
    )
    if ($Relative -ne 0) {
        $Date = $Date.AddDays($Relative)
    }
    Write-Verbose "Date to convert is $Date"
    $result = [RWTodd.Discordian.Date]::new($Date)
    if($PSCmdlet.ParameterSetName -eq 'FormattedOutput') {
        if ($Format.Length -eq 0) {
            $today = [DateTime]::Now
            if (($Date.DayOfYear -eq $today.DayOfYear) -and ($Date.Year -eq $today.Year)) {
                $Format = [RWTodd.Discordian.Date]::TODAY_FMT
            }
            else {
                $Format = [RWTodd.Discordian.Date]::DEFAULT_FMT
            }
        }
        Add-Member -InputObject $result -NotePropertyName "Formatted" -NotePropertyValue $result.Format($Format)
    }
    Write-Output $result
}
