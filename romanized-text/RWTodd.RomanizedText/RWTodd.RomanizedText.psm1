function ConvertFrom-RomanizedText {
    [CmdletBinding(DefaultParameterSetName="Hebrew")]
    param(
        [Parameter(Mandatory, ValueFromPipeline, Position = 0)]
        [string]$Text,

        [Parameter(ParameterSetName='Hebrew')]
        [Switch]$Hebrew,

        [Parameter(ParameterSetName='Greek', Mandatory)]
        [Switch]$Greek,

        [Switch]$Html,

        [Parameter(ParameterSetName='Hebrew')]
        [Switch]$WikiText
    )
    process {
        switch ($PSCmdlet.ParameterSetName) {
            'Hebrew' {  $result = [RWTodd.RomanizedText.Hebrew]::Convert($Text) }
            'Greek' {  $result = [RWTodd.RomanizedText.Greek]::Convert($Text) }
            Default {
                write-error "Bad Parameterset!!!"
                exit 1
            }
        }       
        if ($Html) {
            $result = ($result.ToCharArray() | ForEach-Object { "&#x{0:x4};" -f [int]$_ }) -join '' 
        }
        if ($WikiText) {
            $result = "{{Hebrew Text|$result}}"
        }
        Write-Output $result
    }
}