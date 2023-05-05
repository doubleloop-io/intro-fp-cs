using System.Text.RegularExpressions;
using LanguageExt;
using Xunit;

namespace IntroFp;

// TODO 1: for each test, remove the skip marker and make it green
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
            // TODO 2: get familiar with Try creation
            ? Prelude.Try(new Item(int.Parse(qty)))
            : Prelude.Try<Item>(() => throw new ArgumentException($"can't parse value: {qty}"));


    [Fact(Skip = "TODO")]
    public void checkIn_and_checkOut_after_valid_creation()
    {
        var result = ParseItem("100")
            // TODO 3: use 'map' to check-in 10
            // TODO 4: use 'bind' to check-out 20
            ;

        Assert.Equal(new Item(90), result.Invoke());
    }

    [Fact(Skip = "TODO")]
    public void invalid_creation()
    {
        var result = ParseItem("asd")
            // TODO 5: use 'map' to check-in 10
            // TODO 6: use 'bind' to check-out 20
            ;

        Assert.True(result.Invoke().IsFaulted);
    }

    [Fact(Skip = "TODO")]
    public void invalid_checkOut()
    {
        var result = ParseItem("100")
            // TODO 7: use 'map' to check-in 10
            // TODO 8: use 'bind' to check-out 200
            ;

        Assert.True(result.Invoke().IsFaulted);
    }
}
