namespace DemoWebApp.Models;

public record Item(
    Guid Id,
    string Name,
    int Qty
)
{
    public Item CheckIn(int value) =>
        this with { Qty = Qty + value };
}
