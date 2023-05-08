using System.Text.RegularExpressions;
using Xunit;

namespace Exercises;

// TODO 1: for each test, remove the skip marker and make it green
public class _01_Creation_Phase
{
    private record Item(int Qty);

    // TODO 2: define an OptionalItem type that can be in Valid (with an Item) or Invalid.
    private interface IOptionalItem
    {
    }

    // TODO 3: use OptionalItem as return type and remove the null
    private static Item ParseItem(string qty) =>
        Regex.IsMatch(qty, "^[0-9]+$", RegexOptions.IgnoreCase)
            ? new Item(int.Parse(qty))
            : null; // or throw an exception

    [Fact(Skip = "TODO")]
    public void valid_creation()
    {
        var result = ParseItem("100");

        // TODO 4: update test assert
        Assert.Equal(new Item(100), result);
    }

    [Theory(Skip = "TODO")]
    [InlineData("asd")]
    [InlineData("1 0 0")]
    [InlineData("")]
    public void invalid_creation(string input)
    {
        var result = ParseItem(input);

        // TODO 5: update test assert
        Assert.Null(result);
    }
}
