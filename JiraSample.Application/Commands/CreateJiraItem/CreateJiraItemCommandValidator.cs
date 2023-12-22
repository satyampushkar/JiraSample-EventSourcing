using FluentValidation;
using JiraSample.Application.Commands.CreateJiraItem;

namespace JiraSample.Command.Application.Commands.CreateJiraItem;

public class CreateJiraItemCommandValidator : AbstractValidator<CreateJiraItemCommand>
{
    public CreateJiraItemCommandValidator()
    {
        RuleFor(x => x.name).NotEmpty();
        RuleFor(x => x.ItemType).NotEmpty();
    }
}
