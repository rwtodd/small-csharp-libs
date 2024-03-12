---
external help file: RWTodd.WikiBook-help.xml
Module Name: RWTodd.WikiBook
online version:
schema: 2.0.0
---

# New-WikiBook

## SYNOPSIS
Create a `RWTodd.WikiBook.Book` object, for further manipulation.

## SYNTAX

### Raw
```
New-WikiBook -URL <String> -Title <String> -TocCategory <String> [-Author <String>] [-NavTitle <String>]
 [-ShortTitle <String>] [-NavTemplate <String>] [-Date <String>] [<CommonParameters>]
```

### XML
```
New-WikiBook -FromXML <String> [-ProgressAction <ActionPreference>] [<CommonParameters>]
```

## DESCRIPTION
The object can be made either from parameters and filled with pages programmatically (the "Raw" syntax), or the whole
book can be read from an XML file (the "XML" syntax).  See `New-WikiBookTemplate` for an easy way to get started
with the XML format.

## EXAMPLES

### Example 1
```powershell
PS C:\> $wb = New-WikiBook -FromXML template.xml
```

Generate a Book object and store it in `$wb` for processing.

### Example 2
```powershell
PS C:\> New-WikiBook -FromXML template.xml | Build-WikiBookPageFiles
```

Generate a Book object and pass it directly to `Build-WikiBookPageFiles` for processing.


### Example 3
```powershell
PS C:\> $wb = New-WikiBook -Url "My_Favorite_Book_(Author)" -Title "My Favorite Book" -TocCategory "Novels"
```

Generate a Book object from parameter components.  Pages can be added to `$wb` by calling methods on it.

## PARAMETERS

### -Author
Provide the name of the Book's author.

```yaml
Type: String
Parameter Sets: Raw
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Date
Provide the Book's date of publication.

```yaml
Type: String
Parameter Sets: Raw
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -FromXML
Provide an XML file, from which metadata and pages will be read.  See `New-WikiBookTemplate` for an easy way to generate the XML.

```yaml
Type: String
Parameter Sets: XML
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -NavTemplate
Give the name of the Nav template page to use, if not using the default one.

```yaml
Type: String
Parameter Sets: Raw
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -NavTitle
Give the way you want the title of the book presented in the Nav pane.

```yaml
Type: String
Parameter Sets: Raw
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ShortTitle
Give a short version of the title, which is often appended to every page URL.

```yaml
Type: String
Parameter Sets: Raw
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Title
Give a full title of the book, for use on the TOC page.

```yaml
Type: String
Parameter Sets: Raw
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TocCategory
Tell which mediawiki category the TOC should go into.

```yaml
Type: String
Parameter Sets: Raw
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -URL
Give the mediawiki page url for the TOC of the book.  This will also be the basis for the book pages' category.

```yaml
Type: String
Parameter Sets: Raw
Aliases:

Required: True
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

### RWTodd.WikiBook.Book
## NOTES

## RELATED LINKS
