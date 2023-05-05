using System.Text.RegularExpressions;
using LanguageExt;
using Xunit;

namespace IntroFp.Solutions;

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

    [Fact]
    public void checkOut_after_valid_creation()
    {
        var result = ParseItem("100")
            .Bind(item => item.CheckOut(10));

        Assert.Equal(Prelude.Some(new Item(90)), result);
    }

    [Fact]
    public void checkIn_and_checkOut_after_valid_creation()
    {
        var result = ParseItem("100")
            .Map(item => item.CheckIn(10))
            .Bind(item => item.CheckOut(20));

        Assert.Equal(Prelude.Some(new Item(90)), result);
    }

    [Fact]
    public void invalid_checkOut()
    {
        var result = ParseItem("100")
            .Bind(item => item.CheckOut(110));

        Assert.Equal(Prelude.None, result);
    }

    [Fact]
    public void checkOut_after_invalid_creation()
    {
        var result = ParseItem("asd")
            .Bind(item => item.CheckOut(10));

        Assert.Equal(Prelude.None, result);
    }
}
