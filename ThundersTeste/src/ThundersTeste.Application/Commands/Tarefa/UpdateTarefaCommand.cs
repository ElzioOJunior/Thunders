using MediatR;
using System;
using ThundersTeste.Application.Dtos.Tarefa;
using ThundersTeste.Application.Validations.Tarefa;

namespace ThundersTeste.Application.Commands.Tarefa;

public class UpdateTarefaCommand : Command, IRequest<TarefaDto>
{
    public UpdateTarefaCommand(Guid tarefaId, string name, string description)
    {
        TarefaId = tarefaId;
        Name = name;
        Description = description;
    }

    public Guid TarefaId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public bool IsValid()
    {
        ValidationResult = new UpdateTarefaCommandValidator().Validate(this);
        return ValidationResult.IsValid;
    }
}
