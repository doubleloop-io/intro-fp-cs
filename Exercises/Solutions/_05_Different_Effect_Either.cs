using System.Text.RegularExpressions;
using LanguageExt;
using Xunit;

namespace Exercises.Solutions;

public class _05_Different_Effect_Either
{
    private record Item(int Qty)
    {
        public Item CheckIn(int count) =>
            new(Qty + count);

        public Either<Error, Item> CheckOut(int count) =>
            count <= Qty
                ? Prelude.Right(new Item(Qty - count))
                : Prelude.Left(new Error($"can't checkout {count} from {Qty}"));
    }

    private static Either<Error, Item> ParseItem(string qty) =>
        Regex.IsMatch(qty, "^[0-9]+$", RegexOptions.IgnoreCase)
            ? Prelude.Right(new Item(int.Parse(qty)))
            : Prelude.Left(new Error($"can't parse value: {qty}"));

    private record Error(string Info);

    [Fact]
    public void checkIn_and_checkOut_after_valid_creation()
    {
        var result = ParseItem("100")
            .Map(item => item.CheckIn(10))
            .Bind(item => item.CheckOut(20));

        Assert.Equal(Prelude.Right(new Item(90)), result);
    }

    [Fact]
    public void invalid_creation()
    {
        var result = ParseItem("asd")
            .Map(item => item.CheckIn(10))
            .Bind(item => item.CheckOut(20));

        Assert.Equal(Prelude.Left(new Error("can't parse value: asd")), result);
    }

    [Fact]
    public void invalid_checkOut()
    {
        var result = ParseItem("100")
            .Map(item => item.CheckIn(10))
            .Bind(item => item.CheckOut(200));

        Assert.Equal(Prelude.Left(new Error("can't checkout 200 from 110")), result);
    }
}
