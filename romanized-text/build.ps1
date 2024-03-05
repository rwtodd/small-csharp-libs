# build script to put everything in the right place....

dotnet build -c Release -o $PSScriptRoot\RWTodd.RomanizedText $PSScriptRoot\src\RWTodd.RomanizedText.csproj
New-ExternalHelp -Path $PSScriptRoot\Docs-en-US -OutputPath $PSScriptRoot\RWTodd.RomanizedText\en-US\ -Force
