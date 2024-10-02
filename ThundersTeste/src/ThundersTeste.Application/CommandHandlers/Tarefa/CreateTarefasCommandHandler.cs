using ThundersTeste.Domain.SeedWork;
using ThundersTeste.Application.Dtos.Tarefa;
using System.Threading.Tasks;
using System.Threading;
using MediatR;
using System;
using ThundersTeste.Domain.Aggregates.TarefaAggregate;
using ThundersTeste.Application.Commands.Tarefa;

namespace ThundersTeste.Application.CommandHandlers.Tarefa;

public class CreateTarefaCommandHandler : CommandHandler, IRequestHandler<CreateTarefaCommand, TarefaDto>
{
    private readonly ITarefaRepository _tarefaRepository;

    public CreateTarefaCommandHandler(
        IUnitOfWork uow,
        IMediator bus,
        ITarefaRepository tarefaRepository
        )
        : base(uow)
    {
        _tarefaRepository = tarefaRepository;
    }

    public async Task<TarefaDto> Handle(CreateTarefaCommand command, CancellationToken cancellationToken)
    {

        try
        {

            var tarefa = new Domain.Aggregates.TarefaAggregate.Tarefa(command.Name, command.Description);

            _tarefaRepository.Add(tarefa);

            if (await CommitAsync() is false)
                throw new Exception("Erro ao criar a Tarefa");

            return new TarefaDto(tarefa);
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao criar a Tarefa");
        }
    }
}
