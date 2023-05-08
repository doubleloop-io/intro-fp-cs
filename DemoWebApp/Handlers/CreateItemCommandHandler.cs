using DemoWebApp.Features;
using DemoWebApp.Models;
using LanguageExt;

namespace DemoWebApp.Handlers;

public class CreateItemCommandHandler
{
    private readonly IItemRepository _repository;

    public CreateItemCommandHandler(IItemRepository repository) =>
        _repository = repository;

    public TryAsync<Guid> Handle(CreateItemCommand command)
    {
        var item = new Item(Guid.NewGuid(), command.Name, command.Qty);

        return _repository.Save(item)
            .Map(_ => item.Id);
    }
}
