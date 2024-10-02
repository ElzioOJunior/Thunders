using FluentValidation;
using ThundersTeste.Application.Commands.Tarefa;

namespace ThundersTeste.Application.Validations.Tarefa;

public class UpdateTarefaCommandValidator : AbstractValidator<UpdateTarefaCommand>
{
    public UpdateTarefaCommandValidator()
    {
        ValidateName();
        ValidateId();
    }

    private void ValidateName()
    {
        RuleFor(entity => entity.Name)
            .NotEmpty()
            .WithErrorCode("001");
    }

    private void ValidateId()
    {
        RuleFor(entity => entity.TarefaId)
            .NotEmpty()
            .WithErrorCode("002");
    }

}
