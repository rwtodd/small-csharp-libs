---
external help file: RWTodd.RomanizedText-help.xml
Module Name: RWTodd.RomanizedText
online version:
schema: 2.0.0
---

# ConvertFrom-RomanizedText

## SYNOPSIS
Convert romanized text in Hebrew or Greek to the equivalent unicode.

## SYNTAX

### Hebrew (Default)
```
ConvertFrom-RomanizedText [-Text] <String> [-Hebrew] [-Html] [-WikiText] [-ProgressAction <ActionPreference>]
 [<CommonParameters>]
```

### Greek
```
ConvertFrom-RomanizedText [-Text] <String> [-Greek] [-Html] [-ProgressAction <ActionPreference>]
 [<CommonParameters>]
```

## DESCRIPTION
It used to be common to encode non-roman letters into roman letters for ease of printing and understanding.
As a result a lot of text contains romanized versions of Hebrew and Greek (amond others).  This cmdlet
converts specific flavors of romanized Hebrew and Greek to unicode.  See the documentation on the 
`-Greek` and `-Hebrew` parameters for details on the dialect supported.

## EXAMPLES

### Example 1
```powershell
PS C:\> ConvertFrom-RomanizedText -Greek THELEMA
τηελεμα
```

You put romanized text in, you get unicode text out.

### Example 2
```powershell
PS C:\> ConvertFrom-RomanizedText -Greek -Html THELEMA
&#x03c4;&#x03b7;&#x03b5;&#x03bb;&#x03b5;&#x03bc;&#x03b1;
```

You can request the results in HTML entities if you like... this is especially conventient for
right-to-left Hebrew, since the entities are still written left-to-right.

## PARAMETERS

### -Greek
Specify that the romanized text is in Greek.

Here is the supported romanization:

Letters:
```
    *A/A  alpha         *B/B  beta
    *C/C  xi            *D/D  delta
    *E/E  epsilon       *F/F  phi
    *G/G  gamma         *H/H  eta
    *I/I  iota          *K/K  kappa
    *L/L  lambda        *M/M  mu
    *N/N  nu            *O/O  omicron
    *P/P  pi            *Q/Q  theta
    *R/R  rho           *S/S  sigma
    S1    medial sigma  S2  final sigma
    *S3/S3 lunate sigma *T/T  tau
    *U/U  upsilon       *V/V  digamma
    *W/W  omega         *X/X  Chi
    *Y/Y  Psi           *Z/Z  Zeta
```

Accents:
```
    )  smooth breathing (  rough breathing
    /  acute            =  circumflex
    \\  grave            +  diaeresis
    |  iota subscript   &  macron
    '  breve            ?  dot below
```

```yaml
Type: SwitchParameter
Parameter Sets: Greek
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Hebrew
Specify that the romanized text is in Hebrew.  This is the default if no option is given.

Here is the supported romanization:

Letters:
```
    A  = aleph   B  = beth    G  = gimel    D  = dalet
    H  = heh     V  = vav     Z  = zayin    Ch = chet
    T  = teth    I  = yod     K  = kaf      L  = lamed
    M  = mem     N  = nun     S  = samekh   O  = ayin
    P  = peh     Tz = tzaddi  Q  = qoph     R  = resh
    Sh = shin    Th = tav
```

Ligatures:
```
    Ii = yod-yod   Vi = vav-yod     Vv = vav-vav
```

Niqqud:
```
    ;  = Sh'va                *  = Dagesh
    \\ =  Kubutz              `  = Holam
    1  = Hiriq                2  = Zeire                
    3  = Segol                ;3 = Reduced Segol        
    _  = Patach               ;_ = Reduced Patach       
    7  = Kamatz               ;7 = Reduced Kamatz       
    Shl = Shin dot left       Shr = Shin dot right
```

```yaml
Type: SwitchParameter
Parameter Sets: Hebrew
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Html
Request output as HTML entities, rather than unicode characters.

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

### -Text
The text to convert to unicode.  See the `-Greek` and `-Hebrew` parameters for more on the style of
romanization supported.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -WikiText
For Hebrew, my mediawiki has a template that specifies a font family and size for Hebrew letters. This option
wraps the output in that mediawiki template. 

```yaml
Type: SwitchParameter
Parameter Sets: Hebrew
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

### System.String

The romanized text you want to convert.

## OUTPUTS

### System.Object

The unicode text.

## NOTES

## RELATED LINKS
