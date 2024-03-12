---
external help file: RWTodd.WikiBook-help.xml
Module Name: RWTodd.WikiBook
online version:
schema: 2.0.0
---

# Build-WikiBookPageFiles

## SYNOPSIS
Generate page file templates from a `RWTodd.WikiBook.Book` object.

## SYNTAX

```
Build-WikiBookPageFiles [-Book] <Book> [-Force] [-WhatIf] [-Confirm]
 [<CommonParameters>]
```

## DESCRIPTION
Generates files in the current directory. It skips files that already exist, so as not to overwrite any
work that has been done.  The `-Force` flag will force the writes if that is desirable.

## EXAMPLES

### Example 1
```powershell
PS C:\> New-WikiBook -FromXML toc.xml | Build-WikiBookPageFiles
```

Pipe a parsed `Book` object from New-WikiBook straight to Build-WikiBookPageFiles.

## PARAMETERS

### -Book
The Book object that has all the page definitions already loaded.

```yaml
Type: Book
Parameter Sets: (All)
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Confirm
Prompts you for confirmation before running the cmdlet.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: cf

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Force
Use this flag to force the cmdlet to overwrite any existing files.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WhatIf
Shows what would happen if the cmdlet runs.
The cmdlet is not run.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: wi

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### RWTodd.WikiBook.Book
## OUTPUTS

### System.Object
## NOTES

## RELATED LINKS
