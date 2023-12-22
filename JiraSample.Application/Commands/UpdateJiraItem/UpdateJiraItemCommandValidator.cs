using FluentValidation;
using JiraSample.Application.Commands.UpdateJiraItem;

namespace JiraSample.Command.Application.Commands.UpdateJiraItem;

public class UpdateJiraItemCommandValidator : AbstractValidator<UpdateJiraItemCommand>
{
    public UpdateJiraItemCommandValidator()
    {
        RuleFor(x => x.id).NotEmpty();
        RuleFor(x => x.name).NotEmpty();
        RuleFor(x => x.ItemType).NotEmpty();
        RuleFor(x => x.Status).NotEmpty();
    }
}
