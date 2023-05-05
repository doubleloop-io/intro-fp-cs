using LanguageExt;
using Xunit;

namespace IntroFp;

// TODO 1: for each test, remove the skip marker and make it green
public class _00_List_Review
{
    [Fact(Skip = "TODO")]
    public void linq_filled_list()
    {
        // create a list of ints
        var result = new List<int> { 12, 9, 555 }
            // convert every value into a string
            // keep the list with the same size
            .Select(n => n.ToString())
            // duplicate every value
            // keep the list but change the size
            .SelectMany(s => new List<string> { s, s })
            // remove the list with a cumulative result
            .Aggregate("", (acc, cur) => acc + cur + " ");

        // TODO: complete the assert
        // Assert.Equal(   , result);
    }

    [Fact(Skip = "TODO")]
    public void linq_empty_list()
    {
        // create an empty list of ints
        var result = new List<int>()
            // convert every value into a string
            // keep the list with the same size
            .Select(n => n.ToString())
            // duplicate every value
            // keep the list but change the size
            .SelectMany(s => new List<string> { s, s })
            // remove the list with a cumulative result
            .Aggregate("", (acc, cur) => acc + cur + " ");

        // TODO: complete the assert
        // Assert.Equal(   , result);
    }

    [Fact(Skip = "TODO")]
    public void not_linq_but_functional_programming()
    {
        // create a list of ints
        var result = new List<int> { 12, 9, 555 }
            // convert every value into a string (LangExt)
            // keep the list with the same size
            .Map(n => n.ToString())
            // duplicate every value (LangExt)
            // keep the list but change the size
            .Bind(s => new List<string> { s, s })
            // remove the list with a cumulative result
            .Fold("", (acc, cur) => acc + cur + " ");

        // TODO: complete the assert
        // Assert.Equal(   , result);
    }

    [Fact(Skip = "TODO")]
    public void immutable_list()
    {
        // given an immutable list of ints (LangExt)
        var result = Prelude.List(12, 9, 555)
            // convert every value into a string (LangExt)
            // keep the list with the same size
            .Map(n => n.ToString())
            // duplicate every value (LangExt)
            // keep the list but change the size
            .Bind(s => Prelude.List(s, s))
            // remove the list with a cumulative result (LangExt)
            .Fold("", (acc, cur) => acc + cur + " ");

        // TODO: complete the assert
        // Assert.Equal(   , result);
    }

    [Fact(Skip = "TODO")]
    public void query_expression_list()
    {
        var result =
            // create a list of ints
            (from n in new List<int> { 12, 9, 555 }
                // convert every value into a string
                // keep the list with the same size
                let s = n.ToString()
                // duplicate every value
                // keep the list but change the size
                from doubled in new List<string> { s, s }
                select doubled)
            // remove the list with a cumulative result
            .Aggregate("", (acc, cur) => acc + cur + " ");

        // TODO: complete the assert
        // Assert.Equal(   , result);
    }

    [Fact(Skip = "TODO")]
    public void query_expression_immutable_list()
    {
        var result =
            // create a list of ints
            (from n in Prelude.List(12, 9, 555)
                // convert every value into a string
                // keep the list with the same size
                let s = n.ToString()
                // duplicate every value
                // keep the list but change the size
                from doubled in Prelude.List(s, s)
                select doubled)
            // remove the list with a cumulative result
            .Aggregate("", (acc, cur) => acc + cur + " ");

        // TODO: complete the assert
        // Assert.Equal(   , result);
    }
}
