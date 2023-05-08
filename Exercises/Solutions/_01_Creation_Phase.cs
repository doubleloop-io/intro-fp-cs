using System.Text.RegularExpressions;
using LanguageExt;
using Xunit;

namespace Exercises.Solutions;

public class _01_Creation_Phase
{
    private record Item(int Qty);

    private interface IOptionalItem
    {
    }

    private record Valid(Item Item) : IOptionalItem;

    private record Invalid() : IOptionalItem;

    private static IOptionalItem ParseItem(string qty) =>
        Regex.IsMatch(qty, "^[0-9]+$", RegexOptions.IgnoreCase)
            ? new Valid(new Item(int.Parse(qty)))
            : new Invalid();

    [Fact]
    public void valid_creation()
    {
        var result = ParseItem("100");

        Assert.Equal(new Valid(new Item(100)), result);
    }

    [Theory]
    [InlineData("asd")]
    [InlineData("1 0 0")]
    [InlineData("")]
    public void invalid_creation(string input)
    {
        var result = ParseItem(input);

        Assert.Equal(new Invalid(), result);
    }

    private static Option<Item> ParseItem_LangExt(string qty) =>
        Regex.IsMatch(qty, "^[0-9]+$", RegexOptions.IgnoreCase)
            ? Prelude.Some(new Item(int.Parse(qty)))
            : Prelude.None;

    [Fact]
    public void valid_creation_langext()
    {
        var result = ParseItem_LangExt("100");

        Assert.Equal(Prelude.Some(new Item(100)), result);
    }
    
    [Theory]
    [InlineData("asd")]
    [InlineData("1 0 0")]
    [InlineData("")]
    public void invalid_creation_langext(string input)
    {
        var result = ParseItem_LangExt(input);

        Assert.Equal(Prelude.None, result);
    }
}
