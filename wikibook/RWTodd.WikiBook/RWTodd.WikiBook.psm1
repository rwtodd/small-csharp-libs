function New-WikiBook {
    [CmdletBinding()]
    [OutputType([RWTodd.WikiBook.Book])]
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
            [RWTodd.WikiBook.Page]::new($this, $pgUrl)
        }
        return $wb
    } else {
        if (-not (Test-Path $FromXML)) {
            Write-Error "Must have an XML file to parse!"
            return $null
        }
        [xml]$x = get-content -raw $FromXML
        if ($x.DocumentElement.Name -ne "book") {
            Write-Error "XML isn't a <book>!"
            return $null
        }
        [System.Xml.XmlElement]$root = $x.DocumentElement
        $tocCat = $root.GetAttribute('tocCategory')
        $booktitle = $root['title']
        $bookurl = $booktitle.GetAttribute('url') ? $booktitle.GetAttribute('url') : $booktitle.InnerText
        Write-Verbose "Book URL is: <$bookurl> in category <$tocCat>"
        $wb = [RWTodd.WikiBook.Book]::New($bookurl,$tocCat)

        $wb.Title = $bookTitle.InnerText
        $tmp = $booktitle.GetAttribute('short') 
        if($tmp) { $wb.ShortTitle = $tmp }
        $tmp = $booktitle.GetAttribute('navtitle')
        if($tmp) { $wb.NavTitle = $tmp }

        $tmp = $root['date'].InnerText
        if($tmp) { $wb.Date = $tmp }
        
        $tmp = $root['author'].InnerText
        if($tmp) { $wb.Author = $tmp }
        
        $chaps = $root['chapters']
        $tmp = $chaps.GetAttribute('navtemplate')
        if($tmp) { $wb.NavTemplate = $tmp }

        foreach($chap in $chaps.ChildNodes) {
            if($chap.Name -eq 'raw') {
                Write-Verbose "Adding raw text <$($chap.InnerText)>"
                $wb.AddRawText($chap.InnerText)
            } elseif ($chap.Name -eq 'c') {
                $pgUrl = $chap.GetAttribute('url')
                $pgShort = $chap.GetAttribute('short')
                $pgText = $chap.InnerText
                $pgListMarks = $null
                # get any list marks from the front of the text
                if($pgText -match '^\s*[*#]*\s*') {
                    $pgListMarks = $matches[0]
                    $pgText = $pgText.Substring($pgListMarks.Length)
                }
                # if there is no short, scan the Text for [bracketed] material
                if($pgShort -eq '') {
                    if($pgText -match '\[(.*?)\]') {
                        $pgShort = $matches[1]
                        $pgText = $pgText -replace '[][]', ''
                    }
                }
                # if there is no url, it must be the remaining pgText...
                if($pgUrl -eq '') { $pgUrl = $pgText }
                # add the book's shorttitle as a suffix
                $pgUrl = "{0} ({1})" -f $pgUrl,$wb.ShortTitle
                
                Write-Verbose "Adding page at URL <$pgUrl>"
                $pg = [RWTodd.WikiBook.Page]::new($wb, $pgUrl)
                if($pgShort) { $pg.ShortName = $pgShort }
                if($pgText) { $pg.DisplayName = $pgText }
                if($pgListMarks) { $pg.TOCListMarkers = $pgListMarks }

                $wb.AddPage($pg)
            } elseif($chap.Name -eq '#comment') {
                # skip!
            } else {
                Write-Error "Unknown chapter node $($chap.Name)! Skipping..."
            }
        }
        $wb
    }
}

function New-WikiBookTemplate {
    [OutputType([string])]
    param()
    get-content $PSScriptRoot\example_toc.xml
}

function Build-WikiBookPageFiles {
    [CmdletBinding(SupportsShouldProcess)]
    param(
        [Parameter(Mandatory,Position=0,ValueFromPipeline)]
        [RWTodd.WikiBook.Book]$Book,
        [switch]$Force
    )
    # first put out the TOC
    $fname = $Book.TOC.Filename
    if((Test-Path $fname) -and (-not $Force)) {
        Write-Verbose "Skipping $fname... it's already there."
    } else {
        if($PSCmdlet.ShouldProcess($fname, "Generating WikiBook toc file")) {
            $Book.TOC.GeneratePageTemplate() > $fname
        }
    }

    foreach($pg in $Book.GetPages()) {
        $fname = $pg.Filename
        if((Test-Path $fname) -and (-not $Force)) {
            Write-Verbose "Skipping $fname... it's already there."
        } else {
            if($PSCmdlet.ShouldProcess($fname, "Generating WikiBook page file")) {
                $pg.GeneratePageTemplate() > $fname
            }
        }    
    }
}
