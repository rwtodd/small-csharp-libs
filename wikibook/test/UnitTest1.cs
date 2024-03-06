namespace RWTodd.Wikibook.Test;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var b = new Book("Furthest Places, The (M. Rosas)");
        b.ShortTitle = "Furthest Places";
        var p1 = new MutablePage(b,"First Chapter, The") { DisplayName = "The First Chapter", ShortName="First Chap."};
        var p2 = new MutablePage(b,"Second Chapter, The") { DisplayName = "The Second Chapter", ShortName="Second Chap."};
        
        b.AddPage(p1);
        b.AddPage(p2);
        Assert.Null(p1.PreviousPage);
        Assert.Null(p2.NextPage);
        Assert.Equal(p2, p1.NextPage);
        Assert.Same(p2,p1.NextPage); 
        Assert.Same(p1,p2.PreviousPage);
        Assert.Equal("[[First Chapter, The (Furthest Places)|First Chap.]]", p1.MakeShortLink());      
        Assert.Equal("[[Second Chapter, The (Furthest Places)|The Second Chapter]]", p2.MakeLink());
        Assert.Equal("* [[Second Chapter, The (Furthest Places)|The Second Chapter]]", p2.MakeTOCListLink());
        Assert.Equal("[[Category:Furthest Places, The (M. Rosas)]]", b.BookCategoryMark);
    }
}