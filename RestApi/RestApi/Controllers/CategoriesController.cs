using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RestApi.Application.Categories.Commands.Add;
using RestApi.Application.Categories.Commands.Delete;
using RestApi.Application.Categories.Commands.Update;
using RestApi.Application.Categories.Queries.Get;

namespace RestApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CategoriesController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken token)
    {
        var list = await _mediator.Send(new GetCategoriesQuery(), token);
        return Ok(list);
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddCategoryCommand command, CancellationToken token)
    {
        var createdId = await _mediator.Send(command, token);
        return Created("/categories/" + createdId, createdId);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute]string id, [FromBody]UpdateCategoryModel model, CancellationToken token)
    {
        var command = _mapper.Map<UpdateCategoryCommand>(model);
        command.Id = id;
        await _mediator.Send(command, token);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id, CancellationToken token)
    {
        await _mediator.Send(new DeleteCategoryCommand { Id = id }, token);
        return Ok();
    }
}
