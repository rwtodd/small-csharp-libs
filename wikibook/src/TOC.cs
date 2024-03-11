using System.Text;

namespace RWTodd.WikiBook;

internal interface IContentsEntry
{
    string MakeTOCListEntry();
}

internal class ContentsRawText(string text) : IContentsEntry
{
    public string MakeTOCListEntry() => text;
}

/// <summary>
/// A Page in a WikiBook
/// </summary>
public class Page(Book book, string url) : IContentsEntry
{
    protected string? _displayName;
    protected string? _shortName;
    protected string? _tocListMarkers;

    public Page? PreviousPage { get; internal set; }
    public Page? NextPage { get; internal set; }

    public Book Book => book;

    public string URL => url;
    public string DisplayName 
    {
        get => _displayName ?? URL;
        set { _displayName = value; }
    }

    public virtual string ShortName
    {
        get
        {
            string candidate = _shortName ?? DisplayName;
            if(candidate.Length > 20)
            {
                if(candidate.StartsWith("A ")) 
                {
                    candidate = candidate[2..];
                } else if(candidate.StartsWith("An "))
                {
                    candidate = candidate[3..];
                } else if(candidate.StartsWith("The "))
                {
                    candidate = candidate[4..];
                }
                if(candidate.Length > 20)
                {
                    candidate = candidate[..18] + "&hellip;";
                }
            }
            return candidate;
        }
        set
        {
            _shortName = value;
        }
    }

    public virtual string TOCListMarkers
    {
        get => _tocListMarkers ?? "* ";
        set { _tocListMarkers = value; }
    }

    public bool IsCategory { get; set; } = false;
    
    public virtual string FileName => $"{URL}.wikitext".Replace(' ', '_');

    public virtual string MakeLink(string? text = null)
    {
        string linktxt = text ?? DisplayName;
        string cat = IsCategory ? ":Category:" : string.Empty;
        var start = $"[[{cat}{url}";
        return start + ((string.IsNullOrWhiteSpace(linktxt)) ? "]]" : $"|{linktxt}]]");
    }

    public virtual string MakeTOCListEntry() =>
        $"{TOCListMarkers}{MakeLink()}";
    
    public virtual string MakeShortLink() =>
        MakeLink(ShortName);


    public virtual string MakeCategoryMarker()
    {
        if(!IsCategory)
            throw new InvalidOperationException("Category marker requested for non-category!");
        return $"[[Category:{URL}]]";
    }

    public virtual string GeneratePageTemplate() =>
        $@"{{{{{book.NavTemplate}
|1 = {book.NavTitle}
|2 = {book.TOC.MakeLink()}
|3 = {(PreviousPage ?? book.TOC).MakeShortLink()}
|4 = {(NextPage ?? book.TOC).MakeShortLink()}
}}}}

&rarr; {(NextPage ?? book.TOC).MakeLink()} &rarr;
{book.BookCategoryMark}";

}

/// <summary>
/// A kind of page that tracks entries and produces a table of contents in its
/// template file.
/// </summary>
public class TableOfContents : Page
{
    public Page ParentCategory { get; protected set; }
    private List<IContentsEntry> entries;

    public TableOfContents(Book book, string urlName, string parentCat) : base(book, urlName)
    {
        ShortName = "Contents";
        DisplayName = "Table of Contents";
        TOCListMarkers = string.Empty;
        ParentCategory = new Page(book, parentCat) { IsCategory = true };
        entries = new();
    }

    public override string GeneratePageTemplate() 
    {
        StringBuilder sb = new();
        sb.AppendLine($"; Title: {Book.Title ?? "Unknown!"}");
        sb.AppendLine($"; Author: {Book.Author ?? "Unknown!"}");
        sb.AppendLine($"; Date: {Book.Date ?? "Unknown!"}");
        sb.AppendLine();
        sb.AppendLine($"[[File:{Book.ShortTitle ?? "UNKNOWN BOOK"} CoverImage.jpg|thumb|Cover Image]]".Replace(' ','_'));
        sb.AppendLine("== Contents ==");
        foreach(var e in entries) sb.AppendLine(e.MakeTOCListEntry());
        sb.AppendLine();
        sb.AppendLine(ParentCategory.MakeCategoryMarker());
        return sb.ToString();
    }

    internal void AddEntry(IContentsEntry e) => entries.Add(e); 
}

/// <summary>
/// The WikiBook Table of Contents
/// </summary>
public class Book
{
    private static readonly string UNK = "Unknown!";

    protected string? _title;
    protected string? _navTitle;
    protected string? _shortTitle;
    protected string? _date;
    protected string? _author;
    private readonly List<Page> pages;
    private readonly HashSet<String> uniqueURLs;
    public virtual string Title
    { 
        get => _title ?? UNK;
        set
        {
            _title = value;
        }
    }

    public virtual string Date 
    { 
        get => _date ?? UNK;
        set
        {
            _date = value;
        }
    }

    public virtual string NavTitle 
    {
        get => _navTitle ?? _title ?? UNK;
        set
        {
            _navTitle = value;
        }
    }

    public string ShortTitle
    {
        get => _shortTitle ?? _title ?? UNK;
        set
        {
            _shortTitle = value;
        }
    }
    public string Author
     { 
        get => _author ?? UNK;
        set
        {
            _author = value;
        }
    }

    public string NavTemplate { get; set; }

    public TableOfContents TOC { get; }

    public string BookCategoryMark { get; set; }

    public void AddPage(Page p)
    {
        if(!uniqueURLs.Add(p.FileName)) 
            throw new ArgumentException("Adding the same URL page twice! " + p.FileName);
        var lastp = pages.LastOrDefault();
        if(lastp != null) lastp.NextPage = p;
        p.PreviousPage = lastp;
        pages.Add(p);
        TOC.AddEntry(p);
    }

    public void AddRawText(string text) => TOC.AddEntry(new ContentsRawText(text));

    public Book(string bookUrl, string tocCat)
    {
        TOC = new(this, bookUrl, tocCat);
        BookCategoryMark = (new Page(this, bookUrl) { IsCategory = true }).MakeCategoryMarker();
        NavTemplate = "Generic WikiBook Nav";
        pages = new();
        uniqueURLs = new();
    }

    public IEnumerable<Page> GetPages() => pages.AsEnumerable();
}
