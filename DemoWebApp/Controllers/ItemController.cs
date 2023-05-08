using DemoWebApp.Features;
using DemoWebApp.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace DemoWebApp.Controllers;

[ApiController]
[Route("[controller]")]
public class ItemController : ControllerBase
{
    private readonly GetItemQueryHandler _getItemQueryHandler;
    private readonly CreateItemCommandHandler _createItemCommandHandler;
    private readonly CheckInItemsCommandHandler _checkInItemsCommandHandler;

    public ItemController(
        GetItemQueryHandler getItemQueryHandler,
        CreateItemCommandHandler createItemCommandHandler,
        CheckInItemsCommandHandler checkInItemsCommandHandler)
    {
        _getItemQueryHandler = getItemQueryHandler;
        _createItemCommandHandler = createItemCommandHandler;
        _checkInItemsCommandHandler = checkInItemsCommandHandler;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne([FromRoute] GetItemQuery query)
    {
        var result = await _getItemQueryHandler.Handle(query).Invoke();
        return result.Match<IActionResult>(item => Ok(item), ex => HandleError(ex));
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateItemCommand command)
    {
        var result = await _createItemCommandHandler.Handle(command).Invoke();
        return result.Match<IActionResult>(id => Ok(id.ToString()), ex => HandleError(ex));
    }

    [HttpPost("checkin")]
    public async Task<IActionResult> CheckIn([FromBody] CheckInItemsCommand command)
    {
        var result = await _checkInItemsCommandHandler.Handle(command).Invoke();
        return result.Match<IActionResult>(_ => NoContent(), ex => HandleError(ex));
    }

    private IActionResult HandleError(Exception ex) =>
        ex switch
        {
            InvalidOperationException ioex => NotFound(ioex.ToString()),
            _ => BadRequest(ex.ToString())
        };
}
