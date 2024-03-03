# build script to put everything in the right place....

dotnet build -c Release -o $PSScriptRoot\RWTodd.DiscordianDate $PSScriptRoot\src\RWTodd.DiscordianDate.csproj
New-ExternalHelp -Path $PSScriptRoot\Docs-en-US -OutputPath $PSScriptRoot\RWTodd.DiscordianDate\en-US\ -Force
