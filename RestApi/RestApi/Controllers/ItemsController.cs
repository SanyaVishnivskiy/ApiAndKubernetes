using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RestApi.Application.Items.Commands.Add;
using RestApi.Application.Items.Commands.Delete;
using RestApi.Application.Items.Commands.Update;
using RestApi.Application.Items.Queries.Get;

namespace RestApi.Controllers;

[ApiController]
[Route("categories/{categoryId}/[controller]")]
public class ItemsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ItemsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Get(
        [FromRoute] string categoryId,
        [FromQuery] int? page,
        [FromQuery] int? pageSize,
        CancellationToken token)
    {
        var query = new GetItemsQuery
        {
            CategoryId = categoryId,
            PageNumber = page,
            PageSize = pageSize,
        };

        var list = await _mediator.Send(query, token);
        return Ok(list);
    }

    [HttpPost]
    public async Task<IActionResult> Add(
        [FromRoute] string categoryId,
        [FromBody] AddItemModel model,
        CancellationToken token)
    {
        var command = _mapper.Map<AddItemCommand>(model);
        command.CategoryId = categoryId;
        var createdId = await _mediator.Send(command, token);
        return Created($"/categories/{categoryId}/items/{createdId}", createdId);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        [FromRoute] string id,
        [FromRoute] string categoryId,
        [FromBody] UpdateItemModel model,
        CancellationToken token)
    {
        var command = _mapper.Map<UpdateItemCommand>(model);
        command.Id = id;
        command.CategoryId = categoryId;
        await _mediator.Send(command, token);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(
        [FromRoute] string categoryId,
        [FromRoute] string id,
        CancellationToken token)
    {
        var command = new DeleteItemCommand
        {
            Id = id,
            CategoryId = categoryId
        };
        await _mediator.Send(command, token);
        return Ok();
    }
}
