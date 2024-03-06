using System.Text;

namespace RWTodd.WikiBook;

/// <summary>
/// A Page in a WikiBook
/// </summary>
public class Page
{
    public Page? PreviousPage { get; internal set; }
    public Page? NextPage { get; internal set; }

    protected readonly Book book;
    protected string url;
    protected string? defaultDisplayName;
    protected string? defaultShortName;
    protected string? tocListMarkers;
    protected bool isCategory = false;

    public Page(Book book, string urlName, bool addSuffix = true)
    {
        this.book = book;
        url = addSuffix ? urlName + (book.PageSuffix ?? "") : urlName;
    }

    public virtual string MakeLink(string? text = null)
    {
        string linktxt = text ?? defaultDisplayName ?? url;
        string cat = isCategory ? ":Category:" : string.Empty;
        var start = $"[[{cat}{url}";
        return start + ((string.IsNullOrWhiteSpace(linktxt)) ? "]]" : $"|{linktxt}]]");
    }

    public virtual string MakeTOCListLink() =>
        $"{tocListMarkers ?? "* "}{MakeLink()}";
    
    public virtual string MakeShortLink() =>
        MakeLink(defaultShortName ?? defaultDisplayName ?? url);

    public virtual string FileName => $"{url}.wikitext".Replace(' ', '_');

    public virtual string MakeCategoryMarker()
    {
        if(!isCategory)
            throw new InvalidOperationException("Category marker requested for non-category!");
        return $"[[Category:{url}]]";
    }

    public virtual string GeneratePageTemplate() =>
        $@"{{{{{book.NavPage}
|1 = {book.NavTitle ?? book.Title ?? "Unknown"}
|2 = {book.TOC.MakeLink()}
|3 = {(PreviousPage ?? book.TOC).MakeShortLink()}
|4 = {(NextPage ?? book.TOC).MakeShortLink()}
}}}}

&rarr; {(NextPage ?? book.TOC).MakeLink()} &rarr;
{book.BookCategoryMark}";

}

public class MutablePage(Book book, string urlName, bool addSuffix = true) 
    : Page(book, urlName, addSuffix)
{
    public string URL { set { url = value; } }
    public string DisplayName { set { defaultDisplayName = value; } }
    public string ShortName { set { defaultShortName = value; } }
    public string TOCListMarkers { set { tocListMarkers = value; } }
    public bool IsCategory { set { isCategory = value; } }
}

public class TableOfContents : Page
{
    public Page Category { get; protected set; }
    private List<string> tocText;

    public TableOfContents(Book book, string urlName) : base(book, urlName, false)
    {
        defaultShortName = "Contents";
        defaultDisplayName = "Table of Contents";
        tocListMarkers = string.Empty;
        isCategory = true;
        Category = this;
        tocText = new();
    }

    /// <summary>
    /// Some small books may not have their own category, but rather
    /// be pages of some other category.  That includes the TOC page!
    /// </summary>
    /// <param name="cat">the category that the pages of the book belong to</param>
    public void MakePageOfCategory(string cat)
    {
        isCategory = false;
        Category = new MutablePage(book, cat, false) { IsCategory = true };
    }

    public override string FileName => isCategory ? "__toc.wikitext" : base.FileName;

    public override string GeneratePageTemplate() 
    {
        StringBuilder sb = new();
        sb.AppendLine($"; Title: {book.Title ?? "Unknown!"}");
        sb.AppendLine($"; Author: {book.Author ?? "Unknown!"}");
        sb.AppendLine($"; Date: {book.Date ?? "Unknown!"}");
        sb.AppendLine();
        sb.AppendLine($"[[File:{book.ShortTitle ?? "UNKNOWN BOOK"} CoverImage.jpg|thumb|Cover Image]]".Replace(' ','_'));
        sb.AppendLine("== Contents ==");
        foreach(string s in tocText) sb.AppendLine(s);
        sb.AppendLine();
        sb.AppendLine(isCategory ? "[[Category:WikiBooks]]" : Category.MakeCategoryMarker());
        return sb.ToString();
    }

    public void AddTOCText(string line) => tocText.Add(line);
}

/// <summary>
/// The WikiBook Table of Contents
/// </summary>
public class Book
{
    private readonly List<Page> pages;
    private readonly HashSet<String> uniqueURLs;
    public string? Title { get; set; }
    public string? Date { get; set; }
    public string? NavTitle { get; set; }
    public string? ShortTitle { get; set; }
    public string? Author { get; set; }
    public string NavPage { get; set; }

    public TableOfContents TOC { get; }

    public string BookCategoryMark { get => TOC.Category.MakeCategoryMarker(); }

    public virtual string? PageSuffix { 
        get
        {
            string? s = ShortTitle ?? Title;
            return string.IsNullOrEmpty(s) ? null : $" ({s})";
        }
    }

    public void AddPage(Page p)
    {
        if(!uniqueURLs.Add(p.FileName)) 
            throw new ArgumentException("Adding the same URL page twice! " + p.FileName);
        var lastp = pages.LastOrDefault();
        if(lastp != null) lastp.NextPage = p;
        p.PreviousPage = lastp;
        pages.Add(p);
        TOC.AddTOCText(p.MakeTOCListLink());
    }

    public void AddRawText(string text) => TOC.AddTOCText(text);

    public Book(string bookUrl)
    {
        TOC = new(this, bookUrl);
        NavPage = "Generic WikiBook Nav";
        pages = new();
        uniqueURLs = new();
    }

    public IEnumerable<Page> GetPages() => pages.AsEnumerable();
}
