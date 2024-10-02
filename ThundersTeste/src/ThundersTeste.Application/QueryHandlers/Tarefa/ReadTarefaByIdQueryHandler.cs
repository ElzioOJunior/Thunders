using System.Threading.Tasks;
using System.Threading;
using MediatR;
using ThundersTeste.Application.Queries.Tarefa;
using ThundersTeste.Application.Dtos.Tarefa;
using ThundersTeste.Domain.Aggregates.TarefaAggregate;

namespace ThundersTeste.Application.QueryHandlers.Tarefa;

public class ReadTarefaByIdQueryHandler : QueryHandler<ReadTarefaByIdQuery, TarefaDto>
{
    private readonly ITarefaRepository _tarefaRepository;

    public ReadTarefaByIdQueryHandler(
        IMediator bus,
        ITarefaRepository tarefaRepository
        )
        : base(bus)
    {
        _tarefaRepository = tarefaRepository;
    }

    public override async Task<TarefaDto> Handle(ReadTarefaByIdQuery query, CancellationToken cancellationToken)
    {

        var entity = await _tarefaRepository.ReadById(query.Id);
        if (entity is null)
            return default;

        var tarefasDto = new TarefaDto(entity);

        return tarefasDto;
    }
}
