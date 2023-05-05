using System.Text.RegularExpressions;
using LanguageExt;
using Xunit;

namespace IntroFp;

// TODO 1: for each test, remove the skip marker and make it green
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
            // TODO 2: get familiar with Either creation
            ? Prelude.Right(new Item(int.Parse(qty)))
            : Prelude.Left(new Error($"can't parse value: {qty}"));

    private record Error(string Info);

    [Fact(Skip = "TODO")]
    public void checkIn_and_checkOut_after_valid_creation()
    {
        var result = ParseItem("100")
            // TODO 3: use 'map' to check-in 10
            // TODO 4: use 'bind' to check-out 20
            ;

        Assert.Equal(Prelude.Right(new Item(90)), result);
    }

    [Fact(Skip = "TODO")]
    public void invalid_creation()
    {
        var result = ParseItem("asd")
            // TODO 5: use 'map' to check-in 10
            // TODO 6: use 'bind' to check-out 20
            ;

        Assert.Equal(Prelude.Left(new Error("can't parse value: asd")), result);
    }

    [Fact(Skip = "TODO")]
    public void invalid_checkOut()
    {
        var result = ParseItem("100")
            // TODO 7: use 'map' to check-in 10
            // TODO 8: use 'bind' to check-out 200
            ;

        Assert.Equal(Prelude.Left(new Error("can't checkout 200 from 110")), result);
    }
}
