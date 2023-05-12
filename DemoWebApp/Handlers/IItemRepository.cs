using DemoWebApp.Models;
using LanguageExt;

namespace DemoWebApp.Handlers;

public interface IItemRepository
{
    TryAsync<Item> LoadOneRequired(Guid id);
    TryAsync<Option<Item>> LoadOne(Guid id);
    TryAsync<Unit> Save(Item item);
}
