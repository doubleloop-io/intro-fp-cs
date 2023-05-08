using System.Text.RegularExpressions;
using LanguageExt;
using Xunit;

namespace Exercises.Solutions;

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
    
    [Fact]
    public void checkIn_after_valid_creation()
    {
        var result = ParseItem("100")
            .Map(item => item.CheckIn(10));

        Assert.Equal(Prelude.Some(new Item(110)), result);
    }
    
    [Fact]
    public void checkIn_after_invalid_creation()
    {
        var result = ParseItem("asd")
            .Map(item => item.CheckIn(10));

        Assert.Equal(Prelude.None, result);
    }
}
