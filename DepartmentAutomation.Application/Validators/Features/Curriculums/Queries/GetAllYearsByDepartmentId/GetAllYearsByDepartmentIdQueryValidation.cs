using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Features.Curriculums.Queries.GetAllYearsByDepartmentId;
using DepartmentAutomation.Application.Validators.PropertyValidators;
using DepartmentAutomation.Domain.Entities.DepartmentInfo;
using FluentValidation;

namespace DepartmentAutomation.Application.Validators.Features.Curriculums.Queries.GetAllYearsByDepartmentId
{
    public class GetAllYearsByDepartmentIdQueryValidation : AbstractValidator<GetAllYearsByDepartmentIdQuery>
    {
        public GetAllYearsByDepartmentIdQueryValidation(IApplicationDbContext context)
        {
            RuleFor(x => x.DepartmentId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<Department>(context));
        }
    }
}