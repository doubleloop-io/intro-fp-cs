using System.Text.RegularExpressions;
using LanguageExt;
using Xunit;

namespace Exercises;

// TODO 1: for each test, remove the skip marker and make it green
public class _02_Combination_Phase_Normal
{
    private record Item(int Qty)
    {
        public Item CheckIn(int count) =>
            new(Qty + count);
    }

    private static Option<Item> ParseItem(string qty) =>
        Regex.IsMatch(qty, "^[0-9]+$", RegexOptions.IgnoreCase)
            ? Prelude.Some(new Item(int.Parse(qty)))
            : Prelude.None;

    [Fact(Skip = "TODO")]
    public void checkIn_after_valid_creation()
    {
        var result = ParseItem("100")
            // TODO 2: use 'map' to check-in 10
            // https://louthy.github.io/language-ext/LanguageExt.Core/Monads/Alternative%20Value%20Monads/Option/Option/index.html#Option_1_Map_1
            ;

        Assert.Equal(Prelude.Some(new Item(110)), result);
    }

    [Fact(Skip = "TODO")]
    public void checkIn_after_invalid_creation()
    {
        var result = ParseItem("asd")
            // TODO 3: use 'map' to check-in 10
            ;

        Assert.Equal(Prelude.None, result);
    }
}
