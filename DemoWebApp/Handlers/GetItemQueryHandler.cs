using DemoWebApp.Features;
using DemoWebApp.Models;
using LanguageExt;

namespace DemoWebApp.Handlers;

public class GetItemQueryHandler
{
    private readonly IItemRepository _repository;

    public GetItemQueryHandler(IItemRepository repository) =>
        _repository = repository;

    public TryAsync<Item> Handle(GetItemQuery query) =>
        _repository.LoadOne(query.Id)
            .Map(optItem =>
                optItem.IfNone(() => throw new InvalidOperationException($"Item not found: {query.Id}")));
}
