using System.Text.RegularExpressions;
using LanguageExt;
using Xunit;

namespace IntroFp.Solutions;

public class _04_Removal_Phase
{
    private record Item(int Qty)
    {
        public Item CheckIn(int count) =>
            new(Qty + count);

        public Option<Item> CheckOut(int count) =>
            count <= Qty
                ? Prelude.Some(new Item(Qty - count))
                : Prelude.None;
    }

    private static Option<Item> ParseItem(string qty) =>
        Regex.IsMatch(qty, "^[0-9]+$", RegexOptions.IgnoreCase)
            ? Prelude.Some(new Item(int.Parse(qty)))
            : Prelude.None;

    [Fact]
    public void match_after_valid_creation()
    {
        var result = ParseItem("100")
            .Match(item => item.Qty.ToString(), "alternative value");

        Assert.Equal("100", result);
    }

    [Fact]
    public void match_after_invalid_creation()
    {
        var result = ParseItem("asd")
            .Match(item => item.Qty.ToString(), "alternative value");

        Assert.Equal("alternative value", result);
    }

    // NOTE: see OptionExt class
    [Fact]
    public void get_after_valid_creation()
    {
        var result = ParseItem("100")
            .ValueOrDefault(new Item(0));

        Assert.Equal(new Item(100), result);
    }

    // NOTE: see OptionExt class
    [Fact]
    public void get_after_invalid_creation()
    {
        var result = ParseItem("asd")
            .ValueOrDefault(new Item(0));

        Assert.Equal(new Item(0), result);
    }
}

public static class OptionExt
{
    public static T ValueOrDefault<T>(this Option<T> actual, T @default) =>
        actual.Match(x => x, @default);
}
