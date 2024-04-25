using Application.Queries;
using FluentValidation;

namespace Application.Validators.Queries
{
    public class GetEmployeeByIdQueryValidator : AbstractValidator<GetEmployeeByIdQuery>
    {
        public GetEmployeeByIdQueryValidator()
        {
            RuleFor(q => q.Id).GreaterThan(0);
        }
    }
}