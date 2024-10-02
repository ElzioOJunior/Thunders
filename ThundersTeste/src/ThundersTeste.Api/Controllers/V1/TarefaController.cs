using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;
using System;
using System.Collections.Generic;
using ThundersTeste.Application.Commands.Tarefa;
using ThundersTeste.Application.Queries.Tarefa;
using ThundersTeste.Application.Dtos.Tarefa;

namespace ThundersTeste.Api.Controllers.V1;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
//[Route("api/v1/tarefas")] 
public class TarefaController : ControllerBase 
{
    private readonly IMediator _bus;

    public TarefaController(IMediator bus)
    {
        _bus = bus;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ICollection<TarefaDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ReadAll()
    {
        var response = await _bus.Send(new ReadAllTarefaQuery());
        return Ok(response);
    }

    [HttpGet("{id:guid}")] 
    [ProducesResponseType(typeof(TarefaDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> ReadById(Guid id)
    {
        var response = await _bus.Send(new ReadTarefaByIdQuery(id));
        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(TarefaDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] CreateTarefaCommand request)
    {
        var response = await _bus.Send(request);
        return CreatedAtAction(nameof(ReadById), new { id = response.Id }, response); 
    }

    [HttpPatch("{TarefaId:guid}")] 
    [ProducesResponseType(typeof(TarefaDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(Guid TarefaId, [FromBody] TarefaUpdateDto request)
    {
        var response = await _bus.Send(new UpdateTarefaCommand(TarefaId, request.Name, request.Description));
        return Ok(response);
    }

    [HttpDelete("{TarefaId:guid}")] 
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(Guid TarefaId)
    {
        await _bus.Send(new DeleteTarefaCommand(TarefaId));
        return NoContent();
    }
}
