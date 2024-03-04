using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
namespace RWTodd.RomanizedText;

public static partial class Hebrew
{
    private static readonly Dictionary<string, string> HEBTBL = new() {
        { "A", "\u05d0" }, // alef
        { "B", "\u05d1" }, // bet
        { "G", "\u05d2" }, // gimel
        { "D",  "\u05d3" }, // dalet,
        { "H",  "\u05d4" }, // heh,
        { "I",  "\u05d9" }, // yod,
        { "L",  "\u05dc" }, // lamed,
        { "O",  "\u05e2" }, // ayin,
        { "Q",  "\u05e7" }, // qof,
        { "R",  "\u05e8" }, // resh,
        { "S",  "\u05e1" }, // samech,
        { "T",  "\u05d8" }, // teth,
        { "V",  "\u05d5" }, // vav,
        { "Z",  "\u05d6" }, // zain,
        { "Ch", "\u05d7" }, // cheth,
        { "K",  "\u05db" }, // kaf initial,
        { "Ki",  "\u05db" }, // kaf initial,
        { "Kf",  "\u05da" }, // kaf final,
        { "M",  "\u05de" }, // mem initial,
        { "Mi",  "\u05de" }, // mem initial,
        { "Mf",  "\u05dd" }, // mem final,
        { "N",  "\u05e0" }, // nun initial,
        { "Ni",  "\u05e0" }, // nun initial,
        { "Nf",  "\u05df" }, // nun final,
        { "P",  "\u05e4" }, // peh initial,
        { "Pi",  "\u05e4" }, // peh initial,
        { "Pf",  "\u05e3" }, // peh final,
        { "Sh",  "\u05e9" }, // shin,
        { "Th",  "\u05ea" }, // tav,
        { "Tz",  "\u05e6" }, // tzaddi initial,
        { "Tzi",  "\u05e6" }, // tzaddi initial,
        { "Tzf",  "\u05e5" }, // tzaddi final,
        { "Vv",  "\u05f0" }, // vav-vav ligature,
        { "Vi",  "\u05f1" }, // vav-yod ligature,
        { "Ii",  "\u05f2" }, // yod-yod ligature,
        { ";",  "\u05b0" }, // Sh''va,
        { ";3", "\u05b1" }, // Reduced Segol,
        { ";_", "\u05b2" }, // Reduced Patach,
        { ";7", "\u05b3" }, // Reduced Kamatz,
        { "1", "\u05b4" }, // Hiriq,
        { "2", "\u05b5" }, // Zeire,
        { "3", "\u05b6" }, // Segol,
        { "_", "\u05b7" }, // Patach,
        { "7", "\u05b8" }, // Kamatz,
        { "*", "\u05bc" }, // Dagesh,
        { "\\", "\u05bb" }, // Kubutz,
        { "`",  "\u05b9" }, // Holam,
        { "l",  "\u05c2" }, // Dot Left,
        { "r",  "\u05c1" }  // Dot Right
    };

    [GeneratedRegex(@"
        (?>K|M|N|P|Tz)                     # a potential final letter
        (?=(?>(?:;[3_7]?|[123_7*\\`lr])*)  # lookahead to a possible series of niqqud...
          (?:\W|\Z))                       # ...and then a non-letter",
        RegexOptions.IgnorePatternWhitespace)]
    private static partial Regex POSSIBLE_FINAL_RX();

    [GeneratedRegex(@"
        ((?>Ch|Sh|Tz|Th|Vv|[A-Z])(?>[if]?)) # a letter
        ((?>;[3_7]?|[123_7*\\`lr])?)    # up to 3 niqqud
        ((?>;[3_7]?|[123_7*\\`lr])?)    # up to 3 niqqud
        ((?>;[3_7]?|[123_7*\\`lr])?)    # up to 3 niqqud",
        RegexOptions.IgnorePatternWhitespace)]
    private static partial Regex TOKENS_RX();

    /// <summary>
    /// Convert romanized text to unicode hebrew.
    /// </summary>
    /// <remarks><para>
    ///  A  = aleph   B  = beth    G  = gimel    D  = dalet
    ///  H  = heh     V  = vav     Z  = zayin    Ch = chet
    ///  T  = teth    I  = yod     K  = kaf      L  = lamed
    ///  M  = mem     N  = nun     S  = samekh   O  = ayin
    ///  P  = peh     Tz = tzaddi  Q  = qoph     R  = resh
    ///  Sh = shin    Th = tav</para>
    ///
    ///  <para>Ligatures:
    ///  Ii = yod-yod   Vi = vav-yod     Vv = vav-vav</para>
    ///
    ///  <para>Niqqud:
    ///  ;  = Sh'va                *  = Dagesh
    ///  \\ =  Kubutz              `  = Holam
    ///  1  = Hiriq                2  = Zeire                
    ///  3  = Segol                ;3 = Reduced Segol        
    ///  _  = Patach               ;_ = Reduced Patach       
    ///  7  = Kamatz               ;7 = Reduced Kamatz       
    ///  Shl = Shin dot left       Shr = Shin dot right</para>
    /// </remarks>
    /// <param name="romanized">the input string of romanized Hebrew text</param>
    /// <returns>a unicode string of Hebrew text</returns>
    public static string Convert(string romanized)
    {
        var finalized = POSSIBLE_FINAL_RX().Replace(romanized, "$0f");
        return  TOKENS_RX().Replace(finalized, m => string.Join("",
            m.Groups.Cast<Group>().Skip(1)
                .Select(g => HEBTBL.TryGetValue(g.Value, out var heb) ? heb : g.Value)
        ));        
    }
}
