using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Features.Weeks.Queries.GetTrainingModuleNumbers;
using DepartmentAutomation.Application.Validators.PropertyValidators;
using DepartmentAutomation.Domain.Entities;
using FluentValidation;

namespace DepartmentAutomation.Application.Validators.Features.Weeks.Queries.GetTrainingModuleNumbers
{
    public class GetTrainingModuleNumbersQueryValidation : AbstractValidator<GetTrainingModuleNumbersQuery>
    {
        public GetTrainingModuleNumbersQueryValidation(IApplicationDbContext context)
        {
            RuleFor(x => x.EducationalProgramId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<EducationalProgram>(context));
        }
    }
}