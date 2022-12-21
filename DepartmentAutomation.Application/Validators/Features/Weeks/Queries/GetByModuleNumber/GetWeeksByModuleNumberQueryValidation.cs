using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Features.Weeks.Queries.GetByModuleNumber;
using DepartmentAutomation.Application.Validators.PropertyValidators;
using DepartmentAutomation.Domain.Entities;
using DepartmentAutomation.Domain.Entities.SemesterInfo;
using FluentValidation;

namespace DepartmentAutomation.Application.Validators.Features.Weeks.Queries.GetByModuleNumber
{
    public class GetWeeksByModuleNumberQueryValidation : AbstractValidator<GetWeeksByModuleNumberQuery>
    {
        public GetWeeksByModuleNumberQueryValidation(IApplicationDbContext context)
        {
            RuleFor(x => x.EducationalProgramId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<EducationalProgram>(context));

            RuleFor(x => x.SemesterId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<Semester>(context));

            RuleFor(x => x.ModuleNumber)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1);
        }
    }
}