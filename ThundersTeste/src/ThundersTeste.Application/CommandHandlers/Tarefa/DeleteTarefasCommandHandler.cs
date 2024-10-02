using ThundersTeste.Domain.SeedWork;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using MediatR;
using System;
using ThundersTeste.Application.Commands.Tarefa;
using ThundersTeste.Domain.Aggregates.TarefaAggregate;

namespace ThundersTeste.Application.CommandHandlers.Tarefa;

public class DeleteTarefaCommandHandler : CommandHandler, IRequestHandler<DeleteTarefaCommand, Unit>
{
    private readonly ITarefaRepository _tarefaRepository;

    public DeleteTarefaCommandHandler(
        IUnitOfWork uow,
        IMediator bus,
        ITarefaRepository tarefaRepository
        )
        : base(uow)
    {
        _tarefaRepository = tarefaRepository;
    }

    public async Task<Unit> Handle(DeleteTarefaCommand command, CancellationToken cancellationToken)
    {

        try
        {
            var tarefa = await _tarefaRepository.ReadById(command.Id);
            if (tarefa is null)
                throw new Exception("Tarefa com id informado não existe");

            await _tarefaRepository.DeleteById(tarefa.Id);

            if (await CommitAsync() is false)
                throw new Exception("Erro ao excluir a Tarefa");

            return Unit.Value;
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao excluir a Tarefa");
        }
    }
}
