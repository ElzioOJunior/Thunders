using ThundersTeste.Domain.Aggregates.TarefaAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using MediatR;
using ThundersTeste.Application.Dtos.Tarefa;
using ThundersTeste.Application.Queries.Tarefa;

namespace ThundersTeste.Application.QueryHandlers.Tarefa;

public class ReadAllTarefaQueryHandler : QueryHandler<ReadAllTarefaQuery, ICollection<TarefaDto>>
{
    private readonly ITarefaRepository _tarefaRepository;

    public ReadAllTarefaQueryHandler(
        IMediator bus,
        ITarefaRepository tarefaRepository
        )
        : base(bus)
    {
        _tarefaRepository = tarefaRepository;
    }

    public override async Task<ICollection<TarefaDto>> Handle(ReadAllTarefaQuery query, CancellationToken cancellationToken)
    {

        var entity = await _tarefaRepository.ReadAll();
        var tarefasDto = entity.Select(entity => new TarefaDto(entity));

        return tarefasDto.ToList();
    }
}
