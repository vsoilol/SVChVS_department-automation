using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Features.Semesters.Queries.GetAllSemestersByProgramId;
using DepartmentAutomation.Application.Validators.PropertyValidators;
using DepartmentAutomation.Domain.Entities;
using FluentValidation;

namespace DepartmentAutomation.Application.Validators.Features.Semesters.Queries.GetAllSemestersByProgramId
{
    public class GetAllSemestersByProgramIdQueryValidation : AbstractValidator<GetAllSemestersByProgramIdQuery>
    {
        public GetAllSemestersByProgramIdQueryValidation(IApplicationDbContext context)
        {
            RuleFor(x => x.EducationalProgramId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<EducationalProgram>(context));
        }
    }
}