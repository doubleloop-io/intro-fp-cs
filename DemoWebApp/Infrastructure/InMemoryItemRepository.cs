using System.Collections.Concurrent;
using DemoWebApp.Handlers;
using DemoWebApp.Models;
using LanguageExt;

namespace DemoWebApp.Infrastructure;

public class InMemoryItemRepository : IItemRepository
{
    private ConcurrentDictionary<Guid, Item> _state;

    public InMemoryItemRepository()
    {
        _state = new ConcurrentDictionary<Guid, Item>();
    }

    public TryAsync<Item> LoadOneRequired(Guid id) =>
        LoadOne(id)
            .Map(optItem => 
                optItem.IfNone(() => throw new InvalidOperationException($"Item not found: {id}")));

    public TryAsync<Option<Item>> LoadOne(Guid id) =>
        Prelude.Try(() => TryGetValue(id)).ToAsync();

    private Option<Item> TryGetValue(Guid id) =>
        _state.TryGetValue(id, out var value) ? Prelude.Some(value) : Prelude.None;

    public TryAsync<Unit> Save(Item item) =>
        Prelude.Try(() =>
        {
            _state.AddOrUpdate(item.Id, _ => item, (_, _) => item);
            return Unit.Default;
        }).ToAsync();
}
