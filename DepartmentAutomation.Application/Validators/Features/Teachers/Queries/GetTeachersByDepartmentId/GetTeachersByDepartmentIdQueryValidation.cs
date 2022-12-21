using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Features.Teachers.Queries.GetTeachersByDepartmentId;
using DepartmentAutomation.Application.Validators.PropertyValidators;
using DepartmentAutomation.Domain.Entities.DepartmentInfo;
using FluentValidation;

namespace DepartmentAutomation.Application.Validators.Features.Teachers.Queries.GetTeachersByDepartmentId
{
    public class GetTeachersByDepartmentIdQueryValidation : AbstractValidator<GetTeachersByDepartmentIdQuery>
    {
        public GetTeachersByDepartmentIdQueryValidation(IApplicationDbContext context)
        {
            RuleFor(x => x.DepartmentId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<Department>(context));
        }
    }
}