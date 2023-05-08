using System.Text.RegularExpressions;
using LanguageExt;
using Xunit;

namespace Exercises;

// TODO 1: for each test, remove the skip marker and make it green
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

    [Fact(Skip = "TODO")]
    public void match_after_valid_creation()
    {
        var result = ParseItem("100")
            // TODO 2: use 'match' to produce a string (valid case)
            // https://louthy.github.io/language-ext/LanguageExt.Core/Monads/Alternative%20Value%20Monads/Option/Option/index.html#Option_1_Match_1
            ;

        // TODO 3: uncomment the assert
        // Assert.Equal("100", result);
    }

    [Fact(Skip = "TODO")]
    public void match_after_invalid_creation()
    {
        var result = ParseItem("asd")
            // TODO 4: use 'match' to produce an alternative string (invalid case)
            ;

        // TODO 5: uncomment the assert
        // Assert.Equal("alternative value", result);
    }

    [Fact(Skip = "TODO")]
    public void get_after_valid_creation()
    {
        var result = ParseItem("100")
            // TODO 6: see OptionExt class
            .ValueOrDefault(new Item(0));

        Assert.Equal(new Item(100), result);
    }

    // NOTE: see OptionExt class
    [Fact(Skip = "TODO")]
    public void get_after_invalid_creation()
    {
        var result = ParseItem("asd")
            // TODO 7: see OptionExt class
            .ValueOrDefault(new Item(0));

        Assert.Equal(new Item(0), result);
    }
}

public static class OptionExt
{
    public static T ValueOrDefault<T>(this Option<T> actual, T @default) =>
        actual.Match(x => x, @default);
}
