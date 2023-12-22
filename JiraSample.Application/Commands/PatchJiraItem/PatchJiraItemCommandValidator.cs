using FluentValidation;

namespace JiraSample.Command.Application.Commands.PatchJiraItem;

public class PatchJiraItemCommandValidator : AbstractValidator<PatchJiraItemCommand>
{
    public PatchJiraItemCommandValidator()
    {
        RuleFor(x => x.id).NotEmpty();
    }
}
