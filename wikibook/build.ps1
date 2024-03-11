# build script to put everything in the right place....

dotnet build -c Release -o $PSScriptRoot\RWTodd.WikiBook $PSScriptRoot\src\RWTodd.WikiBook.csproj
# New-ExternalHelp -Path $PSScriptRoot\Docs-en-US -OutputPath $PSScriptRoot\RWTodd.WikiBook\en-US\ -Force
