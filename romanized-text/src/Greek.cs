using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System;
namespace RWTodd.RomanizedText;

public static partial class Greek
{
    private static readonly Dictionary<string, string> GRKCHR = new() {
        { "*A","\u0391" }, { "A","\u03b1" }, // alpha
        { "*B","\u0392" }, { "B","\u03b2" }, // beta
        { "*C","\u039e" }, { "C","\u03be" }, // xi
        { "*D","\u0394" }, { "D","\u03b4" }, // delta
        { "*E","\u0395" }, { "E","\u03b5" }, // epsilon
        { "*F","\u03a6" }, { "F","\u03c6" }, // phi
        { "*G","\u0393" }, { "G","\u03b3" }, // gamma
        { "*H","\u0397" }, { "H","\u03b7" }, // eta
        { "*I","\u0399" }, { "I","\u03b9" }, // iota
        { "*K","\u039a" }, { "K","\u03ba" }, // kappa
        { "*L","\u039b" }, { "L","\u03bb" }, // lambda
        { "*M","\u039c" }, { "M","\u03bc" }, // mu
        { "*N","\u039d" }, { "N","\u03bd" }, // nu
        { "*O","\u039f" }, { "O","\u03bf" }, // omicron
        { "*P","\u03a0" }, { "P","\u03c0" }, // pi
        { "*Q","\u0398" }, { "Q","\u03b8" }, // theta
        { "*R","\u03a1" }, { "R","\u03c1" }, // rho
        { "*S","\u03a3" }, { "S","\u03c3" }, // sigma, medial sigma
        { "*S1","\u03a3" }, { "S1","\u03c3" }, // sigma, medial sigma
        { "*S2","\u03a3" }, { "S2","\u03c2" }, // sigma, final sigma
        { "*S3","\u03f9" }, { "S3","\u03f2" }, // lunate sigma
        { "*T","\u03a4" }, { "T","\u03c4" }, // tau
        { "*U","\u03a5" }, { "U","\u03c5" }, // upsilon
        { "*V","\u03dc" }, { "V","\u03dd" }, // digamma
        { "*W","\u03a9" }, { "W","\u03c9" }, // omega
        { "*X","\u03a7" }, { "X","\u03c7" }, // Chi
        { "*Y","\u03a8" }, { "Y","\u03c8" }, // Psi
        { "*Z","\u0396" }, { "Z","\u03b6" }, // Zeta
    };
    private static readonly Dictionary<char, string> GRKACC = new() {
        // accents
        { ')',"\u0313" }, // smooth breathing
        { '(',"\u0314" }, // rough breathing
        { '/',"\u0301" }, // acute
        { '=',"\u0342" }, // circumflex (maybe use 302???)
        { '\\',"\u0300" }, // grave
        { '+',"\u0308" }, // diaeresis
        { '|',"\u0345" }, // iota subscript
        { '&',"\u0304" }, // macron
        { '\'',"\u0306" }, // breve
        { '?',"\u0323" }, // dot below
    };

    [GeneratedRegex(@"
        (?>S)                # a sigma
        (?=(?>[()/=\\+|&'?])*  # lookahead to a possible series of accents 
            (?:\W|\Z))         # ...and then a non-letter",
        RegexOptions.IgnorePatternWhitespace)]
    private static partial Regex SIGMA_FINAL_RX();

    [GeneratedRegex(@"
        ((?>\*)?)               # UC indicator
        ((?>[()/=\\+|&'?])*)    # possible accents
        ((?>[A-Z][123]?))       # letter
        ((?>[()/=\\+|&'?])*)    # possible accents",
        RegexOptions.IgnorePatternWhitespace)]
    private static partial Regex GRK_TOKENS_RX();

    private static string GreekLetterMatch(Match m)
    {
        string ltr = m.Groups[1].Value + m.Groups[3].Value;
        var accs = m.Groups[2].Value.Concat(m.Groups[4].Value);
        return (GRKCHR.TryGetValue(ltr, out var part1) ? part1 : ltr) +
            String.Join("",
                accs.Select(ch => 
                    GRKACC.TryGetValue(ch, out var accstr) ? accstr : ch.ToString())
                );
    }

    public static string Convert(string romanized)
    {
        var finalized = SIGMA_FINAL_RX().Replace(romanized, "S2");
        return GRK_TOKENS_RX().Replace(finalized, GreekLetterMatch);
    }
}