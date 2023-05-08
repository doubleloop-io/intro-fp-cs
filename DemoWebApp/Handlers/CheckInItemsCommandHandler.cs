using DemoWebApp.Features;
using DemoWebApp.Models;
using LanguageExt;

namespace DemoWebApp.Handlers;

public class CheckInItemsCommandHandler
{
    private readonly IItemRepository _repository;

    public CheckInItemsCommandHandler(IItemRepository repository) =>
        _repository = repository;

    public TryAsync<Unit> Handle(CheckInItemsCommand command)
    {
        return from optItem in _repository.LoadOne(command.Id)
            let item = OnItemFound(command.Id, optItem, item => item.CheckIn(command.Qty))
            from _ in _repository.Save(item)
            select Unit.Default;
    }

    private static Item OnItemFound(Guid id, Option<Item> optItem, Func<Item, Item> action) =>
        optItem.Match(action, () => throw new InvalidOperationException($"Item not found: {id}"));
}
