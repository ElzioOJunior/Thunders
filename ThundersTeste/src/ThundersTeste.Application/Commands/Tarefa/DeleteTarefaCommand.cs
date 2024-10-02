using ThundersTeste.Application.Dtos.Tarefa;
using MediatR;
using System;

namespace ThundersTeste.Application.Commands.Tarefa;

public class DeleteTarefaCommand : Command, IRequest<Unit>
{
    public DeleteTarefaCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }

}
