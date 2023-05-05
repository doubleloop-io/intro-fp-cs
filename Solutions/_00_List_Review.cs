using LanguageExt;
using Xunit;

namespace IntroFp.Solutions;

public class _00_List_Review
{
    [Fact(Skip = "TODO")]
    public void linq_filled_list()
    {
        var result = new List<int> { 12, 9, 555 }
            .Select(n => n.ToString())
            .SelectMany(s => new List<string> { s, s })
            .Aggregate("", (acc, cur) => acc + cur + " ");

        Assert.Equal("12 12 9 9 555 555 ", result);
    }

    [Fact(Skip = "TODO")]
    public void linq_empty_list()
    {
        var result = new List<int>()
            .Select(n => n.ToString())
            .SelectMany(s => new List<string> { s, s })
            .Aggregate("", (acc, cur) => acc + cur + " ");

        Assert.Equal("", result);
    }

    [Fact(Skip = "TODO")]
    public void not_linq_but_functional_programming()
    {
        var result = new List<int> { 12, 9, 555 }
            .Map(n => n.ToString())
            .Bind(s => new List<string> { s, s })
            .Fold("", (acc, cur) => acc + cur + " ");

        Assert.Equal("12 12 9 9 555 555 ", result);
    }

    [Fact(Skip = "TODO")]
    public void immutable_list()
    {
        var result = Prelude.List(12, 9, 555)
            .Map(n => n.ToString())
            .Bind(s => Prelude.List(s, s))
            .Fold("", (acc, cur) => acc + cur + " ");

        Assert.Equal("12 12 9 9 555 555 ", result);
    }

    [Fact(Skip = "TODO")]
    public void query_expression_list()
    {
        var result =
            (from n in new List<int> { 12, 9, 555 }
                let s = n.ToString()
                from doubled in new List<string> { s, s }
                select doubled)
            .Aggregate("", (acc, cur) => acc + cur + " ");


        Assert.Equal("12 12 9 9 555 555 ", result);
    }

    [Fact(Skip = "TODO")]
    public void query_expression_immutable_list()
    {
        var result =
            (from n in Prelude.List(12, 9, 555)
                let s = n.ToString()
                from doubled in Prelude.List(s, s)
                select doubled)
            .Aggregate("", (acc, cur) => acc + cur + " ");


        Assert.Equal("12 12 9 9 555 555 ", result);
    }
}
