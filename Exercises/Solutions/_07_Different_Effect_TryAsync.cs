using System.Text.RegularExpressions;
using LanguageExt;
using Xunit;

namespace Exercises.Solutions;

public class _07_Different_Effect_TryAsync
{
    private record Item(int Qty)
    {
        public Item CheckIn(int count) =>
            new(Qty + count);

        public TryAsync<Item> CheckOut(int count) =>
            count <= Qty
                ? Prelude.TryAsync(new Item(Qty - count))
                : Prelude.TryAsync<Item>(() => throw new AggregateException($"can't checkout {count} from {Qty}"));
    }

    private static TryAsync<Item> ParseItem(string qty) =>
        Regex.IsMatch(qty, "^[0-9]+$", RegexOptions.IgnoreCase)
            ? Prelude.TryAsync(new Item(int.Parse(qty)))
            : Prelude.TryAsync<Item>(() => throw new ArgumentException($"can't parse value: {qty}"));


    [Fact]
    public async Task checkIn_and_checkOut_after_valid_creation()
    {
        var result = ParseItem("100")
            .Map(item => item.CheckIn(10))
            .Bind(item => item.CheckOut(20));

        var value = await result.Invoke();

        Assert.Equal(new Item(90), value);
    }

    [Fact]
    public async Task invalid_creation()
    {
        var result = ParseItem("asd")
            .Map(item => item.CheckIn(10))
            .Bind(item => item.CheckOut(20));

        var value = await result.Invoke();

        Assert.True(value.IsFaulted);
    }

    [Fact]
    public async Task invalid_checkOut()
    {
        var result = ParseItem("100")
            .Map(item => item.CheckIn(10))
            .Bind(item => item.CheckOut(200));

        var value = await result.Invoke();

        Assert.True(value.IsFaulted);
    }
}
