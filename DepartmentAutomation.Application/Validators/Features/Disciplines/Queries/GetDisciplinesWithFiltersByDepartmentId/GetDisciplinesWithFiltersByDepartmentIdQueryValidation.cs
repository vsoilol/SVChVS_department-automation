using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Features.Disciplines.Queries.GetDisciplinesWithFiltersByDepartmentId;
using DepartmentAutomation.Application.Validators.PropertyValidators;
using DepartmentAutomation.Domain.Entities;
using DepartmentAutomation.Domain.Entities.DepartmentInfo;
using FluentValidation;

namespace DepartmentAutomation.Application.Validators.Features.Disciplines.Queries.GetDisciplinesWithFiltersByDepartmentId
{
    public class GetDisciplinesWithFiltersByDepartmentIdQueryValidation : AbstractValidator<GetDisciplinesWithFiltersByDepartmentIdQuery>
    {
        public GetDisciplinesWithFiltersByDepartmentIdQueryValidation(IApplicationDbContext context)
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1);

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1);

            When(x => x.Filter != null, () =>
            {
                RuleFor(x => x.Filter.DepartmentId)
                    .Cascade(CascadeMode.StopOnFirstFailure)
                    .NotNull()
                    .GreaterThanOrEqualTo(1)
                    .SetValidator(new SqlIdValidatorFor<Department>(context));

                RuleFor(x => x.Filter.PropertyName)
                    .Cascade(CascadeMode.StopOnFirstFailure)
                    .NotEmpty()
                    .SetValidator(new PropertyNameValidatorFor<Discipline>());
            });
        }
    }
}