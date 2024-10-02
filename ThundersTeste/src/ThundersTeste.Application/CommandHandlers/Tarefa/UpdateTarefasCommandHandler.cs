using ThundersTeste.Domain.SeedWork;
using System.Threading.Tasks;
using System.Threading;
using MediatR;
using System;
using ThundersTeste.Application.Commands.Tarefa;
using ThundersTeste.Application.Dtos.Tarefa;
using ThundersTeste.Domain.Aggregates.TarefaAggregate;

namespace ThundersTeste.Application.CommandHandlers.Tarefa;

public class UpdateTarefaCommandHandler : CommandHandler, IRequestHandler<UpdateTarefaCommand, TarefaDto>
{
    private readonly ITarefaRepository _tarefaRepository;
    public UpdateTarefaCommandHandler(
        IUnitOfWork uow,
        IMediator bus,
        ITarefaRepository tarefaRepository
        )
        : base(uow)
    {
        _tarefaRepository = tarefaRepository;
    }

    public async Task<TarefaDto> Handle(UpdateTarefaCommand command, CancellationToken cancellationToken)
    {

        try
        {
            var tarefa = await _tarefaRepository.ReadById(command.TarefaId);
            if (tarefa is null)
                throw new Exception("Tarefa com id informado não existe");

            tarefa.Update(command.Name, command.Description);

            _tarefaRepository.Update(tarefa);

            if (await CommitAsync() is false)
                throw new Exception("Erro ao atualizar a Tarefa");

            return new TarefaDto(tarefa);
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao atualizar a Tarefa");
        }
    }
}
