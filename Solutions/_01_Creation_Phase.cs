using System.Text.RegularExpressions;
using LanguageExt;
using Xunit;

namespace IntroFp.Solutions;

public class _01_Creation_Phase
{
    private record Item(int Qty);

    private interface IOptionalItem
    {
        public record Valid(Item Item) : IOptionalItem;

        public record Invalid() : IOptionalItem;
    }

    private static IOptionalItem ParseItem(string qty) =>
        Regex.IsMatch(qty, "^[0-9]+$", RegexOptions.IgnoreCase)
            ? new IOptionalItem.Valid(new Item(int.Parse(qty)))
            : new IOptionalItem.Invalid();

    private static Option<Item> ParseItem_LangExt(string qty) =>
        Regex.IsMatch(qty, "^[0-9]+$", RegexOptions.IgnoreCase)
            ? Prelude.Some(new Item(int.Parse(qty)))
            : Prelude.None;

    [Fact]
    public void valid_creation()
    {
        var result = ParseItem("100");

        Assert.Equal(new IOptionalItem.Valid(new Item(100)), result);
    }

    [Theory]
    [InlineData("asd")]
    [InlineData("1 0 0")]
    [InlineData("")]
    public void invalid_creation(string input)
    {
        var result = ParseItem(input);

        Assert.Equal(new IOptionalItem.Invalid(), result);
    }
}
