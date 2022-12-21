using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Features.Disciplines.Queries.GetAdditionalDisciplineInfoById;
using DepartmentAutomation.Application.Validators.PropertyValidators;
using DepartmentAutomation.Domain.Entities;
using FluentValidation;

namespace DepartmentAutomation.Application.Validators.Features.Disciplines.Queries.GetAdditionalDisciplineInfoById
{
    public class GetAdditionalDisciplineInfoByIdQueryValidation : AbstractValidator<GetAdditionalDisciplineInfoByIdQuery>
    {
        public GetAdditionalDisciplineInfoByIdQueryValidation(IApplicationDbContext context)
        {
            RuleFor(x => x.DisciplineId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<Discipline>(context));
        }
    }
}