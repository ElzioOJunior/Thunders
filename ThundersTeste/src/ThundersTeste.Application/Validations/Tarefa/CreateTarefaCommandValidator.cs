using FluentValidation;
using ThundersTeste.Application.Commands.Tarefa;

namespace ThundersTeste.Application.Validations.Tarefa;

public class CreateTarefaCommandValidator : AbstractValidator<CreateTarefaCommand>
{
    public CreateTarefaCommandValidator()
    {
        ValidateName();
    }

    private void ValidateName()
    {
        RuleFor(entity => entity.Name)
            .NotEmpty()
            .WithErrorCode("083");
    }
}
