function New-WikiBook {
    [CmdletBinding()]
    param (
        [Parameter(Mandatory,ParameterSetName="Raw")]
        [string]$URL,        
        [Parameter(Mandatory,ParameterSetName="Raw")]
        [string]$Title,
        [Parameter(ParameterSetName="Raw")]
        [string]$Author,
        [Parameter(ParameterSetName="Raw")]
        [string]$NavTitle,
        [Parameter(ParameterSetName="Raw")]
        [string]$ShortTitle,
        [Parameter(ParameterSetName="Raw")]
        [string]$NavPage,
        [Parameter(ParameterSetName="Raw")]
        [string]$Date,
        [Parameter(Mandatory, ParameterSetName="XML")]
        [string]$FromXML
    )
    if($PSCmdlet.ParameterSetName -eq 'Raw') {
        $wb = [RWTodd.WikiBook.Book]::New($URL)
        $wb.Title = $Title
        if ($Author) { $wb.Author = $Author }
        if ($Date) { $wb.Date = $Date }
        if ($NavTitle) { $wb.NavTitle = $NavTitle }
        if ($ShortTile) { $wb.ShortTitle = $ShortTile }
        if ($NavPage) { $wb.NavPage = $NavPage }
        $wb | Add-Member -MemberType ScriptMethod -Name "NewPage" -Value { 
            param($pgUrl)
            [RWTodd.WikiBook.MutablePage]::new($this, $pgUrl)
        }
        return $wb
    } else {
        Write-Error "TODO... Load from XML"
        $null
    }
}

function New-WikiBookTemplate {
    get-content $PSScriptRoot\example_toc.xml
}
