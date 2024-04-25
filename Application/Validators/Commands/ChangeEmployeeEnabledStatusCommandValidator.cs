using Application.Commands;
using FluentValidation;

namespace Application.Validators.Commands
{
    public class ChangeEmployeeEnabledStatusCommandValidator : AbstractValidator<ChangeEmployeeEnabledStatusCommand>
    {
        public ChangeEmployeeEnabledStatusCommandValidator()
        {
            RuleFor(c => c.Id).GreaterThan(0);
            RuleFor(c => c.EnbaledStatus).InclusiveBetween(0, 1);
        }
    }
}