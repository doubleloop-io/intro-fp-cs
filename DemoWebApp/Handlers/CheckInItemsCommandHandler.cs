using DemoWebApp.Features;
using LanguageExt;

namespace DemoWebApp.Handlers;

public class CheckInItemsCommandHandler
{
    private readonly IItemRepository _repository;

    public CheckInItemsCommandHandler(IItemRepository repository) =>
        _repository = repository;

    public TryAsync<Unit> Handle(CheckInItemsCommand command)
    {
        return _repository.LoadOneRequired(command.Id)
            .Map(item => item.CheckIn(command.Qty))
            .Bind(item => _repository.Save(item));

        // return from optItem in _repository.LoadOneRequired(command.Id)
        //     let item = item.CheckIn(command.Qty)
        //     from _ in _repository.Save(item)
        //     select Unit.Default;
    }

    public TryAsync<Unit> HandleOptional(CheckInItemsCommand command)
    {
        return _repository.LoadOne(command.Id)
            .Map(optItem => optItem
                .Map(x => x.CheckIn(command.Qty))
                .IfNone(() => throw new InvalidOperationException($"Item not found: {command.Id}")))
            .Bind(item => _repository.Save(item));

        // return from optItem in _repository.LoadOne(command.Id)
        //     let item = optItem
        //         .Map(x => x.CheckIn(command.Qty))
        //         .IfNone(() => throw new InvalidOperationException($"Item not found: {command.Id}"))
        //     from _ in _repository.Save(item)
        //     select Unit.Default;
    }
}
