using System.Text.RegularExpressions;
using LanguageExt;
using Xunit;

namespace Exercises;

// TODO 1: for each test, remove the skip marker and make it green
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
            // TODO 2: get familiar with TryAsync creation
            ? Prelude.TryAsync(new Item(int.Parse(qty)))
            : Prelude.TryAsync<Item>(() => throw new ArgumentException($"can't parse value: {qty}"));


    [Fact(Skip = "TODO")]
    public async Task checkIn_and_checkOut_after_valid_creation()
    {
        var result = ParseItem("100")
            // TODO 3: use 'map' to check-in 10
            // TODO 4: use 'bind' to check-out 20
            ;

        var value = await result.Invoke();

        Assert.Equal(new Item(90), value);
    }

    [Fact(Skip = "TODO")]
    public async Task invalid_creation()
    {
        var result = ParseItem("asd")
            // TODO 5: use 'map' to check-in 10
            // TODO 6: use 'bind' to check-out 20
            ;

        var value = await result.Invoke();

        Assert.True(value.IsFaulted);
    }

    [Fact(Skip = "TODO")]
    public async Task invalid_checkOut()
    {
        var result = ParseItem("100")
            // TODO 7: use 'map' to check-in 10
            // TODO 8: use 'bind' to check-out 20
            ;

        var value = await result.Invoke();

        Assert.True(value.IsFaulted);
    }
}
