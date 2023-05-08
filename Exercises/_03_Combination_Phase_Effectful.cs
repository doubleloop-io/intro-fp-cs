using System.Text.RegularExpressions;
using LanguageExt;
using Xunit;

namespace Exercises;

// TODO 1: for each test, remove the skip marker and make it green
public class _03_Combination_Phase_Effectful
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
    public void checkOut_after_valid_creation()
    {
        var result = ParseItem("100")
            // TODO 2: use 'bind' to check-out 10
            // https://louthy.github.io/language-ext/LanguageExt.Core/Monads/Alternative%20Value%20Monads/Option/Option/index.html#Option_1_Bind_1
            ;

        Assert.Equal(Prelude.Some(new Item(90)), result);
    }

    [Fact(Skip = "TODO")]
    public void checkIn_and_checkOut_after_valid_creation()
    {
        var result = ParseItem("100")
            // TODO 3: use 'map' to check-in 10
            // TODO 4: use 'bind' to check-out 20
            ;

        Assert.Equal(Prelude.Some(new Item(90)), result);
    }

    [Fact(Skip = "TODO")]
    public void invalid_checkOut()
    {
        var result = ParseItem("100")
            // TODO 5: use 'bind' to check-out 110
            ;

        Assert.Equal(Prelude.None, result);
    }

    [Fact(Skip = "TODO")]
    public void checkOut_after_invalid_creation()
    {
        var result = ParseItem("asd")
            // TODO 6: use 'bind' to check-out 10
            ;

        Assert.Equal(Prelude.None, result);
    }
}
