using MediatR;
using ThundersTeste.Application.Dtos.Tarefa;
using ThundersTeste.Application.Validations.Tarefa;

namespace ThundersTeste.Application.Commands.Tarefa;

public class CreateTarefaCommand : Command, IRequest<TarefaDto>
{
    public CreateTarefaCommand(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public CreateTarefaCommand()
    { }

    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsValid()
    {
        ValidationResult = new CreateTarefaCommandValidator().Validate(this);
        return ValidationResult.IsValid;
    }
}
