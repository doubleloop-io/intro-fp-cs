using System.Text.RegularExpressions;
using LanguageExt;
using Xunit;

namespace Exercises.Solutions;

public class _06_Different_Effect_Try
{
    private record Item(int Qty)
    {
        public Item CheckIn(int count) =>
            new(Qty + count);

        public Try<Item> CheckOut(int count) =>
            count <= Qty
                ? Prelude.Try(new Item(Qty - count))
                : Prelude.Try<Item>(() => throw new AggregateException($"can't checkout {count} from {Qty}"));
    }

    private static Try<Item> ParseItem(string qty) =>
        Regex.IsMatch(qty, "^[0-9]+$", RegexOptions.IgnoreCase)
            ? Prelude.Try(new Item(int.Parse(qty)))
            : Prelude.Try<Item>(() => throw new ArgumentException($"can't parse value: {qty}"));


    [Fact]
    public void checkIn_and_checkOut_after_valid_creation()
    {
        var result = ParseItem("100")
            .Map(item => item.CheckIn(10))
            .Bind(item => item.CheckOut(20));

        Assert.Equal(new Item(90), result.Invoke());
    }

    [Fact]
    public void invalid_creation()
    {
        var result = ParseItem("asd")
            .Map(item => item.CheckIn(10))
            .Bind(item => item.CheckOut(20));

        Assert.True(result.Invoke().IsFaulted);
    }

    [Fact]
    public void invalid_checkOut()
    {
        var result = ParseItem("100")
            .Map(item => item.CheckIn(10))
            .Bind(item => item.CheckOut(200));

        Assert.True(result.Invoke().IsFaulted);
    }
}
