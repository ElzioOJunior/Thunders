using FluentValidation.Results;

namespace ThundersTeste.Application.Commands;

public abstract class Command
{
	protected ValidationResult ValidationResult { get; set; }

	public ValidationResult GetValidationResult()
	{
		return ValidationResult;
	}

}
